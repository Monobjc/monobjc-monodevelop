//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2011 - Laurent Etiemble
//
// Monobjc is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.
//
// Monobjc is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Monobjc.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using Monobjc.Tools.InterfaceBuilder;
using MonoDevelop.Core;
using MonoDevelop.Core.Collections;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Monobjc.Utilities;

namespace MonoDevelop.Monobjc.CodeGeneration
{
	/// <summary>
	///   Base implementation for a code-behind generator. As this implementation is language agnostic, language specific must be implemented in subclasses.
	/// </summary>
	public abstract class BaseCodeBehindGenerator : ICodeBehindGenerator
	{
		/// <summary>
		/// Gets a value indicating whether this generator supports partial classes.
		/// </summary>
		/// <value>
		/// <c>true</c> if support partial classes; otherwise, <c>false</c>.
		/// </value>
		public abstract bool SupportPartialClasses {
			get;
		}

		/// <summary>
		/// Gets a value indicating whether this generator support partial methods.
		/// </summary>
		/// <value>
		/// <c>true</c> if support partial methods; otherwise, <c>false</c>.
		/// </value>
		public abstract bool SupportPartialMethods {
			get;
		}		

		/// <summary>
		///   Generates the design code for framework loading.
		/// </summary>
		/// <param name = "resolver">The type resolver.</param>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>The path to the designer file.</returns>
		public FilePath GenerateFrameworkLoadingCode (ProjectTypeCache cache, String[] frameworks)
		{
			IEnumerable<IType> entryPoints = cache.GetEntryPoints ();
			IType entryPoint = entryPoints.SingleOrDefault ();
			if (entryPoint != null) {
				IMethod method = cache.GetMainMethod (entryPoint);
				if (method != null) {
					// Get the start line of the method
					DomRegion region = method.BodyRegion;
					int startLine = region.BeginLine;

					// Load the entry point file
					String fileName = region.FileName;
					List<String> lines = File.ReadAllLines (fileName).ToList ();

					// Search for desginer region indices
					int startIndex = lines.FindIndex (startLine - 1, l => this.IsDesignerRegionDelimiter (l, true));
					int endIndex = -1;
					if (startIndex != -1) {
						endIndex = lines.FindIndex (startIndex, l => this.IsDesignerRegionDelimiter (l, false));
					}
					if (startIndex != -1 && endIndex != -1) {
						// Build a new list of lines with the new code
						List<String> newLines = new List<String> ();
						newLines.AddRange (lines.Take (startIndex));
						newLines.AddRange (this.GenerateFrameworkLoadingcode (frameworks));
						newLines.AddRange (lines.Skip (endIndex + 1));

						// Write it to the file
						File.WriteAllLines (fileName, newLines.ToArray ());

						return fileName;
					}
				}
			}
			return null;
		}

		/// <summary>
		///   Determines whether a line is a region delimiter.
		/// </summary>
		/// <param name = "line">The line.</param>
		/// <param name = "start">if set to <c>true</c>, check for a region start.</param>
		/// <returns>
		///   <c>true</c> if the line is a region delimiter; otherwise, <c>false</c>.
		/// </returns>
		protected abstract bool IsDesignerRegionDelimiter (String line, bool start);

		/// <summary>
		///   Generates the framework loading code which is language specific.
		/// </summary>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>A list of lines for the code.</returns>
		protected abstract IEnumerable<String> GenerateFrameworkLoadingcode (String[] frameworks);

