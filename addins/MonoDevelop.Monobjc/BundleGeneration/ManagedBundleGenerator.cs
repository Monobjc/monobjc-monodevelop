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
using Monobjc.Tools.Generators;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.BundleGeneration
{
	public class ManagedBundleGenerator : BundleGenerator
	{
		public override BuildResult Generate (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration)
		{
			BuildResult result = new BuildResult ();
			
			// Infer application name from configuration
			string applicationName = project.GetApplicationName (configuration);
			
			// Create the bundle maker
			BundleMaker maker = new BundleMaker (applicationName, this.Output);
			
			// Compile the XIB files
			BuildHelper.CompileXIBFiles (monitor, project, maker, result);
			if (result.ErrorCount > 0) {
				monitor.ReportError (GettextCatalog.GetString ("Failed to compile XIB files"), null);
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
	}
}
