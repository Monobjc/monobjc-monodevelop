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
using System.Linq;
using Gtk;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Ide;
using MonoDevelop.Core;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the general options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class MonobjcPreferencesWidget : Bin
	{
		/// <summary>
        ///   Initializes a new instance of the <see cref = "MonobjcPreferencesWidget" /> class.
		/// </summary>
        public MonobjcPreferencesWidget()
		{
			this.Build ();

            this.comboboxXcodeVersion.Model = new ListStore(typeof(String), typeof(int));
		}

		/// <summary>
		///   Loads the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Load ()
		{
            // Set up the SDKs
            ListStore versionStore = (ListStore)this.comboboxXcodeVersion.Model;
            versionStore.Clear();
            versionStore.AppendValues("Xcode 3.2", 320);
            versionStore.AppendValues("Xcode 4.0", 400);
			
			// Load values
			this.filechooserbuttonDeveloperTools.SetFilename(PropertyService.Get<String>(Tracking.XcodeProjectTracker.DEVELOPER_TOOLS_ROOT, "/Developer"));
			this.XcodeVersion = PropertyService.Get<int>(Tracking.XcodeProjectTracker.XCODE_VERSION, 320);
        }

		/// <summary>
		///   Saves the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Save ()
		{
			// Save value
			PropertyService.Set(Tracking.XcodeProjectTracker.DEVELOPER_TOOLS_ROOT, this.filechooserbuttonDeveloperTools.Filename);
			PropertyService.Set(Tracking.XcodeProjectTracker.XCODE_VERSION, this.XcodeVersion);
		}
		
		private int XcodeVersion {
			get {
				TreeIter iter;
				if (this.comboboxXcodeVersion.GetActiveIter (out iter)) {
					ListStore listStore = (ListStore)this.comboboxXcodeVersion.Model;
					return (int)listStore.GetValue (iter, 1);
				}
				throw new InvalidOperationException ("Unrecognized Target Version");
			}
			set {
				ListStore store = (ListStore)this.comboboxXcodeVersion.Model;
				TreeIter iter;
				store.GetIterFirst (out iter);
				do {
					if ((int)store.GetValue (iter, 1) == value) {
						this.comboboxXcodeVersion.SetActiveIter (iter);
						return;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				this.comboboxXcodeVersion.Active = 0;
			}
		}
	}
}
