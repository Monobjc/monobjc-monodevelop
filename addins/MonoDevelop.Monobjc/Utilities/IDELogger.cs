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
using System.Diagnostics;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Utilities
{
	static class IDELogger
	{
		[Conditional("DEBUG")]
		internal static void Log (String message)
		{
			LoggingService.LogInfo (message);
		}
		
		[Conditional("DEBUG")]
		internal static void Log (String message, Exception ex)
		{
			LoggingService.LogInfo (message, ex);
		}
		
		[Conditional("DEBUG")]
		internal static void Log (String messageFormat, params Object[] args)
		{
			LoggingService.LogInfo (messageFormat, args);
		}

		[Conditional("DEBUG")]
		internal static void Log (String messageFormat, ProjectFileEventArgs e)
		{
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				Log (messageFormat, projectFile.Name);
			}
		}
	}
}