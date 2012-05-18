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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gtk;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.CSharp;

namespace MonoDevelop.Monobjc.Refactoring
{
	public partial class ImplementProtocolDialog
	{
		protected override void DoRefactor ()
		{
			// Get some useful objects
			TextEditorData data = options.GetTextEditorData ();
			INRefactoryASTProvider provider = options.GetASTProvider ();
			ResolveResult resolveResult = this.options.ResolveResult;
			
			TextEditor editor = data.Parent;
			IType declaringType = resolveResult.ResolvedType.Type;
			
			// Get the indentation
			String indent = this.options.GetIndent (declaringType) + "\t";
			StringBuilder code = new StringBuilder ();
			
			// Generate the code for checked members
			foreach (IMember member in this.CheckedMembers) {
				if (declaringType.Members.Any (m => m.Name == member.Name)) {
					continue;
				}
				
				switch (member.MemberType) {
				case MonoDevelop.Projects.Dom.MemberType.Method:
					code.Append (this.GenerateMethod (declaringType, (IMethod)member, provider, indent));
					code.AppendLine ();
					break;
				case MonoDevelop.Projects.Dom.MemberType.Property:
					IMethod getter = member.DeclaringType.Members.FirstOrDefault (m => String.Equals (m.Name, "get_" + member.Name)) as IMethod;
					IMethod setter = member.DeclaringType.Members.FirstOrDefault (m => String.Equals (m.Name, "set_" + member.Name)) as IMethod;
					
					code.Append (this.GenerateProperty (declaringType, (IProperty)member, getter, setter, provider, indent));
					code.AppendLine ();
					break;
				}
			}
			
			InsertionCursorEditMode mode = new InsertionCursorEditMode (editor, CodeGenerationService.GetInsertionPoints (options.Document, declaringType));
			ModeHelpWindow helpWindow = new ModeHelpWindow ();
			helpWindow.TransientFor = IdeApp.Workbench.RootWindow;
			helpWindow.TitleText = GettextCatalog.GetString ("<b>Implement Interface -- Targeting</b>");
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Key</b>"), GettextCatalog.GetString ("<b>Behavior</b>")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Up</b>"), GettextCatalog.GetString ("Move to <b>previous</b> target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Down</b>"), GettextCatalog.GetString ("Move to <b>next</b> target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Enter</b>"), GettextCatalog.GetString ("<b>Declare interface implementation</b> at target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Esc</b>"), GettextCatalog.GetString ("<b>Cancel</b> this refactoring.")));
			mode.HelpWindow = helpWindow;
			mode.CurIndex = mode.InsertionPoints.Count - 1;
			mode.StartMode ();
			mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
				if (args.Success) {
					args.InsertionPoint.Insert (data, code.ToString ());
				}
			};
		}

		private String GenerateMethod (IType declaringType, IMethod method, INRefactoryASTProvider provider, String indent)
		{
			// Retrieve the Objective-C message in the attribute
			String message = AttributeHelper.GetAttributeValue (method, AttributeHelper.OBJECTIVE_C_MESSAGE);
			AttributeSection attributeSection = new AttributeSection ();
			if (!String.IsNullOrEmpty (message)) {
				var attribute = this.GetAttribute("ObjectiveCMessage", message);
				attributeSection.Attributes.Add (attribute);
			}
			
			// Create the method declaration
			MethodDeclaration methodDeclaration = new MethodDeclaration ();
			methodDeclaration.Name = method.Name;
			methodDeclaration.Attributes.Add (attributeSection);
			methodDeclaration.Modifiers = ICSharpCode.NRefactory.CSharp.Modifiers.Public | ICSharpCode.NRefactory.CSharp.Modifiers.Virtual;
			methodDeclaration.ReturnType = this.Shorten (declaringType, method.ReturnType);
			
			foreach (var parameter in method.Parameters) {
				ParameterDeclaration parameterDeclaration = new ParameterDeclaration(this.Shorten (declaringType, parameter.ReturnType), parameter.Name);
				if (parameter.IsOut) {
					parameterDeclaration.ParameterModifier |= ParameterModifier.Out;
				}
				if (parameter.IsRef) {
					parameterDeclaration.ParameterModifier |= ParameterModifier.Ref;
				}
				methodDeclaration.Parameters.Add (parameterDeclaration);
			}
			
			// Create the method body
			methodDeclaration.Body = new BlockStatement ();
			ThrowStatement throwStatement = this.GetThrowStatement ("System.NotImplementedException");
			methodDeclaration.Body.Add (throwStatement);
			
			// Return the result of the AST generation
			return provider.OutputNode (this.options.Dom, methodDeclaration, indent);
		}

		private String GenerateProperty (IType declaringType, IProperty property, IMethod getterMethod, IMethod setterMethod, INRefactoryASTProvider provider, String indent)
		{
			// Create the property declaration
			var propertyType = this.Shorten (declaringType, property.ReturnType);
			PropertyDeclaration propertyDeclaration = this.GetPropertyDeclaration(property.Name, propertyType, null);
			
			if (property.HasGet && getterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (getterMethod, AttributeHelper.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					var attribute = this.GetAttribute("ObjectiveCMessage", message);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "get" region
				ThrowStatement throwStatement = this.GetThrowStatement ("System.NotImplementedException");
				propertyDeclaration.Getter = new Accessor();
				propertyDeclaration.Getter.Attributes.Add(attributeSection);
				propertyDeclaration.Getter.Body = new BlockStatement ();
				propertyDeclaration.Getter.Body.Add(throwStatement);
			}
			
			if (property.HasSet && setterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (setterMethod, AttributeHelper.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					var attribute = this.GetAttribute("ObjectiveCMessage", message);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "set" region
				ThrowStatement throwStatement = this.GetThrowStatement ("System.NotImplementedException");
				propertyDeclaration.Setter = new Accessor();
				propertyDeclaration.Setter.Attributes.Add(attributeSection);
				propertyDeclaration.Setter.Body = new BlockStatement ();
				propertyDeclaration.Setter.Body.Add(throwStatement);
			}
			
			// Return the result of the AST generation
			return provider.OutputNode (this.options.Dom, propertyDeclaration, indent);
		}
	}
}
