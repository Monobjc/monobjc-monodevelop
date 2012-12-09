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
using System.IO;
using System.Linq;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	class DependencyHandler : ProjectHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public DependencyHandler (MonobjcProject project) : base(project)
		{
			this.DesignerExtension = Constants.DOT_DESIGNER + this.SourceExtension;
		}

		private String DesignerExtension { get; set; }

		public IEnumerable<FilePath> GuessDependencies (ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return new FilePath[0];
			}
			
			// Collect dependencies for each added items
			List<FilePath> dependencies = new List<FilePath> ();
			foreach (ProjectFileEventInfo info in e) {
				IDELogger.Log ("DependencyHandler::GuessDependencies -- collecting for {0}", info.ProjectFile);
				IEnumerable<FilePath> files = this.GuessDependencies (info.ProjectFile);
				dependencies.AddRange (files);
			}

			return dependencies;
		}

		public void AddFiles (IEnumerable<FilePath> files)
		{
			// Add dependencies if needed
			foreach (FilePath file in files.Where(f => !this.Project.IsFileInProject(f))) {
				IDELogger.Log ("DependencyHandler::GuessDependencies -- adding {0}", file);
				this.Project.AddFile (file);
			}
		}

		private IEnumerable<FilePath> GuessDependencies (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			String extension = filePath.Extension;

			if (String.Equals (extension, this.SourceExtension, StringComparison.InvariantCultureIgnoreCase)) {
				// If the dependant file is being added
				if (filePath.FileName.EndsWith (this.DesignerExtension, StringComparison.InvariantCultureIgnoreCase)) {
					FilePath peerPath = filePath.ToString ().Replace (this.DesignerExtension, this.SourceExtension);
					if (File.Exists (peerPath)) {
						if (this.Project.IsFileInProject (peerPath) && String.IsNullOrEmpty (file.DependsOn)) {
							file.DependsOn = peerPath.FileName;
						}
						return new[] { peerPath };
					}
				}

				// If the master file is being added
				else {
					FilePath childName = filePath.ChangeExtension (this.DesignerExtension);
					if (File.Exists (childName)) {
						return new[] { childName };
					}
				}
			}

			// If NIB is added, search for XIB
			else if (String.Equals (extension, Constants.DOT_NIB, StringComparison.InvariantCultureIgnoreCase)) {
				FilePath peerPath = filePath.ChangeExtension (Constants.DOT_XIB);
				if (File.Exists (peerPath)) {
					if (this.Project.IsFileInProject (peerPath) && String.IsNullOrEmpty (file.DependsOn)) {
						file.DependsOn = peerPath.FileName;
					}
					return new[] { peerPath };
				}
			}

			// If XIB is added, search for NIB
			else if (String.Equals (extension, Constants.DOT_XIB, StringComparison.InvariantCultureIgnoreCase)) {
				FilePath peerPath = filePath.ChangeExtension (Constants.DOT_NIB);
				if (File.Exists (peerPath)) {
					return new[] { peerPath };
				}
			}

			return new FilePath[0];
		}
	}
}
