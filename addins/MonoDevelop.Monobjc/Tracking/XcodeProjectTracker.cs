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
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Tracking
{
    public class XcodeProjectTracker : ProjectTracker
    {
        // TODO: Move constant
        public const String DEVELOPER_TOOLS_ROOT = "MonoDevelop.Monobjc.DeveloperToolsRoot";
        public const String XCODE_VERSION = "MonoDevelop.Monobjc.XcodeVersion";

        /// <summary>
        /// Initializes a new instance of the <see cref="XcodeProjectTracker"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public XcodeProjectTracker(MonobjcProject project) : base(project)
        {
            PropertyService.PropertyChanged += this.PropertyService_PropertyChanged;
			ProjectDomService.TypesUpdated += this.ProjectDomService_TypesUpdated;
        }
		
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            PropertyService.PropertyChanged -= this.PropertyService_PropertyChanged;
			ProjectDomService.TypesUpdated -= this.ProjectDomService_TypesUpdated;
			base.Dispose();
        }
		
		internal void GenerateSurrogateProject()
		{
			// 1. Collect references information
			
			// 2. For each Monobjc project, ask for surrogate project generation
			
			// 3. Gather types information in this project
			
			// 4. Generate the header files
			
			// 5. Construct the surrogate project
			
			// 6. Return the path to the surrogate project
			
			var references = this.Project.References;
			foreach(var reference in references)
			{
				if (reference.ReferenceType == ReferenceType.Project) 
				{
					// If this is a Monobjc project, ask for surrogate project generation
					
				}
			}
		}
		
		protected override void HandleReferenceAddedToProject (object sender, ProjectReferenceEventArgs e)
		{
#if DEBUG
			LoggingService.LogInfo("XcodeProjectTracker::HandleReferenceAddedToProject " + e.ProjectReference.ReferenceType);
#endif
		}
		
		protected override void HandleReferenceRemovedFromProject (object sender, ProjectReferenceEventArgs e)
		{
#if DEBUG
			LoggingService.LogInfo("XcodeProjectTracker::HandleReferenceRemovedFromProject " + e.ProjectReference.ReferenceType);
#endif
		}
		
		private bool Enabled
		{
			get
			{
				// Only enable Xcode tracking if Xcode 4.0 if selected
				return true;
			}
		}

        private void PropertyService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.Key)
            {
                case DEVELOPER_TOOLS_ROOT:
                case XCODE_VERSION:
#if DEBUG
		            LoggingService.LogInfo("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
#endif
                    break;
                default:
                    break;
            }
        }
		
        private void ProjectDomService_TypesUpdated(object sender, TypeUpdateInformationEventArgs e)
        {
			if (e.Project != this.Project)
			{
				return;
			}
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated");
#endif
			foreach (IType cls in e.TypeUpdateInformation.Removed) 
			{
#if DEBUG
//				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Removed " + cls);
#endif
			}
			foreach (IType cls in e.TypeUpdateInformation.Modified) 
			{
#if DEBUG
//				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Modified " + cls);
#endif
			}
			foreach (IType cls in e.TypeUpdateInformation.Added) 
			{
#if DEBUG
//				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Added " + cls);
#endif
			}
        }
    }
}