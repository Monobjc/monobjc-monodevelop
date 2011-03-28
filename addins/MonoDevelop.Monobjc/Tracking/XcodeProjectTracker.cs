using System;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Tracking
{
    public class XcodeProjectTracker : ProjectTracker
    {
        // TODO: Move constant
        public const String DEVELOPER_TOOLS_ROOT = "MonoDevelop.Monobjc.DeveloperToolsRoot";
        public const String XCODE_VERSION = "MonoDevelop.Monobjc.XcodeVersion";

        private ProjectResolver resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="XcodeProjectTracker"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public XcodeProjectTracker(MonobjcProject project) : base(project)
        {
            PropertyService.PropertyChanged += this.PropertyService_PropertyChanged;
			ProjectDomService.TypesUpdated += this.ProjectDomService_TypesUpdated;
        }
		
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            PropertyService.PropertyChanged -= this.PropertyService_PropertyChanged;
			ProjectDomService.TypesUpdated -= this.ProjectDomService_TypesUpdated;
			base.Dispose();
        }

        protected override void Project_FileAddedToProject(object sender, ProjectFileEventArgs e)
        {
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::Project_FileAddedToProject " + e.ProjectFile);
#endif
        }

        protected override void Project_FileChangedInProject(object sender, ProjectFileEventArgs e)
        {
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::Project_FileChangedInProject " + e.ProjectFile);
#endif
        }

        protected override void Project_FileRemovedFromProject(object sender, ProjectFileEventArgs e)
        {
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::Project_FileRemovedFromProject " + e.ProjectFile);
#endif
        }

		private bool Enabled
		{
			get
			{
				// Only enable Xcode tracking if Xcode 4.0 if selected
				return true;
			}
		}

        private void PropertyService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::PropertyService_PropertyChanged " + e.Key);
#endif
            switch (e.Key)
            {
                case DEVELOPER_TOOLS_ROOT:
                case XCODE_VERSION:
                    break;
                default:
                    break;
            }
        }
		
        private void ProjectDomService_TypesUpdated(object sender, TypeUpdateInformationEventArgs e)
        {
			if (e.Project != this.Project)
			{
				return;
			}
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated");
#endif
			foreach (IType cls in e.TypeUpdateInformation.Removed) 
			{
#if DEBUG
				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Removed " + cls);
#endif
			}
			foreach (IType cls in e.TypeUpdateInformation.Modified) 
			{
#if DEBUG
				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Modified " + cls);
#endif
			}
			foreach (IType cls in e.TypeUpdateInformation.Added) 
			{
#if DEBUG
				LoggingService.LogInfo("XcodeProjectTracker::ProjectDomService_TypesUpdated - Added " + cls);
#endif
			}
        }
    }
}