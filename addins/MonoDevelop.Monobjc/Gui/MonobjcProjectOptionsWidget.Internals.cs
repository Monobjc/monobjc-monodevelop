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
using System.Linq;
using Gtk;
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using Monobjc.Tools.External;
using System.IO;

namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcProjectOptionsWidget
	{
		private static void FillTypes (ComboBox combobox)
		{
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();
			store.AppendValues (GettextCatalog.GetString ("Cocoa Application"), MonobjcProjectType.CocoaApplication);
			store.AppendValues (GettextCatalog.GetString ("Console Application"), MonobjcProjectType.ConsoleApplication);
			store.AppendValues (GettextCatalog.GetString ("Cocoa Library"), MonobjcProjectType.CocoaLibrary);
		}
		
		private static void FillApplicationCategories (ComboBox combobox)
		{
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();

			store.AppendValues (GettextCatalog.GetString ("(None)"), "");

			store.AppendValues (GettextCatalog.GetString ("Business"), "public.app-category.business");
			store.AppendValues (GettextCatalog.GetString ("Developer Tools"), "public.app-category.developer-tools");
			store.AppendValues (GettextCatalog.GetString ("Education"), "public.app-category.education");
			store.AppendValues (GettextCatalog.GetString ("Entertainment"), "public.app-category.entertainment");
			store.AppendValues (GettextCatalog.GetString ("Finance"), "public.app-category.finance");
			store.AppendValues (GettextCatalog.GetString ("Games"), "public.app-category.games");
			store.AppendValues (GettextCatalog.GetString ("Graphics & Design"), "public.app-category.graphics-design");
			store.AppendValues (GettextCatalog.GetString ("Healthcare & Fitness"), "public.app-category.healthcare-fitness");
			store.AppendValues (GettextCatalog.GetString ("Lifestyle"), "public.app-category.lifestyle");
			store.AppendValues (GettextCatalog.GetString ("Medical"), "public.app-category.medical");
			store.AppendValues (GettextCatalog.GetString ("Music"), "public.app-category.music");
			store.AppendValues (GettextCatalog.GetString ("News"), "public.app-category.news");
			store.AppendValues (GettextCatalog.GetString ("Photography"), "public.app-category.photography");
			store.AppendValues (GettextCatalog.GetString ("Productivity"), "public.app-category.productivity");
			store.AppendValues (GettextCatalog.GetString ("Reference"), "public.app-category.reference");
			store.AppendValues (GettextCatalog.GetString ("Social Networking"), "public.app-category.social-networking");
			store.AppendValues (GettextCatalog.GetString ("Sports"), "public.app-category.sports");
			store.AppendValues (GettextCatalog.GetString ("Travel"), "public.app-category.travel");
			store.AppendValues (GettextCatalog.GetString ("Utilities"), "public.app-category.utilities");
			store.AppendValues (GettextCatalog.GetString ("Video"), "public.app-category.video");
			store.AppendValues (GettextCatalog.GetString ("Weather"), "public.app-category.weather");
			
			store.AppendValues (GettextCatalog.GetString ("Action Games"), "public.app-category.action-games");
			store.AppendValues (GettextCatalog.GetString ("Adventure Games"), "public.app-category.adventure-games");
			store.AppendValues (GettextCatalog.GetString ("Arcade Games"), "public.app-category.arcade-games");
			store.AppendValues (GettextCatalog.GetString ("Board Games"), "public.app-category.board-games");
			store.AppendValues (GettextCatalog.GetString ("Card Games"), "public.app-category.card-games");
			store.AppendValues (GettextCatalog.GetString ("Casino Games"), "public.app-category.casino-games");
			store.AppendValues (GettextCatalog.GetString ("Dice Games"), "public.app-category.dice-games");
			store.AppendValues (GettextCatalog.GetString ("Educational Games"), "public.app-category.educational-games");
			store.AppendValues (GettextCatalog.GetString ("Family Games"), "public.app-category.family-games");
			store.AppendValues (GettextCatalog.GetString ("Kids Games"), "public.app-category.kids-games");
			store.AppendValues (GettextCatalog.GetString ("Music Games"), "public.app-category.music-games");
			store.AppendValues (GettextCatalog.GetString ("Puzzle Games"), "public.app-category.puzzle-games");
			store.AppendValues (GettextCatalog.GetString ("Racing Games"), "public.app-category.racing-games");
			store.AppendValues (GettextCatalog.GetString ("Role Playing Games"), "public.app-category.role-playing-games");
			store.AppendValues (GettextCatalog.GetString ("Simulation Games"), "public.app-category.simulation-games");
			store.AppendValues (GettextCatalog.GetString ("Sports Games"), "public.app-category.sports-games");
			store.AppendValues (GettextCatalog.GetString ("Strategy Games"), "public.app-category.strategy-games");
			store.AppendValues (GettextCatalog.GetString ("Trivia Games"), "public.app-category.trivia-games");
			store.AppendValues (GettextCatalog.GetString ("Word Games"), "public.app-category.word-games");
		}
		
		private static void FillMacOSVersion (ComboBox combobox)
		{
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();
			store.AppendValues (GettextCatalog.GetString ("Mac OS X 10.5"), MacOSVersion.MacOS105);
			store.AppendValues (GettextCatalog.GetString ("Mac OS X 10.6"), MacOSVersion.MacOS106);
			store.AppendValues (GettextCatalog.GetString ("Mac OS X 10.7"), MacOSVersion.MacOS107);
			store.AppendValues (GettextCatalog.GetString ("Mac OS X 10.8"), MacOSVersion.MacOS108);
		}
		
		private static  void FillFrameworks (TreeView treeView, MonobjcProject project)
		{
			TreeStore store = (TreeStore)treeView.Model;
			store.Clear ();
			IEnumerable<String> assemblies = (from a in project.EveryMonobjcAssemblies
			                                  where a.Name.Contains ("Monobjc.")
			                                  select a.Name.Substring ("Monobjc.".Length)).Distinct ();
			foreach (String assembly in assemblies) {
				store.AppendValues (false, ImageService.GetPixbuf ("md-monobjc-fmk", IconSize.Menu), assembly);
			}
		}

		private static void FillArchitectures (ComboBox combobox)
		{
			// Set up the architectures
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();
			
			MacOSArchitecture architecture = Lipo.GetArchitecture ("/usr/bin/mono");
			if (architecture == MacOSArchitecture.None) {
				// Humm, there was an error, so add only i386
				store.AppendValues (GettextCatalog.GetString ("Intel i386 (32 bits)"), MacOSArchitecture.X86);
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
				store.AppendValues (GettextCatalog.GetString ("Intel i386 (32 bits)"), MacOSArchitecture.X86);
			}
			if ((architecture & MacOSArchitecture.X8664) == MacOSArchitecture.X8664) {
				store.AppendValues (GettextCatalog.GetString ("Intel x86_64 (64 bits)"), MacOSArchitecture.X8664);
			}
			if ((architecture & MacOSArchitecture.Intel) == MacOSArchitecture.Intel) {
				store.AppendValues (GettextCatalog.GetString ("Intel (32/64 bits)"), MacOSArchitecture.Intel);
			}
			if (((architecture & MacOSArchitecture.PPC) == MacOSArchitecture.PPC) && !isXcode4) {
				store.AppendValues (GettextCatalog.GetString ("Power PC (32 bits)"), MacOSArchitecture.PPC);
			}
			if (((architecture & MacOSArchitecture.Universal32) == MacOSArchitecture.Universal32) && !isXcode4) {
				store.AppendValues (GettextCatalog.GetString ("Universal PowerPC/Intel (32 bits)"), MacOSArchitecture.Universal32);
			}
			if (((architecture & MacOSArchitecture.Universal3264) == MacOSArchitecture.Universal3264) && !isXcode4) {
				store.AppendValues (GettextCatalog.GetString ("Universal PowerPC/Intel (32/64 bits)"), MacOSArchitecture.Universal3264);
			}
		}

		private static void FillCertificates (ComboBox combobox, IList<String> identities, String defaultText)
		{
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();
			
			// Append first default value
			store.AppendValues (GettextCatalog.GetString (defaultText), String.Empty);
			
			if (identities.Count == 0) {
				return;
			}
			
			// Append each identity
			foreach (String identity in identities) {
				store.AppendValues (identity, identity);
			}
		}

		private static void FillDevelopmentRegions (ComboBox combobox, MonobjcProject project)
		{
			ListStore store = (ListStore)combobox.Model;
			store.Clear ();
			FilePath projectDir = project.BaseDirectory;
			String[] folders = Directory.GetDirectories (projectDir, "*.lproj");
			if (folders.Length == 0) {
				store.AppendValues (GettextCatalog.GetString ("en"), "en");
			} else {
				foreach (String folder in folders) {
					if (!Directory.Exists (folder)) {
						continue;
					}
					String language = System.IO.Path.GetFileNameWithoutExtension (folder);
					store.AppendValues (GettextCatalog.GetString (language), language);
				}
			}
		}
	
		private static TreeViewColumn GetFrameworkTableColumn (ToggledHandler handler)
		{
			TreeViewColumn column = new TreeViewColumn ();
			
			CellRendererToggle checkRenderer = new CellRendererToggle ();
			checkRenderer.Toggled += handler;
			column.PackStart (checkRenderer, false);
			column.AddAttribute (checkRenderer, "active", 0);
			
			CellRendererPixbuf iconRenderer = new CellRendererPixbuf ();
			column.PackStart (iconRenderer, false);
			column.AddAttribute (iconRenderer, "pixbuf", 1);
			
			CellRendererText nameRenderer = new CellRendererText ();
			column.PackStart (nameRenderer, true);
			column.AddAttribute (nameRenderer, "text", 2);

			return column;
		}

		private static TreeViewColumn GetListTableColumn(EditedHandler handler)
		{
			TreeViewColumn column = new TreeViewColumn ();
			CellRendererText renderer = new CellRendererText ();
			renderer.Editable = true;
			renderer.Edited += handler;
			column.PackStart (renderer, true);
			column.AddAttribute (renderer, "text", 0);
			return column;
		}

		private static T GetSingleValue<T> (ComboBox comboBox)
		{
			TreeIter iter;
			if (comboBox.GetActiveIter (out iter)) {
				ListStore listStore = (ListStore)comboBox.Model;
				return (T)listStore.GetValue (iter, 1);
			}
			throw new InvalidOperationException ();
		}
		
		private static void SetSingleValue<T> (ComboBox comboBox, T value)
		{
			ListStore store = (ListStore)comboBox.Model;
			TreeIter iter;
			store.GetIterFirst (out iter);
			do {
				if (((T)store.GetValue (iter, 1)).Equals (value)) {
					comboBox.SetActiveIter (iter);
					return;
				}
				if (!store.IterNext (ref iter)) {
					break;
				}
			} while (true);
			comboBox.Active = 0;
		}
		
		private static IEnumerable<T> GetMultipleValues<T> (TreeView treeView)
		{
			IList<T> result = new List<T> ();
			TreeStore store = (TreeStore)treeView.Model;
			TreeIter iter;
			if (!store.GetIterFirst (out iter)) {
				return result;
			}
			do {
				T value = (T)store.GetValue (iter, 2);
				bool state = (bool)store.GetValue (iter, 0);
				if (state) {
					result.Add (value);
				}
				
				if (!store.IterNext (ref iter)) {
					break;
				}
			} while (true);
			return result;
		}
		
		private static void SetMultipleValues<T> (TreeView treeView, IEnumerable<T> values)
		{
			TreeStore store = (TreeStore)treeView.Model;
			TreeIter iter;
			if (!store.GetIterFirst (out iter)) {
				return;
			}
			do {
				T value = (T)store.GetValue (iter, 2);
				bool state = values.Contains<T> (value);
				store.SetValue (iter, 0, state);
				
				if (!store.IterNext (ref iter)) {
					break;
				}
			} while (true);
		}
		
		private static String ExtractFromModel (TreeModel model)
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
		
		private static String InjectIntoModel (TreeModel model, String value)
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

		private static void EditItem (TreeView treeView, EditedArgs args)
		{
			TreeStore store = (TreeStore)treeView.Model;
			
			TreeIter iter;
			store.GetIter(out iter, new TreePath(args.Path));
			store.SetValue(iter, 0, args.NewText);
		}
		
		private static void AddEmptyItem(TreeView treeView, String defaultValue) {
			TreeStore store = (TreeStore)treeView.Model;
			store.AppendValues(defaultValue);
		}
		
		private static void RemoveItem(TreeView treeView) {
			TreeIter iter;
			if (!treeView.Selection.GetSelected(out iter)) {
				return;
			}
			
			TreeStore store = (TreeStore)treeView.Model;
			store.Remove (ref iter);
		}
	}
}
