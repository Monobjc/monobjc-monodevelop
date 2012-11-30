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

namespace MonoDevelop.Monobjc
{
	public class DeveloperToolsDesktopApplication : DesktopApplication
	{
		public const String APPLICATION_TITLE = "Apple Developer Tools";
		public const String DEVELOPER_TOOLS = "MonoDevelop.Monobjc.DeveloperTools";
		public const String XCODE_APPLICATION = "Applications/Xcode.app";
		public const String INTERFACE_BUILDER_APPLICATION = "Applications/Interface Builder.app";
		private static Version developerToolsVersion;
		private readonly MonobjcProject project;

		public DeveloperToolsDesktopApplication (MonobjcProject project) : base(DEVELOPER_TOOLS, APPLICATION_TITLE, true)
		{
			this.project = project;
		}

		public override void Launch (params string[] files)
		{
			String arguments = GetArguments (this.project, files [0]);
			Process.Start ("open", arguments);
		}

		public static String DeveloperToolsFolder {
			get { return PropertyService.Get<String> (DEVELOPER_TOOLS, "/Developer"); }
			set {
				developerToolsVersion = null;
				PropertyService.Set (DEVELOPER_TOOLS, value);
			}
		}

		public static Version DeveloperToolsVersion {
			get {
				if (developerToolsVersion == null) {
					developerToolsVersion = DeveloperToolsVersionForFolder (DeveloperToolsFolder);
				}
				return developerToolsVersion;
			}
		}
		
		internal static Version DeveloperToolsVersionForFolder (String folder)
		{
			String path = Path.Combine (folder, XCODE_APPLICATION);
			if (Directory.Exists (path)) {
				return NativeVersionExtractor.GetVersion (path);
			}
			return null;
		}

		private static String GetArguments (MonobjcProject project, String file)
		{
			Version version = DeveloperToolsVersion;
			if (version == null) {
				return null;
			}
			
			StringBuilder arguments = new StringBuilder ();
			arguments.Append ("-a ");
			switch (version.Major) {
			case 3:
				{
					String path = Path.Combine (DeveloperToolsFolder, INTERFACE_BUILDER_APPLICATION);
					//arguments.AppendFormat("\"{0}\" \"{1}\"", path, file);
					break;
				}
			case 4:
				{
					String path = Path.Combine (DeveloperToolsFolder, XCODE_APPLICATION);
					//arguments.AppendFormat("\"{0}\" \"{1}\"", path, project.XcodeTracker.ProjectFolder);
					break;
				}
			default:
				throw new NotSupportedException ();
			}

			return arguments.ToString ();
		}
	}
}
