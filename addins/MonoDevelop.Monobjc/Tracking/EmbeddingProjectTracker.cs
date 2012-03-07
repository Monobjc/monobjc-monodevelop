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

namespace MonoDevelop.Monobjc.Tracking
{
	public class EmbeddingProjectTracker : ProjectTracker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmbeddingProjectTracker"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public EmbeddingProjectTracker (MonobjcProject project) : base(project)
		{
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Collect file added
			IList<ProjectFile> projectFiles = new List<ProjectFile> ();
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				if (BuildHelper.IsEmbeddedXIBFile (projectFile)) {
#if DEBUG
					LoggingService.LogInfo("EmbeddingProjectTracker::HandleFileAddedToProject " + projectFile);
#endif
					projectFiles.Add (projectFile);
				}
			}

			if (projectFiles.Count > 0) {
				this.AddEmbedding (projectFiles, true);
			}
		}
		
		protected override void HandleFilePropertyChangedInProject (object sender, ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Collect file added
			IList<ProjectFile> filesToAdd = new List<ProjectFile> ();
			IList<ProjectFile> filesToRemove = new List<ProjectFile> ();
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				if (BuildHelper.IsEmbeddedXIBFile (projectFile)) {
					filesToAdd.Add (projectFile);
				}
				if (BuildHelper.IsNormalXIBFile (projectFile)) {
					filesToRemove.Add (projectFile);
				}
			}

			if (filesToAdd.Count > 0) {
				this.AddEmbedding (filesToAdd, true);
			}
			if (filesToRemove.Count > 0) {
				this.RemoveEmbedding (filesToRemove, true);
			}
		}
		
		private void AddEmbedding (IList<ProjectFile> projectFiles, bool defer)
		{
			// Queue the generation in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.AddEmbedding (projectFiles, false);
				});
				return;
			}
			
			LoggingService.LogInfo ("EmbeddingProjectTracker::AddEmbedding files " + projectFiles.Count);
			
			IList<FilePair> pairs = new List<FilePair> ();
			foreach (ProjectFile projectFile in projectFiles) {
				FilePair pair = this.Project.GetIBFile (projectFile, BuildHelper.EmbeddedInterfaceDefinition, null);
				if (pair == null) {
					continue;
				}
				pairs.Add (pair);
			}
			
			LoggingService.LogInfo ("EmbeddingProjectTracker::AddEmbedding pairs " + pairs.Count ());
			
			bool modified = false;
			foreach (FilePair pair in pairs) {
				// Create file if needed
				if (!File.Exists (pair.Destination)) {
					File.Create (pair.Destination);
					modified |= true;
				}
				
				// Add file if needed
				ProjectFile destinationFile;
				if (this.Project.IsFileInProject (pair.Destination)) {
					destinationFile = this.Project.GetProjectFile (pair.Destination);
				} else { 
					destinationFile = this.Project.AddFile (pair.Destination);
					modified |= true;
				}
			
				// Set the resource id (default namepace + name without extension + locale)
				String resourceId = this.Project.DefaultNamespace + "." + System.IO.Path.GetFileNameWithoutExtension (pair.Destination);
				FilePath parentDirectory = destinationFile.FilePath.ParentDirectory;
				if (parentDirectory.Extension == ".lproj") {
					resourceId = resourceId + "." + parentDirectory.FileNameWithoutExtension;
				}
				destinationFile.ResourceId = resourceId;
			}
			
			if (modified) {
				this.SaveProject ();
			}
		}

		private void RemoveEmbedding (IList<ProjectFile> projectFiles, bool defer)
		{
			// Queue the generation in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.RemoveEmbedding (projectFiles, false);
				});
				return;
			}
			
			bool modified = false;
			
			// Remove dependent files
			foreach (ProjectFile file in projectFiles) {
				foreach (ProjectFile child in file.DependentChildren) {
					child.DependsOn = null;
					modified |= true;
				}
			}

			if (modified) {
				this.SaveProject ();
			}
		}
		
		public void SaveProject ()
		{
			DispatchService.GuiDispatch (() => {
				using (IProgressMonitor monitor = new NullProgressMonitor()) {
					this.Project.Save (monitor);
				}
			});
		}
	}
}
