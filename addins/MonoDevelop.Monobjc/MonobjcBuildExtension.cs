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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Monobjc.Tools.Generators;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	/// Service extension for building the bundle.
	/// </summary>
	public class MonobjcBuildExtension : ProjectServiceExtension
	{
		/// <summary>
		///   Builds the specified solution item.
		/// </summary>
		/// <param name = "monitor">The monitor.</param>
		/// <param name = "item">The item.</param>
		/// <param name = "configuration">The configuration.</param>
		/// <returns>The build result.</returns>
		protected override BuildResult Build (IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			MonobjcProject project = item as MonobjcProject;
			if (project == null || project.CompileTarget != CompileTarget.Exe) {
				return base.Build (monitor, item, configuration);
			}
			
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)project.GetConfiguration (configuration);
			
			// Infer application name from configuration
			string applicationName = project.GetApplicationName (configuration);
			
			// Create the bundle maker
			BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
			
			// Do the base build
			BuildResult result = base.Build (monitor, item, configuration);
			if (result.ErrorCount > 0) {
				return result;
			}
			
			// Compile the XIB files
			BuildHelper.CompileXIBFiles (monitor, project, maker, result);
			if (result.ErrorCount > 0) {
				return result;
			}
			
			// Copy the output and dependencies
			BuildHelper.CopyOutputFiles (monitor, project, configuration, maker);
			
			// Copy the content files
			BuildHelper.CopyContentFiles (monitor, project, configuration, maker);
			
			// Create the Info.plist
			BuildHelper.CreateInfoPList (monitor, project, configuration, maker);
			
			// Write the native runtime
			monitor.BeginTask (GettextCatalog.GetString ("Copying native launcher..."), 0);
			maker.WriteRuntime (project.TargetOSVersion);
			monitor.EndTask ();
			
			return result;
		}

		/// <summary>
		///   Checks if the solution items needs building.
		/// </summary>
		/// <param name = "item">The item.</param>
		/// <param name = "configuration">The configuration.</param>
		/// <returns></returns>
		protected override bool GetNeedsBuilding (SolutionEntityItem item, ConfigurationSelector configuration)
		{
			if (base.GetNeedsBuilding (item, configuration)) {
				return true;
			}
			
			MonobjcProject proj = item as MonobjcProject;
			if (proj == null || proj.CompileTarget != CompileTarget.Exe) {
				return false;
			}
			
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)proj.GetConfiguration (configuration);
			
			// Infer application name from configuration
			string applicationName = proj.GetApplicationName (configuration);
			
			// Create the bundle maker
			BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
			
			// Info.plist
			if (!File.Exists (Path.Combine (maker.ContentsDirectory, "Info.plist"))) {
				return true;
			}
			
			// Runtime executable
			if (!File.Exists (maker.Runtime)) {
				return true;
			}
			
			// The IB files
			if (proj.GetIBFiles (maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
				return true;
			}
			
			// The output files (output assembly and references)
			if (proj.GetOutputFiles (configuration, maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
				return true;
			}
			
			// The content files (file marked as content)
			if (proj.GetContentFiles (configuration, maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
				return true;
			}
			
			return false;
		}

		/// <summary>
		///   Cleans the specified solution item.
		/// </summary>
		/// <param name = "monitor">The monitor.</param>
		/// <param name = "item">The item.</param>
		/// <param name = "configuration">The configuration.</param>
		protected override void Clean (IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			base.Clean (monitor, item, configuration);
			
			MonobjcProject proj = item as MonobjcProject;
			if (proj == null || proj.CompileTarget != CompileTarget.Exe) {
				return;
			}
			
			MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)proj.GetConfiguration (configuration);
			
			// Infer application name from configuration
			string applicationName = proj.GetApplicationName (configuration);
			
			// Create the bundle maker
			BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
			
			// Remove the application bundle
			Directory.Delete (maker.ApplicationDirectory, true);
		}
	}
}
