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
using System.Reflection;
using Gtk;
using Monobjc.Tools.Utilities;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;

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
			
			// Retrieve the target architecture
			this.TargetOSArch = project.TargetOSArch;
			
			// Retrieve the signing certificate
			this.SigningIdentity = project.SigningIdentity;
			
			// Retrieve the packaging certificate
			this.ArchiveIdentity = project.ArchiveIdentity;
			
			// Retrieve the additionnal assemblies
			
			// Retrieve the excluded assemblies
			
			// Retrieve the additional frameworks
			
			// Retrieve the additional libraries
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
			
			// Save the packaging certificate
			project.ArchiveIdentity = this.ArchiveIdentity;
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
			archStore.AppendValues ("Intel i386 (32 bits)", MacOSArchitecture.X86);
			archStore.AppendValues ("Power PC (32 bits)", MacOSArchitecture.PPC);
			archStore.AppendValues ("Universal PowerPC/Intel (32 bits)", MacOSArchitecture.Universal32);
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
	}
}
