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
	public partial class ProjectOptionsWidget : Bin
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Gui.MonobjcProjectOptionsWidget"/> class.
		/// </summary>
		public ProjectOptionsWidget ()
		{
			this.Build ();

			IList<String> identities = KeyChainAccess.SigningIdentities;

			this.comboboxType.Model = new ListStore (typeof(string), typeof(MonobjcProjectType));
			this.comboboxApplicationCategory.Model = new ListStore (typeof(string), typeof(string));
			this.comboboxOSVersion.Model = new ListStore (typeof(string), typeof(MacOSVersion));
			this.comboboxSigningCertificates.Model = new ListStore (typeof(String), typeof(String));
			this.treeviewFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));
			this.treeviewFrameworks.AppendColumn(GetFrameworkTableColumn(this.HandleCheckRendererToggled));

			this.comboboxArchitectures.Model = new ListStore (typeof(string), typeof(MacOSArchitecture));
			this.treeviewEmbeddedFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));
			this.treeviewEmbeddedFrameworks.AppendColumn(GetFrameworkTableColumn(this.HandleCheckRendererToggled));
			this.treeviewAdditionnalAssemblies.Model = new TreeStore (typeof(String));
			this.treeviewAdditionnalAssemblies.AppendColumn (GetListTableColumn(this.HandleAdditionnalAssembliesEdited));
			this.treeviewExcludedAssemblies.Model = new TreeStore (typeof(String));
			this.treeviewExcludedAssemblies.AppendColumn (GetListTableColumn(this.HandleExcludedAssembliesEdited));
			this.treeviewAdditionnalLibraries.Model = new TreeStore (typeof(String));
			this.treeviewAdditionnalLibraries.AppendColumn (GetListTableColumn(this.HandleAdditionnalLibrariesEdited));

			this.comboboxPackagingCertificates.Model = new ListStore (typeof(String), typeof(String));

			this.comboboxDevelopmentRegion.Model = new ListStore (typeof(string), typeof(String));

			this.comboboxType.Changed += this.HandleComboboxTypeChanged;
			this.comboboxOSVersion.Changed += this.HandleComboboxOSVersionChanged;
			this.checkbuttonSigning.Toggled += this.HandleCheckbuttonSigningToggled;
			this.checkbuttonArchivePackage.Toggled += this.HandleCheckbuttonArchivePackageToggled;

			this.buttonAddAdditionnalAssemblies.Clicked += HandleButtonAddAdditionnalAssembliesHandleClicked;
			this.buttonRemoveAdditionnalAssemblies.Clicked += HandleButtonRemoveAdditionnalAssembliesHandleClicked;
			this.buttonAddExcludedAssemblies.Clicked += HandleButtonAddExcludedAssembliesHandleClicked;
			this.buttonRemoveExcludedAssemblies.Clicked += HandleButtonRemoveExcludedAssembliesHandleClicked;
			this.buttonAddAdditionnalLibraries.Clicked += HandleButtonAddAdditionnalLibrariesHandleClicked;
			this.buttonRemoveAdditionnalLibraries.Clicked += HandleButtonRemoveAdditionnalLibrariesHandleClicked;

			this.HandleCheckbuttonSigningToggled(this, EventArgs.Empty);
			this.HandleCheckbuttonArchivePackageToggled(this, EventArgs.Empty);

			FillTypes (this.comboboxType);
			FillApplicationCategories (this.comboboxApplicationCategory);
			FillMacOSVersion (this.comboboxOSVersion);
			FillCertificates (this.comboboxSigningCertificates, identities, "(None)");
			FillArchitectures (this.comboboxArchitectures);
			FillCertificates (this.comboboxPackagingCertificates, identities, "(None)");
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

			this.filechooserbuttonMainNib.SetCurrentFolder (project.BaseDirectory.ToString ());
			this.filechooserbuttonBundleIcon.SetCurrentFolder (project.BaseDirectory.ToString ());

			FillFrameworks(this.treeviewFrameworks, project);
			FillDevelopmentRegions(this.comboboxDevelopmentRegion, project);

			this.ApplicationType = project.ApplicationType;
			this.ApplicationCategory = project.ApplicationCategory ?? String.Empty;
			this.BundleId = project.BundleId;
			this.BundleVersion = project.BundleVersion;
			this.TargetOSVersion = project.TargetOSVersion;
			this.MainNibFile = project.MainNibFile.ToString () ?? project.BaseDirectory.Combine ("en.lproj", "MainMenu.xib");
			this.BundleIcon = project.BundleIcon.ToString () ?? project.BaseDirectory.Combine ("Monobjc.icns");
			this.Signing = project.Signing;
			this.SigningIdentity = project.SigningIdentity ?? String.Empty;
			this.UseEntitlements = project.UseEntitlements;
			this.OSFrameworks = project.OSFrameworks;

			this.TargetOSArch = project.TargetOSArch;
			this.EmbeddedFrameworks = project.EmbeddedFrameworks;
			this.AdditionalAssemblies = project.AdditionalAssemblies;
			this.ExcludedAssemblies = project.ExcludedAssemblies;
			this.AdditionalLibraries = project.AdditionalLibraries;

			this.Archive = project.Archive;
			this.ArchiveIdentity = project.ArchiveIdentity ?? String.Empty;

			this.DevelopmentRegion = project.DevelopmentRegion ?? "en";
			this.CombineArtwork = project.CombineArtwork;
			this.EncryptArtwork = project.EncryptArtwork;
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
			project.ApplicationCategory = this.ApplicationCategory ?? String.Empty;
			project.BundleId = this.BundleId;
			project.BundleVersion = this.BundleVersion;
			project.TargetOSVersion = this.TargetOSVersion;
			project.MainNibFile = this.MainNibFile;
			project.BundleIcon = this.BundleIcon;
			project.Signing = this.Signing;
			project.SigningIdentity = this.SigningIdentity ?? String.Empty;
			project.UseEntitlements = this.UseEntitlements;
			project.OSFrameworks = this.OSFrameworks;
			
			project.TargetOSArch = this.TargetOSArch;
			project.EmbeddedFrameworks = this.EmbeddedFrameworks;
			project.AdditionalAssemblies = this.AdditionalAssemblies;
			project.ExcludedAssemblies = this.ExcludedAssemblies;
			project.AdditionalLibraries = this.AdditionalLibraries;
			
			project.Archive = this.Archive;
			project.ArchiveIdentity = this.ArchiveIdentity;
			
			project.DevelopmentRegion = this.DevelopmentRegion;
			project.CombineArtwork = this.CombineArtwork;
			project.EncryptArtwork = this.EncryptArtwork;

			project.UpdateReferences();
		}

		void HandleComboboxTypeChanged (object sender, EventArgs e)
		{
			bool value1 = false;

			switch (this.ApplicationType) {
			case MonobjcProjectType.CocoaApplication:
				value1 = true;
				break;
			case MonobjcProjectType.ConsoleApplication:
				value1 = false;
				break;
			case MonobjcProjectType.CocoaLibrary:
				value1 = false;
				break;
			default:
				throw new NotSupportedException ("Unsupported application type " + this.ApplicationType);
			}		

			this.filechooserbuttonMainNib.Sensitive = value1;
			this.filechooserbuttonBundleIcon.Sensitive = value1;
			this.checkbuttonSigning.Sensitive = value1;
			this.comboboxSigningCertificates.Sensitive = value1;
			this.checkbuttonEntitlements.Sensitive = value1;
			this.tableEmbedding.Sensitive = value1;
			this.tableArchiving.Sensitive = value1;
		}
		
		void HandleComboboxOSVersionChanged (object sender, EventArgs e)
		{
			// Retrieve all the Monobjc assemblies for the given OS version

			// Re-select project frameworks
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

		void HandleCheckbuttonSigningToggled (object sender, EventArgs e)
		{
			this.comboboxSigningCertificates.Sensitive = this.checkbuttonSigning.Active;
		}

		private void HandleAdditionnalAssembliesEdited (object o, EditedArgs args)
		{
			EditItem(this.treeviewAdditionnalAssemblies, args);
		}
		
		private void HandleButtonAddAdditionnalAssembliesHandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewAdditionnalAssemblies, "myassembly.dll");
		}
		
		private void HandleButtonRemoveAdditionnalAssembliesHandleClicked (object sender, EventArgs e)
		{
			RemoveItem(this.treeviewAdditionnalAssemblies);
		}
		
		private void HandleExcludedAssembliesEdited (object o, EditedArgs args)
		{
			EditItem(this.treeviewExcludedAssemblies, args);
		}
		
		private void HandleButtonAddExcludedAssembliesHandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewExcludedAssemblies, "myassembly.dll");
		}
		
		private void HandleButtonRemoveExcludedAssembliesHandleClicked (object sender, EventArgs e)
		{
			RemoveItem(this.treeviewExcludedAssemblies);
		}
		
		private void HandleAdditionnalLibrariesEdited (object o, EditedArgs args)
		{
			EditItem(this.treeviewAdditionnalLibraries, args);
		}
		
		private void HandleButtonAddAdditionnalLibrariesHandleClicked (object sender, EventArgs e)
		{
			AddEmptyItem(this.treeviewAdditionnalLibraries, "mylib.dylib");
		}
		
		void HandleButtonRemoveAdditionnalLibrariesHandleClicked (object sender, EventArgs e)
		{
			RemoveItem(this.treeviewAdditionnalLibraries);
		}
		
		void HandleCheckbuttonArchivePackageToggled (object sender, EventArgs e)
		{
			this.comboboxPackagingCertificates.Sensitive = this.checkbuttonArchivePackage.Active;
		}
	}
}
