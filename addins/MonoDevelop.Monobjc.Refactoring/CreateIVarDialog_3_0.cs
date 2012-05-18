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
using System.Text;
using Mono.TextEditor;
using Mono.TextEditor.PopupWindow;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.TypeSystem;

namespace MonoDevelop.Monobjc.Refactoring
{
	public partial class CreateIVarDialog
	{
		protected override void DoRefactor()
		{
			String propertyName = this.entryName.Text;
			AstType propertyType = new SimpleType (this.entryType.Text);
			
			// Get some useful objects
			TextEditorData data = options.GetTextEditorData ();
			INRefactoryASTProvider provider = options.GetASTProvider ();
			ResolveResult resolveResult = this.options.ResolveResult;
			
			TextEditor editor = data.Parent;
			IType declaringType = resolveResult.ResolvedType.Type;
			
			// Get the indentation
			String indent = this.options.GetIndent (declaringType) + "\t";
			StringBuilder code = new StringBuilder ();
			
			// Generate the instance variable
			//code.Append (this.GenerateProperty (declaringType, propertyName, propertyType, provider, indent));
			code.AppendLine ();
			
			InsertionCursorEditMode mode = new InsertionCursorEditMode (editor, CodeGenerationService.GetInsertionPoints (options.Document, declaringType));
			ModeHelpWindow helpWindow = new ModeHelpWindow ();
			helpWindow.TransientFor = IdeApp.Workbench.RootWindow;
			helpWindow.TitleText = GettextCatalog.GetString ("<b>Implement Interface -- Targeting</b>");
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Key</b>"), GettextCatalog.GetString ("<b>Behavior</b>")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Up</b>"), GettextCatalog.GetString ("Move to <b>previous</b> target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Down</b>"), GettextCatalog.GetString ("Move to <b>next</b> target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Enter</b>"), GettextCatalog.GetString ("<b>Declare interface implementation</b> at target point.")));
			helpWindow.Items.Add (new KeyValuePair<string, string> (GettextCatalog.GetString ("<b>Esc</b>"), GettextCatalog.GetString ("<b>Cancel</b> this refactoring.")));
			mode.HelpWindow = helpWindow;
			mode.CurIndex = mode.InsertionPoints.Count - 1;
			mode.StartMode ();
			mode.Exited += delegate(object s, InsertionCursorEventArgs args) {
				if (args.Success) {
					args.InsertionPoint.Insert (data, code.ToString ());
				}
			};
		}
		
		//private string GenerateProperty (IType declaringType, string propertyName, AstType propertyType, INRefactoryASTProvider provider, string indent)
		//{
		//	return string.Empty;
		//}
	}
}
