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
		public ProjectTracker (MonobjcProject project)
		{
			this.Project = project;
			
			this.Project.FileAddedToProject += this.HandleFileAddedToProject;
			this.Project.FileChangedInProject += this.HandleFileChangedInProject;
			this.Project.FilePropertyChangedInProject += this.HandleFilePropertyChangedInProject;
			this.Project.FileRemovedFromProject += this.HandleFileRemovedFromProject;
			this.Project.FileRenamedInProject += this.HandleFileRenamedInProject;
			
			this.Project.ReferenceAddedToProject += this.HandleReferenceAddedToProject;
			this.Project.ReferenceRemovedFromProject += this.HandleReferenceRemovedFromProject;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public virtual void Dispose ()
		{
			this.Project.FileAddedToProject -= this.HandleFileAddedToProject;
			this.Project.FileChangedInProject -= this.HandleFileChangedInProject;
			this.Project.FilePropertyChangedInProject -= this.HandleFilePropertyChangedInProject;
			this.Project.FileRemovedFromProject -= this.HandleFileRemovedFromProject;
			this.Project.FileRenamedInProject -= this.HandleFileRenamedInProject;
			
			this.Project.ReferenceAddedToProject -= this.HandleReferenceAddedToProject;
			this.Project.ReferenceRemovedFromProject -= this.HandleReferenceRemovedFromProject;
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
		protected virtual void HandleFileAddedToProject (object sender, Projects.ProjectFileEventArgs e)
		{
		}

		/// <summary>
		/// Handles the FileChangedInProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleFileChangedInProject (object sender, Projects.ProjectFileEventArgs e)
		{
		}

		/// <summary>
		/// Handles the FilePropertyChangedInProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleFilePropertyChangedInProject (object sender, Projects.ProjectFileEventArgs e)
		{
		}

		/// <summary>
		/// Handles the FileRemovedFromProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleFileRemovedFromProject (object sender, Projects.ProjectFileEventArgs e)
		{
		}

		/// <summary>
		/// Handles the FileRenamedInProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectFileRenamedEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleFileRenamedInProject (object sender, Projects.ProjectFileRenamedEventArgs e)
		{
		}

		/// <summary>
		/// Handles the ReferenceAddedToProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectReferenceEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleReferenceAddedToProject (object sender, Projects.ProjectReferenceEventArgs e)
		{
		}

		/// <summary>
		/// Handles the ReferenceRemovedFromProject event of the Project control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MonoDevelop.Projects.ProjectReferenceEventArgs"/> instance containing the event data.</param>
		protected virtual void HandleReferenceRemovedFromProject (object sender, Projects.ProjectReferenceEventArgs e)
		{
		}
	}
}
