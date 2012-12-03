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
		private const String DOT_DESIGNER = ".designer";
		private const String DOT_NIB = ".nib";
		private const String DOT_XIB = ".xib";

		private String sourceExtension;
		private String designerExtension;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyProjectTracker"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public DependencyProjectTracker (MonobjcProject project) : base(project)
		{
			String sourceFile = this.Project.LanguageBinding.GetFileName ("ABC");
			this.sourceExtension = sourceFile.Substring (3);
			this.designerExtension = DOT_DESIGNER + this.sourceExtension;
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Collect dependencies for each added items
			List<FilePath> dependencies = new List<FilePath> ();
			foreach (ProjectFileEventInfo info in e) {
				IDELogger.Log("DependencyProjectTracker::HandleFileAddedToProject -- collecting for {0}", info.ProjectFile);
				IEnumerable<FilePath> files = this.GuessDependencies (info.ProjectFile);
				if (files != null) {
					// TODO: Handle dependency here
					dependencies.AddRange (files);
				}
			}
			
			// Add dependencies if needed
			if (dependencies != null) {
				foreach (FilePath file in dependencies.Where(f => !this.Project.IsFileInProject(f))) {
					IDELogger.Log("DependencyProjectTracker::HandleFileAddedToProject -- adding {0}", file);
					this.Project.AddFile (file);
				}
			}
		}
		
		private IEnumerable<FilePath> GuessDependencies (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			if (extension == this.sourceExtension) {
				return this.GuessDependenciesForSource (file);
			} else if (extension == DOT_NIB) {
				return this.GuessDependenciesForIB (file);
			} else if (extension == DOT_XIB) {
				return this.GuessDependenciesForIB (file);
			}
			return null;
		}
		
		private IEnumerable<FilePath> GuessDependenciesForSource (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			
			// If the dependant file is being added
			if (filePath.FileName.EndsWith (this.designerExtension)) {
				FilePath parentName = filePath;
				parentName = parentName.ToString ().Replace (this.designerExtension, this.sourceExtension);
				if (File.Exists (parentName)) {
					// TODO: Move dependency upward
					if (this.Project.IsFileInProject (parentName) && String.IsNullOrEmpty(file.DependsOn)) {
						file.DependsOn = parentName.FileName;
					}
					return new[] { parentName };
				}
			}
			// If the master file is being added
			else {
				FilePath childName = filePath.ChangeExtension (this.designerExtension);
				if (File.Exists (childName)) {
					return new[] { childName };
				}
			}
			
			return null;
		}
		
		private IEnumerable<FilePath> GuessDependenciesForIB (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			String extension = filePath.Extension;
			FilePath peerName = null;
			bool depends = false;
			
			switch (extension) {
			// If NIB is added, search for XIB
			case DOT_NIB:
				peerName = filePath.ChangeExtension (DOT_XIB);
				depends = this.Project.IsFileInProject (peerName);
				break;
			// If XIB is added, search for NIB
			case DOT_XIB:
				peerName = filePath.ChangeExtension (DOT_NIB);
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
