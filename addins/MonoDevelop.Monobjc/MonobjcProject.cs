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
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.Execution;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using System.Collections.Generic;
using MonoDevelop.Core;

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
			IDELogger.Log ("MonobjcProject::ctor0");
			this.Initialize ();
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcProject" /> class.
		/// </summary>
		/// <param name = "languageName">Name of the language.</param>
		public MonobjcProject (String languageName) : base(languageName)
		{
			IDELogger.Log ("MonobjcProject::ctor1");
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
			IDELogger.Log ("MonobjcProject::ctor3");

			this.ApplicationType = GetNodeValue (projectOptions, "MacOSApplicationType", MonobjcProjectType.CocoaApplication);
			this.ApplicationCategory = GetNodeValue (projectOptions, "MacOSApplicationCategory", String.Empty);
			this.BundleId = GetNodeValue (projectOptions, "BundleId", "net.monobjc.application.Test");
			this.BundleVersion = GetNodeValue (projectOptions, "BundleVersion", "1.0");
			this.MainNibFile = GetNodeValue (projectOptions, "MainNibFile", null);
			this.BundleIcon = GetNodeValue (projectOptions, "BundleIcon", null);
			this.TargetOSVersion = GetNodeValue (projectOptions, "MacOSVersion", MacOSVersion.MacOS106);
			this.Signing = Boolean.Parse (GetNodeValue (projectOptions, "Signing", "false"));
			this.SigningIdentity = GetNodeValue (projectOptions, "SigningIdentity", String.Empty);
			this.UseEntitlements = Boolean.Parse (GetNodeValue (projectOptions, "UseEntitlements", "false"));
			this.OSFrameworks = GetNodeValue (projectOptions, "MacOSFrameworks", String.Empty);

			this.TargetOSArch = GetNodeValue (projectOptions, "MacOSArch", MacOSArchitecture.X86);
			this.EmbeddedFrameworks = GetNodeValue (projectOptions, "EmbeddedFrameworks", String.Empty);
			this.AdditionalAssemblies = GetNodeValue (projectOptions, "AdditionalAssemblies", String.Empty);
			this.ExcludedAssemblies = GetNodeValue (projectOptions, "ExcludedAssemblies", String.Empty);
			this.AdditionalLibraries = GetNodeValue (projectOptions, "AdditionalLibraries", String.Empty);

			this.Archive = Boolean.Parse (GetNodeValue (projectOptions, "Archive", "false"));
			this.ArchiveIdentity = GetNodeValue (projectOptions, "ArchiveIdentity", String.Empty);

			this.DevelopmentRegion = GetNodeValue (projectOptions, "MacOSDevelopmentRegion", "en");
			this.CombineArtwork = Boolean.Parse (GetNodeValue (projectOptions, "CombineArtwork", "false"));
			this.EncryptArtwork = Boolean.Parse (GetNodeValue (projectOptions, "EncryptArtwork", "false"));

			this.Initialize ();
		}

		/// <summary>
		///   Releases unmanaged and - optionally - managed resources
		/// </summary>
		public override void Dispose ()
		{
			IDELogger.Log ("MonobjcProject::Dispose");

			this.ResolverHandler.Dispose ();
			this.MigrationHandler.Dispose ();
			this.DependencyHandler.Dispose ();
			this.CodeBehindHandler.Dispose ();

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
			return framework.IsCompatibleWithFramework (TargetFrameworkMoniker.NET_4_0);
		}

		/// <summary>
		///   Gets the default build action.
		/// </summary>
		/// <param name = "fileName">Name of the file.</param>
		/// <returns></returns>
		public override String GetDefaultBuildAction (String fileName)
		{
			if (BuildHelper.IsXIBFile (fileName)) {
				return Constants.InterfaceDefinition;
			}
			if (BuildHelper.IsNIBFile (fileName)) {
				return BuildAction.EmbeddedResource;
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
			if (this.CompileTarget != CompileTarget.Exe) {
				return base.CreateExecutionCommand (configSel, configuration);
			}

			if (this.projectType == MonobjcProjectType.None) {
				return base.CreateExecutionCommand (configSel, configuration);
			}

			// Infer application name from configuration
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)configuration;
			String applicationName = this.GetApplicationName (configSel);
			conf.ApplicationName = applicationName;
			
			switch (this.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				{
					// Create the bundle maker to get the path to the runtime
					BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
					conf.Runtime = maker.Runtime;
				}
				break;
			case MonobjcProjectType.ConsoleApplication:
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

		protected override void OnFileAddedToProject (ProjectFileEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnFileAddedToProject '{0}'", e);

			//IList<FilePath> newFiles = new List<FilePath>();

			this.ResolverHandler.ClearCache();
			this.MigrationHandler.Migrate (e);
			IEnumerable<FilePath> dependencies = this.DependencyHandler.GuessDependencies (e);

			base.OnFileAddedToProject (e);

			this.CodeBehindHandler.GenerateDesignCode (e);
			this.DependencyHandler.AddFiles (dependencies);
		}

		protected override void OnFileChangedInProject (ProjectFileEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnFileChangedInProject '{0}'", e);

			this.ResolverHandler.ClearCache();

			base.OnFileChangedInProject (e);

			this.CodeBehindHandler.GenerateDesignCode (e);
		}

		protected override void OnFilePropertyChangedInProject (ProjectFileEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnFilePropertyChangedInProject '{0}'", e);

			this.ResolverHandler.ClearCache();

			base.OnFilePropertyChangedInProject (e);
		}

		protected override void OnFileRenamedInProject (ProjectFileRenamedEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnFileRenamedInProject '{0}'", e);

			this.ResolverHandler.ClearCache();

			base.OnFileRenamedInProject (e);
		}

		protected override void OnReferenceAddedToProject (ProjectReferenceEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnReferenceAddedToProject '{0}'", e);

			this.ResolverHandler.RecomputeReferences();

			base.OnReferenceAddedToProject (e);
		}

		protected override void OnReferenceRemovedFromProject (ProjectReferenceEventArgs e)
		{
			IDELogger.Log ("MonobjcProject::OnReferenceRemovedFromProject '{0}'", e);

			this.ResolverHandler.RecomputeReferences();

			base.OnReferenceRemovedFromProject (e);
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
				if (!buildActions.Contains (Constants.InterfaceDefinition)) {
					buildActions.Add (Constants.InterfaceDefinition);
				}
				if (!buildActions.Contains (Constants.EmbeddedInterfaceDefinition)) {
					buildActions.Add (Constants.EmbeddedInterfaceDefinition);
				}
			}
			return buildActions;
		}
	}
}