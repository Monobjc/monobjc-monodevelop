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
using System.Threading;
using Monobjc.Tools.Xcode;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;
using MonoDevelop.Ide;

namespace MonoDevelop.Monobjc.Tracking
{
	public class XcodeProjectTracker : ProjectTracker
	{
		private const String GROUP_CLASSES = "Classes";
		private const String GROUP_RESOURCES = "Resources";
		private const String GROUP_FRAMEWORKS = "Frameworks";
		private const String CONFIGURATION_RELEASE = "Release";
		private XcodeProject xcodeProject;
		private Object syncRoot = new Object ();

		/// <summary>
		/// Initializes a new instance of the <see cref="XcodeProjectTracker"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public XcodeProjectTracker (MonobjcProject project) : base(project)
		{
			PropertyService.PropertyChanged += HandlePropertyServicePropertyChanged;
			ProjectDomService.TypesUpdated += this.HandleProjectDomServiceTypesUpdated;
			this.Project.NameChanged += this.HandleProjectNameChanged;
			this.Project.Modified += this.HandleProjectModified;
		}
		
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public override void Dispose ()
		{
			PropertyService.PropertyChanged -= HandlePropertyServicePropertyChanged;
			ProjectDomService.TypesUpdated -= this.HandleProjectDomServiceTypesUpdated;
			this.Project.NameChanged -= this.HandleProjectNameChanged;
			this.Project.Modified -= this.HandleProjectModified;
			
			base.Dispose ();
		}
		
		internal FilePath ProjectFolder {
			get { return this.XcodeProject.ProjectFolder; }
		}
		
		internal void SaveProject (bool defer)
		{
			// Queue the update in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.SaveProject (false); });
				return;
			}
			
			this.XcodeProject.Save ();
		}
		
		internal void UpdateSurrogateSources (IEnumerable<IType> types, bool defer)
		{
			// Queue the creation in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.UpdateSurrogateSources (types, false); });
				return;
			}
			
			lock (this.syncRoot) {
				foreach (IType type in types) {
					Directory.CreateDirectory(this.OutputFolder);
				
					FilePath headerFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".h");
					FilePath sourceFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".m");
					
					using (StreamWriter writer = new StreamWriter(headerFile)) {
						ObjectiveCHeaderWriter headerWriter = new ObjectiveCHeaderWriter (this.Project);
						headerWriter.Write (writer, type);
					}
				
					using (StreamWriter writer = new StreamWriter(sourceFile)) {
						ObjectiveCSourceWriter headerWriter = new ObjectiveCSourceWriter (this.Project);
						headerWriter.Write (writer, type);
					}
				
					this.XcodeProject.AddFile (GROUP_CLASSES, headerFile, this.TargetName);
					this.XcodeProject.AddFile (GROUP_CLASSES, sourceFile, this.TargetName);
				}
			}
			
			this.SaveProject (false);
		}
		
		internal void DeleteSurrogateSources (IEnumerable<IType> types, bool defer)
		{
			// Queue the removal in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.DeleteSurrogateSources (types, false); });
				return;
			}
			
			lock (this.syncRoot) {
				foreach (IType type in types) {
					Directory.CreateDirectory(this.OutputFolder);
				
					FilePath headerFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".h");
					FilePath sourceFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".m");
				
					File.Delete (headerFile);
					File.Delete (sourceFile);
				
					this.XcodeProject.RemoveFile (GROUP_CLASSES, headerFile, this.TargetName);
					this.XcodeProject.RemoveFile (GROUP_CLASSES, sourceFile, this.TargetName);
				}
			}
			
			this.SaveProject (false);
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			if (!this.IsReady) {
				return;
			}
#if MD_2_4
            ProjectFile projectFile = e.ProjectFile;
			this.AddResource(projectFile);
#endif
#if MD_2_6
			foreach(ProjectFileEventInfo info in e)
			{
	            ProjectFile projectFile = info.ProjectFile;
				LoggingService.LogInfo ("XcodeProjectTracker::HandleFileAddedToProject " + projectFile.FilePath);				
				this.AddResource(projectFile);
			}
