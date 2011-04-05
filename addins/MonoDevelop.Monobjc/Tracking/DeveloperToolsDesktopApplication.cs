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
using System.IO;
using System.Text;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Desktop;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	public class DeveloperToolsDesktopApplication
#if MD_2_6
		: DesktopApplication
#endif
	{
        public const String APPLICATION_TITLE = "Xcode/Interface Builder";
        public const String DEVELOPER_TOOLS = "MonoDevelop.Monobjc.DeveloperTools";
		public const String XCODE_APPLICATION = "Applications/Xcode.app";
		public const String INTERFACE_BUILDER_APPLICATION = "Applications/Interface Builder.app";

		private static Version developerToolsVersion;
		
#if MD_2_6
		private readonly MonobjcProject project;

		public DeveloperToolsDesktopApplication(MonobjcProject project) : base(DEVELOPER_TOOLS, APPLICATION_TITLE, true)
		{
			this.project = project;
		}
#endif
		
#if MD_2_6
		public override void Launch(params string[] files)
		{
			String cmd = GenerateCommandLine(this.project, files[0]);
			Process.Start(cmd);
		}
#endif
		
		public static String DeveloperToolsFolder
		{
			get { return PropertyService.Get<String>(DEVELOPER_TOOLS, "/Developer"); }
			set
			{
				PropertyService.Set(DEVELOPER_TOOLS, value);
				developerToolsVersion = null;
			}
		}

		public static Version DeveloperToolsVersion
		{
			get
			{
				if (developerToolsVersion == null)
				{
					String path = Path.Combine(DeveloperToolsFolder, XCODE_APPLICATION);
					if (Directory.Exists(path))
					{
						developerToolsVersion = NativeVersionExtractor.GetVersion(path);
					}
				}
				return developerToolsVersion;
			}
		}
		
		public static DesktopApplication GetDesktopApplication(MonobjcProject project)
		{
#if MD_2_4
			String command = GenerateCommandLine(project, null);
			return new DesktopApplication(DEVELOPER_TOOLS, APPLICATION_TITLE, command);
#endif
#if MD_2_6
			return new DeveloperToolsDesktopApplication(project);
#endif
		}

		public static String[] GetFilesToOpen(MonobjcProject project, String file)
		{
			Version version = DeveloperToolsVersion;
			if (version == null) {
				// TODO: Display an error message
				// throw new NotSupportedException();
				return null;
			}
			
			// TODO: Use an enhanced switch to handle minor number
			String[] files;
			switch(version.Major)
			{
				case 3:
				{
					files = new String[1];
					// Path to the XIB file to open
					files[0] = file;
					break;
				}
				case 4:
				{
					files = new String[2];
					// TODO: Path to the Xcode surrogate project
					//files[0] = project.;
					// Path to the XIB file to open
					files[1] = file;
					break;
				}
				default:
					// TODO: Display an error message
					throw new NotSupportedException();
			}
			
			return files;
		}
		
		private static String GenerateCommandLine(MonobjcProject project, String file)
		{
			Version version = DeveloperToolsVersion;
			if (version == null) {
				// TODO: Display an error message
				// throw new NotSupportedException();
				return null;
			}
			
			// TODO: Use an enhanced switch to handle minor number
			String path;
			switch(version.Major)
			{
				case 3:
				{
					path = Path.Combine(DeveloperToolsFolder, INTERFACE_BUILDER_APPLICATION);
					break;
				}
				case 4:
				{
					path = Path.Combine(DeveloperToolsFolder, XCODE_APPLICATION);
					break;
				}
				default:
					// TODO: Display an error message
					throw new NotSupportedException();
			}
			
			StringBuilder command = new StringBuilder();
			command.AppendFormat("open -a \"{0}\" \"{1}\"", path, file ?? "%f");
			return command.ToString();
		}
	}
}