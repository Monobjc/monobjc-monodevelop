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
using MonoDevelop.Monobjc.Tracking;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private IEnumerable<SystemAssembly> monobjcAssemblies;

		/// <summary>
		///   Gets or sets the application type.
		/// </summary>
		/// <value>The application type.</value>
		[ItemProperty("MacOSApplicationType")]
		public MonobjcApplicationType ApplicationType { get; set; }

		/// <summary>
		///   Gets or sets the main IB file.
		/// </summary>
		/// <value>The main nib file.</value>
		[ProjectPathItemProperty("MainNibFile")]
		public FilePath MainNibFile { get; set; }

		/// <summary>
		///   Gets or sets the bundle icon.
		/// </summary>
		/// <value>The bundle icon.</value>
		[ProjectPathItemProperty("BundleIcon")]
		public FilePath BundleIcon { get; set; }

		/// <summary>
		///   Gets or sets the OS frameworks.
		/// </summary>
		/// <value>The OS frameworks.</value>
		[ItemProperty("MacOSFrameworks")]
		public string OSFrameworks { get; set; }

		/// <summary>
		///   Gets or sets the target OS version.
		/// </summary>
		/// <value>The target OS version.</value>
		[ItemProperty("MacOSVersion")]
		public MacOSVersion TargetOSVersion { get; set; }

		/// <summary>
		///   Gets or sets the target OS arch.
		/// </summary>
		/// <value>The target OS arch.</value>
		[ItemProperty("MacOSArch")]
		public MacOSArchitecture TargetOSArch { get; set; }

		/// <summary>
		///   Gets or sets the signing identity.
		/// </summary>
		/// <value>The signing identity.</value>
		[ItemProperty("SigningIdentity")]
		public String SigningIdentity { get; set; }

		/// <summary>
		///   Gets or sets the archive.
		/// </summary>
		/// <value>The archive.</value>
		[ItemProperty("Archive")]
		public bool Archive { get; set; }

		/// <summary>
		///   Gets or sets the archive identity.
		/// </summary>
		/// <value>The archive identity.</value>
		[ItemProperty("ArchiveIdentity")]
		public String ArchiveIdentity { get; set; }

		/// <summary>
		///   Gets or sets the code behind tracker.
		/// </summary>
		internal CodeBehindProjectTracker CodeBehindTracker { get; private set; }

		/// <summary>
		///   Gets or sets the xcode tracker.
		/// </summary>
		internal XcodeProjectTracker XcodeTracker { get; private set; }

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
	}
}
