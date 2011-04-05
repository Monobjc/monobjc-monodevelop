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
using System.Text;
using Monobjc.Tools.Utilities;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Desktop;
using MonoDevelop.Monobjc.Tracking;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Commands
{
	/// <summary>
	///   Command handler for opening XIB file with Developer Tools.
	/// </summary>
	public class DeveloperToolsCommandHandler : CommandHandler
	{
		/// <summary>
		///   Updates the command.
		/// </summary>
		/// <param name = "info">The info.</param>
		protected override void Update (CommandInfo info)
		{
			ProjectFile file = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
			bool status = (file != null) && BuildHelper.IsXIBFile (file);
			if (status) {
				MonobjcProject project = file.Project as MonobjcProject;
				status |= (project != null);
			}
			info.Enabled = status;
		}

		/// <summary>
		///   Runs the specified command.
		/// </summary>
		/// <param name = "dataItem">The data item.</param>
		protected override void Run (object dataItem)
		{
			// Make sure the file is valid
			ProjectFile file = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
			if (file == null || !BuildHelper.IsXIBFile (file)) {
				return;
			}
			
			// Make sure the project is valid
			MonobjcProject project = file.Project as MonobjcProject;
			if (project == null) {
				return;
			}
			
			// Launch the application
			DesktopApplication application = DeveloperToolsDesktopApplication.GetDesktopApplication(project);
			String[] files = DeveloperToolsDesktopApplication.GetFilesToOpen(project, file.FilePath);
			application.Launch(files);
		}
	}
}
