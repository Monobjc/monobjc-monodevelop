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
using MonoDevelop.Core;

namespace MonoDevelop.Monobjc.Gui
{
	/// <summary>
	///   Widget that provides the project options panel.
	/// </summary>
	[ToolboxItem(true)]
	public partial class ProjectOptionsWidget : Bin
	{
		private MonobjcProject project;

		/// <summary>
		/// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Gui.MonobjcProjectOptionsWidget"/> class.
		/// </summary>
		public ProjectOptionsWidget ()
		{
			TreeViewColumn[] columns;

			this.Build ();

			IList<String> identities = KeyChainAccess.SigningIdentities;

			this.comboboxType.Model = new ListStore (typeof(string), typeof(MonobjcProjectType));
			this.comboboxApplicationCategory.Model = new ListStore (typeof(string), typeof(string));
			this.comboboxOSVersion.Model = new ListStore (typeof(string), typeof(MacOSVersion));
			this.comboboxSigningCertificates.Model = new ListStore (typeof(String), typeof(String));
			this.treeviewFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));
			columns = GetFrameworkTableColumns(this.HandleFrameworksCheckRendererToggled);
			this.treeviewFrameworks.AppendColumn(columns[0]);
			this.treeviewFrameworks.AppendColumn(columns[1]);

			this.comboboxArchitectures.Model = new ListStore (typeof(string), typeof(MacOSArchitecture));
			this.treeviewEmbeddedFrameworks.Model = new TreeStore (typeof(bool), typeof(Gdk.Pixbuf), typeof(String));
			columns = GetFrameworkTableColumns(this.HandleEmbeddedFrameworksCheckRendererToggled);
			this.treeviewEmbeddedFrameworks.AppendColumn(columns[0]);
			this.treeviewEmbeddedFrameworks.AppendColumn(columns[1]);
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
		public void Load (MonobjcProject project)
		{
			if (project == null) {
				throw new ArgumentNullException ("project");
			}
			this.project = project;

			this.filechooserbuttonMainNib.SetCurrentFolder (project.BaseDirectory.ToString ());
			this.filechooserbuttonBundleIcon.SetCurrentFolder (project.BaseDirectory.ToString ());

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
			FillFrameworks(this.treeviewFrameworks, project, project.TargetOSVersion);
			this.OSFrameworks = project.OSFrameworks;

			this.TargetOSArch = project.TargetOSArch;
			FillEmbeddedFrameworks(this.treeviewEmbeddedFrameworks, project);
			this.EmbeddedFrameworks = project.EmbeddedFrameworks;
			this.AdditionalAssemblies = project.AdditionalAssemblies;
			this.ExcludedAssemblies = project.ExcludedAssemblies;
			this.AdditionalLibraries = project.AdditionalLibraries;

			this.Archive = project.Archive;
			this.ArchiveIdentity = project.ArchiveIdentity ?? String.Empty;

			FillDevelopmentRegions(this.comboboxDevelopmentRegion, project);
			this.DevelopmentRegion = project.DevelopmentRegion ?? "en";
			this.CombineArtwork = project.CombineArtwork;
			this.EncryptionSeed = project.EncryptionSeed;
		}

		/// <summary>
		/// Determines whether the preferences can be saved.
		/// </summary>
		public bool CanSave(MonobjcProject project, out String message) {
			if (this.entryBundleIdentifier.Text.Length == 0) {
				message = GettextCatalog.GetString("The value for the bundle identifier is not valid. It cannot be empty.");
				return false;
			}
			if (this.entryBundleVersion.Text.Length == 0) {
				message = GettextCatalog.GetString("The value for the bundle version is not valid. It cannot be empty.");
				return false;
			}
			message = String.Empty;
			return true;
		}

		/// <summary>
		///   Saves the specified project.
		/// </summary>
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
			project.EncryptionSeed = this.EncryptionSeed;

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

			this.comboboxApplicationCategory.Sensitive = value1;
			this.entryBundleIdentifier.Sensitive = value1;
			this.entryBundleVersion.Sensitive = value1;
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
			String frameworks = this.OSFrameworks;
			MacOSVersion version = this.TargetOSVersion;
			FillFrameworks(this.treeviewFrameworks, this.project, version);
			this.OSFrameworks = frameworks;
		}

		private void HandleFrameworksCheckRendererToggled (object o, ToggledArgs args)
		{
			TreeStore store = (TreeStore)this.treeviewFrameworks.Model;
			TreeIter iter;
			if (!store.GetIterFromString (out iter, args.Path)) {
				return;
			}
			bool value = (bool)store.GetValue (iter, 0);
			store.SetValue (iter, 0, !value);
		}
		
		private void HandleEmbeddedFrameworksCheckRendererToggled (object o, ToggledArgs args)
		{
			TreeStore store = (TreeStore)this.treeviewEmbeddedFrameworks.Model;
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
