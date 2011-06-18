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
		private MonobjcApplicationType applicationType;
		private FilePath mainNibFile;
		private FilePath bundleIcon;
		private String osFrameworks;
		private MacOSVersion targetOSVersion;
		private MacOSArchitecture targetOSArch;
		private String signingIdentity;
		private bool archive;
		private String archiveIdentity;
		
		/// <summary>
		///   Gets or sets the application type.
		/// </summary>
		/// <value>The application type.</value>
		[ItemProperty("MacOSApplicationType")]
		public MonobjcApplicationType ApplicationType
		{
			get { return this.applicationType; }
			set
			{
				this.applicationType = value;
				this.NotifyModified("ApplicationType");
			}
		}

		/// <summary>
		///   Gets or sets the main IB file.
		/// </summary>
		/// <value>The main nib file.</value>
		[ProjectPathItemProperty("MainNibFile")]
		public FilePath MainNibFile
		{
			get { return this.mainNibFile; }
			set
			{
				this.mainNibFile = value;
				this.NotifyModified("MainNibFile");
			}
		}

		/// <summary>
		///   Gets or sets the bundle icon.
		/// </summary>
		/// <value>The bundle icon.</value>
		[ProjectPathItemProperty("BundleIcon")]
		public FilePath BundleIcon
		{
			get { return this.bundleIcon; }
			set
			{
				this.bundleIcon = value;
				this.NotifyModified("BundleIcon");
			}
		}

		/// <summary>
		///   Gets or sets the OS frameworks.
		/// </summary>
		/// <value>The OS frameworks.</value>
		[ItemProperty("MacOSFrameworks")]
		public string OSFrameworks
		{
			get { return this.osFrameworks; }
			set
			{
				this.osFrameworks = value;
				this.NotifyModified("OSFrameworks");
			}
		}

		/// <summary>
		///   Gets or sets the target OS version.
		/// </summary>
		/// <value>The target OS version.</value>
		[ItemProperty("MacOSVersion")]
		public MacOSVersion TargetOSVersion
		{
			get { return this.targetOSVersion; }
			set
			{
				this.targetOSVersion = value;
				this.NotifyModified("TargetOSVersion");
			}
		}

		/// <summary>
		///   Gets or sets the target OS arch.
		/// </summary>
		/// <value>The target OS arch.</value>
		[ItemProperty("MacOSArch")]
		public MacOSArchitecture TargetOSArch
		{
			get { return this.targetOSArch; }
			set
			{
				this.targetOSArch = value;
				this.NotifyModified("TargetOSArch");
			}
		}

		/// <summary>
		///   Gets or sets the signing identity.
		/// </summary>
		/// <value>The signing identity.</value>
		[ItemProperty("SigningIdentity")]
		public String SigningIdentity
		{
			get { return this.signingIdentity; }
			set
			{
				this.signingIdentity = value;
				this.NotifyModified("SigningIdentity");
			}
		}

		/// <summary>
		///   Gets or sets the archive.
		/// </summary>
		/// <value>The archive.</value>
		[ItemProperty("Archive")]
		public bool Archive
		{
			get { return this.archive; }
			set
			{
				this.archive = value;
				this.NotifyModified("Archive");
			}
		}

		/// <summary>
		///   Gets or sets the archive identity.
		/// </summary>
		/// <value>The archive identity.</value>
		[ItemProperty("ArchiveIdentity")]
		public String ArchiveIdentity
		{
			get { return this.archiveIdentity; }
			set
			{
				this.archiveIdentity = value;
				this.NotifyModified("ArchiveIdentity");
			}
		}

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
