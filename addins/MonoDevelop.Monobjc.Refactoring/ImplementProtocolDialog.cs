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
using ICSharpCode.NRefactory.Ast;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Refactoring;

namespace MonoDevelop.Monobjc.Refactoring
{
	partial class ImplementProtocolDialog : BaseDialog
	{
		public ImplementProtocolDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project) : base(refactoring, options, project)
		{
			this.Build ();
			
			this.Title = GettextCatalog.GetString ("Implement Objective-C Protocol");
			
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
					case MemberType.Method:
						code.Append (this.GenerateMethod (declaringType, (IMethod)member, provider, indent));
						code.AppendLine ();
						break;
					case MemberType.Property:
						IMethod getter = member.DeclaringType.Members.FirstOrDefault (m => String.Equals (m.Name, "get_" + member.Name)) as IMethod;
						IMethod setter = member.DeclaringType.Members.FirstOrDefault (m => String.Equals (m.Name, "set_" + member.Name)) as IMethod;
						
						code.Append (this.GenerateProperty (declaringType, (IProperty)member, getter, setter, provider, indent));
						code.AppendLine ();
						break;
					}
				}
				
#if MD_2_4
				// Prompt for an insertion point
				InsertionCursorEditMode mode = new InsertionCursorEditMode (editor, HelperMethods.GetInsertionPoints (editor.Document, declaringType));
				mode.CurIndex = 0;
				mode.StartMode ();
				mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
					if (args.Success) {
						args.InsertionPoint.Insert (editor, code.ToString ());
					}
				};
#endif
#if MD_2_6 || MD_2_8
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
#endif
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
			ProjectResolver resolver = new ProjectResolver (this.project);
			IEnumerable<IType> protocols = resolver.GetAllProtocols ();
			foreach (IType type in protocols) {
				String protocol = AttributeHelper.GetAttributeValue (type, AttributeHelper.OBJECTIVE_C_PROTOCOL);
				if (String.IsNullOrEmpty (protocol)) {
					protocol = type.Name;
				}
				
				TreeStore store = (TreeStore)this.treeviewProtocols.Model;
				store.AppendValues (ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Interface, IconSize.Menu), protocol + " (" + type.FullName + ")", type);
			}
		}

		private void PopulateTreeView (IType type)
		{
			TreeStore store = (TreeStore)this.treeviewMembers.Model;
			store.Clear ();
			
			foreach (IMember member in type.Members) {
				switch (member.MemberType) {
				case MemberType.Method:
					if (member.Name.StartsWith ("get_")) {
						continue;
					}
					if (member.Name.StartsWith ("set_")) {
						continue;
					}
					store.AppendValues (false, ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Method, IconSize.Menu), member.Name, member);
					break;
				case MemberType.Property:
					store.AppendValues (false, ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Property, IconSize.Menu), member.Name, member);
					break;
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

		private String GenerateMethod (IType declaringType, IMethod method, INRefactoryASTProvider provider, String indent)
		{
			// Retrieve the Objective-C message in the attribute
			String message = AttributeHelper.GetAttributeValue (method, AttributeHelper.OBJECTIVE_C_MESSAGE);
			AttributeSection attributeSection = new AttributeSection ();
			if (!String.IsNullOrEmpty (message)) {
				ICSharpCode.NRefactory.Ast.Attribute attribute = new ICSharpCode.NRefactory.Ast.Attribute ("ObjectiveCMessage", new List<Expression> { new PrimitiveExpression (message) }, null);
				attributeSection.Attributes.Add (attribute);
			}
			
			// Create the method declaration
			MethodDeclaration methodDeclaration = new MethodDeclaration ();
			methodDeclaration.Attributes.Add (attributeSection);
			methodDeclaration.Modifier = ICSharpCode.NRefactory.Ast.Modifiers.Public | ICSharpCode.NRefactory.Ast.Modifiers.Virtual;
			methodDeclaration.Name = method.Name;
			methodDeclaration.TypeReference = this.Shorten (declaringType, method.ReturnType);
			foreach (var parameter in method.Parameters) {
				ParameterDeclarationExpression parameterDeclarationExpression = new ParameterDeclarationExpression (this.Shorten (declaringType, parameter.ReturnType), parameter.Name);
				if (parameter.IsOut) {
					parameterDeclarationExpression.ParamModifier |= ICSharpCode.NRefactory.Ast.ParameterModifiers.Out;
				}
				if (parameter.IsRef) {
					parameterDeclarationExpression.ParamModifier |= ICSharpCode.NRefactory.Ast.ParameterModifiers.Ref;
				}
				methodDeclaration.Parameters.Add (parameterDeclarationExpression);
			}
			
			// Create the method body
			methodDeclaration.Body = new BlockStatement ();
			ThrowStatement throwStatement = new ThrowStatement (new ObjectCreateExpression (new TypeReference ("System.NotImplementedException"), new List<Expression> ()));
			methodDeclaration.Body.AddChild (throwStatement);
			
			// Return the result of the AST generation
			return provider.OutputNode (this.options.Dom, methodDeclaration, indent);
		}

		private String GenerateProperty (IType declaringType, IProperty property, IMethod getterMethod, IMethod setterMethod, INRefactoryASTProvider provider, String indent)
		{
			// Create the property declaration
			PropertyDeclaration propertyDeclaration = new PropertyDeclaration (ICSharpCode.NRefactory.Ast.Modifiers.Public | ICSharpCode.NRefactory.Ast.Modifiers.Virtual, null, property.Name, null);
			propertyDeclaration.TypeReference = this.Shorten (declaringType, property.ReturnType);
			
			// Create a "throw" statement
			ThrowStatement throwStatement = new ThrowStatement (new ObjectCreateExpression (new TypeReference ("System.NotImplementedException"), new List<Expression> ()));
			
			if (property.HasGet && getterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (getterMethod, AttributeHelper.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					ICSharpCode.NRefactory.Ast.Attribute attribute = new ICSharpCode.NRefactory.Ast.Attribute ("ObjectiveCMessage", new List<Expression> { new PrimitiveExpression (message) }, null);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "set" region
				propertyDeclaration.GetRegion = new PropertyGetRegion (new BlockStatement (), new List<AttributeSection> { attributeSection });
				propertyDeclaration.GetRegion.Block.AddChild (throwStatement);
			}
			
			if (property.HasSet && setterMethod != null) {
				// Retrieve the Objective-C message in the attribute
				String message = AttributeHelper.GetAttributeValue (getterMethod, AttributeHelper.OBJECTIVE_C_MESSAGE);
				AttributeSection attributeSection = new AttributeSection ();
				if (!String.IsNullOrEmpty (message)) {
					ICSharpCode.NRefactory.Ast.Attribute attribute = new ICSharpCode.NRefactory.Ast.Attribute ("ObjectiveCMessage", new List<Expression> { new PrimitiveExpression (message) }, null);
					attributeSection.Attributes.Add (attribute);
				}
				
				// Create the "set" region
				propertyDeclaration.SetRegion = new PropertySetRegion (new BlockStatement (), new List<AttributeSection> { attributeSection });
				propertyDeclaration.SetRegion.Block.AddChild (throwStatement);
			}
			
			// Return the result of the AST generation
			return provider.OutputNode (this.options.Dom, propertyDeclaration, indent);
		}
	}
}
