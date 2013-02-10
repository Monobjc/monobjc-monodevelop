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
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace MonoDevelop.Monobjc.Commands
{
	/// <summary>
	///   Command handler to open the Xcode surrogate project.
	/// </summary>
	public class OpenDeveloperToolsCommandHandler : CommandHandler
	{
		/// <summary>
		///   Updates the command.
		/// </summary>
		/// <param name = "info">The info.</param>
		protected override void Update (CommandInfo info)
		{
			MonobjcProject project = IdeApp.ProjectOperations.CurrentSelectedProject as MonobjcProject;
			info.Enabled = (project != null) && DeveloperToolsDesktopApplication.IsXcode40OrHigher;
		}

		/// <summary>
		///   Runs the specified command.
		/// </summary>
		/// <param name = "dataItem">The data item.</param>
		protected override void Run (object dataItem)
		{
			MonobjcProject project = IdeApp.ProjectOperations.CurrentSelectedProject as MonobjcProject;
			if (project == null) {
				return;
			}

			DeveloperToolsDesktopApplication application = new DeveloperToolsDesktopApplication (project);
			application.Launch (new string[]{ String.Empty });
		}
	}
}