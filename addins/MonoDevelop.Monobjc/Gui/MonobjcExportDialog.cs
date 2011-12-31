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
using MonoDevelop.Projects;
using MonoDevelop.Monobjc.Utilities;

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
			
			this.EnableWidgets (true);
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
			// Check if native generation is required
			bool native = this.radiobuttonNative.Active;
			
			// Set the output directory
			String outputDirectory = this.filechooserbuttonOutput.Filename;
			
			// Launch the bundle creation
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetOutputProgressMonitor (GettextCatalog.GetString ("Monobjc"), MonoDevelop.Ide.Gui.Stock.RunProgramIcon, true, true);
			BuildResult result = new BuildResult();
			Thread thread = new Thread (delegate() {
				using (monitor) {
					DispatchService.GuiDispatch (delegate() { this.EnableWidgets (false); });
					
					try {
						ConfigurationSelector configuration = IdeApp.Workspace.ActiveConfiguration;
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Cleaning Project...", 20); });
						this.project.Clean (monitor, configuration);
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Building Project...", 40); });
						this.project.Build (monitor, configuration);
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Generating Bundle...", 60); });
						BundleGenerator.Generate(monitor, result, project, configuration, outputDirectory, native); 
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Archiving Bundle...", 70); });
						BundleGenerator.Archive(monitor, result, project, configuration, outputDirectory);
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Cleaning Project...", 80); });
						this.project.Clean (monitor, configuration);
						
						DispatchService.GuiDispatch (delegate() { this.ReportProgress ("Finished", 100); });
					} catch (Exception ex) {
						LoggingService.LogError ("Error while generating the bundle.", ex);
						monitor.ReportError (GettextCatalog.GetString ("Error while generating the bundle."), ex);
					}
					
					DispatchService.GuiDispatch (delegate() { this.EnableWidgets (true); this.OnClose(); });
				}
			});
			thread.IsBackground = true;
			thread.Start ();
		}

		private void EnableWidgets (bool value)
		{
			this.radiobuttonManaged.Sensitive = value;
			this.radiobuttonNative.Sensitive = value;
			
			if (value) {
				this.ReportProgress ("Ready", 0);
			}
		}

		private void ReportProgress (String key, int value)
		{
			this.progressbar.Fraction = value / 100.0d;
			this.progressbar.Text = GettextCatalog.GetString (key);
		}

		private void HandleRadiobuttonManagedhandleToggled (object sender, EventArgs e)
		{
		}

		private void HandleRadiobuttonNativehandleToggled (object sender, EventArgs e)
		{
		}
	}
}
