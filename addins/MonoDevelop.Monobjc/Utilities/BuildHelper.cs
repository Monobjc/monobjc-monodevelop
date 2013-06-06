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
using MonoDevelop.Projects;
using Monobjc.Tools.External;
using Monobjc.Tools.Generators;
using System.Reflection;
using System.Security.Cryptography;

namespace MonoDevelop.Monobjc.Utilities
{
	/// <summary>
	///   Helper class for various build tasks.
	/// </summary>
	public static class BuildHelper
	{
		/// <summary>
		///   Determines whether the specified filename is a source file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a source file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSourceFile (ProjectFile projectFile, string sourceExtension)
		{
			return String.Equals(projectFile.FilePath.Extension, sourceExtension, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		///   Determines whether the specified filename is a resource file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a resource file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsResourceFile (ProjectFile file)
		{
			return (BuildHelper.IsInfoPlist (file) || 
				BuildHelper.IsNormalXIBFile (file) ||
				BuildHelper.IsEmbeddedXIBFile (file) ||
				BuildHelper.IsStringsFile (file));
		}
		
		/// <summary>
		///   Determines whether the specified filename is a NIB file.
		/// </summary>
		/// <param name = "filename">The filename.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a NIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNIBFile (String filename)
		{
			String extension = Path.GetExtension (filename);
			return String.Equals (Constants.DOT_NIB, extension);
		}
		
		/// <summary>
		///   Determines whether the specified filename is a XIB file.
		/// </summary>
		/// <param name = "filename">The filename.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a XIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsXIBFile (String filename)
		{
			String extension = Path.GetExtension (filename);
			return String.Equals (Constants.DOT_XIB, extension);
		}
		
		/// <summary>
		///   Determines whether the specified filename is a strings file.
		/// </summary>
		/// <param name = "filename">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a strings file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsStringsFile (String filename)
		{
			String extension = Path.GetExtension (filename);
			return String.Equals (Constants.DOT_STRINGS, extension);
		}
		
		/// <summary>
		///   Determines whether the specified filename is an Info.plist file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is an Info.plist file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsInfoPlist (ProjectFile file)
		{
			return ("Info.plist".Equals (file.FilePath.FileName));
		}
		
		/// <summary>
		///   Determines whether the specified filename is a XIB file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a XIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsXIBFile (ProjectFile file)
		{
			return (BuildHelper.IsNormalXIBFile (file) ||
				BuildHelper.IsEmbeddedXIBFile (file));
		}
		
		/// <summary>
		///   Determines whether the specified filename is a XIB file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a XIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNormalXIBFile (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			return String.Equals (Constants.DOT_XIB, extension) && (file.BuildAction == Constants.InterfaceDefinition);
		}
		
		/// <summary>
		///   Determines whether the specified filename is a XIB file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a XIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEmbeddedXIBFile (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			return String.Equals (Constants.DOT_XIB, extension) && (file.BuildAction == Constants.EmbeddedInterfaceDefinition);
		}

		/// <summary>
		///   Determines whether the specified filename is a NIB file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a NIB file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNIBFile (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			return String.Equals (Constants.DOT_NIB, extension) && (file.BuildAction == BuildAction.EmbeddedResource);
		}
		
		/// <summary>
		///   Determines whether the specified filename is a strings file.
		/// </summary>
		/// <param name = "file">The file.</param>
		/// <returns>
		///   <c>true</c> if the specified filename is a strings file; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsStringsFile (ProjectFile file)
		{
			String extension = file.FilePath.Extension;
			return String.Equals (Constants.DOT_STRINGS, extension) && (file.BuildAction == BuildAction.Content);
		}

		/// <summary>
		///   Determines whether the specified versions are compatible.
		/// </summary>
		/// <param name = "version1">The version1.</param>
		/// <param name = "version2">The version2.</param>
		/// <returns>
		///   <c>true</c> if the specified versions are compatible; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsCompatible (Version version1, Version version2)
		{
			return (version1.Major == version2.Major) && (version1.Minor == version2.Minor);
		}

