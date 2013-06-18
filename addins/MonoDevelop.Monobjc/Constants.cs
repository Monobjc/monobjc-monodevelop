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

namespace MonoDevelop.Monobjc
{
	static class Constants
	{
		public const String APPLICATION_TITLE = "Developer Tools (Monobjc)";

		public const String NOT_IMPLEMENTED_EXCEPTION = "NotImplementedException";

		public const String IBACTION = "Monobjc.IBActionAttribute";
		public const String IBOUTLET = "Monobjc.IBOutletAttribute";
		public const String OBJECTIVE_C_CLASS = "Monobjc.ObjectiveCClassAttribute";
		public const String OBJECTIVE_C_PROTOCOL = "Monobjc.ObjectiveCProtocolAttribute";
		public const String OBJECTIVE_C_MESSAGE = "Monobjc.ObjectiveCMessageAttribute";
		public const String OBJECTIVE_C_MESSAGE_SHORTFORM = "ObjectiveCMessage";
		public const String OBJECTIVE_C_IVAR = "Monobjc.ObjectiveCIVarAttribute";
		public const String OBJECTIVE_C_IVAR_SHORTFORM = "ObjectiveCIVar";
		public const String OBJECTIVE_C_FRAMEWORK = "Monobjc.ObjectiveCFrameworkAttribute";
		
		public const String DEVELOPER_TOOLS = "MonoDevelop.Monobjc.DeveloperTools";
		public const String XCODE_APPLICATION = "Applications/Xcode.app";
		public const String INTERFACE_BUILDER_APPLICATION = "Applications/Interface Builder.app";

		public const String IB_MIME_TYPE = "application/vnd.apple-interface-builder";

		public const String DOT_DESIGNER = ".designer";
		public const String DOT_LPROJ = ".lproj";
		public const String DOT_NIB = ".nib";
		public const String DOT_STRINGS = ".strings";
		public const String DOT_XIB = ".xib";

		public const String EncryptedContent = "EncryptedContent";
		public const String InterfaceDefinition = "InterfaceDefinition";
		public const String EmbeddedInterfaceDefinition = "EmbeddedInterfaceDefinition";

		public const String INFO_PLIST = "Info.plist";
        public const String APP_ENTITLEMENTS = "App.entitlements";
		public const String GROUP_CLASSES = "Classes";
		public const String GROUP_RESOURCES = "Resources";
		public const String GROUP_FRAMEWORKS = "Frameworks";
		public const String CONFIGURATION_RELEASE = "Release";
	}
}
