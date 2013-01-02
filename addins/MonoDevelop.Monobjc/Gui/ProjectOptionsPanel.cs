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
using Gtk;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui.Dialogs;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	/// The options panel for general.
	/// </summary>
	public class ProjectOptionsPanel : ItemOptionsPanel
	{
		private ProjectOptionsWidget widget;

		/// <summary>
		///   Creates the panel widget.
		/// </summary>
		/// <returns></returns>
		public override Widget CreatePanelWidget ()
		{
			if (this.widget == null) {
				this.widget = new ProjectOptionsWidget ();
			}
			this.widget.Load (this.ConfiguredProject as MonobjcProject);
			return this.widget;
		}
		
		/// <summary>
		///   Applies the changes.
		/// </summary>
		public override void ApplyChanges ()
		{
			String message;
			if (this.widget.CanSave(this.ConfiguredProject as MonobjcProject, out message)) {
				this.widget.Save(this.ConfiguredProject as MonobjcProject);
			} else {
				using(MessageDialog dialog = new MessageDialog(this.ParentDialog, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, GettextCatalog.GetString("Please check your values. {0}"), message)) {
					dialog.Run();
					dialog.Destroy();
				}
			}
		}
	}
}