		/// <summary>
		///   Generates the design code for an Interface Builder file.
		/// </summary>
		/// <param name = "resolver">The type resolver.</param>
		/// <param name = "writer">The writer.</param>
		/// <param name = "className">Name of the class.</param>
		/// <param name = "enumerable">The class descriptions.</param>
		/// <returns>The path to the designer file.</returns>
		public FilePath GenerateCodeBehindCode (ProjectTypeCache cache, CodeBehindWriter writer, String className, IEnumerable<IBPartialClassDescription> enumerable)
		{
			FilePath designerFile = null;
			String defaultNamespace;
			MonobjcProject project = cache.Project;

			IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Generate designer code for '{0}'", className);

			IType type = cache.ResolvePartialType (className);
			FilePath mainFile = cache.GetMainFile (type);
			if (mainFile != FilePath.Null) {
				if (mainFile.Extension == ".dll") {
					IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Skipping '{0}' as it comes from a DLL", className);
					return FilePath.Null;
				}

				if (!cache.IsInProject (type)) {
					IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Skipping '{0}' as it comes from another project", className);
					return FilePath.Null;
				}

				// The filname is based on the compilation unit parent folder and the type name
				FilePath parentDirectory = mainFile.ParentDirectory;
				FilePath filename = project.LanguageBinding.GetFileName (type.Name + Constants.DOT_DESIGNER);
				designerFile = parentDirectory.Combine (filename);
				defaultNamespace = type.Namespace;
			} else {
				// Combine the filename in the default directory
				FilePath parentDirectory = project.BaseDirectory;
				FilePath filename = project.LanguageBinding.GetFileName (className + Constants.DOT_DESIGNER);
				designerFile = parentDirectory.Combine (filename);
				defaultNamespace = project.GetDefaultNamespace (designerFile);
			}

			IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Put designer code in '{0}'", designerFile);

			// Create the compilation unit
			CodeCompileUnit ccu = new CodeCompileUnit ();
			CodeNamespace ns = new CodeNamespace (defaultNamespace);
			ccu.Namespaces.Add (ns);

			// Create the partial class
			CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration (className);
			typeDeclaration.IsClass = true;
			typeDeclaration.IsPartial = true;

			// List for import collection
			Set<String> imports = new Set<string> ();
			imports.Add ("Monobjc");

			// Create fields for outlets);
			foreach (IBOutletDescriptor outlet in enumerable.SelectMany(d => d.Outlets)) {
				IType outletType = cache.ResolvePartialType (outlet.ClassName);
				outletType = outletType ?? cache.ResolvePartialType ("id");
				outletType = outletType ?? cache.ResolveType (typeof(IntPtr));

				IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Resolving outlet '{0}' of type '{1}' => '{2}'", outlet.Name, outlet.ClassName, outletType.FullName);

				imports.Add (outletType.Namespace);

				CodeTypeMember property = this.GenerateOutletProperty (outletType, outlet.Name);
				typeDeclaration.Members.Add (property);
			}

			// Create methods for exposed actions
			foreach (IBActionDescriptor action in enumerable.SelectMany(d => d.Actions)) {
				IType argumentType = cache.ResolvePartialType (action.Argument);
				argumentType = argumentType ?? cache.ResolvePartialType ("id");
				argumentType = argumentType ?? cache.ResolveType (typeof(IntPtr));

				IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Resolving action '{0}' of type '{1}' => '{2}'", action.Message, action.Argument, argumentType.FullName);

				imports.Add (argumentType.Namespace);

				CodeTypeMember exposedMethod = this.GenerateActionExposedMethod (action.Message, argumentType);
				typeDeclaration.Members.Add (exposedMethod);
				
				CodeTypeMember partialMethod = this.GenerateActionPartialMethod (action.Message, argumentType);
				typeDeclaration.Members.Add (partialMethod);
			}

			// Add namespaces
			CodeNamespaceImport[] namespaceImports = imports.Select (import => new CodeNamespaceImport (import)).ToArray ();
			ns.Imports.AddRange (namespaceImports);

			// Add the type
			ns.Types.Add (typeDeclaration);

			// Write the result
			writer.WriteFile (designerFile, ccu);

			return designerFile;
		}

