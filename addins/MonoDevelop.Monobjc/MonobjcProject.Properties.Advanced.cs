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

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private String developmentRegion;
		private bool combineArtwork;
		private bool encryptArtwork;

		/// <summary>
		/// Gets or sets the development region.
		/// </summary>
		[ItemProperty("MacOSDevelopmentRegion", DefaultValue = "en")]
		public String DevelopmentRegion {
			get { return this.developmentRegion; }
			set {
				this.developmentRegion = value;
				this.NotifyModified ("MacOSDevelopmentRegion");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to combine artwork.
		/// </summary>
		[ItemProperty("CombineArtwork")]
		public bool CombineArtwork {
			get { return this.combineArtwork; }
			set {
				this.combineArtwork = value;
				this.NotifyModified ("CombineArtwork");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to encrypt artwork.
		/// </summary>
		[ItemProperty("EncryptArtwork")]
		public bool EncryptArtwork {
			get { return this.encryptArtwork; }
			set {
				this.encryptArtwork = value;
				this.NotifyModified ("EncryptArtwork");
			}
		}
	}
}
