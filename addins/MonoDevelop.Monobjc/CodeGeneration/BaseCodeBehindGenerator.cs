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
using Monobjc.Tools.InterfaceBuilder;
using MonoDevelop.Core;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom;

namespace MonoDevelop.Monobjc.CodeGeneration
{
    /// <summary>
    ///   Base implementation for a code-behind generator. As this implementation is language agnostic, language specific must be implemented in subclasses.
    /// </summary>
    public abstract class BaseCodeBehindGenerator : ICodeBehindGenerator
    {
        // TODO: Move constant
        private const String DESIGNER = ".designer";

        /// <summary>
        ///   Generates the design code for framework loading.
        /// </summary>
        /// <param name = "resolver">The type resolver.</param>
        /// <param name = "frameworks">The frameworks.</param>
        /// <returns>The path to the designer file.</returns>
        public FilePath GenerateFrameworkLoadingCode(ProjectResolver resolver, String[] frameworks)
        {
            IEnumerable<IType> entryPoints = resolver.ResolveEntryPoints();
            IType entryPoint = entryPoints.SingleOrDefault();
            if (entryPoint != null)
            {
                IMethod method = entryPoint.Methods.SingleOrDefault(m => m.IsStatic && m.Name == "Main");
                if (method != null)
                {
                    // Get the start line of the method
                    DomRegion region = method.BodyRegion;
                    int startLine = region.Start.Line;

                    // Load the entry point file
                    String fileName = entryPoint.CompilationUnit.FileName;
                    List<String> lines = File.ReadAllLines(fileName).ToList();

                    // Search for desginer region indices
                    int startIndex = lines.FindIndex(startLine - 1, l => this.IsDesignerRegionDelimiter(l, true));
                    int endIndex = -1;
                    if (startIndex != -1)
                    {
                        endIndex = lines.FindIndex(startIndex, l => this.IsDesignerRegionDelimiter(l, false));
                    }
                    if (startIndex != -1 && endIndex != -1)
                    {
                        // Build a new list of lines with the new code
                        List<String> newLines = new List<String>();
                        newLines.AddRange(lines.Take(startIndex));
                        newLines.AddRange(this.GenerateFrameworkLoadingcode(frameworks));
                        newLines.AddRange(lines.Skip(endIndex + 1));

                        // Write it to the file
                        File.WriteAllLines(fileName, newLines.ToArray());

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
        protected abstract bool IsDesignerRegionDelimiter(String line, bool start);

        /// <summary>
        ///   Generates the framework loading code which is language specific.
        /// </summary>
        /// <param name = "frameworks">The frameworks.</param>
        /// <returns>A list of lines for the code.</returns>
        protected abstract IEnumerable<String> GenerateFrameworkLoadingcode(String[] frameworks);

        /// <summary>
        ///   Generates the design code for an Interface Builder file.
        /// </summary>
        /// <param name = "resolver">The type resolver.</param>
        /// <param name = "writer">The writer.</param>
        /// <param name = "className">Name of the class.</param>
        /// <param name = "enumerable">The class descriptions.</param>
        /// <returns>The path to the designer file.</returns>
        public FilePath GenerateCodeBehindCode(ProjectResolver resolver, CodeBehindWriter writer, String className, IEnumerable<IBPartialClassDescription> enumerable)
        {
            FilePath designerFile;
            String defaultNamespace;
            MonobjcProject project = resolver.Project;

            LoggingService.LogInfo("Generate designer code for '" + className + "'");

            IType type = resolver.ResolvePartialType(className);
            if (type != null && type.CompilationUnit != null && type.CompilationUnit.FileName != FilePath.Null)
            {
                if (type.CompilationUnit.FileName.Extension == ".dll")
                {
                    LoggingService.LogInfo("Skipping " + className + " as it comes from a DLL");
                    return FilePath.Null;
                }

                // Make sure that the right filename is taken
                String filename = type.CompilationUnit.FileName.FileNameWithoutExtension;
                String extension = type.CompilationUnit.FileName.Extension;
                if (filename.EndsWith(DESIGNER))
                {
                    filename = filename.Substring(0, filename.Length - DESIGNER.Length);
                }

                // Combine the filename in the proper directory
                designerFile = type.CompilationUnit.FileName.ParentDirectory.Combine(filename + DESIGNER + extension);
                defaultNamespace = type.Namespace;
            }
            else
            {
                // Combine the filename in the default directory
                designerFile = project.BaseDirectory.Combine(project.LanguageBinding.GetFileName(className + DESIGNER));
                defaultNamespace = project.GetDefaultNamespace(designerFile);
            }

            LoggingService.LogInfo("Put designer code in '" + designerFile + "'");

            // Create the compilation unit
            CodeCompileUnit ccu = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace(defaultNamespace);
            ccu.Namespaces.Add(ns);

            // Create the partial class
            CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration(className);
            typeDeclaration.IsClass = true;
            typeDeclaration.IsPartial = true;

            // List for import collection
            IList<String> imports = new List<string>();
            imports.Add("Monobjc");

            // Create fields for outlets);
            foreach (IBOutletDescriptor outlet in enumerable.SelectMany(d => d.Outlets))
            {
                IType outletType = resolver.ResolvePartialType(outlet.ClassName);
                outletType = outletType ?? resolver.ResolvePartialType("id");
                outletType = outletType ?? new DomType("System.IntPtr");

                LoggingService.LogInfo("Resolving outlet '" + outlet.Name + "' of type '" + outlet.ClassName + "' => '" + outletType.FullName + "'");

                imports.Add(outletType.Namespace);

                CodeTypeMember property = this.GenerateOutletProperty(outletType, outlet.Name);
                typeDeclaration.Members.Add(property);
            }

            // Create methods for exposed actions
            foreach (IBActionDescriptor action in enumerable.SelectMany(d => d.Actions))
            {
                IType argumentType = resolver.ResolvePartialType(action.Argument);
                argumentType = argumentType ?? resolver.ResolvePartialType("id");
                argumentType = argumentType ?? new DomType("System.IntPtr");

                LoggingService.LogInfo("Resolving action '" + action.Message + "' with argument '" + action.Argument + "' => '" + argumentType.FullName + "'");

                imports.Add(argumentType.Namespace);

                CodeTypeMember exposedMethod = this.GenerateActionExposedMethod(action.Message, argumentType);
                typeDeclaration.Members.Add(exposedMethod);

                CodeTypeMember partialMethod = this.GenerateActionPartialMethod(action.Message, argumentType);
                typeDeclaration.Members.Add(partialMethod);
            }

            // Add namespaces
            CodeNamespaceImport[] namespaceImports = imports.Distinct().Select(import => new CodeNamespaceImport(import)).ToArray();
            ns.Imports.AddRange(namespaceImports);

            // Add the type
            ns.Types.Add(typeDeclaration);

            // Write the result
            writer.Write(ccu, designerFile);

            return designerFile;
        }

        /// <summary>
        ///   Generates an outlet property.
        /// </summary>
        /// <param name = "outletType">Type of the outlet.</param>
        /// <param name = "name">The name of the outlet.</param>
        /// <returns>Returns the type member.</returns>
        protected virtual CodeTypeMember GenerateOutletProperty(IType outletType, string name)
        {
            // Create various references
            CodeTypeReference typeRef = new CodeTypeReference(outletType.Name);
            CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();
            CodeMethodReferenceExpression getterRef = new CodeMethodReferenceExpression(thisRef, "GetInstanceVariable", typeRef);
            CodeMethodReferenceExpression setterRef = new CodeMethodReferenceExpression(thisRef, "SetInstanceVariable", typeRef);
            CodePrimitiveExpression nameRef = new CodePrimitiveExpression(name);
            CodePropertySetValueReferenceExpression valueRef = new CodePropertySetValueReferenceExpression();

            // Create the property
            CodeMemberProperty property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public;
            property.Name = name;
            property.Type = typeRef;

            // Add the attributes
            property.CustomAttributes.Add(new CodeAttributeDeclaration("IBOutlet"));
            property.CustomAttributes.Add(new CodeAttributeDeclaration("ObjectiveCIVar", new CodeAttributeArgument(new CodePrimitiveExpression(name))));

            // Generate the getter
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(getterRef, nameRef)));

            // Generate the setter
            property.SetStatements.Add(new CodeMethodInvokeExpression(setterRef, nameRef, valueRef));

            return property;
        }

        /// <summary>
        ///   Generates the exposed method for an action.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "argumentType">Type of the argument.</param>
        /// <returns>The type member.</returns>
        protected virtual CodeTypeMember GenerateActionExposedMethod(String message, IType argumentType)
        {
            String selector = message;
            String name = GenerateMethodName(selector);

            // Create various references
            CodeTypeReference typeRef = new CodeTypeReference(argumentType.Name);
            CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();
            CodePrimitiveExpression selectorRef = new CodePrimitiveExpression(selector);
            CodeArgumentReferenceExpression senderRef = new CodeArgumentReferenceExpression("sender");

            // Create a public method
            CodeMemberMethod method = new CodeMemberMethod();
            method.Attributes = MemberAttributes.Public;
            method.Name = "__" + name;
            method.ReturnType = new CodeTypeReference(typeof (void));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeRef, "sender"));

            // Add custom attributes
            method.CustomAttributes.Add(new CodeAttributeDeclaration("IBAction"));
            method.CustomAttributes.Add(new CodeAttributeDeclaration("ObjectiveCMessage", new CodeAttributeArgument(selectorRef)));

            // Add body to call partial method
            method.Statements.Add(new CodeMethodInvokeExpression(thisRef, name, senderRef));

            return method;
        }

        /// <summary>
        ///   Generates the partial method for an action.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "argumentType">Type of the argument.</param>
        /// <returns>The type member.</returns>
        protected abstract CodeTypeMember GenerateActionPartialMethod(String message, IType argumentType);

        /// <summary>
        ///   Generates the name of the method from a selector.
        /// </summary>
        /// <param name = "selector">The selector.</param>
        /// <returns>The method name.</returns>
        protected static String GenerateMethodName(String selector)
        {
            String name = selector.TrimEnd(':');
            if (name.Length > 0)
            {
                name = name.Substring(0, 1).ToUpperInvariant() + name.Substring(1);
            }
            return name;
        }
    }
}