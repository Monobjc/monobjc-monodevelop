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

namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcProjectOptionsWidget
	{
		private bool Archive {
			get { return this.checkbuttonArchivePackage.Active; }
			set { 
				this.checkbuttonArchivePackage.Active = value;
				this.HandleCheckbuttonArchivePackageToggled(this, EventArgs.Empty);
			}
		}
		
		private String ArchiveIdentity {
			get { return GetSingleValue<String>(this.comboboxPackagingCertificates); }
			set { SetSingleValue(this.comboboxPackagingCertificates, value); }
		}
	}
}
