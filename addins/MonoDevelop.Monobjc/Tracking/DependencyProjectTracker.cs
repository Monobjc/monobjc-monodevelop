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
	public class DependencyProjectTracker : ProjectTracker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyProjectTracker"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public DependencyProjectTracker (MonobjcProject project) : base(project)
		{
			this.DesignerExtension = Constants.DOT_DESIGNER + this.SourceExtension;
		}

		/// <summary>
		/// Gets or sets the designer extension.
		/// </summary>
		private String DesignerExtension { get; set; }

		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}

			// Collect dependencies for each added items
			List<FilePath> dependencies = new List<FilePath> ();
			foreach (ProjectFileEventInfo info in e) {
				IDELogger.Log ("DependencyProjectTracker::HandleFileAddedToProject -- collecting for {0}", info.ProjectFile);
				IEnumerable<FilePath> files = this.GuessDependencies (info.ProjectFile);
				dependencies.AddRange (files);
			}
			
			if (dependencies.Count == 0) {
				return;
			}

			// Add dependencies if needed
			foreach (FilePath file in dependencies.Where(f => !this.Project.IsFileInProject(f))) {
				IDELogger.Log ("DependencyProjectTracker::HandleFileAddedToProject -- adding {0}", file);
				this.Project.AddFile (file);
			}
		}

		private IEnumerable<FilePath> GuessDependencies (ProjectFile file)
		{
			String extension = file.FilePath.Extension;

			if (String.Equals(extension, this.SourceExtension, StringComparison.InvariantCultureIgnoreCase)) {
				return this.GuessDependenciesForSource (file);
			} else if (String.Equals(extension, Constants.DOT_NIB, StringComparison.InvariantCultureIgnoreCase)) {
				return this.GuessDependenciesForIB (file, Constants.DOT_NIB);
			} else if (String.Equals(extension, Constants.DOT_XIB, StringComparison.InvariantCultureIgnoreCase)) {
				return this.GuessDependenciesForIB (file, Constants.DOT_XIB);
			}
			return new FilePath[0];
		}
		
		private IEnumerable<FilePath> GuessDependenciesForSource (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			
			// If the dependant file is being added
			if (filePath.FileName.EndsWith (this.DesignerExtension, StringComparison.InvariantCultureIgnoreCase)) {
				FilePath parentName = filePath.ToString ().Replace (this.DesignerExtension, this.SourceExtension);
				if (File.Exists (parentName)) {
					if (this.Project.IsFileInProject (parentName) && String.IsNullOrEmpty (file.DependsOn)) {
						file.DependsOn = parentName.FileName;
					}
					return new[] { parentName };
				}
			}

			// If the master file is being added
			else {
				FilePath childName = filePath.ChangeExtension (this.DesignerExtension);
				if (File.Exists (childName)) {
					return new[] { childName };
				}
			}
			
			return null;
		}
		
		private IEnumerable<FilePath> GuessDependenciesForIB (ProjectFile file, String extension)
		{
			FilePath filePath = file.FilePath;
			FilePath peerName = null;
			bool depends = false;
			
			switch (extension) {
			// If NIB is added, search for XIB
			case Constants.DOT_NIB:
				peerName = filePath.ChangeExtension (Constants.DOT_XIB);
				depends = this.Project.IsFileInProject (peerName);
				break;

			// If XIB is added, search for NIB
			case Constants.DOT_XIB:
				peerName = filePath.ChangeExtension (Constants.DOT_NIB);
				break;

			default:
				break;
			}
			
			if (peerName != null && File.Exists (peerName)) {
				if (depends) {
					file.DependsOn = peerName;
				}
				return new[] { peerName };
			}
			return null;
		}
	}
}
