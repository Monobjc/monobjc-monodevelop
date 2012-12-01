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

namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcProjectOptionsWidget
	{
		public MonobjcApplicationType ApplicationType {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public String BundleId {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public String BundleVersion {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public FilePath MainNibFile {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public FilePath BundleIcon {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public FilePath Entitlements {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public string OSFrameworks {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public MacOSVersion TargetOSVersion {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public bool Signing {
			get { throw new NotImplementedException(); }
			set { }
		}
		
		public String SigningIdentity {
			get { throw new NotImplementedException(); }
			set { }
		}
	}
}
