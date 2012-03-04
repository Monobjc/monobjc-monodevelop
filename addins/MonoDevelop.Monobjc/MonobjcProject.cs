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
using System.Xml;
using Monobjc.Tools.Generators;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.Execution;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using System.Collections.Generic;
using System.IO;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	///   A Monobjc project
	/// </summary>
	public partial class MonobjcProject : DotNetProject
	{				
		private IList<String> buildActions = null;

		/// <summary>
		///   Initializes the <see cref = "MonoDevelop.Monobjc.MonobjcProject" /> class.
		/// </summary>
		static MonobjcProject ()
		{
			CodeBehindGeneratorLoader.Init ();
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		public MonobjcProject ()
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::ctor0");
#endif
			this.Initialize ();
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		/// <param name = "languageName">Name of the language.</param>
		public MonobjcProject (String languageName) : base(languageName)
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::ctor1");
#endif
			this.Initialize ();
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		/// <param name = "language">The language.</param>
		/// <param name = "info">The info.</param>
		/// <param name = "projectOptions">The project options.</param>
		public MonobjcProject (String language, ProjectCreateInformation info, XmlElement projectOptions) : base(language, info, projectOptions)
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::ctor3");
#endif
			XmlNode node = projectOptions.SelectSingleNode ("MacOSFrameworks");
			if (node != null) {
				this.OSFrameworks = node.InnerText;
			}

			node = projectOptions.SelectSingleNode ("MacOSVersion");
			if (node != null) {
				this.TargetOSVersion = (MacOSVersion)Enum.Parse (typeof(MacOSVersion), node.InnerText);
			}

			node = projectOptions.SelectSingleNode ("MacOSArch");
			if (node != null) {
				this.TargetOSArch = (MacOSArchitecture)Enum.Parse (typeof(MacOSArchitecture), node.InnerText);
			}

			this.Initialize ();
		}

		/// <summary>
		///   Releases unmanaged and - optionally - managed resources
		/// </summary>
		public override void Dispose ()
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::Dispose");
#endif
			this.CodeBehindTracker.Dispose ();
			this.XcodeTracker.Dispose ();
			this.EmbeddingTracker.Dispose ();
			base.Dispose ();
		}

		/// <summary>
		///   Gets the type of the project.
		/// </summary>
		/// <value>The type of the project.</value>
		public override string ProjectType {
			get { return "Monobjc"; }
		}

		/// <summary>
		///   Creates the configuration.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <returns></returns>
		public override SolutionItemConfiguration CreateConfiguration (string name)
		{
			MonobjcProjectConfiguration configuration = new MonobjcProjectConfiguration (name);
			configuration.CopyFrom (base.CreateConfiguration (name));
			return configuration;
		}

		/// <summary>
		///   Supportses the framework.
		/// </summary>
		/// <param name = "framework">The framework.</param>
		/// <returns></returns>
		public override bool SupportsFramework (TargetFramework framework)
		{
#if MD_2_4
            return framework.IsCompatibleWithFramework("4.0");
#endif
#if MD_2_6 || MD_2_8
			return framework.IsCompatibleWithFramework (TargetFrameworkMoniker.NET_4_0);
#endif
		}

		/// <summary>
		///   Gets the default build action.
		/// </summary>
		/// <param name = "fileName">Name of the file.</param>
		/// <returns></returns>
		public override String GetDefaultBuildAction (String fileName)
		{
			if (BuildHelper.IsXIBFile (fileName)) {
				return BuildHelper.InterfaceDefinition;
			}
			if (BuildHelper.IsStringsFile (fileName)) {
				return BuildAction.Content;
			}
			return base.GetDefaultBuildAction (fileName);
		}

		/// <summary>
		///   Creates the execution command.
		/// </summary>
		/// <param name = "configSel">The configuration selector.</param>
		/// <param name = "configuration">The configuration.</param>
		/// <returns>The execution command.</returns>
		protected override ExecutionCommand CreateExecutionCommand (ConfigurationSelector configSel, DotNetProjectConfiguration configuration)
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::CreateExecutionCommand");
#endif
			if (this.CompileTarget != CompileTarget.Exe) {
				return base.CreateExecutionCommand (configSel, configuration);
			}
			
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)configuration;

			// Infer application name from configuration
			String applicationName = this.GetApplicationName (configSel);
			conf.ApplicationName = applicationName;
			
			switch (this.ApplicationType) {
			case MonobjcApplicationType.CocoaApplication:
				{
					// Create the bundle maker to get the path to the runtime
					BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
					conf.Runtime = maker.Runtime;
				}
				break;
			case MonobjcApplicationType.ConsoleApplication:
				{
					// Build the command line
					conf.Runtime = FileProvider.GetPath (this.TargetOSVersion, "runtime");
					conf.CommandLineParameters = this.GetOutputFileName (configSel);
				}
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + this.ApplicationType);
			}

			// Create the command
			MonobjcExecutionCommand command = new MonobjcExecutionCommand (conf);
			command.UserAssemblyPaths = this.GetUserAssemblyPaths (configSel);

			return command;
		}

		/// <summary>
		///   Called when a project file is added to this instance.
		/// </summary>
		/// <param name = "e">The <see cref = "ProjectFileEventArgs" /> instance containing the event data.</param>
		protected override void OnFileAddedToProject (ProjectFileEventArgs e)
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::OnFileAddedToProject");
#endif
			// Migrate "Page" to "InterfaceDefinition"
#if MD_2_4
            ProjectFile projectFile = e.ProjectFile;
			if (projectFile.BuildAction == BuildAction.Page) {
				projectFile.BuildAction = BuildHelper.InterfaceDefinition;
			}
#endif
#if MD_2_6 || MD_2_8
			foreach(ProjectFileEventInfo info in e)
			{
	            ProjectFile projectFile = info.ProjectFile;
				if (projectFile.BuildAction == BuildAction.Page) {
					projectFile.BuildAction = BuildHelper.InterfaceDefinition;
				}
			}
#endif
			base.OnFileAddedToProject (e);
		}

		/// <summary>
		///   Called when a project file is changed into this instance.
		/// </summary>
		/// <param name = "e">The <see cref = "ProjectFileEventArgs" /> instance containing the event data.</param>
		protected override void OnFileChangedInProject (ProjectFileEventArgs e)
		{
#if DEBUG
            LoggingService.LogInfo("MonobjcProject::OnFileChangedInProject");
#endif
			base.OnFileChangedInProject (e);
		}
		
		/// <summary>
		/// Gets the common build actions.
		/// </summary>
		protected override IList<String> GetCommonBuildActions ()
		{
			if (buildActions == null) {
				buildActions = new List<String> (base.GetCommonBuildActions ());
				if (buildActions.Contains (BuildAction.Page)) {
					buildActions.Remove (BuildAction.Page);
				}
				if (!buildActions.Contains (BuildHelper.InterfaceDefinition)) {
					buildActions.Add (BuildHelper.InterfaceDefinition);
				}
				if (!buildActions.Contains (BuildHelper.EmbeddedInterfaceDefinition)) {
					buildActions.Add (BuildHelper.EmbeddedInterfaceDefinition);
				}
			}
			return buildActions;
		}
	}
}