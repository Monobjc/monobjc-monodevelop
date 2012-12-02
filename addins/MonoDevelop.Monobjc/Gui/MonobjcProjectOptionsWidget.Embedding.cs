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

namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcProjectOptionsWidget
	{
		public MacOSArchitecture TargetOSArch {
			get { return GetSingleValue<MacOSArchitecture> (this.comboboxArchitectures); }
			set { SetSingleValue (this.comboboxArchitectures, value); }
		}
		
		public String EmbeddedFrameworks {
			get {
				String[] values = GetMultipleValues<String> (this.treeviewEmbeddedFrameworks).ToArray ();
				return String.Join (";", values);
			}
			set {
				IEnumerable<String> values = (value ?? "Foundation;AppKit").Split (' ', ',', ';');
				SetMultipleValues (this.treeviewEmbeddedFrameworks, values);
			}
		}
		
		public String AdditionalAssemblies {
			get { return ExtractFromModel (this.treeviewAdditionnalAssemblies.Model); }
			set { InjectIntoModel (this.treeviewAdditionnalAssemblies.Model, value); }
		}
		
		public String ExcludedAssemblies {
			get { return ExtractFromModel (this.treeviewExcludedAssemblies.Model); }
			set { InjectIntoModel (this.treeviewExcludedAssemblies.Model, value); }
		}
		
		public String AdditionalLibraries {
			get { return ExtractFromModel (this.treeviewAdditionnalLibraries.Model); }
			set { InjectIntoModel (this.treeviewAdditionnalLibraries.Model, value); }
		}
	}
}
