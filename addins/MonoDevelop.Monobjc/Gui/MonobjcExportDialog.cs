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
using System.Threading;
using Gtk;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.BundleGeneration;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   The dialog used for export operations.
	/// </summary>
	public partial class MonobjcExportDialog : Dialog
	{
		private MonobjcProject project;

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcExportDialog" /> class.
		/// </summary>
		public MonobjcExportDialog ()
		{
			this.Build ();
			
			this.radiobuttonManaged.Toggled += HandleRadiobuttonManagedhandleToggled;
			this.radiobuttonNative.Toggled += HandleRadiobuttonNativehandleToggled;
			
			this.HandleRadiobuttonManagedhandleToggled (this, EventArgs.Empty);
		}

		/// <summary>
		///   Uses the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Use (MonobjcProject project)
		{
			this.project = project;
		}

		/// <summary>
		///   Called when the export button is pressed.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		protected virtual void OnExport (object sender, EventArgs e)
		{
			BundleGenerator generator;
			
			// Select the correct generator
			if (this.radiobuttonManaged.Active) {
				generator = new ManagedBundleGenerator ();
			} else {
				generator = new NativeBundleGenerator2 ();
			}
			
			// Set the output directory
			generator.Output = this.filechooserbuttonOutput.Filename;
			
			// Pass the identifiy for signing
			generator.SigningIdentity = String.IsNullOrEmpty (this.project.SigningIdentity) ? null : this.project.SigningIdentity;
			
			// Pass the identifiy for archiving
			generator.ArchiveIdentity = String.IsNullOrEmpty (this.project.ArchiveIdentity) ? null : this.project.ArchiveIdentity;
			
			// Launch the bundle creation
			
			
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetOutputProgressMonitor (GettextCatalog.GetString ("Monobjc"), MonoDevelop.Ide.Gui.Stock.RunProgramIcon, true, true);
			Thread thread = new Thread (delegate() {
				using (monitor) {
					DispatchService.GuiDispatch (delegate() { this.EnableWidgets (false); });
					
					try {
						generator.Generate (monitor, this.project);
					} catch (Exception ex) {
						monitor.ReportError (GettextCatalog.GetString ("Error while generating the bundle."), ex);
					}
					
					DispatchService.GuiDispatch (delegate() { this.EnableWidgets (true); });
				}
			});
			thread.IsBackground = true;
			thread.Start ();
		}

		private void EnableWidgets (bool value)
		{
			this.radiobuttonManaged.Sensitive = value;
			this.radiobuttonNative.Sensitive = value;
			
			this.HandleRadiobuttonManagedhandleToggled (this, EventArgs.Empty);
		}

		private void HandleRadiobuttonManagedhandleToggled (object sender, EventArgs e)
		{
		}

		private void HandleRadiobuttonNativehandleToggled (object sender, EventArgs e)
		{
		}
	}
}
