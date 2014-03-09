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
using System.Threading;
using Monobjc.Tools.InterfaceBuilder;
using Monobjc.Tools.InterfaceBuilder.Visitors;
using MonoDevelop.Core;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
    partial class CodeBehindHandler : ProjectHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeBehindHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public CodeBehindHandler (MonobjcProject project) : base(project)
		{
			this.CodeGenerator = CodeBehindGeneratorLoader.GetGenerator (this.Project.LanguageName);
		}

		/// <summary>
		/// Gets or sets the code generator.
		/// </summary>
		private ICodeBehindGenerator CodeGenerator { get; set; }

		/// <summary>
		/// Generates the framework loading code.
		/// </summary>
		/// <param name='frameworks'>
		/// Frameworks.
		/// </param>
		public void GenerateFrameworkLoadingCode(String[] frameworks)
		{
			ThreadPool.QueueUserWorkItem (delegate {
				IDELogger.Log ("CodeBehindHandler::GenerateFrameworkLoadingCode");

				IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
				monitor.BeginTask (GettextCatalog.GetString ("Generating framework loading code..."), 3);
				
				// Create the resolver
				ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
				monitor.Step (1);
				
				// Generate loading code
				FilePath entryPoint = this.CodeGenerator.GenerateFrameworkLoadingCode (cache, frameworks);
				monitor.Step (1);
				
				DispatchService.GuiDispatch (() => {
					// If generation was successfull, reload the document if is opened
					foreach (Document doc in IdeApp.Workbench.Documents.Where(doc => String.Equals(doc.FileName, entryPoint))) {
						doc.Reload ();
						break;
					}
					
					monitor.EndTask ();
					monitor.Dispose ();
				});
			});
		}
	}
}
