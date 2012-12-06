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
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Monobjc.Tracking;
using System.Xml;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private IEnumerable<SystemAssembly> monobjcAssemblies;

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
			get { return this.monobjcAssemblies ?? (this.monobjcAssemblies = this.AssemblyContext.GetAssemblies ().Where (BuildHelper.IsMonobjcReference)); }
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
		}

		private String GetNodeValue(XmlElement element, String key, String @default)
		{
			XmlNode node = element.SelectSingleNode(key);
			if (node == null) {
				return @default;
			}
			return node.InnerText;
		}

		private T GetNodeValue<T>(XmlElement element, String key, T @default)
		{
			XmlNode node = element.SelectSingleNode(key);
			if (node == null) {
				return @default;
			}
			return (T) Enum.Parse(typeof(T), node.InnerText);
		}
	}
}
