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
	class EmbeddingHandler : ProjectHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmbeddingHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public EmbeddingHandler (MonobjcProject project) : base(project)
		{
		}

		/// <summary>
		/// Set the resource ids.
		/// </summary>
		public void SetResourceIds (IEnumerable<ProjectFileEventInfo> e)
		{
			this.SetResourceIds (e.Select (info => info.ProjectFile));
		}

		/// <summary>
		/// Set the resource ids.
		/// </summary>
		private void SetResourceIds (IEnumerable<ProjectFile> files)
		{
			foreach (ProjectFile file in files) {
				FilePath filePath = file.FilePath;
				if (BuildHelper.IsNIBFile (filePath) && file.BuildAction == BuildAction.EmbeddedResource) {
					this.SetResourceId (file);
				}
			}
		}

		/// <summary>
		/// Set the resource id (default namepace + filename without extension + locale).
		/// </summary>
		private void SetResourceId (ProjectFile file)
		{
			FilePath filePath = file.FilePath;
			String resourceId = this.Project.DefaultNamespace + "." + Path.GetFileNameWithoutExtension (filePath);
			FilePath parentPath = filePath.ParentDirectory;
			if (String.Equals (parentPath.Extension, Constants.DOT_LPROJ, StringComparison.InvariantCultureIgnoreCase)) {
				String parent = parentPath.FileNameWithoutExtension;
				if (parent != this.Project.DevelopmentRegion) {
					resourceId = resourceId + "." + parent;
				}
				file.ResourceId = resourceId;
			}
		}
	}
}
