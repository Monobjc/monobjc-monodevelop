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
	public partial class ImplementProtocolDialog : BaseRefactoringDialog
	{
		public ImplementProtocolDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project) : base(refactoring, options, project)
		{
			this.Build ();

			// Configure the protocol TreeView
			// One column: Image|Text
			{
				TreeStore storeProtocols = new TreeStore (typeof(Gdk.Pixbuf), typeof(string), typeof(IType));
				
				TreeViewColumn column = new TreeViewColumn ();
				column.Title = GettextCatalog.GetString ("Protocol");
				column.Expand = true;
				column.Resizable = true;
				
				CellRendererPixbuf iconRenderer = new CellRendererPixbuf ();
				column.PackStart (iconRenderer, false);
				column.AddAttribute (iconRenderer, "pixbuf", 0);
				
				CellRendererText nameRenderer = new CellRendererText ();
				nameRenderer.Ellipsize = Pango.EllipsizeMode.End;
				column.PackStart (nameRenderer, true);
				column.AddAttribute (nameRenderer, "text", 1);
				
				this.treeviewProtocols.AppendColumn (column);
				storeProtocols.SetSortColumnId (1, SortType.Ascending);
				this.treeviewProtocols.Model = storeProtocols;
				
				this.treeviewProtocols.Selection.Changed += HandleTreeviewProtocolsSelectionhandleChanged;
			}
			
			// Configure the protocol TreeView
			// One column: Check|Image|Text
			{
				TreeStore storeMembers = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(string), typeof(IMember));
				
				TreeViewColumn column = new TreeViewColumn ();
				column.Title = GettextCatalog.GetString ("Members");
				column.Expand = true;
				column.Resizable = true;
				
				CellRendererToggle cbRenderer = new CellRendererToggle ();
				cbRenderer.Activatable = true;
				cbRenderer.Toggled += OnSelectToggled;
				column.PackStart (cbRenderer, false);
				column.AddAttribute (cbRenderer, "active", 0);
				
				CellRendererPixbuf iconRenderer = new CellRendererPixbuf ();
				column.PackStart (iconRenderer, false);
				column.AddAttribute (iconRenderer, "pixbuf", 1);
				
				CellRendererText nameRenderer = new CellRendererText ();
				nameRenderer.Ellipsize = Pango.EllipsizeMode.End;
				column.PackStart (nameRenderer, true);
				column.AddAttribute (nameRenderer, "text", 2);
				
				this.treeviewMembers.AppendColumn (column);
				storeMembers.SetSortColumnId (2, SortType.Ascending);
				this.treeviewMembers.Model = storeMembers;
			}
			
			this.buttonOk.Clicked += OnOKClicked;
			
			this.PopulateTreeView ();
			this.Validate ();
		}

		private void Validate ()
		{
			bool oneChecked = false;
			TreeStore store = (TreeStore)this.treeviewMembers.Model;
			TreeIter iter;
			if (store.GetIterFirst (out iter)) {
				do {
					if (this.GetChecked (store, iter)) {
						oneChecked = true;
						break;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
			}
			buttonOk.Sensitive = oneChecked;
		}
		
		private bool GetChecked (TreeStore store, TreeIter iter)
		{
			return (bool)store.GetValue (iter, 0);
		}
		
		private IMember GetMember (TreeStore store, TreeIter iter)
		{
			return (IMember)store.GetValue (iter, 3);
		}
		
		private void OnOKClicked (object sender, EventArgs e)
		{
			try {
				this.Generate ();
			} finally {
				this.Destroy ();
			}
		}
		
		private void OnSelectToggled (object o, ToggledArgs args)
		{
			TreeStore store = (TreeStore)this.treeviewMembers.Model;
			TreeIter iter;
			if (!store.GetIterFromString (out iter, args.Path)) {
				return;
			}
			
			bool oldValue = this.GetChecked (store, iter);
			store.SetValue (iter, 0, !oldValue);
			
			this.Validate ();
		}
		
		private void HandleTreeviewProtocolsSelectionhandleChanged (object sender, EventArgs e)
		{
			TreeIter iter;
			if (!this.treeviewProtocols.Selection.GetSelected (out iter)) {
				return;
			}
			
			TreeStore store = (TreeStore)this.treeviewProtocols.Model;
			IType type = (IType)store.GetValue (iter, 2);
			this.PopulateTreeView (type);
		}
		
		private void PopulateTreeView ()
		{
			ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
			IEnumerable<IType> protocols = cache.GetAllProtocols (false);
			foreach (IType type in protocols) {
				String protocol = AttributeHelper.GetAttributeValue (type.GetDefinition (), Constants.OBJECTIVE_C_PROTOCOL);
				if (String.IsNullOrEmpty (protocol)) {
					protocol = type.Name;
				}
				
				TreeStore store = (TreeStore)this.treeviewProtocols.Model;
#if MD_4_0 || MD_4_2
                store.AppendValues (ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Interface, IconSize.Menu), protocol + " (" + type.FullName + ")", type);
#endif
#if MD_5_0
                store.AppendValues (ImageService.GetImage (MonoDevelop.Ide.Gui.Stock.Interface, IconSize.Menu), protocol + " (" + type.FullName + ")", type);
#endif
			}
		}
		
		private void PopulateTreeView (IType type)
		{
			TreeStore store = (TreeStore)this.treeviewMembers.Model;
			store.Clear ();
			
			foreach (IMember member in type.GetMembers((m) => true, GetMemberOptions.ReturnMemberDefinitions | GetMemberOptions.IgnoreInheritedMembers)) {
#if MD_4_0 || MD_4_2
                bool isMethod = (member.EntityType == EntityType.Method);
                bool isProperty = (member.EntityType == EntityType.Property);
#endif
#if MD_5_0
                bool isMethod = (member.SymbolKind == SymbolKind.Method);
                bool isProperty = (member.SymbolKind == SymbolKind.Property);
#endif

                if (isMethod) {
                    if (member.Name.StartsWith("get_")) {
                        continue;
                    }
                    if (member.Name.StartsWith("set_")) {
                        continue;
                    }
#if MD_4_0 || MD_4_2
                    store.AppendValues (false, ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Method, IconSize.Menu), member.Name, member);
#endif
#if MD_5_0
                    store.AppendValues(false, ImageService.GetImage(MonoDevelop.Ide.Gui.Stock.Method, IconSize.Menu), member.Name, member);
#endif
                }

                if (isProperty) {
#if MD_4_0 || MD_4_2
                    store.AppendValues (false, ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Property, IconSize.Menu), member.Name, member);
#endif
#if MD_5_0
                    store.AppendValues (false, ImageService.GetImage (MonoDevelop.Ide.Gui.Stock.Property, IconSize.Menu), member.Name, member);
#endif
				}
			}
		}
		
		private IEnumerable<IMember> CheckedMembers {
			get {
				TreeStore store = (TreeStore)this.treeviewMembers.Model;
				TreeIter iter;
				if (store.GetIterFirst (out iter)) {
					do {
						if (this.GetChecked (store, iter)) {
							yield return this.GetMember (store, iter);
						}
						if (!store.IterNext (ref iter)) {
							break;
						}
					} while (true);
				}
			}
		}
		
		private void Generate ()
		{
			Document document = this.Options.Document;
			TextEditor editor = document.Editor.Parent;
			
			var loc = document.Editor.Caret.Location;
			var declaringType = document.ParsedDocument.GetInnermostTypeDefinition (loc);
			var type = this.Options.ResolveResult.Type;
			
			// Generate the code
			String indent = this.Options.GetIndent (type.GetDefinition ());
			String generatedCode = this.GenerateImplementation (declaringType, indent);
			
			var mode = new InsertionCursorEditMode (editor, MonoDevelop.Ide.TypeSystem.CodeGenerationService.GetInsertionPoints (document, declaringType));
			if (mode.InsertionPoints.Count == 0) {
				MessageService.ShowError (GettextCatalog.GetString ("No valid insertion point can be found in type '{0}'.", declaringType.Name));
				return;
			}
			ModeHelpWindow helpWindow = new InsertionCursorLayoutModeHelpWindow ();
            //helpWindow.TransientFor = IdeApp.Workbench.RootWindow;
			helpWindow.TitleText = GettextCatalog.GetString ("Implement Protocol");
			mode.HelpWindow = helpWindow;
			mode.CurIndex = mode.InsertionPoints.Count - 1;
			mode.StartMode ();
			mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
				if (args.Success) {
					args.InsertionPoint.Insert (document.Editor, generatedCode);
				}
			};
		}
		
		private String GenerateImplementation (IUnresolvedTypeDefinition declaringType, String indent)
		{
			StringBuilder code = new StringBuilder ();
			
			IDELogger.Log("ImplementProtocolDialog::GenerateImplementation -- {0}", declaringType.Name);
			
			// Generate the code for checked members
			foreach (IMember member in this.CheckedMembers) {
				if (declaringType.Members.Any (m => m.Name == member.Name)) {
					continue;
				}

				IDELogger.Log("ImplementProtocolDialog::GenerateImplementation -- Member={0}", member.Name);
				IDELogger.Log("ImplementProtocolDialog::GenerateImplementation -- Type={0}", member.DeclaringType.Name);

#if MD_4_0 || MD_4_2
                bool isMethod = (member.EntityType == EntityType.Method);
                bool isProperty = (member.EntityType == EntityType.Property);
#endif
#if MD_5_0
                bool isMethod = (member.SymbolKind == SymbolKind.Method);
                bool isProperty = (member.SymbolKind == SymbolKind.Property);
#endif

                if (isMethod) {
					IMethod method = (IMethod)member;
					String methodContent = this.GenerateMethod (method);
					code.Append (Indent (methodContent, indent));
					code.AppendLine ();
                }
              
                if (isProperty) {
					IProperty property = (IProperty)member;

					IMethod getter = property.CanGet ? property.Getter : null;
					IMethod setter = property.CanSet ? property.Setter : null;
					
					String propertyContent = this.GenerateProperty (property, getter, setter);
					code.Append (Indent (propertyContent, indent));
					code.AppendLine ();
				}
			}
			
			return code.ToString ();
		}
		
		private String GenerateMethod (IMethod method)
		{
			// Retrieve the Objective-C message in the attribute
			String message = AttributeHelper.GetAttributeValue (method, Constants.OBJECTIVE_C_MESSAGE);
			AttributeSection attributeSection = new AttributeSection ();
			if (!String.IsNullOrEmpty (message)) {
				var attribute = this.GetAttribute (Constants.OBJECTIVE_C_MESSAGE_SHORTFORM, message);
				attributeSection.Attributes.Add (attribute);
			}
			
			// Create the method declaration
			MethodDeclaration methodDeclaration = new MethodDeclaration ();
			methodDeclaration.Name = method.Name;
			methodDeclaration.Attributes.Add (attributeSection);
			methodDeclaration.Modifiers = Modifiers.Public | Modifiers.Virtual;
			methodDeclaration.ReturnType = this.Options.CreateShortType (method.ReturnType);
			
			foreach (var parameter in method.Parameters) {
				ParameterDeclaration parameterDeclaration = new ParameterDeclaration (this.Options.CreateShortType (parameter.Type), parameter.Name);
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
			ThrowStatement throwStatement = this.GetThrowStatement (Constants.NOT_IMPLEMENTED_EXCEPTION);
			methodDeclaration.Body.Add (throwStatement);
			
			// Return the result of the AST generation
			return this.Options.OutputNode (methodDeclaration);
		}
		
		private String GenerateProperty (IProperty property, IMethod getterMethod, IMethod setterMethod)
		{
			// Create the property declaration
			var propertyType = this.Options.CreateShortType (property.ReturnType);
			PropertyDeclaration propertyDeclaration = this.GetPropertyDeclaration (property.Name, propertyType, null);
			
			if (property.CanGet && getterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (getterMethod, Constants.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					var attribute = this.GetAttribute (Constants.OBJECTIVE_C_MESSAGE_SHORTFORM, message);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "get" region
				ThrowStatement throwStatement = this.GetThrowStatement (Constants.NOT_IMPLEMENTED_EXCEPTION);
				propertyDeclaration.Getter = new Accessor ();
				propertyDeclaration.Getter.Attributes.Add (attributeSection);
				propertyDeclaration.Getter.Body = new BlockStatement ();
				propertyDeclaration.Getter.Body.Add (throwStatement);
			}
			
			if (property.CanSet && setterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (setterMethod, Constants.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					var attribute = this.GetAttribute (Constants.OBJECTIVE_C_MESSAGE_SHORTFORM, message);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "set" region
				ThrowStatement throwStatement = this.GetThrowStatement (Constants.NOT_IMPLEMENTED_EXCEPTION);
				propertyDeclaration.Setter = new Accessor ();
				propertyDeclaration.Setter.Attributes.Add (attributeSection);
				propertyDeclaration.Setter.Body = new BlockStatement ();
				propertyDeclaration.Setter.Body.Add (throwStatement);
			}
			
			// Return the result of the AST generation
			return this.Options.OutputNode (propertyDeclaration);
		}
	}
}
