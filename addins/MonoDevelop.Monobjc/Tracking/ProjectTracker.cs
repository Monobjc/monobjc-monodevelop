using System;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Tracking
{
    public abstract class ProjectTracker : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTracker"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public ProjectTracker(MonobjcProject project)
        {
            this.Project = project;

            this.Project.FileAddedToProject += this.Project_FileAddedToProject;
            this.Project.FileChangedInProject += this.Project_FileChangedInProject;
            this.Project.FilePropertyChangedInProject += this.Project_FilePropertyChangedInProject;
            this.Project.FileRemovedFromProject += this.Project_FileRemovedFromProject;
            this.Project.FileRenamedInProject += this.Project_FileRenamedInProject;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            this.Project.FileAddedToProject -= this.Project_FileAddedToProject;
            this.Project.FileChangedInProject -= this.Project_FileChangedInProject;
            this.Project.FilePropertyChangedInProject -= this.Project_FilePropertyChangedInProject;
            this.Project.FileRemovedFromProject -= this.Project_FileRemovedFromProject;
            this.Project.FileRenamedInProject -= this.Project_FileRenamedInProject;
        }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>The project.</value>
        protected MonobjcProject Project { get; private set; }

        /// <summary>
        /// Handles the FileAddedToProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected virtual void Project_FileAddedToProject(object sender, Projects.ProjectFileEventArgs e)
        {
        }

        /// <summary>
        /// Handles the FileChangedInProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected virtual void Project_FileChangedInProject(object sender, Projects.ProjectFileEventArgs e)
        {
        }

        /// <summary>
        /// Handles the FilePropertyChangedInProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected virtual void Project_FilePropertyChangedInProject(object sender, Projects.ProjectFileEventArgs e)
        {
        }

        /// <summary>
        /// Handles the FileRemovedFromProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
        protected virtual void Project_FileRemovedFromProject(object sender, Projects.ProjectFileEventArgs e)
        {
        }

        /// <summary>
        /// Handles the FileRenamedInProject event of the Project control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileRenamedEventArgs"/> instance containing the event data.</param>
        protected virtual void Project_FileRenamedInProject(object sender, Projects.ProjectFileRenamedEventArgs e)
        {
        }
    }
}
