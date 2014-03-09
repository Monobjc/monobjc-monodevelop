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
using Antlr4.Runtime;
using MonobjcDevelop.Monobjc.Parsing;

namespace MonoDevelop.Monobjc.Tracking
{
    partial class CodeBehindHandler
	{
        /// <summary>
        /// Generates the design code.
        /// </summary>
        public void GenerateDesignCodeForHeaders (IEnumerable<FilePath> paths)
        {
            // Balk if the project is being deserialized
            if (this.Project.Loading) {
                return;
            }

            // Balk if Xcode 5.0 or higher is used
            if (!DeveloperToolsDesktopApplication.IsXcode50OrHigher) {
                return;
            }

            if (paths.Count() == 0) {
                return;
            }

            this.ScheduleDesignCodeGenerationForHeaders (paths);
        }
        private void ScheduleDesignCodeGenerationForHeaders (IEnumerable<FilePath> paths)
		{
			IProgressMonitor monitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor ("Monobjc", "md-monobjc", false);
			ThreadPool.QueueUserWorkItem (delegate {
				try {
                    monitor.BeginTask (GettextCatalog.GetString ("Generating design code..."), 1 + (paths.Count()));

					ProjectTypeCache cache = ProjectTypeCache.Get (this.Project);
					CodeBehindWriter writer = CodeBehindWriter.CreateForProject (monitor, this.Project);
					
					// Perform the code generation
					List<FilePath> designerFiles = new List<FilePath> ();
                    foreach (FilePath path in paths) {
                        IList<FilePath> files = this.GenerateCodeBehindForHeader (cache, writer, path);
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

        private IList<FilePath> GenerateCodeBehindForHeader (ProjectTypeCache cache, CodeBehindWriter writer, FilePath file)
		{
            IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- Parsing {0}", file);

			// Parse and collect information from the document
            AntlrFileStream stream = new AntlrFileStream (file);
            ObjCLexer lexer = new ObjCLexer (stream);
            CommonTokenStream tokenStream = new CommonTokenStream (lexer);
            ObjCParser parser = new ObjCParser (tokenStream);
			
            ObjCParser.Translation_unitContext context = parser.translation_unit ();
            NativeClassDescriptionCollector<int> visitor = new NativeClassDescriptionCollector<int> ();
            context.Accept (visitor);

            /*
            IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- Dump classes");
            foreach (NativeClassDescriptor classDescriptor in visitor.Descriptors) {
                IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- ClassName={0}", classDescriptor.ClassName);
                IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- SuperClassName={0}", classDescriptor.SuperClassName);
                foreach (NativeMethodDescriptor descriptor in classDescriptor.Methods) {
                    IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- {0}", descriptor);
                }
                foreach (NativeInstanceVariableDescriptor descriptor in classDescriptor.InstanceVariables) {
                    IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- {0}", descriptor);
                }
            }
            */

			List<FilePath> designerFiles = new List<FilePath> ();
            foreach (NativeClassDescriptor classDescriptor in visitor.Descriptors) {
                // Check if the class should be generated
                if (!ShouldGenerateForHeader (classDescriptor)) {
                    IDELogger.Log ("CodeBehindHandler::GenerateCodeBehindForHeader -- Skipping {0} (no outlets or no actions)", classDescriptor.ClassName);
                    continue;
                }

                // Generate the designer part
                FilePath designerFile = this.CodeGenerator.GenerateCodeBehindCode (cache, writer, classDescriptor.ClassName, new []{ classDescriptor });
                if (designerFile != FilePath.Null) {
                    designerFiles.Add (designerFile);
                }
            }

			return designerFiles;
		}
		
		/// <summary>
		/// Returns whether a designed class should be generated.
		/// </summary>
        private static bool ShouldGenerateForHeader (NativeClassDescriptor classDescriptor)
		{
            return classDescriptor.Outlets.Count() > 0 || classDescriptor.Actions.Count() > 0;
		}
	}
}
