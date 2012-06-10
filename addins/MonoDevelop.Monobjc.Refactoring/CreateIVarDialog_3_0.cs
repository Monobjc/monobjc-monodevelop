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
using System.Text;
using ICSharpCode.NRefactory.CSharp;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.TypeSystem;

namespace MonoDevelop.Monobjc.Refactoring
{
    public partial class CreateIVarDialog : BaseDialog
    {
        public CreateIVarDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project) : base(refactoring, options, project)
        {
            this.Build ();
            
            this.Title = GettextCatalog.GetString ("Create Instance Variable");
            
            this.buttonOk.Sensitive = false;
            this.entryName.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
            this.entryType.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
            this.Validate ();
            
            this.buttonOk.Clicked += OnOKClicked;
        }

        private bool Validate ()
        {
            if (entryName.Text.Length == 0) {
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
            AstType propertyType = new SimpleType (this.entryType.Text);

            Document document = options.Document;
            TextEditor editor = document.Editor.Parent;
            
            var loc = document.Editor.Caret.Location;
            var declaringType = document.ParsedDocument.GetInnermostTypeDefinition (loc);
            var type = options.ResolveResult.Type;

            // Generate the code
            String indent = options.GetIndent(type.GetDefinition()) + '\t';
            String generatedCode = this.GenerateProperty (propertyName, propertyType, indent);

            var mode = new InsertionCursorEditMode (editor, CodeGenerationService.GetInsertionPoints (document, declaringType));
            if (mode.InsertionPoints.Count == 0) {
                MessageService.ShowError (GettextCatalog.GetString ("No valid insertion point can be found in type '{0}'.", declaringType.Name));
                return;
            }
            ModeHelpWindow helpWindow = new InsertionCursorLayoutModeHelpWindow ();
            helpWindow.TransientFor = IdeApp.Workbench.RootWindow;
            helpWindow.TitleText = GettextCatalog.GetString ("Create Instance Variable");
            mode.HelpWindow = helpWindow;
            mode.CurIndex = mode.InsertionPoints.Count - 1;
            mode.StartMode ();
            mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
                if (args.Success) {
                    args.InsertionPoint.Insert (document.Editor, generatedCode);
                }
            };
        }
        
        private string GenerateProperty (String propertyName, AstType propertyType, string indent)
        {
            // Create the attribute
            AttributeSection attributeSection = new AttributeSection ();
            var attribute = GetAttribute("ObjectiveCIVar", propertyName);
            attributeSection.Attributes.Add (attribute);
            
            // Create the property declaration
            PropertyDeclaration propertyDeclaration = GetPropertyDeclaration(propertyName, propertyType, attributeSection);
            
            {
                // Create the member this.GetInstanceVariable<T>
                MemberReferenceExpression memberReferenceExpression = new MemberReferenceExpression (new ThisReferenceExpression (), "GetInstanceVariable");
                memberReferenceExpression.TypeArguments.Add (propertyType.Clone());
                
                // Create the invocation with arguments
                InvocationExpression invocationExpression = new InvocationExpression (memberReferenceExpression, new List<Expression> { new PrimitiveExpression (propertyName) });
                
                // Wrap the invocation with a return
                ReturnStatement returnStatement = new ReturnStatement (invocationExpression);
                
                // Create the "get" region
                propertyDeclaration.Getter = new Accessor();
                propertyDeclaration.Getter.Body = new BlockStatement ();
                propertyDeclaration.Getter.Body.Add(returnStatement);
            }
            
            {
                // Create the member this.SetInstanceVariable<T>
                MemberReferenceExpression memberReferenceExpression = new MemberReferenceExpression (new ThisReferenceExpression (), "SetInstanceVariable");
                memberReferenceExpression.TypeArguments.Add (propertyType.Clone());
                
                // Create the invocation with arguments
                InvocationExpression invocationExpression = new InvocationExpression (memberReferenceExpression, new List<Expression> { new PrimitiveExpression (propertyName), new IdentifierExpression ("value") });
                
                // Wrap the invocation with an expression
                ExpressionStatement expressionStatement = new ExpressionStatement (invocationExpression);
                
                // Create the "set" region
                propertyDeclaration.Setter = new Accessor();
                propertyDeclaration.Setter.Body = new BlockStatement ();
                propertyDeclaration.Setter.Body.Add(expressionStatement);
            }

            // Return the result of the AST generation
            String generated = this.options.OutputNode(propertyDeclaration);

            // Add ident space
            return Indent(generated, indent);
        }
    }
}