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
        private const String importCocoaApplication = @"$(MSBuildBinPath)\Monobjc.CocoaApplication.targets";
        private const String importConsoleApplication = @"$(MSBuildBinPath)\Monobjc.ConsoleApplication.targets";
        private const String importCocoaLibrary = @"$(MSBuildBinPath)\Monobjc.CocoaLibrary.targets";
		
		public void UpdateImports (SolutionEntityItem item, List<String> imports)
		{
			// Remove imports
			imports.Remove (importCocoaApplication);
			imports.Remove (importConsoleApplication);
			imports.Remove (importCocoaLibrary);
			
			// Check project nature
			MonobjcProject project = item as MonobjcProject;
			if (project == null) {
				return;
			}
			
			switch (project.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				imports.Add (importCocoaApplication);
				break;
			case MonobjcProjectType.ConsoleApplication:
				imports.Add (importConsoleApplication);
				break;
			case MonobjcProjectType.CocoaLibrary:
				imports.Add (importCocoaLibrary);
				break;
			}
		}
	}
}
