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
using System.Text;
using Gtk;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.TypeSystem;

namespace MonoDevelop.Monobjc.Refactoring
{
	public partial class CreateIVarDialog : BaseDialog
	{
		public CreateIVarDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project) : base(refactoring, options, project)
		{
			this.Build ();
			
			this.Title = GettextCatalog.GetString ("Create Instance Variable");
			
			this.buttonOk.Sensitive = false;
			this.entryName.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
			this.entryType.Changed += delegate { this.buttonOk.Sensitive = this.Validate (); };
			this.Validate ();
			
			this.buttonOk.Clicked += OnOKClicked;
		}

		private bool Validate ()
		{
			return true;
		}

		void OnOKClicked (object sender, EventArgs e)
		{
			try {
			} finally {
				this.Destroy ();
			}
		}
	}
}