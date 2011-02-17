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
using Monobjc.Tools.InterfaceBuilder;
using Monobjc.Tools.InterfaceBuilder.Visitors;
using MonoDevelop.Core;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc.CodeGeneration;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	/// Helper class responsible for code-behind generation.
	/// </summary>
	internal class MonobjcCodeBehind
	{
		private readonly MonobjcProject project;
		private readonly ICodeBehindGenerator codeGenerator;

		/// <summary>
		/// Monobjcs the code bssssssehind.
		/// </summary>
		/// <param name="project">The project.</param>
		public MonobjcCodeBehind (MonobjcProject project)
		{
			this.project = project;
			this.codeGenerator = CodeBehindGeneratorLoader.getGenerator (this.project.LanguageName);
		}

		/// <summary>
		/// Generates the design code for framework loading.
		/// </summary>
		/// <param name="frameworks">The frameworks.</param>
		public void GenerateDesignCodeForFrameworks (String[] frameworks)
		{
			// Create the resolver
			ProjectResolver resolver = new ProjectResolver (this.project);
			
			// Generate loading code
			FilePath entryPoint = this.codeGenerator.GenerateDesignCodeForFrameworks (resolver, frameworks);
			
			// If generation was successfull, reload the document if is opened
			DispatchService.GuiDispatch (delegate {
				foreach (MonoDevelop.Ide.Gui.Document doc in IdeApp.Workbench.Documents) {
					if (String.Equals (doc.FileName, entryPoint)) {
						doc.Reload ();
						break;
					}
				}
			});
		}

		/// <summary>
		/// Generates the design code for Interface Builder.
		/// </summary>
		/// <param name="file">The file.</param>
		public void GenerateDesignCodeForIB (FilePath file)
		{
			// Create the resolver
			ProjectResolver resolver = new ProjectResolver (this.project);
			
			// Create the writer
			CodeBehindWriter writer = CodeBehindWriter.CreateForProject (new NullProgressMonitor (), this.project);
			
			this.GenerateDesignCodeForIB (resolver, writer, file);
		}

		/// <summary>
		/// Generates the design code for Interface Builder.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="file">The file.</param>
		public void GenerateDesignCodeForIB (ProjectResolver resolver, CodeBehindWriter writer, FilePath file)
		{
			LoggingService.LogInfo ("Parsing for code behind " + file);
			
			IBDocument document = IBDocument.LoadFromFile (file);
			ClassDescriptionCollector visitor = new ClassDescriptionCollector ();
			document.Root.Accept (visitor);
			
			List<FilePath> designerFiles = new List<FilePath> ();
			foreach (string className in visitor.ClassNames) {
				// Check if the class should be generated
				if (!ShouldGenerate (visitor, className)) {
					LoggingService.LogInfo ("Skipping " + className + " (no outlets, no actions or reserved name)");
					continue;
				}
				
				// Generate the designer part
				FilePath designerFile = this.codeGenerator.GenerateDesignCodeForIB (resolver, writer, className, visitor[className]);
				if (designerFile != FilePath.Null) {
					designerFiles.Add (designerFile);
				}
			}
			
			writer.WriteOpenFiles ();
			
			DispatchService.GuiDispatch (delegate {
				bool shouldSave = false;
				foreach (FilePath designerFile in designerFiles) {
					// Add generated file if not in project
					if (!this.project.IsFileInProject (designerFile)) {
						this.project.AddFile (designerFile);
						shouldSave = true;
					}
				}
				
				if (shouldSave) {
					// Save the project
					this.project.Save (new NullProgressMonitor ());
				}
			});
		}

		/// <summary>
		/// Guesses the dependencies of the given file.
		/// </summary>
		/// <param name="project">The project.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static IEnumerable<string> GuessDependencies (MonobjcProject project, ProjectFile file)
		{
			String extension = project.LanguageBinding.GetFileName ("abc");
			extension = extension.Substring (3);
			
			if (CodeBehind.IsDesignerFile (file.FilePath)) {
				String parentName = Path.GetFileNameWithoutExtension (file.FilePath);
				parentName = parentName.Substring (0, parentName.Length - 9) + extension;
				
				string path = Path.Combine (Path.GetDirectoryName (file.FilePath), parentName);
				if (File.Exists (path)) {
					file.DependsOn = parentName;
					return new[] { path };
				}
			} else {
				String designerEnd = ".designer" + extension;
				String childName = Path.GetFileNameWithoutExtension (file.FilePath) + designerEnd;
				
				string path = Path.Combine (Path.GetDirectoryName (file.FilePath), childName);
				if (File.Exists (path)) {
					return new[] { path };
				}
			}
			
			return null;
		}

		private static bool ShouldGenerate (ClassDescriptionCollector visitor, String className)
		{
			if (String.Equlas("FirstResponder", className)) {
				return false;
			}
			IEnumerable<IBPartialClassDescription> enumerable = visitor[className];
			return (enumerable.SelectMany (d => d.Outlets).Count () > 0 || enumerable.SelectMany (d => d.Actions).Count () > 0);
		}
	}
}
