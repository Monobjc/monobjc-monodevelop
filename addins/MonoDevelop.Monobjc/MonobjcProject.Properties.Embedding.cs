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
using MonoDevelop.Core.Serialization;
using Monobjc.Tools.Utilities;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private MacOSArchitecture targetOSArch;
		private String embeddedFrameworks;
		private String additionalAssemblies;
		private String excludedAssemblies;
		private String additionalLibraries;

		/// <summary>
		///   Gets or sets the target OS arch.
		/// </summary>
		[ItemProperty("MacOSArch")]
		public MacOSArchitecture TargetOSArch {
			get { return this.targetOSArch; }
			set {
				this.targetOSArch = value;
				this.NotifyModified ("MacOSArch");
			}
		}
		
		/// <summary>
		///   Gets or sets the embedded frameworks.
		/// </summary>
		[ItemProperty("EmbeddedFrameworks")]
		public String EmbeddedFrameworks {
			get { return this.embeddedFrameworks; }
			set {
				this.embeddedFrameworks = value;
				this.NotifyModified ("EmbeddedFrameworks");
			}
		}

		/// <summary>
		///   Gets or sets the additional assemblies.
		/// </summary>
		[ItemProperty("AdditionalAssemblies")]
		public String AdditionalAssemblies {
			get { return this.additionalAssemblies; }
			set {
				this.additionalAssemblies = value;
				this.NotifyModified ("AdditionalAssemblies");
			}
		}

		/// <summary>
		///   Gets or sets the excluded assemblies.
		/// </summary>
		[ItemProperty("ExcludedAssemblies")]
		public String ExcludedAssemblies {
			get { return this.excludedAssemblies; }
			set {
				this.excludedAssemblies = value;
				this.NotifyModified ("ExcludedAssemblies");
			}
		}

		/// <summary>
		///   Gets or sets the additional libraries.
		/// </summary>
		[ItemProperty("AdditionalLibraries")]
		public String AdditionalLibraries {
			get { return this.additionalLibraries; }
			set {
				this.additionalLibraries = value;
				this.NotifyModified ("AdditionalLibraries");
			}
		}
	}
}
