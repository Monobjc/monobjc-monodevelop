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
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	class EmbeddingHandler : ProjectHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmbeddingHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public EmbeddingHandler (MonobjcProject project) : base(project)
		{
		}

		public void ApplyEmbedding(IEnumerable<ProjectFileEventInfo> e)
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
				IDELogger.Log ("EmbeddingHandler::ApplyEmbedding -- {0}", projectFile);
				if (BuildHelper.IsEmbeddedXIBFile (projectFile)) {
					filesToAdd.Add (projectFile);
				}
				if (BuildHelper.IsNormalXIBFile (projectFile)) {
					filesToRemove.Add (projectFile);
				}
			}

			if (filesToAdd.Count == 0 && filesToRemove.Count == 0) {
				return;
			}

			this.ScheduleForEmbedding(filesToAdd, filesToRemove);
		}

		/// <summary>
		/// Set the resource id (default namepace + filename without extension + locale).
		/// </summary>
		private void SetResourceId (ProjectFile file)
		{
			if (BuildHelper.IsNIBFile (file)) {
				return;
			}

			FilePath filePath = file.FilePath;
			String resourceId = this.Project.DefaultNamespace + "." + Path.GetFileNameWithoutExtension (filePath);
			FilePath parentPath = filePath.ParentDirectory;
			if (String.Equals (parentPath.Extension, Constants.DOT_LPROJ, StringComparison.InvariantCultureIgnoreCase)) {
				resourceId = resourceId + "." + parentPath.FileNameWithoutExtension;
			}
			file.ResourceId = resourceId;
		}

		private void ScheduleForEmbedding (IList<ProjectFile> filesToAdd, IList<ProjectFile> filesToRemove)
		{
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
			ThreadPool.QueueUserWorkItem (delegate {
				try {
					monitor.BeginTask (GettextCatalog.GetString ("Setting up embedding..."), 1 + filesToAdd.Count + filesToRemove.Count);

					IList<FilePair> pairs = new List<FilePair> ();
					foreach (ProjectFile projectFile in filesToAdd) {
						FilePair pair = this.Project.GetIBFile (projectFile, Constants.EmbeddedInterfaceDefinition, null);
						if (pair == null) {
							continue;
						}
						pairs.Add (pair);
					}

					foreach (FilePair pair in pairs) {
						// Create file if needed
						if (!File.Exists (pair.Destination)) {
							File.Create (pair.Destination);
						}
						
						// Add file if needed
						ProjectFile destinationFile = this.Project.AddFile (pair.Destination);
						this.SetResourceId (destinationFile);

						monitor.Step(1);
					}

					// Remove dependent files
					foreach (ProjectFile file in filesToRemove) {
						foreach (ProjectFile child in file.DependentChildren) {
							child.DependsOn = null;
						}

						monitor.Step(1);
					}

					this.Project.Save (monitor);
					
					monitor.EndTask ();
				} catch (Exception ex) {
					monitor.ReportError (ex.Message, ex);
				} finally {
					monitor.Dispose ();
				}
			});
		}
	}
}
