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
using System.IO;
using System.Linq;
using System.Reflection;
using Gtk;
using Monobjc.Tools.External;
using Monobjc.Tools.Utilities;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using MonoDevelop.Ide.Gui.Dialogs;
using MonoDevelop.Components;
using MonoDevelop.Components.Extensions;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the deployment options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class MonobjcDeploymentOptionsWidget : Bin
	{
		private IList<String> identities;

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcGeneralOptionsWidget" /> class.
		/// </summary>
		public MonobjcDeploymentOptionsWidget ()
		{
			this.Build ();
			
			this.identities = KeyChainAccess.SigningIdentities;
			
			this.comboboxArch.Model = new ListStore (typeof(string), typeof(MacOSArchitecture));
			this.comboboxSigningCertificates.Model = new ListStore (typeof(String), typeof(String));
			this.comboboxPackagingCertificates.Model = new ListStore (typeof(String), typeof(String));
			
			PopulateArchitectures (this.comboboxArch);
			
			PopulateCertificates (this.comboboxSigningCertificates, this.identities, GettextCatalog.GetString ("(Don't Sign Application)"));
			PopulateCertificates (this.comboboxPackagingCertificates, this.identities, GettextCatalog.GetString ("(Don't Sign Package)"));
			
			this.checkbuttonPackage.Toggled += this.HandleCheckbuttonPackagehandleToggled;
			this.HandleCheckbuttonPackagehandleToggled (this, EventArgs.Empty);
			
			CellRendererToggle checkRenderer;
			CellRendererPixbuf iconRenderer;
			CellRendererText nameRenderer;
			
			this.treeviewEmbeddedFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));

			TreeViewColumn treeviewEmbeddedFrameworksColumn = new TreeViewColumn ();
			checkRenderer = new CellRendererToggle ();
			checkRenderer.Toggled += this.HandleCheckRendererToggled;
			treeviewEmbeddedFrameworksColumn.PackStart (checkRenderer, false);
			treeviewEmbeddedFrameworksColumn.AddAttribute (checkRenderer, "active", 0);
			iconRenderer = new CellRendererPixbuf ();
			treeviewEmbeddedFrameworksColumn.PackStart (iconRenderer, false);
			treeviewEmbeddedFrameworksColumn.AddAttribute (iconRenderer, "pixbuf", 1);
			nameRenderer = new CellRendererText ();
			treeviewEmbeddedFrameworksColumn.PackStart (nameRenderer, true);
			treeviewEmbeddedFrameworksColumn.AddAttribute (nameRenderer, "text", 2);
			this.treeviewEmbeddedFrameworks.AppendColumn (treeviewEmbeddedFrameworksColumn);
			
			this.treeviewAdditionnalAssemblies.Model = new TreeStore (typeof(String));
				
			TreeViewColumn treeviewAdditionnalAssembliesColumn = new TreeViewColumn ();
			nameRenderer = new CellRendererText ();
			nameRenderer.Editable = true;
			treeviewAdditionnalAssembliesColumn.PackStart (nameRenderer, true);
			treeviewAdditionnalAssembliesColumn.AddAttribute (nameRenderer, "text", 0);
			this.treeviewAdditionnalAssemblies.AppendColumn (treeviewAdditionnalAssembliesColumn);
			
			this.buttonAddAdditionnalAssemblies.Clicked += HandleButtonAddAdditionnalAssemblieshandleClicked;
			this.buttonRemoveAdditionnalAssemblies.Clicked += HandleButtonRemoveAdditionnalAssemblieshandleClicked;
			
			this.treeviewExcludedAssemblies.Model = new TreeStore (typeof(String));
			
			TreeViewColumn treeviewExcludedAssembliesColumn = new TreeViewColumn ();
			nameRenderer = new CellRendererText ();
			nameRenderer.Editable = true;
			treeviewExcludedAssembliesColumn.PackStart (nameRenderer, true);
			treeviewExcludedAssembliesColumn.AddAttribute (nameRenderer, "text", 0);
			this.treeviewExcludedAssemblies.AppendColumn (treeviewExcludedAssembliesColumn);
			
			this.buttonAddExcludedAssemblies.Clicked += HandleButtonAddExcludedAssemblieshandleClicked;
			this.buttonRemoveExcludedAssemblies.Clicked += HandleButtonRemoveExcludedAssemblieshandleClicked;
			
			this.treeviewAdditionnalLibraries.Model = new TreeStore (typeof(String));
			
			TreeViewColumn treeviewAdditionnalLibrariesColumn = new TreeViewColumn ();
			nameRenderer = new CellRendererText ();
			nameRenderer.Editable = true;
			treeviewAdditionnalLibrariesColumn.PackStart (nameRenderer, true);
			treeviewAdditionnalLibrariesColumn.AddAttribute (nameRenderer, "text", 0);
			this.treeviewAdditionnalLibraries.AppendColumn (treeviewAdditionnalLibrariesColumn);
			
			this.buttonAddAdditionnalLibraries.Clicked += HandleButtonAddAdditionnalLibrarieshandleClicked;
			this.buttonRemoveAdditionnalLibraries.Clicked += HandleButtonRemoveAdditionnalLibrarieshandleClicked;
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
			TreeStore store;
			
			// Retrieve the target architecture
			this.TargetOSArch = project.TargetOSArch;
			
			// Retrieve the signing certificate
			this.SigningIdentity = project.SigningIdentity;

			// Retrieve the package generation
			this.Archive = project.Archive;
			
			// Retrieve the packaging certificate
			this.ArchiveIdentity = project.ArchiveIdentity;
			
			// Populate the additional frameworks
			store = (TreeStore)this.treeviewEmbeddedFrameworks.Model;
			store.Clear ();
			foreach (ProjectReference reference in project.ProjectMonobjcAssemblies) {
				FilePath location = null;
				String name = null;
				if (reference.ReferenceType == ReferenceType.Assembly) {
					location = reference.Reference;
					name = location;
				} else if (reference.ReferenceType == ReferenceType.Gac) {
					location = project.AssemblyContext.GetAssemblyLocation (reference.Reference, project.TargetFramework);
					name = location.FileNameWithoutExtension;
				}
				if (location == null) {
					continue;
				}
				if (!File.Exists (location)) {
					continue;
				}
				bool systemFramework;
				if (!AttributeHelper.IsWrappingFramework (location, out systemFramework)) {
					continue;
				}
				if (systemFramework) {
					continue;
				}
				store.AppendValues (false, ImageService.GetPixbuf ("md-monobjc-fmk", IconSize.Menu), name.Substring ("Monobjc.".Length));
			}
			
			// Retrieve the additional frameworks
			this.EmbeddedFrameworks = project.EmbeddedFrameworks;
			
			// Retrieve the additionnal assemblies
			this.AdditionalAssemblies = project.AdditionalAssemblies;
			
			// Retrieve the excluded assemblies
			this.ExcludedAssemblies = project.ExcludedAssemblies;
			
			// Retrieve the additional libraries
			this.AdditionalLibraries = project.AdditionalLibraries;			
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
			
			// Save the target architecture
			project.TargetOSArch = this.TargetOSArch;
			
			// Save the signing certificate
			project.SigningIdentity = this.SigningIdentity;
			
			// Retrieve the package generation
			project.Archive = this.Archive;

			// Save the packaging certificate
			project.ArchiveIdentity = this.ArchiveIdentity;
			
			// Save the embedded frameworks
			project.EmbeddedFrameworks = this.EmbeddedFrameworks;
			
			// Save the additionnal assemblies
			project.AdditionalAssemblies = this.AdditionalAssemblies;
			
			// Save the excluded assemblies
			project.ExcludedAssemblies = this.ExcludedAssemblies;
			
			// Save the additional libraries
			project.AdditionalLibraries = this.AdditionalLibraries;
		}

		/// <summary>
		/// Gets or sets the target OS architecture.
		/// </summary>
		private MacOSArchitecture TargetOSArch {
			get {
				TreeIter iter;
				if (this.comboboxArch.GetActiveIter (out iter)) {
					ListStore listStore = (ListStore)this.comboboxArch.Model;
					return (MacOSArchitecture)listStore.GetValue (iter, 1);
				}
				throw new InvalidOperationException ("Unrecognized Target Architecture");
			}
			set {
				ListStore store = (ListStore)this.comboboxArch.Model;
				TreeIter iter;
				store.GetIterFirst (out iter);
				do {
					if ((MacOSArchitecture)store.GetValue (iter, 1) == value) {
						this.comboboxArch.SetActiveIter (iter);
						return;
					}
					if (!store.IterNext (ref iter)) {
						break;
					}
				} while (true);
				this.comboboxArch.Active = 0;
			}
		}

		/// <summary>
		///   Gets or sets the application signing identity.
		/// </summary>
		private String SigningIdentity {
			get { return GetIdentity (this.comboboxSigningCertificates); }
			set { SetIdentity (this.comboboxSigningCertificates, value); }
		}

		/// <summary>
		///   Gets or sets the archive.
		/// </summary>
		private bool Archive {
			get { return this.checkbuttonPackage.Active; }
			set { 
				this.checkbuttonPackage.Active = value; 
				this.HandleCheckbuttonPackagehandleToggled (this, EventArgs.Empty);
			}
		}

		/// <summary>
		///   Gets or sets the archive signing identity.
		/// </summary>
		private String ArchiveIdentity {
			get { return GetIdentity (this.comboboxPackagingCertificates); }
			set { SetIdentity (this.comboboxPackagingCertificates, value); }
		}

		private static String GetIdentity (ComboBox comboBox)
		{
			TreeIter iter;
			if (comboBox.GetActiveIter (out iter)) {
				ListStore certStore = (ListStore)comboBox.Model;
				return (String)certStore.GetValue (iter, 1);
			}
			throw new InvalidOperationException ("Error while getting signing identity");
		}

		private static void SetIdentity (ComboBox comboBox, String value)
		{
			ListStore store = (ListStore)comboBox.Model;
			TreeIter iter;
			store.GetIterFirst (out iter);
			do {
				if ((String)store.GetValue (iter, 1) == value) {
					comboBox.SetActiveIter (iter);
					return;
				}
				if (!store.IterNext (ref iter)) {
					break;
				}
			} while (true);
			
			comboBox.Active = 0;
		}

		private void PopulateArchitectures (ComboBox combobox)
		{
			// Set up the architectures
			ListStore archStore = (ListStore)combobox.Model;
			archStore.Clear ();
			
			MacOSArchitecture architecture = Lipo.GetArchitecture ("/usr/bin/mono");
			if (architecture == MacOSArchitecture.None) {
				// Humm, there was an error, so add only i386
				archStore.AppendValues ("Intel i386 (32 bits)", MacOSArchitecture.X86);
			} else {
				LoggingService.LogInfo ("Detected architecture " + architecture);
			}
			
			// Retrieve some information about the developer tools
			// - Xcode 3.2 => 10.5/10.6 - ppc/i386/x86_64
			// - Xcode 4.0 => 10.6 - i386/x86_64
			// - Xcode 4.1 => 10.6/10.7 - i386/x86_64
			Version version = DeveloperToolsDesktopApplication.DeveloperToolsVersion;
			bool isXcode4 = version != null && version.Major >= 4;
			
			// Add all the detected architectures
			if ((architecture & MacOSArchitecture.X86) == MacOSArchitecture.X86) {
				archStore.AppendValues ("Intel i386 (32 bits)", MacOSArchitecture.X86);
			}
			if ((architecture & MacOSArchitecture.X8664) == MacOSArchitecture.X8664) {
				archStore.AppendValues ("Intel x86_64 (64 bits)", MacOSArchitecture.X8664);
			}
			if ((architecture & MacOSArchitecture.Intel) == MacOSArchitecture.Intel) {
				archStore.AppendValues ("Intel (32/64 bits)", MacOSArchitecture.Intel);
			}
			if (((architecture & MacOSArchitecture.PPC) == MacOSArchitecture.PPC) && !isXcode4) {
				archStore.AppendValues ("Power PC (32 bits)", MacOSArchitecture.PPC);
			}
			if (((architecture & MacOSArchitecture.Universal32) == MacOSArchitecture.Universal32) && !isXcode4) {
				archStore.AppendValues ("Universal PowerPC/Intel (32 bits)", MacOSArchitecture.Universal32);
			}
			if (((architecture & MacOSArchitecture.Universal3264) == MacOSArchitecture.Universal3264) && !isXcode4) {
				archStore.AppendValues ("Universal PowerPC/Intel (32/64 bits)", MacOSArchitecture.Universal3264);
			}
		}

		private static void PopulateCertificates (ComboBox comboBox, IList<String> identities, String defaultText)
		{
			ListStore store = (ListStore)comboBox.Model;
			store.Clear ();
			
			// Append first default value
			store.AppendValues (defaultText, String.Empty);
			
			if (identities.Count == 0) {
				return;
			}
			
			// Append each identity
			foreach (String identity in identities) {
				store.AppendValues (identity, identity);
			}
			
			// Select the first one
			TreeIter iter;
			if (store.GetIterFirst (out iter)) {
				comboBox.SetActiveIter (iter);
			}
		}
		
		private void HandleCheckbuttonPackagehandleToggled (object sender, EventArgs e)
		{
			this.comboboxPackagingCertificates.Sensitive = this.checkbuttonPackage.Active;
		}

		/// <summary>
		/// Gets or sets the embedded frameworks.
		/// </summary>
		private String EmbeddedFrameworks {
			get {
				IList<String > frameworks = new List<string> ();
				TreeStore store = (TreeStore)this.treeviewEmbeddedFrameworks.Model;
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
				string[] frameworks = (value ?? "").Split (' ', ',', ';');
				TreeStore store = (TreeStore)this.treeviewEmbeddedFrameworks.Model;
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
		
		private void HandleCheckRendererToggled (object o, ToggledArgs args)
		{
			TreeStore store = (TreeStore)this.treeviewEmbeddedFrameworks.Model;
			TreeIter iter;
			if (!store.GetIterFromString (out iter, args.Path)) {
				return;
			}
			bool value = (bool)store.GetValue (iter, 0);
			store.SetValue (iter, 0, !value);
		}
		
		/// <summary>
		/// Gets or sets the additional assemblies.
		/// </summary>
		private String AdditionalAssemblies {
			get {
				return this.ExtractFromModel (this.treeviewAdditionnalAssemblies.Model);
			}
			set {
				this.InjectIntoModel (this.treeviewAdditionnalAssemblies.Model, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the additional assemblies.
		/// </summary>
		private String ExcludedAssemblies {
			get {
				return this.ExtractFromModel (this.treeviewExcludedAssemblies.Model);
			}
			set {
				this.InjectIntoModel (this.treeviewExcludedAssemblies.Model, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the additional assemblies.
		/// </summary>
		private String AdditionalLibraries {
			get {
				return this.ExtractFromModel (this.treeviewAdditionnalLibraries.Model);
			}
			set {
				this.InjectIntoModel (this.treeviewAdditionnalLibraries.Model, value);
			}
		}
		
		private String ExtractFromModel (TreeModel model)
		{
			TreeStore store = (TreeStore)model;
			TreeIter iter;
			if (!store.GetIterFirst (out iter)) {
				return String.Empty;
			}
			IList<String > parts = new List<String> ();
			do {
				String part = (String)store.GetValue (iter, 0);
				parts.Add (part);
				if (!store.IterNext (ref iter)) {
					break;
				}
			} while (true);
			return String.Join (":", parts.ToArray ());
		}
		
		private String InjectIntoModel (TreeModel model, String value)
		{
			TreeStore store = (TreeStore)model;
			store.Clear ();
			if (value != null) {
				String[] parts = value.Split (new []{':'}, StringSplitOptions.RemoveEmptyEntries);
				foreach (String part in parts) {
					store.AppendValues (part);
				}
			}
			return String.Empty;
		}
		
		private void HandleButtonAddAdditionnalAssemblieshandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewAdditionnalAssemblies, "myassembly.dll");
		}

		private void HandleButtonRemoveAdditionnalAssemblieshandleClicked (object sender, EventArgs e)
		{
			Remove(this.treeviewAdditionnalAssemblies);
		}

		private void HandleButtonAddExcludedAssemblieshandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewExcludedAssemblies, "myassembly.dll");
		}

		private void HandleButtonRemoveExcludedAssemblieshandleClicked (object sender, EventArgs e)
		{
			Remove(this.treeviewExcludedAssemblies);
		}
		
		private void HandleButtonAddAdditionnalLibrarieshandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewAdditionnalLibraries, "mylib.dylib");
		}

		void HandleButtonRemoveAdditionnalLibrarieshandleClicked (object sender, EventArgs e)
		{
			Remove(this.treeviewAdditionnalLibraries);
		}
		
		private static void AddEmptyItem(TreeView treeView, String defaultValue) {
			TreeStore store = (TreeStore)treeView.Model;
			store.AppendValues(defaultValue);
		}
		
		private static void RemoveItem(TreeView treeView) {
			TreeIter iter;
			if (!treeView.Selection.GetSelectedRows (out iter)) {
				LoggingService.LogInfo("No selection !!!");
				return;
			}
			
			TreeStore store = (TreeStore)treeView.Model;
			store.Remove (ref iter);
		}
	}
}
