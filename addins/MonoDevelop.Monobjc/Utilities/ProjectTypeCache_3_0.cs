using System;
using System.Linq;
using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;
using MonoDevelop.Core;
using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Utilities
{
    public partial class ProjectTypeCache
    {
        private TypeSystemService.ProjectContentWrapper projectWrapper;
        private List<TypeSystemService.ProjectContentWrapper> projectWrappers;
        private IDictionary<String, IType> typeCache = new Dictionary<String, IType>();

        static ProjectTypeCache() {
            TypeSystemService.ProjectUnloaded += HandleProjectUnloaded;
        }

        private static void HandleProjectUnloaded (object sender, ProjectUnloadEventArgs e)
        {
            MonobjcProject project = e.Project as MonobjcProject;
            if (project != null) {
                caches.Remove(project);
            }
        }

        public void InternalClearCache()
        {
            this.typeCache.Clear();
        }

        private void InternalRecommputeReferences()
        {
            this.projectWrappers = null;
        }

        private FilePath InternalGetMainFile (IType type)
        {
            if (type == null) {
                return FilePath.Null;
            }
            ITypeDefinition definition = type.GetDefinition ();
            IEnumerable<IUnresolvedTypeDefinition> parts = definition.Parts;
            IEnumerable<String> files = parts.Select (td => td.Region.FileName).OrderBy (s => s.Length);
            return (FilePath)files.FirstOrDefault ();
        }

        private IEnumerable<IType> InternalGetAllClasses (bool projectOnly)
        {
            Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Class && AttributeHelper.HasAttribute (td, AttributeHelper.OBJECTIVE_C_CLASS);
            IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, projectOnly);
            return ConvertTo (typeDefinitions);
        }

        private IEnumerable<IType> InternalGetAllProtocols (bool projectOnly)
        {
            Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Interface && AttributeHelper.HasAttribute (td, AttributeHelper.OBJECTIVE_C_PROTOCOL);
            IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, projectOnly);
            return ConvertTo (typeDefinitions);
        }

        private IEnumerable<IType> InternalGetEntryPoints()
        {
            // Collect types that have a Main static method
            Func<ITypeDefinition, bool> matcher = td => td.GetMethods (m => m.IsStatic && m.Name == "Main", GetMemberOptions.ReturnMemberDefinitions).Count () > 0;
            IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, true);
            return ConvertTo (typeDefinitions);
        }

        private IMethod InternalGetMainMethod (IType type)
        {
            return type.GetMethods (m => m.IsStatic && m.Name == "Main").SingleOrDefault ();
        }

        private IType InternalResolveType (Type type)
        {
            ITypeReference typeReference = ReflectionHelper.ToTypeReference(type);
            return typeReference.Resolve (this.ProjectWrapper.Compilation);
        }

        private IType InternalResolveType (IType type)
        {
            return type;
        }

        private IType InternalResolvePartialType (String className)
        {
            Func<ITypeDefinition, bool> matcher, attributeMatcher;
            IEnumerable<ITypeDefinition> typeDefinitions;
            ITypeDefinition typeDefinition = null;
            IType type = null;

            if (this.typeCache.TryGetValue(className, out type)) {
                return type;
            }

            if (typeDefinition == null) {
                // Search for types matches in classes
                matcher = td => String.Equals (
                    td.Name,
                    className
                );
                typeDefinitions = GetMatchingTypeDefinitions(matcher, false);
    
                switch (typeDefinitions.Count ()) {
                case 1:
                    typeDefinition = typeDefinitions.First ();
                    break;
                default:
                    // If there is multiple match, filter by looking at the attributes
                    attributeMatcher = td => AttributeHelper.HasAttribute (
                        td,
                        AttributeHelper.OBJECTIVE_C_CLASS
                    );
                    typeDefinitions = GetMatchingTypeDefinitions (typeDefinitions, attributeMatcher);
                    if (typeDefinitions.Count () == 1) {
                        typeDefinition = typeDefinitions.First ();
                    }
                    break;
                }
            }

            if (typeDefinition == null) {
                // Search for types matches in protocols
                matcher = td => String.Equals (
                    td.Name,
                   "I" + className
                );
                typeDefinitions = GetMatchingTypeDefinitions(matcher, false);
    
                switch (typeDefinitions.Count ()) {
                case 1:
                    typeDefinition = typeDefinitions.First ();
                    break;
                default:
                    // If there is multiple match, filter by looking at the attributes
                    attributeMatcher = td => AttributeHelper.HasAttribute (
                        td,
                        AttributeHelper.OBJECTIVE_C_PROTOCOL
                    );
                    typeDefinitions = GetMatchingTypeDefinitions (typeDefinitions, attributeMatcher);
                    if (typeDefinitions.Count () == 1) {
                        typeDefinition = typeDefinitions.First ();
                    }
                    break;
                }
            }

            if (typeDefinition != null) {
                type = typeDefinition.ToTypeReference ().Resolve (this.ProjectWrapper.Compilation);
                this.typeCache.Add(className, type);
            }

            return type;
        }

        private bool InternalIsInProject (IType type)
        {
            ITypeDefinition definition = type.GetDefinition();
            if (definition == null) {
                return false;
            }
            return definition.ParentAssembly.Equals(this.ProjectWrapper.Compilation.MainAssembly);
        }

        private TypeSystemService.ProjectContentWrapper ProjectWrapper
        {
            get {
                if (this.projectWrapper == null) {
                    this.projectWrapper = TypeSystemService.GetProjectContentWrapper(this.Project);
                }
                return this.projectWrapper;
            }
        }

        private IList<TypeSystemService.ProjectContentWrapper> ProjectWrappers
        {
            get {
                if (this.projectWrappers == null) {
                    this.projectWrappers = new List<TypeSystemService.ProjectContentWrapper>();
                    CollectReferences(this.projectWrappers, this.ProjectWrapper);
                }
                return this.projectWrappers;
            }
        }

        private static void CollectReferences (ICollection<TypeSystemService.ProjectContentWrapper> wrappers, TypeSystemService.ProjectContentWrapper wrapper)
        {
            if (wrapper == null) {
                return;
            }
            if (wrappers.Contains (wrapper)) {
                return;
            }
            wrappers.Add (wrapper);
            foreach (Project project in wrapper.ReferencedProjects) {
                TypeSystemService.ProjectContentWrapper referenceWrapper = TypeSystemService.GetProjectContentWrapper (project);
                CollectReferences (wrappers, referenceWrapper);
            }
        }

        private IEnumerable<IType> ConvertTo (IEnumerable<ITypeDefinition> typeDefinitions)
        {
            return typeDefinitions.Select (td => td.ToTypeReference ().Resolve (this.ProjectWrapper.Compilation));
        }

        private static IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions (IEnumerable<ITypeDefinition> typeDefinitions, Func<ITypeDefinition, bool> matcher)
        {
            IEnumerable<ITypeDefinition> result = typeDefinitions.Where (td => matcher (td));
            return result;
        }

        private static IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions (TypeSystemService.ProjectContentWrapper wrapper, Func<ITypeDefinition, bool> matcher)
        {
            IEnumerable<ITypeDefinition> typeDefinitions = wrapper.Compilation.MainAssembly.GetAllTypeDefinitions ();
            return GetMatchingTypeDefinitions (typeDefinitions, matcher);
        }

        private IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions (Func<ITypeDefinition, bool> matcher, bool projectOnly)
        {
            TypeSystemService.ForceUpdate (this.ProjectWrapper);

            // Search only in the project wrapper
            if (projectOnly) {
                return GetMatchingTypeDefinitions (this.ProjectWrapper, matcher);
            }
            
            // Search in all compilation
            IEnumerable<ITypeDefinition> typeDefinitions = this.ProjectWrapper.Compilation.GetAllTypeDefinitions();
            return GetMatchingTypeDefinitions (typeDefinitions, matcher);
        }
    }
}
