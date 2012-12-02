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
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	public class ResolverProjectTracker : ProjectTracker
	{
		private readonly String extension;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Tracking.ResolverProjectTracker"/> class.
		/// </summary>
		public ResolverProjectTracker (MonobjcProject project) : base(project)
		{
			FilePath file = project.LanguageBinding.GetFileName ("ABC");
			this.extension = file.Extension;
		}
		
		/// <summary>
		/// Releases all resource used by the <see cref="MonoDevelop.Monobjc.Tracking.ResolverProjectTracker"/> object.
		/// </summary>
		public override void Dispose ()
		{
			ProjectTypeCache.Remove(this.Project);
			base.Dispose ();
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			IDELogger.Log ("ResolverProjectTracker::HandleFileAddedToProject");
			ClearIfNeeded (e);
		}
		
		protected override void HandleFileChangedInProject (object sender, ProjectFileEventArgs e)
		{
			IDELogger.Log ("ResolverProjectTracker::HandleFileChangedInProject");
			ClearIfNeeded (e);
		}
		
		protected override void HandleFileRemovedFromProject (object sender, ProjectFileEventArgs e)
		{
			IDELogger.Log ("ResolverProjectTracker::HandleFileRemovedFromProject");
			ClearIfNeeded (e);
		}
		
		protected override void HandleReferenceAddedToProject (object sender, ProjectReferenceEventArgs e)
		{
			IDELogger.Log ("ResolverProjectTracker::HandleReferenceAddedToProject");
			ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
			cache.RecomputeReferences ();
		}
		
		protected override void HandleReferenceRemovedFromProject (object sender, ProjectReferenceEventArgs e)
		{
			IDELogger.Log ("ResolverProjectTracker::HandleReferenceRemovedFromProject");
			ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
			cache.RecomputeReferences ();
		}
		
		private void ClearIfNeeded (ProjectFileEventArgs e)
		{
			if (this.NeedClear (e)) {
				ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
				cache.ClearCache ();
			}
		}
		
		private bool NeedClear (ProjectFileEventArgs e)
		{
			bool needClear = false;
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				needClear |= (projectFile.FilePath.Extension == this.extension);
			}
			return needClear;
		}
	}
}