		protected virtual CodeCompileUnit GenerateCodeCompileUnit(ProjectTypeCache cache, String defaultNamespace, String className, IEnumerable<IBPartialClassDescription> enumerable)
		{
			// Create the compilation unit
			CodeCompileUnit ccu = new CodeCompileUnit ();
			CodeNamespace ns = new CodeNamespace (defaultNamespace);
			ccu.Namespaces.Add (ns);
			
			// Create the partial class
			CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration (className);
			typeDeclaration.IsClass = true;
			typeDeclaration.IsPartial = true;
			
			// List for import collection
			Set<String> imports = new Set<string> ();
			imports.Add ("Monobjc");
			
			// Create fields for outlets);
			foreach (IBOutletDescriptor outlet in enumerable.SelectMany(d => d.Outlets)) {
				IType outletType = cache.ResolvePartialType (outlet.ClassName);
				outletType = outletType ?? cache.ResolvePartialType ("id");
				outletType = outletType ?? cache.ResolveType (typeof(IntPtr));
				
				IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Resolving outlet '{0}' of type '{1}' => '{2}'", outlet.Name, outlet.ClassName, outletType.FullName);
				
				imports.Add (outletType.Namespace);
				
				CodeTypeMember property = this.GenerateOutletProperty (outletType, outlet.Name);
				typeDeclaration.Members.Add (property);
			}
			
			// Create methods for exposed actions
			foreach (IBActionDescriptor action in enumerable.SelectMany(d => d.Actions)) {
				IType argumentType = cache.ResolvePartialType (action.Argument);
				argumentType = argumentType ?? cache.ResolvePartialType ("id");
				argumentType = argumentType ?? cache.ResolveType (typeof(IntPtr));
				
				IDELogger.Log ("BaseCodeBehindGenerator::GenerateCodeBehindCode -- Resolving action '{0}' of type '{1}' => '{2}'", action.Message, action.Argument, argumentType.FullName);
				
				imports.Add (argumentType.Namespace);
				
				CodeTypeMember exposedMethod = this.GenerateActionExposedMethod (action.Message, argumentType);
				typeDeclaration.Members.Add (exposedMethod);
				
				CodeTypeMember partialMethod = this.GenerateActionPartialMethod (action.Message, argumentType);
				typeDeclaration.Members.Add (partialMethod);
			}
			
			// Add namespaces
			CodeNamespaceImport[] namespaceImports = imports.Select (import => new CodeNamespaceImport (import)).ToArray ();
			ns.Imports.AddRange (namespaceImports);
			
			// Add the type
			ns.Types.Add (typeDeclaration);

			return ccu;
		}

		/// <summary>
		///   Generates an outlet property.
		/// </summary>
		/// <param name = "outletType">Type of the outlet.</param>
		/// <param name = "name">The name of the outlet.</param>
		/// <returns>Returns the type member.</returns>
		protected virtual CodeTypeMember GenerateOutletProperty (IType outletType, string name)
		{
			// Create various references
			CodeTypeReference typeRef = new CodeTypeReference (outletType.Name);
			CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression ();
			CodePrimitiveExpression nameRef = new CodePrimitiveExpression (name);
			CodePropertySetValueReferenceExpression valueRef = new CodePropertySetValueReferenceExpression ();

			// Create the property
			CodeMemberProperty property = new CodeMemberProperty ();
			property.Attributes = MemberAttributes.Public;
			property.Name = name;
			property.Type = typeRef;

			// Add the attributes
			property.CustomAttributes.Add (new CodeAttributeDeclaration ("IBOutlet"));
			property.CustomAttributes.Add (new CodeAttributeDeclaration ("ObjectiveCIVar", new CodeAttributeArgument (new CodePrimitiveExpression (name))));

			// Generate the getter
			CodeStatement getInstanceVariableStatement = this.GenerateGetInstanceVariableStatement (thisRef, typeRef, nameRef);
			property.GetStatements.Add (getInstanceVariableStatement);

			// Generate the setter
			CodeExpression setInstanceVariableExpression = this.GenerateSetInstanceVariableStatement (thisRef, typeRef, nameRef, valueRef);
			property.SetStatements.Add (setInstanceVariableExpression);

			return property;
		}

