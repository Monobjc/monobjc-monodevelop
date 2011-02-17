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
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Refactoring;
using MonoDevelop.Projects.Dom;

namespace MonoDevelop.Monobjc.Refactoring
{
	public class CreateIVarOperation : BaseOperation
	{
		public override string GetMenuDescription (RefactoringOptions options)
		{
			return GettextCatalog.GetString ("Create Instance Variable");
		}

		public override bool IsValid (RefactoringOptions options)
		{
			if (options.ResolveResult == null) {
				return false;
			}
			
			MonobjcProject project = options.Document.Project as MonobjcProject;
			if (project == null) {
				return false;
			}

			IType type = options.Dom.GetType (options.ResolveResult.ResolvedType);
			if (type == null || type.ClassType != MonoDevelop.Projects.Dom.ClassType.Class) {
				return false;
			}
			
			return true;
		}

		public override void Run (RefactoringOptions options)
		{
			MonobjcProject project = options.Dom.Project as MonobjcProject;
			MessageService.ShowCustomDialog (new CreateIVarDialog (this, options, project));
		}
	}
}
