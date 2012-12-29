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
using System.IO;
using System.Linq;
using Monobjc.Tools.Generators;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using System;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	///   Service extension for building the bundle.
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
			BuildResult result = new BuildResult ();

			// Pre-build
			this.PreBuild (result, monitor, item, configuration);
			if (result.ErrorCount > 0) {
				return result;
			}
			
			// Build
			result.Append (base.Build (monitor, item, configuration));
			if (result.ErrorCount > 0) {
				return result;
			}
			
			// Post-build
			this.PostBuild (result, monitor, item, configuration);
			
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
			// Call base implementation
			bool result = base.GetNeedsBuilding (item, configuration);
			
			// Balk if the project is not a Monobjc one
			MonobjcProject project = item as MonobjcProject;
			if (result || project == null) {
				return result;
			}

			// Call specific implementation
			switch (project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				{
					MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)project.GetConfiguration (configuration);
		
					// Infer application name from configuration
					string applicationName = project.GetApplicationName (configuration);
		
					// Create the bundle maker
					BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
		
					// Info.plist
					if (!File.Exists (Path.Combine (maker.ContentsDirectory, Constants.INFO_PLIST))) {
						return true;
					}
		
					// Runtime executable
					if (!File.Exists (maker.Runtime)) {
						return true;
					}
		
					// The IB files
					if (project.GetIBFiles (Constants.InterfaceDefinition, maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
						return true;
					}
		
					// The IB files
					if (project.GetIBFiles (Constants.EmbeddedInterfaceDefinition, null).Where (p => p.NeedsBuilding).Any ()) {
						return true;
					}
		
					// The output files (output assembly and references)
					if (project.GetOutputFiles (configuration, maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
						return true;
					}
		
					// The content files (file marked as content)
					if (project.GetContentFiles (configuration, maker.ResourcesFolder).Where (p => p.NeedsBuilding).Any ()) {
						return true;
					}
				}
				break;
			case MonobjcProjectType.ConsoleApplication:
				{
					// Do nothing
				}
				break;
			case MonobjcProjectType.CocoaLibrary:
				{
					// The IB files
					if (project.GetIBFiles (Constants.EmbeddedInterfaceDefinition, null).Where (p => p.NeedsBuilding).Any ()) {
						return true;
					}
				}
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + project.ApplicationType);
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
			// Call base implementation
			base.Clean (monitor, item, configuration);

			// Balk if the project is not a Monobjc one
			MonobjcProject project = item as MonobjcProject;
			if (project == null) {
				return;
			}
			
			// Call specific implementation
			switch (project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				{
					MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)project.GetConfiguration (configuration);
		
					// Infer application name from configuration
					string applicationName = project.GetApplicationName (configuration);
		
					// Create the bundle maker
					BundleMaker maker = new BundleMaker (applicationName, conf.OutputDirectory);
		
					// Remove the application bundle
					Directory.Delete (maker.ApplicationDirectory, true);
				}
				break;
			case MonobjcProjectType.ConsoleApplication:
				{
					// Do nothing
				}
				break;
			case MonobjcProjectType.CocoaLibrary:
				{
					// Do nothing
				}
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + project.ApplicationType);
			}
		}
		
		/// <summary>
		/// Performs the pre-build for a Monobjc project.
		/// </summary>
		/// <param name = "result">The build result.</param>
		/// <param name = "monitor">The monitor.</param>
		/// <param name = "item">The item.</param>
		/// <param name = "configuration">The configuration.</param>
		private void PreBuild (BuildResult result, IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			MonobjcProject project = item as MonobjcProject;
			if (project == null) {
				return;
			}
			
			switch (project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				{
					BuildHelper.EmbedXIBFiles (monitor, project, result);
				}
				break;
			case MonobjcProjectType.ConsoleApplication:
				{
					// Do nothing
				}
				break;
			case MonobjcProjectType.CocoaLibrary:
				{
					BuildHelper.EmbedXIBFiles (monitor, project, result);
				}
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + project.ApplicationType);
			}
		}
		
		/// <summary>
		/// Performs the pre-build for a Monobjc project.
		/// </summary>
		/// <param name = "result">The build result.</param>
		/// <param name = "monitor">The monitor.</param>
		/// <param name = "item">The item.</param>
		/// <param name = "configuration">The configuration.</param>
		private void PostBuild (BuildResult result, IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			MonobjcProject project = item as MonobjcProject;
			if (project == null) {
				return;
			}

			switch (project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				{
					MonobjcProjectConfiguration conf = (MonobjcProjectConfiguration)project.GetConfiguration (configuration);
					BundleGenerator.Generate (monitor, result, project, configuration, conf.OutputDirectory, false);
				}
				break;
			case MonobjcProjectType.ConsoleApplication:
				{
					// Do nothing
				}
				break;
			case MonobjcProjectType.CocoaLibrary:
				{
					// Do nothing
				}
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + project.ApplicationType);
			}
		}
	}
}