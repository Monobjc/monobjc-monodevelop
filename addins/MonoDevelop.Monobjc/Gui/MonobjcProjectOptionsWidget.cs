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
using System.ComponentModel;
using Gtk;
using Monobjc.Tools.Utilities;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the project options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class MonobjcProjectOptionsWidget : Bin
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Gui.MonobjcProjectOptionsWidget"/> class.
		/// </summary>
		public MonobjcProjectOptionsWidget ()
		{
			this.Build ();

			IList<String> identities = KeyChainAccess.SigningIdentities;

			// General tab
			this.comboboxType.Model = new ListStore (typeof(string), typeof(MonobjcApplicationType));
			this.comboboxApplicationCategory.Model = new ListStore (typeof(string), typeof(string));
			this.comboboxOSVersion.Model = new ListStore (typeof(string), typeof(MacOSVersion));
			this.comboboxSigningCertificates.Model = new ListStore (typeof(String), typeof(String));
			this.treeviewFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));

			FillTypes(this.comboboxType);
			FillApplicationCategories(this.comboboxApplicationCategory);
			FillMacOSVersion(this.comboboxOSVersion);
			FillCertificates(this.comboboxSigningCertificates, identities, "(None)");

			// Embedding tab
			this.comboboxArchitectures.Model = new ListStore (typeof(string), typeof(MacOSArchitecture));
			FillArchitectures(this.comboboxArchitectures);

			// Packaging tab
			this.comboboxPackagingCertificates.Model = new ListStore (typeof(String), typeof(String));
			FillCertificates(this.comboboxPackagingCertificates, identities, "(None)");

			// Advanced tab
			this.comboboxDevelopmentRegion.Model = new ListStore (typeof(string), typeof(String));

		}

		/// <summary>
		///   Loads the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Load (MonobjcProject project)
		{
		}
		
		/// <summary>
		///   Saves the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Save (MonobjcProject project)
		{
		}
	}
}
