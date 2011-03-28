using System;
using MonoDevelop.Core;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
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
            this.Project.Modified += this.Project_Modified;
            PropertyService.PropertyChanged += this.PropertyService_PropertyChanged;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            this.Project.Modified -= this.Project_Modified;
            PropertyService.PropertyChanged -= this.PropertyService_PropertyChanged;
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

        private void Project_Modified(object sender, SolutionItemModifiedEventArgs e)
        {
#if DEBUG
//            LoggingService.LogInfo("XcodeProjectTracker::Project_Modified " + e.Hint);
#endif
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
    }
}