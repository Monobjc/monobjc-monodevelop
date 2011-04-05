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
using Monobjc.Tools.Xcode;
using System.Collections.Generic;

namespace MonoDevelop.Monobjc.Tracking
{
    public class XcodeProjectTracker : ProjectTracker
    {
		private XcodeProject xcodeProject;
		
        /// <summary>
        /// Initializes a new instance of the <see cref="XcodeProjectTracker"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public XcodeProjectTracker(MonobjcProject project) : base(project)
        {
            PropertyService.PropertyChanged += this.PropertyService_PropertyChanged;
        }
		
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            PropertyService.PropertyChanged -= this.PropertyService_PropertyChanged;
			base.Dispose();
        }
		
		internal void GenerateSurrogateProject()
		{
			LoggingService.LogInfo("GenerateSurrogateProject");
			
			// Collect references information
			ProjectReferenceCollection references = this.Project.References;
			
			//if (this.xcodeProject == null) {
	            this.xcodeProject = new XcodeProject(this.Project.BaseDirectory, this.Project.Name);
				
				//PBXProject pbxProject = this.xcodeProject.Document.Project;
				//pbxProject.ProjectDirPath = ".";
			//}
			
			this.xcodeProject.AddGroup("Classes");
			IList<String> headerFiles = HeaderGenerator.GenerateHeaders(this.Project, this.Project.BaseDirectory);
			foreach(String headerFile in headerFiles) {
				this.xcodeProject.AddFile("Classes", headerFile);
			}
			
            XCBuildConfiguration buildConfiguration1 = new XCBuildConfiguration("Release");
            buildConfiguration1.BuildSettings.Add("ARCHS", "$(ARCHS_STANDARD_32_64_BIT)");
            buildConfiguration1.BuildSettings.Add("MACOSX_DEPLOYMENT_TARGET", "10.6");
            buildConfiguration1.BuildSettings.Add("SDKROOT", "macosx");
            xcodeProject.AddBuildConfiguration(buildConfiguration1, null);
			
			xcodeProject.Save();
			
			// 2. For each Monobjc project, ask for surrogate project generation
			
			// 3. Gather types information in this project
			
			// 4. Generate the header files
			
			// 5. Construct the surrogate project
			
			// 6. Return the path to the surrogate project
			
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject();
		}
		
		protected override void HandleFileChangedInProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject();
		}
		
		protected override void HandleFileRemovedFromProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject();
		}
		
		protected override void HandleReferenceAddedToProject (object sender, ProjectReferenceEventArgs e)
		{
			this.GenerateSurrogateProject();
		}
		
		protected override void HandleReferenceRemovedFromProject (object sender, ProjectReferenceEventArgs e)
		{
			this.GenerateSurrogateProject();
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
                case DeveloperToolsDesktopApplication.DEVELOPER_TOOLS:
#if DEBUG
		            LoggingService.LogInfo("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
#endif
                    break;
                default:
                    break;
            }
        }
    }
}