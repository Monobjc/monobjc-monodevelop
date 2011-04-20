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
		public XcodeProjectTracker (MonobjcProject project) : base(project)
		{
			PropertyService.PropertyChanged += this.PropertyService_PropertyChanged;
			ProjectDomService.TypesUpdated += HandleProjectDomServiceTypesUpdated;
		}
		
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public override void Dispose ()
		{
			PropertyService.PropertyChanged -= this.PropertyService_PropertyChanged;
			base.Dispose ();
		}

		internal void GenerateSurrogateProject ()
		{
			if (!this.IsEnabled || !this.IsProjectReady) {
				LoggingService.LogInfo("Project is not ready yet");
				return;
			}
			
			LoggingService.LogInfo ("GenerateSurrogateProject " + this.Project.BaseDirectory + "/" + this.Project.Name);

			// Collect references information
			ProjectReferenceCollection references = this.Project.References;
			
			
			
			
			// Create the main project
			String projectName = this.Project.Name;
			this.xcodeProject = new XcodeProject (this.Project.BaseDirectory, projectName);

            xcodeProject.AddTarget(projectName, PBXProductType.Application);

			foreach(var framework in this.Project.OSFrameworks.Split(';')) {
				this.xcodeProject.AddFramework("Frameworks", framework, projectName);
			}

			IEnumerable<String> headerFiles = HeaderGenerator.GenerateHeaders (this.Project, this.Project.BaseDirectory);
			foreach (String headerFile in headerFiles) {
				LoggingService.LogInfo ("Adding " + headerFile);
				this.xcodeProject.AddFile ("Files", headerFile, projectName);
			}
			
			foreach (var xibFile in this.Project.Files) {
				if (BuildHelper.IsXIBFile (xibFile)) {
					this.xcodeProject.AddFile ("Files", xibFile.FilePath, projectName);
				}
			}
			
            xcodeProject.AddBuildConfigurationSettings("Release", null, "ARCHS", "$(ARCHS_STANDARD_32_64_BIT)");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "SDKROOT", "macosx");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "GCC_VERSION", "com.apple.compilers.llvm.clang.1_0");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "MACOSX_DEPLOYMENT_TARGET", "10.6");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "GCC_C_LANGUAGE_STANDARD", "gnu99");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "GCC_WARN_64_TO_32_BIT_CONVERSION", "YES");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "GCC_WARN_ABOUT_RETURN_TYPE", "YES");
            xcodeProject.AddBuildConfigurationSettings("Release", null, "GCC_WARN_UNUSED_VARIABLE", "YES");

            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "COPY_PHASE_STRIP", "YES");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "INFOPLIST_FILE", "Info.plist");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "PRODUCT_NAME", "$(TARGET_NAME)");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "WRAPPER_EXTENSION", "app");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "ALWAYS_SEARCH_USER_PATHS", "NO");
            xcodeProject.AddBuildConfigurationSettings("Release", projectName, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");

            xcodeProject.Save();

						
			// 2. For each Monobjc project, ask for surrogate project generation
			
			// 3. Gather types information in this project
			
			// 4. Generate the header files
			
			// 5. Construct the surrogate project
			
			// 6. Return the path to the surrogate project
		}

		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject ();
		}

		protected override void HandleFileChangedInProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject ();
		}

		protected override void HandleFileRemovedFromProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateSurrogateProject ();
		}

		protected override void HandleReferenceAddedToProject (object sender, ProjectReferenceEventArgs e)
		{
			this.GenerateSurrogateProject ();
		}

		protected override void HandleReferenceRemovedFromProject (object sender, ProjectReferenceEventArgs e)
		{
			this.GenerateSurrogateProject ();
		}

		private void PropertyService_PropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			switch (e.Key) {
			case DeveloperToolsDesktopApplication.DEVELOPER_TOOLS:
				#if DEBUG
				LoggingService.LogInfo ("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
				#endif
				this.GenerateSurrogateProject();
				break;
			default:
				break;
			}
		}
		
		private void HandleProjectDomServiceTypesUpdated (object sender, TypeUpdateInformationEventArgs e)
		{
			if (e.Project != this.Project) {
				return;
			}
			
			// Maintain a map of the types
			// Couple it to the XcodeProject
			
			foreach (var type in e.TypeUpdateInformation.Added) {
				LoggingService.LogInfo ("HandleProjectDomServiceTypesUpdated :: Added => " + type.Name);
			}
			foreach (var type in e.TypeUpdateInformation.Modified) {
				LoggingService.LogInfo ("HandleProjectDomServiceTypesUpdated :: Modified => " + type.Name);
			}
			foreach (var type in e.TypeUpdateInformation.Removed) {
				LoggingService.LogInfo ("HandleProjectDomServiceTypesUpdated :: Removed => " + type.Name);
			}
		}

		private bool IsEnabled
		{
			get
			{ 
				Version version = DeveloperToolsDesktopApplication.DeveloperToolsVersion;
				return (version != null) && (version.Major == 4);
			}
		}
	}
}
