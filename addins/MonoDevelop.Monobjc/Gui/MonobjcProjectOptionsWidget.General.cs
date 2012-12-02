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

namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcProjectOptionsWidget
	{
		public MonobjcProjectType ApplicationType {
			get { return GetSingleValue<MonobjcProjectType> (this.comboboxType); }
			set { SetSingleValue (this.comboboxType, value); }
		}
		
		public String ApplicationCategory {
			get { return GetSingleValue<String> (this.comboboxApplicationCategory); }
			set { SetSingleValue (this.comboboxApplicationCategory, value); }
		}
		
		public String BundleId {
			get { return this.entryBundleIdentifier.Text; }
			set { this.entryBundleIdentifier.Text = value; }
		}
		
		public String BundleVersion {
			get { return this.entryBundleVersion.Text; }
			set { this.entryBundleVersion.Text = value; }
		}
		
		public FilePath MainNibFile {
			get { return this.filechooserbuttonMainNib.Filename; }
			set { this.filechooserbuttonMainNib.SetFilename (value); }
		}
		
		public FilePath BundleIcon {
			get { return this.filechooserbuttonBundleIcon.Filename; }
			set { this.filechooserbuttonBundleIcon.SetFilename (value); }
		}
		
		public MacOSVersion TargetOSVersion {
			get { return GetSingleValue<MacOSVersion> (this.comboboxOSVersion); }
			set { SetSingleValue (this.comboboxOSVersion, value); }
		}
		
		public bool Signing {
			get { return this.checkbuttonSigning.Active; }
			set { this.checkbuttonSigning.Active = value; }
		}
		
		public String SigningIdentity {
			get { return GetSingleValue<String> (this.comboboxSigningCertificates); }
			set { SetSingleValue (this.comboboxSigningCertificates, value); }
		}

		public bool UseEntitlements {
			get { return this.checkbuttonEntitlements.Active; }
			set { this.checkbuttonEntitlements.Active = value; }
		}

		public String OSFrameworks {
			get {
				String[] values = GetMultipleValues<String> (this.treeviewFrameworks).ToArray ();
				return String.Join(";", values);
			}
			set {
				IEnumerable<String> values = (value ?? "Foundation;AppKit").Split (' ', ',', ';');
				SetMultipleValues (this.treeviewFrameworks, values);
			}
		}
	}
}
