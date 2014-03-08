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
using Monobjc.Tools.Xcode;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System.Collections.Generic;
using Monobjc.Tools.Utilities;
using MonoDevelop.Monobjc.Utilities;
using ICSharpCode.NRefactory.TypeSystem;
using System.IO;
using System.Linq;

namespace MonoDevelop.Monobjc.Tracking
{
	class XcodeHandler : ProjectHandler
	{
		private XcodeProject xcodeProject = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="XcodeHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public XcodeHandler (MonobjcProject project) : base(project)
		{
		}

		public XcodeProject XcodeProject {
			get {
				if (this.xcodeProject == null) {
					this.BuildXcodeProject ();
				}
				return this.xcodeProject;
			}
		}

		public void ClearXcodeProject ()
		{
			this.xcodeProject = null;
		}

		private void BuildXcodeProject ()
		{
			IDELogger.Log("XcodeHandler::BuildXcodeProject");

			String name = this.Project.Name;
			String targetName = this.TargetName;
			this.xcodeProject = new XcodeProject (this.OutputFolder, name);
			this.xcodeProject.BaseDir = "../..";

			// Add standard folders
			this.xcodeProject.AddGroup (Constants.GROUP_CLASSES);
			this.xcodeProject.AddGroup (Constants.GROUP_RESOURCES);
			this.xcodeProject.AddGroup (Constants.GROUP_FRAMEWORKS);

			// Set default settings
			foreach (KeyValuePair<String, String> pair in DEFAULT_SETTINGS) {
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, null, pair.Key, pair.Value);
			}

			// Set version setting
			String version;
			switch (this.Project.TargetOSVersion) {
			case MacOSVersion.MacOS105:
				version = "10.5";
				break;
			case MacOSVersion.MacOS106:
				version = "10.6";
				break;
			case MacOSVersion.MacOS107:
				version = "10.7";
				break;
            case MacOSVersion.MacOS108:
                version = "10.8";
                break;
            case MacOSVersion.MacOS109:
                version = "10.9";
                break;
			default:
				version = "10.6";
				break;
			}
			this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, null, "MACOSX_DEPLOYMENT_TARGET", version);

			// Set project type settings
			switch (this.Project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
			case MonobjcProjectType.ConsoleApplication:
				this.xcodeProject.AddTarget (targetName, PBXProductType.Application);
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "PRODUCT_NAME", "$(TARGET_NAME)");
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "INFOPLIST_FILE", "../../Info.plist");
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "WRAPPER_EXTENSION", "app");
				break;
			case MonobjcProjectType.CocoaLibrary:
			default:
				this.xcodeProject.AddTarget (targetName, PBXProductType.LibraryDynamic);
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "PRODUCT_NAME", "$(TARGET_NAME)");
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "DYLIB_COMPATIBILITY_VERSION", "1");
				this.xcodeProject.AddBuildConfigurationSettings (Constants.CONFIGURATION_RELEASE, targetName, "DYLIB_CURRENT_VERSION", "1");
				break;
			}

			this.AddClasses ();
			this.AddResources ();
			this.AddFrameworks ();
			this.AddProjectReferences ();

			this.xcodeProject.Save ();
		}

		private void AddClasses ()
		{
			IDELogger.Log("XcodeHandler::AddClasses");
			ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
			IEnumerable<IType > types = cache.GetAllClasses (true);
			this.GenerateSurrogateSources (types);
		}

		private void ClearClasses()
		{
			IDELogger.Log("XcodeHandler::ClearClasses");
			this.XcodeProject.ClearGroup(Constants.GROUP_CLASSES);
		}

		private void UpdateClasses()
		{
			this.ClearClasses();
			this.AddClasses();
		}
		
		private void AddResources ()
		{
			IDELogger.Log("XcodeHandler::AddResources");
			foreach (ProjectFile projectFile in this.Project.Files) {
				if (BuildHelper.IsResourceFile (projectFile) && File.Exists (projectFile.FilePath)) {
					this.XcodeProject.AddFile (Constants.GROUP_RESOURCES, projectFile.FilePath, this.TargetName);
				}
			}
		}

		private void ClearResources()
		{
			IDELogger.Log("XcodeHandler::ClearResources");
			this.XcodeProject.ClearGroup(Constants.GROUP_RESOURCES);
		}
		
		private void UpdateResources()
		{
			this.ClearResources();
			this.AddResources();
		}
		
		private void AddFrameworks ()
		{
			IDELogger.Log("XcodeHandler::AddFrameworks");
			foreach (String framework in this.Project.OSFrameworks.Split(';')) {
				this.XcodeProject.AddFramework (Constants.GROUP_FRAMEWORKS, framework, this.TargetName);
			}
		}

		private void ClearFrameworks()
		{
			IDELogger.Log("XcodeHandler::ClearFrameworks");
			this.XcodeProject.ClearGroup(Constants.GROUP_FRAMEWORKS);
		}
		
		private void AddProjectReferences ()
		{
			IDELogger.Log("XcodeHandler::AddProjectReferences");
			foreach (var reference in this.Project.References.Where(r => r.ReferenceType == ReferenceType.Project)) {
				MonobjcProject projectReference = this.Project.ParentSolution.FindProjectByName (reference.Reference) as MonobjcProject;
				if (projectReference == null) {
					continue;
				}
				XcodeProject otherProject = projectReference.XcodeHandler.XcodeProject;
				this.XcodeProject.AddDependantProject (otherProject, this.TargetName);
			}
		}

		private void GenerateSurrogateSources (IEnumerable<IType> types)
		{
			foreach (IType type in types) {
				IDELogger.Log("XcodeHandler::GenerateSurrogateSources -- {0}", type.Name);
				
				Directory.CreateDirectory (this.OutputFolder);
				
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
				
				this.XcodeProject.AddFile (Constants.GROUP_CLASSES, headerFile, this.TargetName);
				this.XcodeProject.AddFile (Constants.GROUP_CLASSES, sourceFile, this.TargetName);
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

		private static IDictionary<String, String> DEFAULT_SETTINGS = new Dictionary<String, String> () {
			{ "ARCHS", "$(ARCHS_STANDARD_64_BIT)" },
			{ "SDKROOT", "macosx" },
			{ "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym" },
			{ "COPY_PHASE_STRIP", "YES" },
			{ "ALWAYS_SEARCH_USER_PATHS", "NO" },

			{ "CLANG_CXX_LANGUAGE_STANDARD", "gnu++0x" },
			{ "CLANG_CXX_LIBRARY", "libc++" },
			{ "CLANG_WARN_EMPTY_BODY", "YES" },
			{ "CLANG_WARN__DUPLICATE_METHOD_MATCH", "YES" },

			{ "GCC_VERSION", "com.apple.compilers.llvm.clang.1_0" },
			{ "GCC_C_LANGUAGE_STANDARD", "gnu99" },
			{ "GCC_ENABLE_OBJC_EXCEPTIONS", "YES" },
			{ "GCC_WARN_64_TO_32_BIT_CONVERSION", "YES" },
			{ "GCC_WARN_ABOUT_RETURN_TYPE", "YES" },
			{ "GCC_WARN_UNINITIALIZED_AUTOS", "YES" },
			{ "GCC_WARN_UNUSED_VARIABLE", "YES" },
		};
	}
}
