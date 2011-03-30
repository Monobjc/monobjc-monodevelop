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
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	///   Command handler for opening XIB file with Developer Tools.
	/// </summary>
	public class OpenWithDeveloperToolsCommandHandler : CommandHandler
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
			ProjectFile file = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
			if (file == null || !BuildHelper.IsXIBFile (file)) {
				return;
			}
			
			MonobjcProject project = file.Project as MonobjcProject;
			if (project == null) {
				return;
			}
			
			// Retrieve the configuration
			String program = PropertyService.Get<String> (Tracking.XcodeProjectTracker.DEVELOPER_TOOLS_ROOT, "/Developer");
			int xcodeVersion = PropertyService.Get<int> (Tracking.XcodeProjectTracker.XCODE_VERSION, 320);
			String target;
			
			switch (xcodeVersion) {
			case 320:
				program += "/Application/Interface Builder.app";
				target = file.FilePath.ToAbsolute ();
				break;
			case 400:
				program += "/Application/Xcode.app";
				// TODO: Get the surrogate project file
				MonobjcProject project = file.Project as MonobjcProject;
				target = null;
				break;
			default:
				return;
			}
			
			// Build the arguments
			StringBuilder arguments = new StringBuilder ();
			arguments.Append ("-a");
			arguments.Append (" \"");
			arguments.Append (program);
			arguments.Append ("\" \"");
			arguments.Append (target);
			arguments.Append ("\"");
			
			// Launch the developer tools
			ProcessHelper helper = new ProcessHelper ("open", arguments.ToString ());
			helper.Execute ();
		}
	}
}
