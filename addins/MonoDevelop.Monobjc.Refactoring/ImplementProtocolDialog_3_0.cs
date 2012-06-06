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
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.TypeSystem;

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
			ProjectTypeCache resolver = ProjectTypeCache.Get (this.project);
			IEnumerable<IType> protocols = resolver.GetAllProtocols (false);
			foreach (IType type in protocols) {
				String protocol = AttributeHelper.GetAttributeValue (type.GetDefinition (), AttributeHelper.OBJECTIVE_C_PROTOCOL);
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
			
			foreach (IMember member in type.GetMembers((m) => true, GetMemberOptions.ReturnMemberDefinitions)) {
				switch (member.EntityType) {
				case EntityType.Method:
					if (member.Name.StartsWith ("get_")) {
						continue;
					}
					if (member.Name.StartsWith ("set_")) {
						continue;
					}
					store.AppendValues (false, ImageService.GetPixbuf (MonoDevelop.Ide.Gui.Stock.Method, IconSize.Menu), member.Name, member);
					break;
				case EntityType.Property:
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

		private String GenerateMethod (IType declaringType, IMethod method, String provider, String indent)
		{
			return "";
		}

		private String GenerateProperty (IType declaringType, IProperty property, IMethod getterMethod, IMethod setterMethod, String provider, String indent)
		{
			return "";
		}
	}
}