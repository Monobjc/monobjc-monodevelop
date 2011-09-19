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
using Mono.Addins;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Formats.MSBuild;

namespace MonoDevelop.Monobjc
{
	[Extension]
	public class MonobjcMSBuildImportProvider : IMSBuildImportProvider
	{
		const string importCocoaApplication = @"$(MSBuildExtensionsPath)\Monobjc.CocoaApplication.targets";
		const string importConsoleApplication = @"$(MSBuildExtensionsPath)\Monobjc.ConsoleApplication.targets";
		
		public void UpdateImports (SolutionEntityItem item, List<string> imports)
		{
			// Remove imports
			imports.Remove (importCocoaApplication);
			imports.Remove (importConsoleApplication);
			
			/*
			// Check project nature
			MonobjcProject project = item as MonobjcProject;
			if (project == null) {
				return;
			}
			
			switch (project.ApplicationType) {
			case MonobjcApplicationType.CocoaApplication:
				imports.Add (importCocoaApplication);
				break;
			case MonobjcApplicationType.ConsoleApplication:
				imports.Add (importConsoleApplication);
				break;
			}
			*/
		}
	}
}