		/// <summary>
		///   Generates the exposed method for an action.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "argumentType">Type of the argument.</param>
		/// <returns>The type member.</returns>
		protected virtual CodeTypeMember GenerateActionExposedMethod (String message, IType argumentType)
		{
			String selector = message;
			String name = GenerateMethodName (selector);

			// Create various references
			CodeTypeReference typeRef = new CodeTypeReference (argumentType.Name);
			CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression ();
			CodePrimitiveExpression selectorRef = new CodePrimitiveExpression (selector);
			CodeArgumentReferenceExpression senderRef = new CodeArgumentReferenceExpression ("sender");

			// Create a public method
			CodeMemberMethod method = new CodeMemberMethod ();
			method.Attributes = MemberAttributes.Public;
			method.Name = "__" + name;
			method.ReturnType = new CodeTypeReference (typeof(void));
			method.Parameters.Add (new CodeParameterDeclarationExpression (typeRef, "sender"));

			// Add custom attributes
			method.CustomAttributes.Add (new CodeAttributeDeclaration ("IBAction"));
			method.CustomAttributes.Add (new CodeAttributeDeclaration ("ObjectiveCMessage", new CodeAttributeArgument (selectorRef)));

			// Add body to call partial method
			method.Statements.Add (new CodeMethodInvokeExpression (thisRef, name, senderRef));

			return method;
		}

		/// <summary>
		/// Generates the partial method for an action.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "argumentType">Type of the argument.</param>
		/// <returns>The type member.</returns>
		protected abstract CodeTypeMember GenerateActionPartialMethod (String message, IType argumentType);

		/// <summary>
		/// Generates the "GetInstanceVariable" statement.
		/// </summary>
		/// <returns>The "GetInstanceVariable" statement.</returns>
		/// <param name="thisRef">This reference.</param>
		/// <param name="typeRef">Type reference.</param>
		/// <param name="nameRef">Name reference.</param>
		protected virtual CodeStatement GenerateGetInstanceVariableStatement (CodeThisReferenceExpression thisRef, CodeTypeReference typeRef, CodePrimitiveExpression nameRef)
		{
			CodeMethodReferenceExpression getterRef = new CodeMethodReferenceExpression (thisRef, "GetInstanceVariable", typeRef);
			CodeStatement statement = new CodeMethodReturnStatement (new CodeMethodInvokeExpression (getterRef, nameRef));
			return statement;
		}
		
		/// <summary>
		/// Generates the "SetInstanceVariable" expression.
		/// </summary>
		/// <returns>The "SetInstanceVariable" expression.</returns>
		/// <param name="thisRef">This reference.</param>
		/// <param name="typeRef">Type reference.</param>
		/// <param name="nameRef">Name reference.</param>
		/// <param name="valueRef">Value reference</param>
		protected virtual CodeExpression GenerateSetInstanceVariableStatement (CodeThisReferenceExpression thisRef, CodeTypeReference typeRef, CodePrimitiveExpression nameRef, CodePropertySetValueReferenceExpression valueRef)
		{
			CodeMethodReferenceExpression setterRef = new CodeMethodReferenceExpression (thisRef, "SetInstanceVariable", typeRef);
			CodeExpression expression = new CodeMethodInvokeExpression (setterRef, nameRef, valueRef);
			return expression;
		}
		
		/// <summary>
		///   Generates the name of the method from a selector.
		/// </summary>
		/// <param name = "selector">The selector.</param>
		/// <returns>The method name.</returns>
		protected static String GenerateMethodName (String selector)
		{
			String name = selector.TrimEnd (':');
			if (name.Length > 0) {
				name = name.Substring (0, 1).ToUpperInvariant () + name.Substring (1);
			}
			return name;
		}
	}
}