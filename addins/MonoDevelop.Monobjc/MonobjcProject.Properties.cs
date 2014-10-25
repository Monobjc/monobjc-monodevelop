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
using System.Linq;
using System.Xml;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Monobjc.Tracking;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private static IDictionary<Version, MacOSVersion> versionMap = new Dictionary<Version, MacOSVersion> () {
			{ new Version(10, 5, 0, 0), MacOSVersion.MacOS105 },
			{ new Version(10, 6, 0, 0), MacOSVersion.MacOS106 },
			{ new Version(10, 7, 0, 0), MacOSVersion.MacOS107 },
            { new Version(10, 8, 0, 0), MacOSVersion.MacOS108 },
            { new Version(10, 9, 0, 0), MacOSVersion.MacOS109 },
            { new Version(10, 10, 0, 0), MacOSVersion.MacOS1010 },
		};

		internal CodeBehindHandler CodeBehindHandler { get; private set; }

		internal DependencyHandler DependencyHandler { get; private set; }
		
		internal EmbeddingHandler EmbeddingHandler { get; private set; }
		
		internal MigrationHandler MigrationHandler { get; private set; }

		internal ResolverHandler ResolverHandler { get; private set; }

		internal XcodeHandler XcodeHandler { get; private set; }

		internal FilePath XcodeProjectFolder {
			get { return this.XcodeHandler.XcodeProject.ProjectFolder; }
		}

		/// <summary>
		///   Gets the project Monobjc assemblies.
		/// </summary>
		internal IEnumerable<ProjectReference> ProjectMonobjcAssemblies {
			get { return this.References.Where (BuildHelper.IsMonobjcReference); }
		}

		/// <summary>
		///   Returns all the registered Monobjc assemblies.
		/// </summary>
		internal IEnumerable<SystemAssembly> EveryMonobjcAssemblies {
			get { return this.AssemblyContext.GetAssemblies ().Where (BuildHelper.IsMonobjcReference); }
		}

		internal IEnumerable<SystemAssembly> GetMonobjcAssemblies(MacOSVersion version) {
			// If there is no assemblies, then skip the retrieval
			IEnumerable<SystemAssembly> assemblies = this.EveryMonobjcAssemblies;
			if (assemblies.Count() == 0) {
				return assemblies;
			}

			// Get the min/max version
			Version minVersion = new Version(assemblies.OrderBy(sa => sa.Version).First().Version);
			Version maxVersion = new Version(assemblies.OrderBy(sa => sa.Version).Last().Version);

			IDELogger.Log("MonobjcProject::GetMonobjcAssemblies -- Found minVersion: {0}", minVersion);
			IDELogger.Log("MonobjcProject::GetMonobjcAssemblies -- Found maxVersion: {0}", maxVersion);

			// Clamp the version
			MacOSVersion min = versionMap[minVersion];
			MacOSVersion max = versionMap[maxVersion];
			if (version < min) {
				version = min;
			}
			if (version > max) {
				version = max;
			}

			// Only return assemblies that have the selected version
			Version selectedVersion = versionMap.First(kvp => kvp.Value == version).Key;
			return assemblies.Where(sa => sa.Version == selectedVersion.ToString());
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize ()
		{
			IDELogger.Log ("MonobjcProject::Initialize");

			this.ApplicationType = this.ApplicationType == MonobjcProjectType.None ? MonobjcProjectType.CocoaApplication : this.ApplicationType;
			this.ApplicationCategory = this.ApplicationCategory ?? String.Empty;
			this.BundleId = this.BundleId ?? "net.monobjc.application.Test";
			this.BundleVersion = this.BundleVersion ?? "1.0";
			this.TargetOSVersion = this.TargetOSVersion == MacOSVersion.None ? MacOSVersion.MacOS106 : this.TargetOSVersion;
			this.SigningIdentity = this.SigningIdentity ?? String.Empty;
			this.OSFrameworks = this.OSFrameworks ?? "Foundation;AppKit";

			this.TargetOSArch = this.TargetOSArch == MacOSArchitecture.None ? MacOSArchitecture.X86 : this.TargetOSArch;
			this.EmbeddedFrameworks = this.EmbeddedFrameworks ?? String.Empty;
			this.AdditionalAssemblies = this.AdditionalAssemblies ?? String.Empty;
			this.ExcludedAssemblies = this.ExcludedAssemblies ?? String.Empty;
			this.AdditionalLibraries = this.AdditionalLibraries ?? String.Empty;

			this.ArchiveIdentity = this.ArchiveIdentity ?? String.Empty;
			
			this.DevelopmentRegion = this.DevelopmentRegion ?? "en";
			this.EncryptionSeed = this.EncryptionSeed ?? String.Empty;

			// Create the handlers
			this.CodeBehindHandler = new CodeBehindHandler (this);
			this.DependencyHandler = new DependencyHandler (this);
			this.EmbeddingHandler = new EmbeddingHandler (this);
			this.MigrationHandler = new MigrationHandler (this);
			this.ResolverHandler = new ResolverHandler (this);
			this.XcodeHandler = new XcodeHandler (this);
		}

		private String GetNodeValue (XmlElement element, String key, String @default)
		{
			XmlNode node = element.SelectSingleNode (key);
			if (node == null) {
				return @default;
			}
			return node.InnerText;
		}

		private T GetNodeValue<T> (XmlElement element, String key, T @default)
		{
			XmlNode node = element.SelectSingleNode (key);
			if (node == null) {
				return @default;
			}
			return (T)Enum.Parse (typeof(T), node.InnerText);
		}
	}
}
