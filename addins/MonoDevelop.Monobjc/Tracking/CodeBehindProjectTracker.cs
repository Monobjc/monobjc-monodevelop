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
using System.IO;
using System.Linq;
using System.Threading;
using Monobjc.Tools.InterfaceBuilder;
using Monobjc.Tools.InterfaceBuilder.Visitors;
using MonoDevelop.Core;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Tasks;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	public class CodeBehindProjectTracker : ProjectTracker
	{
		private ICodeBehindGenerator codeGenerator;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeBehindProjectTracker"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		public CodeBehindProjectTracker (MonobjcProject project) : base(project)
		{
		}

		/// <summary>
		/// Generates the design code for framework loading.
		/// </summary>
		/// <param name="frameworks">The frameworks.</param>
		/// <param name="defer">if set to <c>true</c> [defer].</param>
		internal void GenerateFrameworkLoadingCode (String[] frameworks, bool defer)
		{
			// Don't generate anything if the project is not ready
			if (!this.IsDomReady) {
#if DEBUG
				LoggingService.LogInfo ("CodeBehindProjectTracker => Project is not ready yet");
#endif
				return;
			}
			
			// Queue the generation in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.GenerateFrameworkLoadingCode (frameworks, false); });
				return;
			}
#if DEBUG
            LoggingService.LogInfo("CodeBehindProjectTracker::GenerateFrameworkLoadingCode");
#endif
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
			monitor.BeginTask (GettextCatalog.GetString ("Generating framework loading code..."), 3);
			
			// Get the resolver
			ProjectTypeCache resolver = ProjectTypeCache.Get(this.Project);
			monitor.Step (1);
			
			// Generate loading code
			FilePath entryPoint = this.CodeGenerator.GenerateFrameworkLoadingCode (resolver, frameworks);
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
		}

		/// <summary>
		/// Generates the design code for an IB file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="defer">if set to <c>true</c> defer generation in a separate thread.</param>
		internal void GenerateDesignCode (IList<ProjectFile> projectFiles, bool defer)
		{
			// Don't generate anything if the project is not ready
			if (!this.IsDomReady) {
#if DEBUG
				LoggingService.LogInfo ("CodeBehindProjectTracker => Project is not ready yet");
#endif
				return;
			}
			
			// Queue the generation in another thread if defer is wanted
			if (defer) {
				ThreadPool.QueueUserWorkItem (delegate {
					this.GenerateDesignCode (projectFiles, false); });
				return;
			}

			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
			monitor.BeginTask (GettextCatalog.GetString ("Generating design code..."), 1 + projectFiles.Count);
			
			// Get the resolver
            ProjectTypeCache resolver = ProjectTypeCache.Get(this.Project);

			// Create the writer
			CodeBehindWriter writer = CodeBehindWriter.CreateForProject (monitor, this.Project);

			// Perform the code generation
			foreach (ProjectFile file in projectFiles) {
				this.GenerateCodeBehind (resolver, writer, file.FilePath);
				monitor.Step (1);
			}
			
			DispatchService.GuiDispatch (() => {
				monitor.EndTask ();
				monitor.Dispose ();
			});
		}

		/// <summary>
		/// Handles the FileAddedToProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
		protected override void HandleFileAddedToProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateDesignCode (e);
		}

		/// <summary>
		/// Handles the FileChangedInProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
		protected override void HandleFileChangedInProject (object sender, ProjectFileEventArgs e)
		{
			this.GenerateDesignCode (e);
		}
		
		/// <summary>
		/// Generates the design code for applicable files.
		/// </summary>
		private void GenerateDesignCode (ProjectFileEventArgs e)
		{
			// Balk if the project is being deserialized
			if (this.Project.Loading) {
				return;
			}
			
			// Run CodeBehind if it is a XIB file
			IList<ProjectFile> projectFiles = new List<ProjectFile> ();
			foreach (ProjectFileEventInfo info in e) {
				ProjectFile projectFile = info.ProjectFile;
				if (BuildHelper.IsXIBFile (projectFile) && BuildHelper.IsInDevelopmentRegion (this.Project, projectFile)) {
#if DEBUG
					LoggingService.LogInfo("CodeBehindProjectTracker::GenerateDesignCode " + projectFile);
#endif
					projectFiles.Add (projectFile);
				}
			}
			
			if (projectFiles.Count > 0) {
				this.GenerateDesignCode (projectFiles, true);
			}
		}
		
		/// <summary>
		/// Generates the design code for Interface Builder.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="file">The file.</param>
		private void GenerateCodeBehind (ProjectTypeCache resolver, CodeBehindWriter writer, FilePath file)
		{
#if DEBUG
            LoggingService.LogInfo("Parsing for code behind " + file);
#endif
			IBDocument document = IBDocument.LoadFromFile (file);
			ClassDescriptionCollector visitor = new ClassDescriptionCollector ();
			document.Root.Accept (visitor);

			List<FilePath> designerFiles = new List<FilePath> ();
			foreach (string className in visitor.ClassNames) {
				// Check if the class should be generated
				if (!ShouldGenerate (visitor, className)) {
#if DEBUG
                    LoggingService.LogInfo("Skipping " + className + " (no outlets, no actions or reserved name)");
#endif
					continue;
				}

				// Generate the designer part
				FilePath designerFile = this.CodeGenerator.GenerateCodeBehindCode (resolver, writer, className, visitor [className]);
				if (designerFile != FilePath.Null) {
					designerFiles.Add (designerFile);
				}
			}

			writer.WriteOpenFiles ();

			DispatchService.GuiDispatch (() =>
			{
				bool shouldSave = false;
				foreach (FilePath designerFile in designerFiles) {
					// Add generated file if not in project
					if (!this.Project.IsFileInProject (designerFile)) {
						this.Project.AddFile (designerFile);
						shouldSave |= true;
					}
				}

				// Save the project if needed
				if (shouldSave) {
					using (IProgressMonitor monitor = new NullProgressMonitor()) {
						this.Project.Save (monitor);
					}
				}
			});
		}
		
		/// <summary>
		/// Gets the code generator.
		/// </summary>
		/// <value>The code generator.</value>
		private ICodeBehindGenerator CodeGenerator {
			get {
				if (this.codeGenerator == null) {
					this.codeGenerator = CodeBehindGeneratorLoader.getGenerator (this.Project.LanguageName);
				}
				return codeGenerator;
			}
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
			IEnumerable<IBPartialClassDescription> enumerable = visitor [className];
			return (enumerable.SelectMany (d => d.Outlets).Count () > 0 || enumerable.SelectMany (d => d.Actions).Count () > 0);
		}
	}
}