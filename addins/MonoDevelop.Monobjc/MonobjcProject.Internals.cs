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
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		/// <summary>
		///   Gets the name of the application.
		/// </summary>
		/// <param name = "configSel">The configuration selector.</param>
		/// <returns></returns>
		internal String GetApplicationName (ConfigurationSelector configSel)
		{
			String outputFile = this.GetOutputFileName (configSel);
			return Path.GetFileNameWithoutExtension (outputFile);
		}

		/// <summary>
		///   Gets the IB file pairs.
		/// </summary>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs.</returns>
		internal IEnumerable<FilePair> GetIBFiles (String buildAction, FilePath destinationDirectory)
		{
			// Return all the IB files
			foreach (ProjectFile file in this.Files) {
				FilePair pair = this.GetIBFile (file, buildAction, destinationDirectory);
				if (pair == null) {
					continue;
				}
				yield return pair;
			}
		}
		
		/// <summary>
		///   Gets the IB file pairs.
		/// </summary>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs.</returns>
		internal FilePair GetIBFile (ProjectFile file, String buildAction, FilePath destinationDirectory)
		{
			if (!BuildHelper.IsXIBFile (file)) {
				return null;
			}
			if (file.BuildAction != buildAction) {
				return null;
			}

			// Compute destination file
			FilePath relativePath = file.FilePath.ToRelative (this.BaseDirectory);
			FilePath destination = file.FilePath;
			if (destinationDirectory != null) {
				destination = destinationDirectory.Combine (relativePath);
			}
			
			return new FilePair (file.FilePath, destination.ChangeExtension ("nib"));
		}

		/// <summary>
		///   Gets the content files pairs.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetOutputFiles (ConfigurationSelector configuration, FilePath destinationDirectory)
		{
			// Return the main assembly file and its symbols
			foreach (FilePath file in this.GetOutputFiles(configuration)) {
				FilePath destination = destinationDirectory.Combine (file.FileName);
				yield return new FilePair (file, destination);
			}

			// Return the assembly dependencies and their symbols
			foreach (FileCopySet.Item file in this.GetSupportFileList(configuration)) {
				FilePath destination = destinationDirectory.Combine (file.Target.FileName);
				yield return new FilePair (file.Src, destination);
			}
		}

		/// <summary>
		///   Gets the content files pairs.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetContentFiles (ConfigurationSelector configuration, FilePath destinationDirectory)
		{
			return GetFiles(configuration, destinationDirectory, BuildAction.Content);
		}
		
		/// <summary>
		///   Gets the encrypted content files pairs.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetEncryptedContentFiles (ConfigurationSelector configuration, FilePath destinationDirectory)
		{
			return GetFiles(configuration, destinationDirectory, Constants.EncryptedContent);
		}
		
		/// <summary>
		/// Determines whether the project file is in the development region.
		/// </summary>
		internal bool IsInDevelopmentRegion (ProjectFile file)
		{
			bool result = false;
			String developmentRegion = this.DevelopmentRegion;

			FilePath baseDirectory = this.BaseDirectory;
			FilePath localizedFolder = baseDirectory.Combine (developmentRegion + Constants.DOT_LPROJ);
			FilePath ibFile = file.FilePath;
			
			if (ibFile.ParentDirectory.Equals (baseDirectory)) {
				result = true;
			} else if (ibFile.ParentDirectory.Equals (localizedFolder)) {
				result = true;
			}
			
			IDELogger.Log ("MonobjcProject::IsInDevelopmentRegion -- '{0}' {1} in development region", ibFile, result ? "is" : "is not");

			return result;
		}

		/// <summary>
		///   Updates the references.
		/// </summary>
		internal void UpdateReferences ()
		{
			// Remove any Monobjc references
			List<ProjectReference> references = new List<ProjectReference> (this.References.Where (BuildHelper.IsMonobjcReference));
			IDELogger.Log ("MonobjcProject::UpdateReferences -- Removing {0} references", references.Count ());
			foreach (ProjectReference item in references) {
				this.References.Remove (item);
			}

			// Retrieve assembly from the project
			List<SystemAssembly> assemblies = new List<SystemAssembly> (this.GetMonobjcAssemblies (this.TargetOSVersion));
			String[] names = this.OSFrameworks.Split (';');
			IDELogger.Log ("MonobjcProject::UpdateReferences -- Adding {0} names", names.Length);

			// Addition function
			Action<SystemAssembly> adder = delegate(SystemAssembly a) {
				ProjectReference reference = new ProjectReference (a);
				// NOTE: Starting with Monobjc 4.0, assembly references use a fixed numbering scheme (ex: 10.7.0.0)
				// In this case, we can require a specific version
				reference.SpecificVersion = a.Version.EndsWith (".0.0");
				this.References.Add (reference);
			};

			// Add core library
			SystemAssembly assembly = assemblies.FirstOrDefault (a => a.Name == "Monobjc");
			if (assembly != null) {
				adder(assembly);
			}

			// Add reference libraries
			foreach (String name in names) {
				String qualifiedName = "Monobjc." + name;
				assembly = assemblies.FirstOrDefault (a => a.Name == qualifiedName);
				if (assembly != null) {
					adder(assembly);
				}
			}
			this.Save (new NullProgressMonitor ());

			// Defer the framework loading code generation in a separate thread
			this.CodeBehindHandler.GenerateFrameworkLoadingCode(names);
		}

		/// <summary>
		///   Gets the files pairs.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		/// <param name = "destinationDirectory">The destination directory.</param>
		/// <returns>A list of pairs</returns>
		internal IEnumerable<FilePair> GetFiles (ConfigurationSelector configuration, FilePath destinationDirectory, String fileType)
		{
			// Return each content file
			foreach (ProjectFile file in this.Files) {
				if (file.BuildAction != fileType) {
					continue;
				}
				
				FilePath destination = destinationDirectory.Combine (file.FilePath.ToRelative (this.BaseDirectory));
				yield return new FilePair (file.FilePath, destination);
			}
		}
	}
}
