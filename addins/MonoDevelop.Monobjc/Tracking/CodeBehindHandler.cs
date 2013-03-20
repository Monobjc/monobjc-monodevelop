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
	class CodeBehindHandler : ProjectHandler
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

		/// <summary>
		/// Generates the design code.
		/// </summary>
		public void GenerateDesignCode (IEnumerable<ProjectFileEventInfo> e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}

			// Run CodeBehind if it is a XIB file
			IList<ProjectFile> projectFiles = new List<ProjectFile> ();
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				IDELogger.Log ("CodeBehindHandler::GenerateDesignCode -- checking {0}", projectFile);
				if (BuildHelper.IsXIBFile (projectFile) && this.Project.IsInDevelopmentRegion (projectFile)) {
					IDELogger.Log ("CodeBehindHandler::GenerateDesignCode -- collecting {0}", projectFile);
					projectFiles.Add (projectFile);
				}
			}
			
			if (projectFiles.Count == 0) {
				return;
			}

			this.ScheduleDesignCodeGeneration (projectFiles);
		}

		private void ScheduleDesignCodeGeneration (IList<ProjectFile> projectFiles)
		{
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
			ThreadPool.QueueUserWorkItem (delegate {
				try {
					monitor.BeginTask (GettextCatalog.GetString ("Generating design code..."), 1 + projectFiles.Count);

					ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
					CodeBehindWriter writer = CodeBehindWriter.CreateForProject (monitor, this.Project);
					
					// Perform the code generation
					List<FilePath> designerFiles = new List<FilePath> ();
					foreach (ProjectFile file in projectFiles) {
						IList<FilePath> files = this.GenerateCodeBehind (cache, writer, file.FilePath);
						designerFiles.AddRange (files);
						monitor.Step (1);
					}
					writer.WriteOpenFiles ();

					// Add all the designer files
					foreach (FilePath designerFile in designerFiles) {
						this.Project.AddFile (designerFile);
					}
					this.Project.Save (monitor);

					monitor.EndTask ();
				} catch (Exception ex) {
					monitor.ReportError (ex.Message, ex);
				} finally {
					monitor.Dispose ();
				}
			});
		}

		private IList<FilePath> GenerateCodeBehind (ProjectTypeCache cache, CodeBehindWriter writer, FilePath file)
		{
			IDELogger.Log ("CodeBehindHandler::GenerateCodeBehind -- Parsing {0}", file);

			// Parse and collect information from the document
			IBDocument document = IBDocument.LoadFromFile (file);
			ClassDescriptionCollector visitor = new ClassDescriptionCollector ();
			document.Root.Accept (visitor);
			
			List<FilePath> designerFiles = new List<FilePath> ();
			foreach (string className in visitor.ClassNames) {
				// Check if the class should be generated
				if (!ShouldGenerate (visitor, className)) {
					IDELogger.Log ("CodeBehindHandler::GenerateCodeBehind -- Skipping {0} (no outlets, no actions or reserved name)", className);
					continue;
				}
				
				// Generate the designer part
				FilePath designerFile = this.CodeGenerator.GenerateCodeBehindCode (cache, writer, className, visitor [className]);
				if (designerFile != FilePath.Null) {
					designerFiles.Add (designerFile);
				}
			}

			return designerFiles;
		}
		
		/// <summary>
		/// Returns whether a designed class should be generated.
		/// </summary>
		private static bool ShouldGenerate (ClassDescriptionCollector visitor, String className)
		{
			// Not clean, but needed anyway...
			if (String.Equals ("FirstResponder", className)) {
				return false;
			}
			// TODO: Skip classes that are standard ones
			IEnumerable<IBPartialClassDescription> enumerable = visitor [className];
			return (enumerable.SelectMany (d => d.Outlets).Count () > 0 || enumerable.SelectMany (d => d.Actions).Count () > 0);
		}
	}
}
