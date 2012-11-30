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
		private bool archive;
		private String archiveIdentity;

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
	}
}
