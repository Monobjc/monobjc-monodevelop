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
using MonoDevelop.Ide.Desktop;

namespace MonoDevelop.Monobjc
{
	public class DeveloperToolsDesktopApplication : DesktopApplication
	{
		private static Version developerToolsVersion;
		private readonly MonobjcProject project;

		public DeveloperToolsDesktopApplication (MonobjcProject project) : base(Constants.DEVELOPER_TOOLS, Constants.APPLICATION_TITLE, true)
		{
			this.project = project;
		}

		public override void Launch (params string[] files)
		{
			String arguments = GetArguments (this.project, files [0]);
			Process.Start ("open", arguments);
		}

		public static String DeveloperToolsFolder {
			get { return PropertyService.Get<String> (Constants.DEVELOPER_TOOLS, "/"); }
			set {
				developerToolsVersion = null;
				PropertyService.Set (Constants.DEVELOPER_TOOLS, value);
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

		internal static bool IsXcode40OrHigher {
			get {
				Version version = DeveloperToolsVersion;
				if (version == null) {
					return false;
				}
				return (version.Major >= 4);
			}
		}

		internal static Version DeveloperToolsVersionForFolder (String folder)
		{
			String path = Path.Combine (folder, Constants.XCODE_APPLICATION);
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
					String path = Path.Combine (DeveloperToolsFolder, Constants.INTERFACE_BUILDER_APPLICATION);
					arguments.AppendFormat("\"{0}\" \"{1}\"", path, file);
					break;
				}
			case 4:
				{
					String path = Path.Combine (DeveloperToolsFolder, Constants.XCODE_APPLICATION);
					arguments.AppendFormat("\"{0}\" \"{1}\"", path, project.XcodeProjectFolder);
					break;
				}
			default:
				throw new NotSupportedException ();
			}

			return arguments.ToString ();
		}
	}
}
