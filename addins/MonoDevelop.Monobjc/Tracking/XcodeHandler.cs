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
        private long timestamp = 0;
        private IList<String> headerFiles = new List<String>();

		/// <summary>
		/// Initializes a new instance of the <see cref="XcodeHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public XcodeHandler (MonobjcProject project) : base(project)
		{
            IdeApp.FocusIn += this.HandleApplicationFocusIn;
		}

        /// <summary>
        /// Clean up the handler
        /// </summary>
        public override void Dispose()
        {
            IdeApp.FocusIn -= this.HandleApplicationFocusIn;
            base.Dispose();
        }

        /// <summary>
        /// Gets the Xcode project. The project will be created if needed
        /// </summary>
		public XcodeProject XcodeProject {
			get {
				if (this.xcodeProject == null) {
					this.BuildXcodeProject ();
				}
				return this.xcodeProject;
			}
		}

        /// <summary>
        /// Clears the Xcode project.
        /// </summary>
		public void ClearXcodeProject ()
		{
			this.xcodeProject = null;
		}

        public void HandleApplicationFocusIn (Object sender, EventArgs e)
        {
            IList<FilePath> touchedHeaders = new List<FilePath>();

            // Each time the IDE got focus:
            // - check if header files are more recent than the reference timestamp. If so, generate code-behind code
            // - once code-behind has been generated, update the last timestamp
            foreach (FilePath path in this.headerFiles) {
                long lastModified = File.GetLastWriteTimeUtc(path).Ticks;
                if (lastModified < this.timestamp) {
                    continue;
                }
                touchedHeaders.Add(path);
            }

            IDELogger.Log ("XcodeHandler::HandleApplicationFocusIn -- Found {0} touched headers", touchedHeaders.Count);
            this.timestamp = DateTime.UtcNow.Ticks;

            if (touchedHeaders.Count > 0) {
                this.Project.CodeBehindHandler.GenerateDesignCodeForHeaders(touchedHeaders);
            }
        }

        /// <summary>
        /// Builds the Xcode project and all its parts.
        /// </summary>
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

        /// <summary>
        /// Adds the surrogate classes to the project.
        /// </summary>
		private void AddClasses ()
		{
			IDELogger.Log("XcodeHandler::AddClasses");
			ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
			IEnumerable<IType > types = cache.GetAllClasses (true);
			this.GenerateSurrogateSources (types);
		}

        /// <summary>
        /// Clears the classes from the project.
        /// </summary>
		private void ClearClasses()
		{
			IDELogger.Log("XcodeHandler::ClearClasses");
			this.XcodeProject.ClearGroup(Constants.GROUP_CLASSES);
            this.headerFiles.Clear();
		}

        /// <summary>
        /// Updates the classes in the project.
        /// </summary>
		private void UpdateClasses()
		{
			this.ClearClasses();
			this.AddClasses();
		}
		
        /// <summary>
        /// Adds the resources to the project.
        /// </summary>
		private void AddResources ()
		{
			IDELogger.Log("XcodeHandler::AddResources");
			foreach (ProjectFile projectFile in this.Project.Files) {
				if (BuildHelper.IsResourceFile (projectFile) && File.Exists (projectFile.FilePath)) {
					this.XcodeProject.AddFile (Constants.GROUP_RESOURCES, projectFile.FilePath, this.TargetName);
				}
			}
		}

        /// <summary>
        /// Clears the resources from the project.
        /// </summary>
		private void ClearResources()
		{
			IDELogger.Log("XcodeHandler::ClearResources");
			this.XcodeProject.ClearGroup(Constants.GROUP_RESOURCES);
		}
		
        /// <summary>
        /// Updates the resources in the project.
        /// </summary>
		private void UpdateResources()
		{
			this.ClearResources();
			this.AddResources();
		}
		
        /// <summary>
        /// Adds the frameworks to the project.
        /// </summary>
		private void AddFrameworks ()
		{
			IDELogger.Log("XcodeHandler::AddFrameworks");
			foreach (String framework in this.Project.OSFrameworks.Split(';')) {
				this.XcodeProject.AddFramework (Constants.GROUP_FRAMEWORKS, framework, this.TargetName);
			}
		}

        /// <summary>
        /// Clears the frameworks from the project.
        /// </summary>
		private void ClearFrameworks()
		{
			IDELogger.Log("XcodeHandler::ClearFrameworks");
			this.XcodeProject.ClearGroup(Constants.GROUP_FRAMEWORKS);
		}
		
        /// <summary>
        /// Adds the project references to the project.
        /// </summary>
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

        /// <summary>
        /// Generates the surrogate sources for the given types.
        /// </summary>
		private void GenerateSurrogateSources (IEnumerable<IType> types)
		{
            // Clear the tracking list
            this.headerFiles.Clear();

            // For each type, generate header/source files
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
				
                // Add the files to the project
				this.XcodeProject.AddFile (Constants.GROUP_CLASSES, headerFile, this.TargetName);
				this.XcodeProject.AddFile (Constants.GROUP_CLASSES, sourceFile, this.TargetName);

                // Store the file so it can be tracked
                this.headerFiles.Add(headerFile);
			}

            this.timestamp = DateTime.UtcNow.Ticks;
		}
		
        /// <summary>
        /// Gets the output folder where the Xcode project will be stored
        /// </summary>
        /// <value>The output folder.</value>
		private FilePath OutputFolder {
			get {
				ConfigurationSelector configurationSelector = IdeApp.Workspace.ActiveConfiguration;
				FilePath outputFile = this.Project.GetOutputFileName (configurationSelector);
				return outputFile.ParentDirectory;
			}
		}
		
        /// <summary>
        /// Gets the name of the target. This is required for a proper Xcode project configuration.
        /// </summary>
        /// <value>The name of the target.</value>
		private String TargetName {
			get { return (this.Project != null) ? this.Project.Name : "Monobjc"; }
		}

        /// <summary>
        /// The default settings for a Xcode project.
        /// </summary>
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
