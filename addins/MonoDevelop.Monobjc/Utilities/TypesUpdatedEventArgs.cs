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
using MonoDevelop.Core;
using MonoDevelop.Projects;

#if MD_2_6 || MD_2_8
using MonoDevelop.Projects.Dom;
#endif

namespace MonoDevelop.Monobjc.Utilities
{
	/// <summary>
	/// Event arguments for type update/deletion.
	/// </summary>
	public class TypesUpdatedEventArgs : EventArgs
	{
		public TypesUpdatedEventArgs(Project project, IList<IType> typesUpdated, IList<IType> typesDeleted)
		{
			this.Project = project;
			this.TypesUpdated = typesUpdated;
			this.TypesDeleted = typesDeleted;
		}
		
		public Project Project { get; set; }
		
		public IList<IType> TypesUpdated { get; set; }
		
		public IList<IType> TypesDeleted { get; set; }
	}
	
}