#endif
			this.SaveProject (true);
		}

		protected override void HandleFileRemovedFromProject (object sender, ProjectFileEventArgs e)
		{
			if (!this.IsReady) {
				return;
			}			
#if MD_2_4
            ProjectFile projectFile = e.ProjectFile;
			this.RemoveResource(projectFile);
#endif
#if MD_2_6
			foreach(ProjectFileEventInfo info in e)
			{
	            ProjectFile projectFile = info.ProjectFile;
				LoggingService.LogInfo ("XcodeProjectTracker::HandleFileRemovedFromProject " + projectFile.FilePath);				
				this.RemoveResource(projectFile);
			}
#endif
			this.SaveProject (false);
		}

		private void HandlePropertyServicePropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			switch (e.Key) {
			case DeveloperToolsDesktopApplication.DEVELOPER_TOOLS:
				LoggingService.LogInfo ("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
				break;
			default:
				return;
			}
			
			if (!this.IsReady) {
				return;
			}
			
			this.SaveProject (true);
		}
		
		private void HandleProjectDomServiceTypesUpdated (object sender, TypeUpdateInformationEventArgs e)
		{
			if (!this.IsEnabled || e.Project != this.Project) {
				return;
			}
			
			IList<IType > typesUpdated = new List<IType> ();
			IList<IType > typesDeleted = new List<IType> ();
			
			foreach (var type in e.TypeUpdateInformation.Added) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
				LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectDomServiceTypesUpdated :: Added => " + type.Name);
				typesUpdated.Add (type);
			}
			foreach (var type in e.TypeUpdateInformation.Modified) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
				LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectDomServiceTypesUpdated :: Modified => " + type.Name);
				typesUpdated.Add (type);
			}
			foreach (var type in e.TypeUpdateInformation.Removed) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
				LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectDomServiceTypesUpdated :: Removed => " + type.Name);
				typesDeleted.Add (type);
			}
			
			this.UpdateSurrogateSources (typesUpdated, true);
			this.DeleteSurrogateSources (typesDeleted, true);
		}

		private void HandleProjectNameChanged (object sender, SolutionItemRenamedEventArgs e)
		{
			if (!this.IsReady) {
				return;
			}
			
			LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectNameChanged");
			
			this.XcodeProject.Delete ();
			this.XcodeProject = null;
			
			this.SaveProject (true);
		}

		private void HandleProjectModified (object sender, SolutionItemModifiedEventArgs e)
		{
			if (!this.IsReady) {
				return;
			}
			
			bool frameworksChanged = false;
			bool referencesChanged = false;
#if MD_2_4
			switch(e.Hint) {
			case "References":
				referencesChanged = true;
				break;
			case "MacOSFrameworks":
				frameworksChanged = true;
				break;
			default:
				break;
			}
#endif
#if MD_2_6
			foreach(SolutionItemModifiedEventInfo info in e) {
				switch(info.Hint) {
				case "References":
					referencesChanged = true;
					break;
				case "MacOSFrameworks":
					frameworksChanged = true;
					break;
				default:
					break;
				}
			}
#endif
			if (referencesChanged) {
				this.ClearProjectReferences();
				this.AddProjectReferences();
			}
			if (frameworksChanged) {
				this.ClearFrameworks ();
				this.AddFrameworks ();
			}
			if (referencesChanged || frameworksChanged) {
				this.SaveProject (true);
			}
		}
		
		private bool IsEnabled {
			get { 
				Version version = DeveloperToolsDesktopApplication.DeveloperToolsVersion;
				return (version != null) && (version.Major >= 4);
			}
		}
		
		private bool IsReady {
			get {
				return this.IsEnabled && !this.Project.Loading;
			}
		}
		
		private XcodeProject XcodeProject {
			get {
				lock (this) {
					if (this.xcodeProject == null) {
						String name = this.Project.Name;
						String targetName = this.TargetName;
						this.xcodeProject = new XcodeProject (this.OutputFolder, name);
						
						this.xcodeProject.Document.Project.ProjectRoot = "../..";
						
						this.xcodeProject.AddGroup (GROUP_CLASSES);
						this.xcodeProject.AddGroup (GROUP_RESOURCES);
						this.xcodeProject.AddGroup (GROUP_FRAMEWORKS);
						
						// Set default settings
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "ARCHS", "$(ARCHS_STANDARD_32_64_BIT)");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "SDKROOT", "macosx");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "GCC_VERSION", "com.apple.compilers.llvm.clang.1_0");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "MACOSX_DEPLOYMENT_TARGET", "10.6");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "GCC_C_LANGUAGE_STANDARD", "gnu99");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "GCC_WARN_64_TO_32_BIT_CONVERSION", "YES");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "GCC_WARN_ABOUT_RETURN_TYPE", "YES");
						this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, null, "GCC_WARN_UNUSED_VARIABLE", "YES");
	
						// Add the main target
						CompileTarget compileTarget = this.Project.CompileTarget;
						switch (compileTarget) {
						case CompileTarget.Exe:
							this.xcodeProject.AddTarget (targetName, PBXProductType.Application);
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "COPY_PHASE_STRIP", "YES");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "INFOPLIST_FILE", "../../Info.plist");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "PRODUCT_NAME", "$(TARGET_NAME)");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "WRAPPER_EXTENSION", "app");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "ALWAYS_SEARCH_USER_PATHS", "NO");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
							break;
						case CompileTarget.Library:
							this.xcodeProject.AddTarget (targetName, PBXProductType.LibraryDynamic);
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "ALWAYS_SEARCH_USER_PATHS", "NO");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "COPY_PHASE_STRIP", "YES");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "DYLIB_COMPATIBILITY_VERSION", "1");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "DYLIB_CURRENT_VERSION", "1");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
							this.xcodeProject.AddBuildConfigurationSettings (CONFIGURATION_RELEASE, targetName, "PRODUCT_NAME", "$(TARGET_NAME)");
							break;
						default:
							throw new NotSupportedException ();
						}
						
						this.AddClasses ();
						this.AddResources ();
						this.AddFrameworks ();
						this.AddProjectReferences();
					}
					return this.xcodeProject;
				}
			}
			set {
				this.xcodeProject = value;
			}
		}
		
		private FilePath OutputFolder {
			get {
				ConfigurationSelector configurationSelector = IdeApp.Workspace.ActiveConfiguration;
				FilePath outputFile = this.Project.GetOutputFileName (configurationSelector);
				return outputFile.ParentDirectory;
			}
		}
		
		private String TargetName {
			get { return (this.Project != null) ? this.Project.Name : "Monobjc"; }
		}
		
		private void AddClasses ()
		{
			ProjectResolver resolver = new ProjectResolver (this.Project);
			IEnumerable<IType > types = resolver.GetAllClasses (true);
			this.UpdateSurrogateSources (types, true);
		}
		
		private void AddResources ()
		{
			foreach (ProjectFile projectFile in this.Project.Files) {
				this.AddResource (projectFile);
			}
		}
		
		private void AddResource (ProjectFile projectFile)
		{
			if (BuildHelper.IsInfoPlist (projectFile) || BuildHelper.IsXIBFile (projectFile) || BuildHelper.IsStringsFile (projectFile)) {
				this.XcodeProject.AddFile (GROUP_RESOURCES, projectFile.FilePath, this.TargetName);
			}
		}
		
		private void RemoveResource (ProjectFile projectFile)
		{
			if (BuildHelper.IsInfoPlist (projectFile) || BuildHelper.IsXIBFile (projectFile) || BuildHelper.IsStringsFile (projectFile)) {
				this.XcodeProject.RemoveFile (GROUP_RESOURCES, projectFile.FilePath);
			}
		}
		
		private void ClearFrameworks ()
		{
			IList<String > frameworks = new List<String> (this.XcodeProject.GetFrameworks (this.TargetName));
			foreach (String framework in frameworks) {
				this.XcodeProject.RemoveFramework (GROUP_FRAMEWORKS, framework, this.TargetName);
			}
		}
		
		private void AddFrameworks ()
		{
			foreach (String framework in this.Project.OSFrameworks.Split(';')) {
				this.XcodeProject.AddFramework (GROUP_FRAMEWORKS, framework, this.TargetName);
			}
		}
		
		private void ClearProjectReferences ()
		{
		}
		
		private void AddProjectReferences ()
		{
		}
	}
}
