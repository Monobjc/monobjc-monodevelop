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
		private XcodeProject xcodeProject;

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
		
		internal FilePath ProjectFolder
		{
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
			
			foreach (IType type in types) {
				FilePath headerFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".h");
				FilePath sourceFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".m");
				
				using(StreamWriter writer = new StreamWriter(headerFile)) {
					ObjectiveCHeaderWriter headerWriter = new ObjectiveCHeaderWriter(this.Project);
					headerWriter.Write(writer, type);
				}
				
				using(StreamWriter writer = new StreamWriter(sourceFile)) {
					ObjectiveCSourceWriter headerWriter = new ObjectiveCSourceWriter(this.Project);
					headerWriter.Write(writer, type);
				}
				
				this.XcodeProject.AddFile ("Classes", headerFile, this.TargetName);
				this.XcodeProject.AddFile ("Classes", sourceFile, this.TargetName);
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
			
			foreach (IType type in types) {
				FilePath headerFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".h");
				FilePath sourceFile = this.OutputFolder.Combine (type.Name).ChangeExtension (".m");
				
				File.Delete (headerFile);
				File.Delete (sourceFile);
				
				this.XcodeProject.RemoveFile ("Classes", headerFile, this.TargetName);
				this.XcodeProject.RemoveFile ("Classes", sourceFile, this.TargetName);
			}
			
			this.SaveProject (false);
		}
		
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			if (!this.IsEnabled) {
				return;
			}
			
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}

			// Handle the following:
			// - *.xib files
			// - Info.plist files
#if MD_2_4
            ProjectFile projectFile = e.ProjectFile;
            if (BuildHelper.IsXIBFile(projectFile))
            {
            }
#endif
#if MD_2_6
			foreach(ProjectFileEventInfo info in e)
			{
	            ProjectFile projectFile = info.ProjectFile;
				LoggingService.LogInfo ("XcodeProjectTracker::HandleFileAddedToProject " + projectFile.FilePath);				
				
				if ("Info.plist".Equals(projectFile.FilePath.FileName))
				{
					this.XcodeProject.AddFile("Resources", projectFile.FilePath);
				}
	            else if (BuildHelper.IsXIBFile(projectFile))
	            {
					this.XcodeProject.AddFile("Resources", projectFile.FilePath, this.TargetName);
	            }
			}
#endif
			this.SaveProject (true);
		}

		protected override void HandleFileRemovedFromProject (object sender, ProjectFileEventArgs e)
		{
			if (!this.IsEnabled) {
				return;
			}
			
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Handle the following:
			// - *.xib files
			// - Info.plist files
#if MD_2_4
            ProjectFile projectFile = e.ProjectFile;
            if (BuildHelper.IsXIBFile(projectFile))
            {
            }
#endif
#if MD_2_6
			foreach(ProjectFileEventInfo info in e)
			{
	            ProjectFile projectFile = info.ProjectFile;
				LoggingService.LogInfo ("XcodeProjectTracker::HandleFileRemovedFromProject " + projectFile.FilePath);				
				
				if ("Info.plist".Equals(projectFile.FilePath.FileName))
				{
					this.XcodeProject.RemoveFile("Resources", projectFile.FilePath);
				}
	            else if (BuildHelper.IsXIBFile(projectFile))
	            {
					this.XcodeProject.RemoveFile("Resources", projectFile.FilePath, this.TargetName);
	            }
			}
#endif
			this.SaveProject (false);
		}

		private void HandlePropertyServicePropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			switch (e.Key) {
			case DeveloperToolsDesktopApplication.DEVELOPER_TOOLS:
#if DEBUG
				LoggingService.LogInfo ("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
#endif
				this.SaveProject (true);
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
			
			if (!this.IsEnabled) {
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
			if (!this.IsEnabled) {
				return;
			}
			
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Rename Xcode project
			LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectNameChanged");
			
			this.XcodeProject.Delete();
			this.XcodeProject = null;
			
			this.SaveProject (true);
		}

		private void HandleProjectModified (object sender, SolutionItemModifiedEventArgs e)
		{
			if (!this.IsEnabled) {
				return;
			}
			
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			bool frameworksChanged = false;
#if MD_2_4
#endif
#if MD_2_6
			foreach(SolutionItemModifiedEventInfo info in e) {
				if (info.SolutionItem == this.Project &&
					info.Hint == "MacOSFrameworks") {
					frameworksChanged = true;
				}
			}
#endif
			if (frameworksChanged) {
				LoggingService.LogInfo ("XcodeProjectTracker::HandleProjectModified");
			
				this.ClearFrameworks ();
				this.AddFrameworks ();
				this.SaveProject (true);
			}
		}
		
		private bool IsEnabled {
			get { 
				Version version = DeveloperToolsDesktopApplication.DeveloperToolsVersion;
				return (version != null) && (version.Major >= 4);
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
						
						this.xcodeProject.AddGroup ("Classes");
						this.xcodeProject.AddGroup ("Resources");
						this.xcodeProject.AddGroup ("Frameworks");
						
						// Set default settings
						// TODO: Constant
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "ARCHS", "$(ARCHS_STANDARD_32_64_BIT)");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "SDKROOT", "macosx");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "GCC_VERSION", "com.apple.compilers.llvm.clang.1_0");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "MACOSX_DEPLOYMENT_TARGET", "10.6");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "GCC_C_LANGUAGE_STANDARD", "gnu99");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "GCC_WARN_64_TO_32_BIT_CONVERSION", "YES");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "GCC_WARN_ABOUT_RETURN_TYPE", "YES");
						this.xcodeProject.AddBuildConfigurationSettings ("Release", null, "GCC_WARN_UNUSED_VARIABLE", "YES");
	
						// Add the main target
						CompileTarget compileTarget = this.Project.CompileTarget;
						switch (compileTarget) {
						case CompileTarget.Exe:
							this.xcodeProject.AddTarget (targetName, PBXProductType.Application);
							
							// TODO: Constant
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "COPY_PHASE_STRIP", "YES");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "INFOPLIST_FILE", "../../Info.plist");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "PRODUCT_NAME", "$(TARGET_NAME)");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "WRAPPER_EXTENSION", "app");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "ALWAYS_SEARCH_USER_PATHS", "NO");
							this.xcodeProject.AddBuildConfigurationSettings ("Release", targetName, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
							break;
						case CompileTarget.Library:
							this.xcodeProject.AddTarget (targetName, PBXProductType.LibraryDynamic);
							
							break;
						default:
							throw new NotSupportedException ();
						}
						
						this.AddClasses ();
						this.AddResources ();
						this.AddFrameworks ();
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
				if ("Info.plist".Equals (projectFile.FilePath.FileName)) {
					this.XcodeProject.AddFile ("Resources", projectFile.FilePath);
				} else if (BuildHelper.IsXIBFile (projectFile)) {
					this.XcodeProject.AddFile ("Resources", projectFile.FilePath, this.TargetName);
				}
			}
		}
		
		private void ClearFrameworks ()
		{
			IList<String > frameworks = new List<String> (this.XcodeProject.GetFrameworks (this.TargetName));
			foreach (String framework in frameworks) {
				this.XcodeProject.RemoveFramework ("Frameworks", framework, this.TargetName);
			}
		}
		
		private void AddFrameworks ()
		{
			foreach (String framework in this.Project.OSFrameworks.Split(';')) {
				// TODO: Constant
				this.XcodeProject.AddFramework ("Frameworks", framework, this.TargetName);
			}
		}
	}
}
