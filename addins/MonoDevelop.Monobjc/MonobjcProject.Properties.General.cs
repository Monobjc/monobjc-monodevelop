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
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private MonobjcApplicationType applicationType;
		private String bundleId;
		private String bundleVersion;
		private FilePath mainNibFile;
		private FilePath bundleIcon;
		private FilePath entitlements;
		private String osFrameworks;
		private MacOSVersion targetOSVersion;
		private bool signing;
		private String signingIdentity;

		/// <summary>
		///   Gets or sets the application type.
		/// </summary>
		[ItemProperty("MacOSApplicationType")]
		public MonobjcApplicationType ApplicationType {
			get { return this.applicationType; }
			set {
				this.applicationType = value;
				this.NotifyModified ("MacOSApplicationType");
			}
		}

		/// <summary>
		/// Gets or sets the bundle identifier.
		/// </summary>
		[ItemProperty("BundleId")]
		public String BundleId {
			get { return this.bundleId; }
			set {
				this.bundleId = value;
				this.NotifyModified ("BundleId");
			}
		}

		/// <summary>
		/// Gets or sets the bundle identifier.
		/// </summary>
		[ItemProperty("BundleVersion")]
		public String BundleVersion {
			get { return this.bundleVersion; }
			set {
				this.bundleVersion = value;
				this.NotifyModified ("BundleVersion");
			}
		}

		/// <summary>
		///   Gets or sets the main IB file.
		/// </summary>
		[ProjectPathItemProperty("MainNibFile")]
		public FilePath MainNibFile {
			get { return this.mainNibFile; }
			set {
				this.mainNibFile = value;
				this.NotifyModified ("MainNibFile");
			}
		}

		/// <summary>
		///   Gets or sets the bundle icon.
		/// </summary>
		[ProjectPathItemProperty("BundleIcon")]
		public FilePath BundleIcon {
			get { return this.bundleIcon; }
			set {
				this.bundleIcon = value;
				this.NotifyModified ("BundleIcon");
			}
		}
		
		/// <summary>
		///   Gets or sets the bundle icon.
		/// </summary>
		[ProjectPathItemProperty("MacOSEntitlements")]
		public FilePath Entitlements {
			get { return this.entitlements; }
			set {
				this.entitlements = value;
				this.NotifyModified ("MacOSEntitlements");
			}
		}
		
		/// <summary>
		///   Gets or sets the OS frameworks.
		/// </summary>
		[ItemProperty("MacOSFrameworks")]
		public string OSFrameworks {
			get { return this.osFrameworks; }
			set {
				this.osFrameworks = value;
				this.NotifyModified ("MacOSFrameworks");
			}
		}

		/// <summary>
		///   Gets or sets the target OS version.
		/// </summary>
		[ItemProperty("MacOSVersion")]
		public MacOSVersion TargetOSVersion {
			get { return this.targetOSVersion; }
			set {
				this.targetOSVersion = value;
				this.NotifyModified ("MacOSVersion");
			}
		}

		/// <summary>
		///   Gets or sets the signing.
		/// </summary>
		[ItemProperty("Signing")]
		public bool Signing {
			get { return this.signing; }
			set {
				this.signing = value;
				this.NotifyModified ("Signing");
			}
		}

		/// <summary>
		///   Gets or sets the signing identity.
		/// </summary>
		[ItemProperty("SigningIdentity")]
		public String SigningIdentity {
			get { return this.signingIdentity; }
			set {
				this.signingIdentity = value;
				this.NotifyModified ("SigningIdentity");
			}
		}
	}
}
