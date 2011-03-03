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
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using Monobjc.Tools.Generators;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.Execution;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	/// A Monobjc project
	/// </summary>
	public partial class MonobjcProject : DotNetProject
	{
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
			// Set default values
			this.OSFrameworks = "Foundation;AppKit";
			this.TargetOSVersion = MacOSVersion.None;
			this.TargetOSArch = MacOSArchitecture.None;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		/// <param name = "languageName">Name of the language.</param>
		public MonobjcProject (string languageName) : base(languageName)
		{
			// Set default values
			this.OSFrameworks = "Foundation;AppKit";
			this.TargetOSVersion = MacOSVersion.None;
			this.TargetOSArch = MacOSArchitecture.None;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		/// <param name = "language">The language.</param>
		/// <param name = "info">The info.</param>
		/// <param name = "projectOptions">The project options.</param>
		public MonobjcProject (String language, ProjectCreateInformation info, XmlElement projectOptions) : base(language, info, projectOptions)
		{
			// Set default values
			this.OSFrameworks = "Foundation;AppKit";
			this.TargetOSVersion = MacOSVersion.None;
			this.TargetOSArch = MacOSArchitecture.None;
			
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
			
			this.UpdateReferences ();
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
			return framework.IsCompatibleWithFramework ("3.5");
#endif
#if MD_2_6
			return framework.IsCompatibleWithFramework (TargetFrameworkMoniker.NET_3_5);
#endif
		}

		/// <summary>
		///   Gets the default build action.
		/// </summary>
		/// <param name = "fileName">Name of the file.</param>
		/// <returns></returns>
		public override string GetDefaultBuildAction (string fileName)
		{
			return BuildHelper.IsXIBFile (fileName) ? BuildAction.Page : base.GetDefaultBuildAction (fileName);
		}

		/// <summary>
		/// Creates the execution command.
		/// </summary>
		/// <param name="configSel">The configuration selector.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The execution command.</returns>
		protected override ExecutionCommand CreateExecutionCommand (ConfigurationSelector configSel, DotNetProjectConfiguration configuration)
		{
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)configuration;
			
			// Infer application name from configuration
			string applicationName = this.GetApplicationName (configSel);
			
			// Create the bundle maker
			BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
			
			conf.ApplicationName = applicationName;
			conf.Runtime = maker.Runtime;
			
			// TODO: If Runtime is null, raise an exception
			
			MonobjcExecutionCommand command = new MonobjcExecutionCommand (conf);
			command.UserAssemblyPaths = this.GetUserAssemblyPaths (configSel);
			
			return command;
		}

		/// <summary>
		/// Called when this project executes.
		/// </summary>
		/// <param name="monitor">The monitor.</param>
		/// <param name="context">The context.</param>
		/// <param name="configSel">The configuration selector.</param>
		protected override void OnExecute (IProgressMonitor monitor, MonoDevelop.Projects.ExecutionContext context, ConfigurationSelector configSel)
		{
			//MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration) this.GetConfiguration(configSel);
			
			// Infer application name from configuration
			//string applicationName = this.GetApplicationName(configSel);
			
			// Create the bundle maker
			//BundleMaker maker = new BundleMaker(applicationName, conf.OutputDirectory);
			
			base.OnExecute (monitor, context, configSel);
		}

		/// <summary>
		/// Called when a project file is added to this instance.
		/// </summary>
		/// <param name="e">The <see cref="ProjectFileEventArgs"/> instance containing the event data.</param>
		protected override void OnFileAddedToProject (ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Loading) {
				base.OnFileAddedToProject (e);
				return;
			}
			
			// Collect dependencies
			IEnumerable<string> filesToAdd = MonobjcCodeBehind.GuessDependencies (this, e.ProjectFile);
			
			base.OnFileAddedToProject (e);
			
			// Add dependencies
			if (filesToAdd != null) {
				foreach (string file in filesToAdd.Where (f => !this.IsFileInProject (f))) {
					this.AddFile (file);
				}
			}
			
			// Run CodeBehind if it is a XIB file
			this.GenerateDesignCode (e.ProjectFile.FilePath);
		}

		/// <summary>
		/// Called when a project file is changed into this instance.
		/// </summary>
		/// <param name="e">The <see cref="ProjectFileEventArgs"/> instance containing the event data.</param>
		protected override void OnFileChangedInProject (ProjectFileEventArgs e)
		{
			// Collect dependencies
			IEnumerable<string> filesToAdd = MonobjcCodeBehind.GuessDependencies (this, e.ProjectFile);
			
			base.OnFileChangedInProject (e);
			
			// Add dependencies
			if (filesToAdd != null) {
				foreach (string file in filesToAdd.Where (f => !this.IsFileInProject (f))) {
					this.AddFile (file);
				}
			}
			
			// Run CodeBehind if it is a XIB file
			this.GenerateDesignCode (e.ProjectFile.FilePath);
		}
	}
}
