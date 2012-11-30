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
using MonoDevelop.Ide;
using MonoDevelop.Core;
using System.IO;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the general options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class MonobjcGeneralOptionsWidget : Bin
	{
		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcGeneralOptionsWidget" /> class.
		/// </summary>
		public MonobjcGeneralOptionsWidget ()
		{
			this.Build ();
			
			this.filechooserbuttonMainNib.SelectionChanged += this.HandleFilechooserbuttonMainNibhandleSelectionChanged;
			this.filechooserbuttonBundleIcon.SelectionChanged += this.HandleFilechooserbuttonBundleIconhandleSelectionChanged;
			this.comboboxVersion.Changed += this.HandleComboboxVersionhandleChanged;
			
			this.comboboxType.Model = new ListStore (typeof(string), typeof(MonobjcApplicationType));
			this.comboboxRegion.Model = new ListStore (typeof(string), typeof(String));
			this.comboboxVersion.Model = new ListStore (typeof(string), typeof(MacOSVersion));
			this.treeviewFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));
			
			TreeViewColumn column = new TreeViewColumn ();
			
			CellRendererToggle checkRenderer = new CellRendererToggle ();
			checkRenderer.Toggled += this.HandleCheckRendererToggled;
			column.PackStart (checkRenderer, false);
			column.AddAttribute (checkRenderer, "active", 0);
			
			CellRendererPixbuf iconRenderer = new CellRendererPixbuf ();
			column.PackStart (iconRenderer, false);
			column.AddAttribute (iconRenderer, "pixbuf", 1);
			
			CellRendererText nameRenderer = new CellRendererText ();
			column.PackStart (nameRenderer, true);
			column.AddAttribute (nameRenderer, "text", 2);
			
			this.treeviewFrameworks.AppendColumn (column);
		}

		/// <summary>
		///   Loads the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Load (MonobjcProject project)
		{
			if (project == null) {
				throw new ArgumentNullException ("project");
			}
			
			// Load the application type
			ListStore typeStore = (ListStore)this.comboboxType.Model;
			typeStore.Clear ();
			typeStore.AppendValues (GettextCatalog.GetString ("Cocoa Application"), MonobjcApplicationType.CocoaApplication);
			typeStore.AppendValues (GettextCatalog.GetString ("Console Application"), MonobjcApplicationType.ConsoleApplication);
			typeStore.AppendValues (GettextCatalog.GetString ("Cocoa Library"), MonobjcApplicationType.CocoaLibrary);
			this.ApplicationType = project.ApplicationType;
			
			// Load the development languages
			ListStore regionStore = (ListStore)this.comboboxRegion.Model;
			regionStore.Clear ();
			FilePath projectDir = project.BaseDirectory;
			String[] folders = Directory.GetDirectories(projectDir, "*.lproj");
			if (folders.Length == 0) {
				regionStore.AppendValues (GettextCatalog.GetString ("en"), "en");
			} else {
				foreach(String folder in folders) {
					if (!Directory.Exists(folder)) {
						continue;
					}
					String language = System.IO.Path.GetFileNameWithoutExtension(folder);
					regionStore.AppendValues (GettextCatalog.GetString (language), language);
				}
			}
			this.DevelopmentRegion = project.DevelopmentRegion;
			
			// Retrieve some information about the developer tools
			// - Xcode 3.2 => 10.5/10.6 - ppc/i386/x86_64
			// - Xcode 4.0 => 10.6 - i386/x86_64
			// - Xcode 4.1 => 10.6/10.7 - i386/x86_64
			//Version version = DeveloperToolsDesktopApplication.DeveloperToolsVersion;
			//bool isXcode4 = version != null && version.Major >= 4;
			//bool isXcode41 = isXcode4 && version.Minor >= 1;
			
			// Set up the SDKs
			ListStore versionStore = (ListStore)this.comboboxVersion.Model;
			versionStore.Clear ();
			versionStore.AppendValues ("Mac OS X 10.5", MacOSVersion.MacOS105);
			versionStore.AppendValues ("Mac OS X 10.6", MacOSVersion.MacOS106);
			versionStore.AppendValues ("Mac OS X 10.7", MacOSVersion.MacOS107);
			
			// Set the base folder and retrieve the main NIB file
			this.filechooserbuttonMainNib.SetCurrentFolder (project.BaseDirectory.ToString ());
			String mainNibFile = project.MainNibFile.ToString () ?? project.BaseDirectory.Combine ("en.lproj", "MainMenu.xib");
			this.filechooserbuttonMainNib.SetFilename (mainNibFile);
			
			// Set the base folder and retrieve the bundle icon
			this.filechooserbuttonBundleIcon.SetCurrentFolder (project.BaseDirectory.ToString ());
			String bundleIcon = project.BundleIcon.ToString () ?? project.BaseDirectory.Combine ("Monobjc.icns");
			this.filechooserbuttonBundleIcon.SetFilename (bundleIcon);
			
			// Retrieve the target version
			this.TargetOSVersion = project.TargetOSVersion;
			
			// Set the framework list
			TreeStore frameworkStore = (TreeStore)this.treeviewFrameworks.Model;
			frameworkStore.Clear ();
			IEnumerable<String> assemblies = (from a in project.EveryMonobjcAssemblies
				where a.Name.Contains ("Monobjc.")
				select a.Name.Substring ("Monobjc.".Length)).Distinct ();
			foreach (String assembly in assemblies) {
				frameworkStore.AppendValues (false, ImageService.GetPixbuf ("md-monobjc-fmk", IconSize.Menu), assembly);
			}
			
			// Load the selected frameworks
			this.OSFrameworks = project.OSFrameworks;
		}

		/// <summary>
		///   Saves the specified project.
		/// </summary>
		/// <param name = "project">The project.</param>
		public void Save (MonobjcProject project)
		{
			if (project == null) {
				throw new ArgumentNullException ("project");
			}
			
			project.ApplicationType = this.ApplicationType;
			project.DevelopmentRegion = this.DevelopmentRegion;
			project.MainNibFile = this.filechooserbuttonMainNib.Filename;
			project.BundleIcon = this.filechooserbuttonBundleIcon.Filename;
			project.TargetOSVersion = this.TargetOSVersion;
			project.OSFrameworks = this.OSFrameworks;
			
			project.UpdateReferences ();
		}

		/// <summary>
		/// Gets or sets the application type.
		/// </summary>
		private MonobjcApplicationType ApplicationType {
			get {
				TreeIter iter;
				if (this.comboboxType.GetActiveIter (out iter)) {
					ListStore listStore = (ListStore)this.comboboxType.Model;
					return (MonobjcApplicationType)listStore.GetValue (iter, 1);
				}
				throw new InvalidOperationException ("Unrecognized Application type");
			}
			set {
				ListStore store = (ListStore)this.comboboxType.Model;
				TreeIter iter;
				store.GetIterFirst (out iter);
				do {
					if ((MonobjcApplicationType)store.GetValue (iter, 1) == value) {
						this.comboboxType.SetActiveIter (iter);
						return;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				this.comboboxType.Active = 0;
			}
		}

		/// <summary>
		/// Gets or sets the development region.
		/// </summary>
		private string DevelopmentRegion {
			get {
				TreeIter iter;
				if (this.comboboxRegion.GetActiveIter (out iter)) {
					ListStore listStore = (ListStore)this.comboboxRegion.Model;
					return (string)listStore.GetValue (iter, 1);
				}
				throw new InvalidOperationException ("Unrecognized Application type");
			}
			set {
				ListStore store = (ListStore)this.comboboxRegion.Model;
				TreeIter iter;
				store.GetIterFirst (out iter);
				do {
					if ((string)store.GetValue (iter, 1) == value) {
						this.comboboxRegion.SetActiveIter (iter);
						return;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				this.comboboxRegion.Active = 0;
			}
		}

		/// <summary>
		/// Gets or sets the target OS version.
		/// </summary>
		private MacOSVersion TargetOSVersion {
			get {
				TreeIter iter;
				if (this.comboboxVersion.GetActiveIter (out iter)) {
					ListStore listStore = (ListStore)this.comboboxVersion.Model;
					return (MacOSVersion)listStore.GetValue (iter, 1);
				}
				throw new InvalidOperationException ("Unrecognized Target Version");
			}
			set {
				ListStore store = (ListStore)this.comboboxVersion.Model;
				TreeIter iter;
				store.GetIterFirst (out iter);
				do {
					if ((MacOSVersion)store.GetValue (iter, 1) == value) {
						this.comboboxVersion.SetActiveIter (iter);
						return;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				this.comboboxVersion.Active = 0;
			}
		}

		/// <summary>
		/// Gets or sets the OS frameworks.
		/// </summary>
		private String OSFrameworks {
			get {
				IList<String> frameworks = new List<string> ();
				TreeStore store = (TreeStore)this.treeviewFrameworks.Model;
				TreeIter iter;
				if (!store.GetIterFirst (out iter)) {
					return null;
				}
				do {
					String framework = (String)store.GetValue (iter, 2);
					bool state = (bool)store.GetValue (iter, 0);
					if (state) {
						frameworks.Add (framework);
					}
					
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				
				return String.Join (";", frameworks.ToArray ());
			}
			set {
				string[] frameworks = (value ?? "Foundation;AppKit").Split (' ', ',', ';');
				TreeStore store = (TreeStore)this.treeviewFrameworks.Model;
				TreeIter iter;
				if (!store.GetIterFirst (out iter)) {
					return;
				}
				do {
					String framework = (String)store.GetValue (iter, 2);
					bool state = frameworks.Contains (framework);
					store.SetValue (iter, 0, state);
					
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
			}
		}

		private void HandleFilechooserbuttonMainNibhandleSelectionChanged (object sender, EventArgs e)
		{
			// TODO
		}

		private void HandleFilechooserbuttonBundleIconhandleSelectionChanged (object sender, EventArgs e)
		{
			// TODO
		}

		private void HandleComboboxVersionhandleChanged (object sender, EventArgs e)
		{
			switch (this.ApplicationType) {
			case MonobjcApplicationType.CocoaApplication:
				this.filechooserbuttonMainNib.Sensitive = true;
				this.filechooserbuttonBundleIcon.Sensitive = true;
				break;
			case MonobjcApplicationType.ConsoleApplication:
				this.filechooserbuttonMainNib.Sensitive = false;
				this.filechooserbuttonBundleIcon.Sensitive = false;
				break;
			case MonobjcApplicationType.CocoaLibrary:
				this.filechooserbuttonMainNib.Sensitive = false;
				this.filechooserbuttonBundleIcon.Sensitive = false;
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + this.ApplicationType);
			}			
		}

		private void HandleCheckRendererToggled (object o, ToggledArgs args)
		{
			TreeStore store = (TreeStore)this.treeviewFrameworks.Model;
			TreeIter iter;
			if (!store.GetIterFromString (out iter, args.Path)) {
				return;
			}
			bool value = (bool)store.GetValue (iter, 0);
			store.SetValue (iter, 0, !value);
		}
	}
}
