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
using System.Collections.Generic;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	class MigrationHandler : ProjectHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MigrationHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public MigrationHandler (MonobjcProject project) : base(project)
		{
		}

		public void Migrate (IEnumerable<ProjectFileEventInfo> e)
		{
			// Migrate "Page" to "InterfaceDefinition" when project is loaded (upward compatibility)
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				if (projectFile.BuildAction == BuildAction.Page) {
					projectFile.BuildAction = Constants.InterfaceDefinition;
				}
			}
		}
	}
}