		/// <summary>
		///   Determines whether the specified reference is a Monobjc one.
		/// </summary>
		/// <param name = "reference">The reference.</param>
		/// <returns>
		///   <c>true</c> if the specified reference is a Monobjc one; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsMonobjcReference (ProjectReference reference)
		{
			return reference.Reference.StartsWith ("Monobjc");
		}

		/// <summary>
		///   Determines whether the specified reference is a Monobjc one.
		/// </summary>
		/// <param name = "assembly">The assembly.</param>
		/// <returns>
		///   <c>true</c> if the specified reference is a Monobjc one; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsMonobjcReference (SystemAssembly assembly)
		{
			return assembly.Name.StartsWith ("Monobjc");
		}

		/// <summary>
		///   Compiles the XIB files.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		/// <param name = 'result'>The build result.</param>
		public static void CompileXIBFiles (IProgressMonitor monitor, MonobjcProject project, BundleMaker maker, BuildResult result)
		{
			XibCompiler xibCompiler = new XibCompiler ();
			IEnumerable<FilePair> files = project.GetIBFiles (Constants.InterfaceDefinition, maker.ResourcesFolder);
            if (files == null || files.Count() == 0) {
                return;
			}
			List<FilePair> pairs = new List<FilePair> (files);
			monitor.BeginTask (GettextCatalog.GetString ("Compiling XIB files..."), files.Count ());
			foreach (FilePair pair in pairs) {
				monitor.Log.WriteLine (GettextCatalog.GetString ("Compiling {0}", pair.Source.ToRelative (project.BaseDirectory)));
				xibCompiler.Logger = new BuildLogger (pair.Source, monitor, result);
				xibCompiler.Compile (pair.Source, pair.DestinationDir);
				monitor.Step (1);
			}
			monitor.EndTask ();
		}

		/// <summary>
		///   Embeds the XIB files.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		/// <param name = 'result'>The build result.</param>
		public static void EmbedXIBFiles (IProgressMonitor monitor, MonobjcProject project, BuildResult result)
		{
			XibCompiler xibCompiler = new XibCompiler ();
			IEnumerable<FilePair> files = project.GetIBFiles (Constants.EmbeddedInterfaceDefinition, null);
            if (files == null || files.Count() == 0) {
                return;
			}
			
			monitor.BeginTask (GettextCatalog.GetString ("Embed XIB files..."), files.Count ());
			foreach (FilePair pair in files) {
				// If the destination file is a place-holder, change its dates
				FileInfo sourceInfo = new FileInfo (pair.Source);
				FileInfo destInfo = new FileInfo (pair.Destination);
				if (destInfo.Length == 0) {
					DateTime dateTime = sourceInfo.CreationTime.Subtract (new TimeSpan (0, 0, 1));
					File.SetCreationTime (pair.Destination, dateTime);
					File.SetLastAccessTime (pair.Destination, dateTime);
					File.SetLastWriteTime (pair.Destination, dateTime);
				}
				
				FilePath relativeFile = pair.Source.ToRelative (project.BaseDirectory);
				monitor.Log.WriteLine (GettextCatalog.GetString ("Compiling {0}", relativeFile));
				xibCompiler.Logger = new BuildLogger (pair.Source, monitor, result);
				xibCompiler.Compile (pair.Source, pair.DestinationDir);
				
				monitor.Step (1);
			}
			monitor.EndTask ();
		}

		/// <summary>
		///   Copies the content files.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'configuration'>The configuration.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void CopyOutputFiles (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
		{
			IEnumerable<FilePair> files = project.GetOutputFiles (configuration, maker.ResourcesFolder);
            if (files == null || files.Count() == 0) {
                return;
			}
			monitor.BeginTask (GettextCatalog.GetString ("Copying output files..."), files.Count ());
			foreach (FilePair pair in files) {
				monitor.Log.WriteLine (GettextCatalog.GetString ("Copying {0}", pair.Source.ToRelative (project.BaseDirectory)));
				pair.Copy (false);
				monitor.Step (1);
			}
			monitor.EndTask ();
		}

