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
using System.Linq;
using System.Threading;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		/// <summary>
		/// Gets the name of the application.
		/// </summary>
		/// <param name="configSel">The configuration selector.</param>
		/// <returns></returns>
		internal String GetApplicationName (ConfigurationSelector configSel)
		{
			String outputFile = this.GetOutputFileName (configSel);
			return Path.GetFileNameWithoutExtension (outputFile);
		}

		/// <summary>
		/// Generates the design code for an IB file.
		/// </summary>
		/// <param name="file">The file.</param>
		internal void GenerateDesignCode (FilePath file)
		{
			LoggingService.LogInfo ("MonobjcProject::GenerateDesignCode " + file);
			
			if (!BuildHelper.IsXIBFile (file)) {
				return;
			}
			
			LoggingService.LogInfo ("MonobjcProject::GenerateDesignCode2 " + file);
			
			// Defer IB extract in a separate thread
			ThreadPool.QueueUserWorkItem (delegate { this.CodeBehind.GenerateDesignCodeForIB (file); });
		}

		/// <summary>
		/// Gets the IB file pairs.
		/// </summary>
		/// <param name="destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs.</returns>
		internal IEnumerable<FilePair> GetIBFiles (FilePath destinationDirectory)
		{
			// Return all the IB files
			foreach (ProjectFile file in this.Files) {
				if (!BuildHelper.IsXIBFile (file)) {
					continue;
				}
				
				// Compute destination file
				FilePath relativePath = file.FilePath.ToRelative (this.BaseDirectory);
				FilePath destination = destinationDirectory.Combine (relativePath);
				yield return new FilePair (file.FilePath, destination.ChangeExtension ("nib"));
			}
		}

		/// <summary>
		/// Gets the content files pairs.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetOutputFiles (ConfigurationSelector configuration, FilePath destinationDirectory)
		{
			// Return the main assembly file and its symbols
			foreach (FilePath file in this.GetOutputFiles (configuration)) {
				FilePath destination = destinationDirectory.Combine (file.FileName);
				yield return new FilePair (file, destination);
			}
			
			// Return the assembly dependencies and their symbols
			foreach (FileCopySet.Item file in this.GetSupportFileList (configuration)) {
				FilePath destination = destinationDirectory.Combine (file.Target.FileName);
				yield return new FilePair (file.Src, destination);
			}
		}

		/// <summary>
		/// Gets the content files pairs.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetContentFiles (ConfigurationSelector configuration, FilePath destinationDirectory)
		{
			// Return each content file
			foreach (ProjectFile file in this.Files) {
				if (file.BuildAction != BuildAction.Content) {
					continue;
				}
				
				FilePath destination = destinationDirectory.Combine (file.FilePath.ToRelative (this.BaseDirectory));
				yield return new FilePair (file.FilePath, destination);
			}
		}

		/// <summary>
		/// Updates the references.
		/// </summary>
		internal void UpdateReferences ()
		{
			// Retrieve assembly from the project
			IEnumerable<ProjectReference> references = new List<ProjectReference> (this.References.Where (BuildHelper.IsMonobjcReference));
			String[] names = this.OSFrameworks.Split (';');
			
			// Remove any Monobjc references
			foreach (ProjectReference item in references) {
				this.References.Remove (item);
			}
			
			// Set the compatible version
			Version version;
			switch (this.TargetOSVersion) {
			case MacOSVersion.MacOS105:
				version = new Version (10, 5);
				break;
			case MacOSVersion.MacOS106:
				version = new Version (10, 6);
				break;
			default:
				version = new Version (10, 0);
				break;
			}
			
			// Defer the framework loading code generation in a separate thread
			ThreadPool.QueueUserWorkItem (delegate { this.CodeBehind.GenerateDesignCodeForFrameworks (names); });
			
			// Take the list of frameworks and add project references
			List<String> assemblyNames = new List<String> ();
			assemblyNames.Add ("Monobjc");
			assemblyNames.AddRange (names.Select (f => "Monobjc." + f));
			foreach (String assemblyName in assemblyNames) {
				// Find all matching assemblies with this name and a compatible version
				IEnumerable<SystemAssembly> matching = this.MonobjcAssemblies.Where (a => a.Name.Equals (assemblyName) && BuildHelper.IsCompatible (new Version (a.Version), version));
				
				// If there is a match, then add the reference
				if (matching != null && matching.Count () == 1) {
					SystemAssembly specificAssembly = matching.First ();
					ProjectReference reference = new ProjectReference (specificAssembly);
					reference.SpecificVersion = false;
					this.References.Add (reference);
				}
			}
		}
	}
}
