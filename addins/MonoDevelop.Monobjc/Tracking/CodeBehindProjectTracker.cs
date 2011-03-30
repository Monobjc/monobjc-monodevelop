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
        public CodeBehindProjectTracker(MonobjcProject project) : base(project)
        {
        }

        /// <summary>
        /// Generates the design code for framework loading.
        /// </summary>
        /// <param name="frameworks">The frameworks.</param>
        /// <param name="defer">if set to <c>true</c> [defer].</param>
        public void GenerateFrameworkLoadingCode(String[] frameworks, bool defer)
        {
            // Queue the generation in another thread if defer is wanted
            if (defer)
            {
                ThreadPool.QueueUserWorkItem(delegate { this.GenerateFrameworkLoadingCode(frameworks, false); });
                return;
            }
#if DEBUG
//            LoggingService.LogInfo("CodeBehindProjectTracker::GenerateFrameworkLoadingCode");
#endif
            // Create the resolver
            ProjectResolver resolver = new ProjectResolver(this.Project);

            // Generate loading code
            FilePath entryPoint = this.CodeGenerator.GenerateFrameworkLoadingCode(resolver, frameworks);

            // If generation was successfull, reload the document if is opened
            DispatchService.GuiDispatch(() =>
                                            {
                                                foreach (Document doc in IdeApp.Workbench.Documents.Where(doc => String.Equals(doc.FileName, entryPoint)))
                                                {
                                                    doc.Reload();
                                                    break;
                                                }
                                            });
        }

        /// <summary>
        /// Generates the design code for an IB file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="defer">if set to <c>true</c> defer generation in a separate thread.</param>
        public void GenerateDesignCode(FilePath file, bool defer)
        {
            // Queue the generation in another thread if defer is wanted
            if (defer)
            {
                ThreadPool.QueueUserWorkItem(delegate { this.GenerateDesignCode(file, false); });
                return;
            }
#if DEBUG
//            LoggingService.LogInfo("CodeBehindProjectTracker::GenerateDesignCode");
#endif
            // Create the resolver
            ProjectResolver resolver = new ProjectResolver(this.Project);

            // Create the writer
            CodeBehindWriter writer = CodeBehindWriter.CreateForProject(new NullProgressMonitor(), this.Project);

            this.GenerateCodeBehind(resolver, writer, file);
        }

        /// <summary>
        /// Handles the FileAddedToProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected override void Project_FileAddedToProject(object sender, ProjectFileEventArgs e)
        {
            // Balk if the project is being deserialized
            if (this.Project.Loading)
            {
                return;
            }

            // Collect dependencies
            IEnumerable<string> filesToAdd = GuessDependencies(this.Project, e.ProjectFile);

            // Add dependencies
            if (filesToAdd != null)
            {
                foreach (string file in filesToAdd.Where(f => !this.Project.IsFileInProject(f)))
                {
                    this.Project.AddFile(file);
                }
            }

            // Run CodeBehind if it is a XIB file
            ProjectFile projectFile = e.ProjectFile;
            if (BuildHelper.IsXIBFile(projectFile))
            {
                this.GenerateDesignCode(projectFile.FilePath, true);
            }
        }

        /// <summary>
        /// Handles the FileChangedInProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected override void Project_FileChangedInProject(object sender, ProjectFileEventArgs e)
        {
            // Balk if the project is being deserialized
            if (this.Project.Loading)
            {
                return;
            }

            // Collect dependencies
            IEnumerable<string> filesToAdd = GuessDependencies(this.Project, e.ProjectFile);

            // Add dependencies
            if (filesToAdd != null)
            {
                foreach (string file in filesToAdd.Where(f => !this.Project.IsFileInProject(f)))
                {
                    this.Project.AddFile(file);
                }
            }

            // Run CodeBehind if it is a XIB file
            ProjectFile projectFile = e.ProjectFile;
            if (BuildHelper.IsXIBFile(projectFile))
            {
                this.GenerateDesignCode(projectFile.FilePath, true);
            }
        }

        /// <summary>
        /// Gets the code generator.
        /// </summary>
        /// <value>The code generator.</value>
        private ICodeBehindGenerator CodeGenerator
        {
            get
            {
                if (this.codeGenerator == null)
                {
                    this.codeGenerator = CodeBehindGeneratorLoader.getGenerator(this.Project.LanguageName);
                }
                return codeGenerator;
            }
        }

        /// <summary>
        /// Generates the design code for Interface Builder.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="writer">The writer.</param>
        /// <param name="file">The file.</param>
        private void GenerateCodeBehind(ProjectResolver resolver, CodeBehindWriter writer, FilePath file)
        {
#if DEBUG
            LoggingService.LogInfo("Parsing for code behind " + file);
#endif
            IBDocument document = IBDocument.LoadFromFile(file);
            ClassDescriptionCollector visitor = new ClassDescriptionCollector();
            document.Root.Accept(visitor);

            List<FilePath> designerFiles = new List<FilePath>();
            foreach (string className in visitor.ClassNames)
            {
                // Check if the class should be generated
                if (!ShouldGenerate(visitor, className))
                {
#if DEBUG
                    LoggingService.LogInfo("Skipping " + className + " (no outlets, no actions or reserved name)");
#endif
                    continue;
                }

                // Generate the designer part
                FilePath designerFile = this.CodeGenerator.GenerateCodeBehindCode(resolver, writer, className, visitor[className]);
                if (designerFile != FilePath.Null)
                {
                    designerFiles.Add(designerFile);
                }
            }

            writer.WriteOpenFiles();

            DispatchService.GuiDispatch(() =>
                                            {
                                                bool shouldSave = false;
                                                foreach (FilePath designerFile in designerFiles)
                                                {
                                                    // Add generated file if not in project
                                                    if (!this.Project.IsFileInProject(designerFile))
                                                    {
                                                        this.Project.AddFile(designerFile);
                                                        shouldSave = true;
                                                    }
                                                }

                                                if (shouldSave)
                                                {
                                                    // Save the project
                                                    this.Project.Save(new NullProgressMonitor());
                                                }
                                            });
        }

        /// <summary>
        /// Guesses the dependencies of the given file.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private static IEnumerable<string> GuessDependencies(MonobjcProject project, ProjectFile file)
        {
            String extension = project.LanguageBinding.GetFileName("abc");
            extension = extension.Substring(3);

            if (CodeBehind.IsDesignerFile(file.FilePath))
            {
                String parentName = Path.GetFileNameWithoutExtension(file.FilePath);
                parentName = parentName.Substring(0, parentName.Length - 9) + extension;

                string path = Path.Combine(Path.GetDirectoryName(file.FilePath), parentName);
                if (File.Exists(path))
                {
                    file.DependsOn = parentName;
                    return new[] { path };
                }
            }
            else
            {
                String designerEnd = ".designer" + extension;
                String childName = Path.GetFileNameWithoutExtension(file.FilePath) + designerEnd;

                string path = Path.Combine(Path.GetDirectoryName(file.FilePath), childName);
                if (File.Exists(path))
                {
                    return new[] { path };
                }
            }

            return null;
        }

        /// <summary>
        /// Returns whether a designed class should be generated.
        /// </summary>
        private static bool ShouldGenerate(ClassDescriptionCollector visitor, String className)
        {
            // Not clean, but needed anyway...
            if (String.Equals("FirstResponder", className))
            {
                return false;
            }
            IEnumerable<IBPartialClassDescription> enumerable = visitor[className];
            return (enumerable.SelectMany(d => d.Outlets).Count() > 0 || enumerable.SelectMany(d => d.Actions).Count() > 0);
        }
    }
}