		/// <summary>
		///   Copies the content files.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'configuration'>The configuration.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void CopyContentFiles (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
		{
			IEnumerable<FilePair> files = project.GetContentFiles (configuration, maker.ResourcesFolder);
            if (files == null || files.Count() == 0) {
                return;
			}
			monitor.BeginTask (GettextCatalog.GetString ("Copying content files..."), files.Count ());
			foreach (FilePair pair in files) {
				monitor.Log.WriteLine (GettextCatalog.GetString ("Copying {0}", pair.Source.ToRelative (project.BaseDirectory)));
				pair.Copy (false);
				monitor.Step (1);
			}
			monitor.EndTask ();
		}

		/// <summary>
		/// Copies the Monobjc assemblies.
		/// </summary>
		/// <param name="monitor">The monitor.</param>
		/// <param name="project">The project.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="maker">The maker.</param>
		public static void CopyMonobjcAssemblies (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
		{
			IEnumerable<String> assemblies = project.ProjectMonobjcAssemblies.Select (a => a.GetReferencedFileNames (configuration) [0]);
			monitor.BeginTask (GettextCatalog.GetString ("Copying Monobjc assemblies..."), assemblies.Count ());
			foreach (String assembly in assemblies) {
				String filename = Path.GetFileName (assembly);
				monitor.Log.WriteLine (GettextCatalog.GetString ("Copying {0}", filename));
				File.Copy (assembly, Path.Combine (maker.ResourcesFolder, filename), true);
				monitor.Step (1);
			}
			monitor.EndTask ();
		}

		/// <summary>
		///   Creates the Info.plist file.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'configuration'>The configuration.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void CreateInfoPList (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
		{
			monitor.BeginTask (GettextCatalog.GetString ("Generating the Info.plist..."), 0);
			
			InfoPListGenerator pListGenerator = new InfoPListGenerator ();
			
			// If an Info.plist exists in the project then use it
			FilePath infoPListFile = project.BaseDirectory.Combine (Constants.INFO_PLIST);
			if (File.Exists (infoPListFile)) {
				pListGenerator.Content = File.ReadAllText (infoPListFile);
			}

			String mainAssembly = project.GetOutputFileName (configuration);
			Assembly assembly = Assembly.ReflectionOnlyLoadFrom (mainAssembly);
			AssemblyName assemblyName = assembly.GetName ();

			// TODO: Review to use new parameters
			pListGenerator.DevelopmentRegion = project.DevelopmentRegion;
			pListGenerator.ApplicationName = assemblyName.Name;
			pListGenerator.Identifier = project.BundleId;
			pListGenerator.Version = project.BundleVersion;
			pListGenerator.Icon = project.BundleIcon.IsNullOrEmpty ? null : project.BundleIcon.FileNameWithoutExtension;
			pListGenerator.MainNibFile = project.MainNibFile.IsNullOrEmpty ? null : project.MainNibFile.FileNameWithoutExtension;
			pListGenerator.TargetOSVersion = project.TargetOSVersion;
			pListGenerator.PrincipalClass = "NSApplication";
			pListGenerator.WriteTo (Path.Combine (maker.ContentsDirectory, Constants.INFO_PLIST));
			
			monitor.EndTask ();
		}

		/// <summary>
		/// Combines the artwork.
		/// </summary>
		public static void CombineArtwork (IProgressMonitor monitor, MonobjcProject project, BundleMaker maker)
		{
			if (!project.CombineArtwork) {
				return;
			}

			monitor.BeginTask (GettextCatalog.GetString ("Combining artwork..."), 0);
			using (StringWriter outputWriter = new StringWriter()) {
				using (StringWriter errorWriter = new StringWriter()) {
					ArtworkCombiner combiner = new ArtworkCombiner();
					combiner.Combine(maker.ResourcesFolder, outputWriter, errorWriter);
					LoggingService.LogInfo ("Combiner returns: " + outputWriter.ToString ());
				}
			}
			monitor.EndTask ();
		}

		/// <summary>
		/// Combines the artwork.
		/// </summary>
		public static void EncryptContentFiles (IProgressMonitor monitor, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
		{
			IEnumerable<FilePair> files = project.GetEncryptedContentFiles (configuration, maker.ResourcesFolder);
			if (files == null || files.Count() == 0) {
				return;
			}
			Aes provider = FileEncrypter.GetProvider (project.EncryptionSeed);
			monitor.BeginTask (GettextCatalog.GetString ("Encrypting content files..."), files.Count ());
			foreach (FilePair pair in files) {
				monitor.Log.WriteLine (GettextCatalog.GetString ("Encrypting {0}", pair.Source.ToRelative (project.BaseDirectory)));
				pair.Encrypt(provider);
				monitor.Step (1);
			}
			monitor.EndTask ();
		}
		
		/// <summary>
		///   Sign the application bundle.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void SignBundle (IProgressMonitor monitor, MonobjcProject project, BundleMaker maker)
		{
            if (project.Signing && !String.IsNullOrEmpty(project.SigningIdentity)) {
                monitor.BeginTask (GettextCatalog.GetString ("Signing bundle..."), 0);

				using (StringWriter outputWriter = new StringWriter()) {
					using (StringWriter errorWriter = new StringWriter()) {
						ProjectFile file = project.GetProjectFile("App.entitlements");
                        if (project.UseEntitlements && file != null) {
							CodeSign.PerformSigning (maker.ApplicationDirectory, project.SigningIdentity, file.FilePath, outputWriter, errorWriter);
						} else {
                            CodeSign.PerformSigning (maker.ApplicationDirectory, project.SigningIdentity, outputWriter, errorWriter);
						}
						LoggingService.LogInfo ("CodeSign returns: " + outputWriter.ToString ());
					}
				}

				monitor.EndTask ();
			}
		}
		
		/// <summary>
		///   Sign the native libraries inside the bundle.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void SignNativeBinaries (IProgressMonitor monitor, MonobjcProject project, BundleMaker maker)
		{
            if (project.Signing && !String.IsNullOrEmpty(project.SigningIdentity)) {
				String[] files = Directory.GetFiles (maker.MacOSDirectory, "*.dylib");
                if (files == null || files.Count() == 0) {
                    return;
                }

				monitor.BeginTask (GettextCatalog.GetString ("Signing native libraries..."), files.Length);

				foreach (String file in files) {
					using (StringWriter outputWriter = new StringWriter()) {
						using (StringWriter errorWriter = new StringWriter()) {
                            CodeSign.PerformSigning (file, project.SigningIdentity, outputWriter, errorWriter);
							LoggingService.LogInfo ("CodeSign returns: " + outputWriter.ToString ());
						}
					}
					monitor.Step (1);
				}

				monitor.EndTask ();
			}
		}
		
		/// <summary>
		///   Archive the application bundle.
		/// </summary>
		/// <param name = 'monitor'>The progress monitor.</param>
		/// <param name = 'project'>The project.</param>
		/// <param name = 'maker'>The bundle maker.</param>
		public static void ArchiveBundle (IProgressMonitor monitor, MonobjcProject project, BundleMaker maker)
		{
            if (project.Archive && project.ArchiveIdentity != null) {
				FilePath definitionFile = project.BaseDirectory.Combine ("Definition.plist");
                String definitionFilename = File.Exists (definitionFile) ? definitionFile.ToString () : null;
                monitor.BeginTask (GettextCatalog.GetString ("Signing archive..."), 0);

				using (StringWriter outputWriter = new StringWriter()) {
					using (StringWriter errorWriter = new StringWriter()) {
						ProductBuild.ArchiveApplication (maker.ApplicationDirectory, project.ArchiveIdentity, definitionFilename, outputWriter, errorWriter);
						LoggingService.LogInfo ("ProductBuild returns: " + outputWriter.ToString ());
					}
				}

				monitor.EndTask ();
			}
		}
	}
}
