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
using System.ComponentModel;
using Gtk;
using MonoDevelop.Core;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the general options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class PreferencesWidget : Bin
	{
		/// <summary>
        ///   Initializes a new instance of the <see cref = "MonobjcPreferencesWidget" /> class.
		/// </summary>
        public PreferencesWidget()
		{
			this.Build ();
			this.filechooserbuttonDeveloperTools.SelectionChanged += this.HandleFilechooserbuttonDeveloperToolshandleSelectionChanged;
		}

		/// <summary>
		///   Loads the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Load ()
		{
			// Load value
			String folder = DeveloperToolsDesktopApplication.DeveloperToolsFolder;
			this.filechooserbuttonDeveloperTools.SetCurrentFolder(folder);
			this.filechooserbuttonDeveloperTools.SetFilename(folder);
			this.UpdateVersion(folder);
        }

		/// <summary>
		///   Saves the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Save ()
		{
			// Save value
			DeveloperToolsDesktopApplication.DeveloperToolsFolder = this.filechooserbuttonDeveloperTools.Filename;
		}

		private void HandleFilechooserbuttonDeveloperToolshandleSelectionChanged (object sender, EventArgs e)
		{
			String folder = this.filechooserbuttonDeveloperTools.Filename;
			this.UpdateVersion(folder);
		}
		
		private void UpdateVersion(String folder)
		{
			Version version  = DeveloperToolsDesktopApplication.DeveloperToolsVersionForFolder(folder);
			if (version == null) {
				this.labelVersion.Text = GettextCatalog.GetString("No Developer Tools found");
			} else {
				this.labelVersion.Text = GettextCatalog.GetString("Developer Tools {0} found", version);
			}
		}
	}
}
