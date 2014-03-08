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
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Refactoring;
using MonoDevelop.Monobjc.Utilities;

namespace MonoDevelop.Monobjc.Gui
{
	public partial class CreatePropertyDialog : BaseRefactoringDialog
	{
		public CreatePropertyDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project) : base(refactoring, options, project)
		{
			this.Build ();

			this.buttonOk.Sensitive = false;
			this.entryName.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
			this.entryType.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
			this.Validate ();
			
			this.buttonOk.Clicked += OnOKClicked;
		}
		
		private bool Validate ()
		{
			if (entryName.Text.Length < 2) {
				return false;
			}
			if (entryType.Text.Length == 0) {
				return false;
			}
			return true;
		}
		
		void OnOKClicked (object sender, EventArgs e)
		{
			try {
				this.Generate();
			} finally {
				this.Destroy ();
			}
		}
		
		private void Generate()
		{
			String propertyName = this.entryName.Text;
			String propertyTypeName = this.entryType.Text;

			Document document = this.Options.Document;
			TextEditor editor = document.Editor.Parent;
			
			var loc = document.Editor.Caret.Location;
			var declaringType = document.ParsedDocument.GetInnermostTypeDefinition (loc);
			var type = this.Options.ResolveResult.Type;
			
			// Generate the code
			String indent = this.Options.GetIndent(type.GetDefinition());
			String generatedCode = this.Generate (propertyName, propertyTypeName, indent);
			
			var mode = new InsertionCursorEditMode (editor, MonoDevelop.Ide.TypeSystem.CodeGenerationService.GetInsertionPoints (document, declaringType));
			if (mode.InsertionPoints.Count == 0) {
				MessageService.ShowError (GettextCatalog.GetString ("No valid insertion point can be found in type '{0}'.", declaringType.Name));
				return;
			}
			ModeHelpWindow helpWindow = new InsertionCursorLayoutModeHelpWindow ();
            //helpWindow.TransientFor = IdeApp.Workbench.RootWindow;
			helpWindow.TitleText = GettextCatalog.GetString ("Create Property");
			mode.HelpWindow = helpWindow;
			mode.CurIndex = mode.InsertionPoints.Count - 1;
			mode.StartMode ();
			mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
				if (args.Success) {
					args.InsertionPoint.Insert (document.Editor, generatedCode);
				}
			};
		}
		
		private string Generate (String fieldName, String propertyTypeName, string indent)
		{
			String lowerName = fieldName.Substring(0, 1).ToLowerInvariant() + fieldName.Substring(1);
			String upperName = fieldName.Substring(0, 1).ToUpperInvariant() + fieldName.Substring(1);

			String getterName = lowerName;
			String setterName = "set" + upperName + ":";

			// Create the field declaration
			//FieldDeclaration fieldDeclaration = GetFieldDeclaration(lowerName, new SimpleType (propertyTypeName));
			String fieldDeclaration = String.Format("private {0} {1};", propertyTypeName, lowerName);
			
			// Create the property declaration
			PropertyDeclaration propertyDeclaration = GetPropertyDeclaration(upperName, new SimpleType (propertyTypeName));
			
			{
				// Create the attribute
				AttributeSection attributeSection = new AttributeSection ();
				var attribute = GetAttribute(Constants.OBJECTIVE_C_MESSAGE_SHORTFORM, getterName);
				attributeSection.Attributes.Add (attribute);

				// Create the member reference
				MemberReferenceExpression memberReferenceExpression = new MemberReferenceExpression(new ThisReferenceExpression (), lowerName);
				ReturnStatement returnStatement = new ReturnStatement (memberReferenceExpression);
				
				// Create the "get" region
				propertyDeclaration.Getter = new Accessor();
				propertyDeclaration.Getter.Attributes.Add(attributeSection);
				propertyDeclaration.Getter.Body = new BlockStatement ();
				propertyDeclaration.Getter.Body.Add(returnStatement);
			}
			
			{
				// Create the attribute
				AttributeSection attributeSection = new AttributeSection ();
				var attribute = GetAttribute(Constants.OBJECTIVE_C_MESSAGE_SHORTFORM, setterName);
				attributeSection.Attributes.Add (attribute);

				BlockStatement blockStatement = new BlockStatement();
				// Create a simple assignment
				MemberReferenceExpression memberReferenceExpression2 = new MemberReferenceExpression(new ThisReferenceExpression (), lowerName);
				AssignmentExpression assignmentExpression = new AssignmentExpression(memberReferenceExpression2, new IdentifierExpression ("value"));
				blockStatement.Add(assignmentExpression);

				// Create the "set" region
				propertyDeclaration.Setter = new Accessor();
				propertyDeclaration.Setter.Attributes.Add(attributeSection);
				propertyDeclaration.Setter.Body = blockStatement;
			}

			// Return the result of the AST generation
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(fieldDeclaration);
			builder.AppendLine();
			builder.AppendLine(this.Options.OutputNode(propertyDeclaration));
			String generated = builder.ToString();
			
			// Add ident space
			return Indent(generated, indent);
		}
	}
}
