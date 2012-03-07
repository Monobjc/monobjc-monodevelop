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
using System.Threading;
using MonoDevelop.Core;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.DesignerSupport;

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
			String sourceFile = this.Project.LanguageBinding.GetFileName ("abc");
			this.sourceExtension = sourceFile.Substring (3);
			this.designerExtension = DOT_DESIGNER + this.sourceExtension;
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Collect dependencies
			List<FilePath> dependencies = new List<FilePath> ();
			foreach(ProjectFileEventInfo info in e) {
#if DEBUG
	            LoggingService.LogInfo("DependencyProjectTracker::AddDependencies -- collecting for:" + info.ProjectFile);
#endif
				IEnumerable<FilePath> files = this.GuessDependencies(info.ProjectFile);
				if (files != null) {
					dependencies.AddRange(files);
				}
			}
			
			// Add dependencies
			if (dependencies != null) {
				foreach (FilePath file in dependencies.Where(f => !this.Project.IsFileInProject(f))) {
#if DEBUG
	            	LoggingService.LogInfo("DependencyProjectTracker::AddDependencies -- adding " + file);
#endif
					this.Project.AddFile (file);
				}
			}
		}
		
		private IEnumerable<FilePath> GuessDependencies (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			if (extension == this.sourceExtension) {
				return this.GuessDependenciesForSource(file);
			} else if (extension == DOT_NIB) {
				return this.GuessDependenciesForIB(file);
			} else if (extension == DOT_XIB) {
				return this.GuessDependenciesForIB(file);
			}
			return null;
		}
		
		private IEnumerable<FilePath> GuessDependenciesForSource (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			
			// If the dependant file is being added
			if (filePath.FileName.EndsWith(this.designerExtension)) {
				FilePath parentName = filePath;
				parentName = parentName.ToString().Replace(this.designerExtension, this.sourceExtension);
				if (File.Exists (parentName)) {
					if (this.Project.IsFileInProject(parentName)) {
						file.DependsOn = parentName.FileName;
					}
					return new[] { parentName };
				}
			}
			// If the master file is being added
			else {
				FilePath childName = filePath.ChangeExtension(this.designerExtension);
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
			
			switch(extension) {
				// If NIB is added
			case DOT_NIB:
				peerName = filePath.ChangeExtension(DOT_XIB);
				depends = this.Project.IsFileInProject(peerName);
				break;
				// If XIB is added
			case DOT_XIB:
				peerName = filePath.ChangeExtension(DOT_NIB);
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
