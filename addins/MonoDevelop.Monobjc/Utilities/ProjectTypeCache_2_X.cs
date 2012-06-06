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
using MonoDevelop.Core;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Utilities
{
    public partial class ProjectTypeCache
    {
        private ProjectDom projectDom;
        private List<ProjectDom> projectDoms;
        private IDictionary<String, IType> typeCache = new Dictionary<String, IType>();

        /// <summary>
        /// Gets the DOM type of the type.
        /// </summary>
        public IType ResolveType(IReturnType type)
        {
            return this.ProjectDom.GetType(type);
        }
		
		/// <summary>
		/// Check if the type is defined in a project.
		/// </summary>
		public bool IsInProject(IReturnType type)
		{
			Func<IType, bool> matcher = t => String.Equals(type.FullName, t.FullName);
			return GetMatchingTypes(this.projectDom, matcher).Count() > 0;
		}
		
        public void InternalClearCache()
        {
            this.typeCache.Clear();
        }

        private void InternalRecommputeReferences()
        {
            this.projectDoms = null;
        }

        private FilePath InternalGetMainFile (IType type)
        {
            LoggingService.LogInfo ("GetMainFile '" + type + "'");
            if (type.HasParts) {
                IEnumerable<IType> parts = type.Parts;
                IEnumerable<FilePath> files = parts.Where(t => t.CompilationUnit != null).Select(t => t.CompilationUnit.FileName).OrderBy(f => f.FileName.Length);
                return files.FirstOrDefault();
            } else {
                ICompilationUnit unit = type.CompilationUnit;
                if (unit == null) {
                    return FilePath.Null;
                }
                return unit.FileName;
            }
        }

        private IEnumerable<IType> InternalGetAllClasses (bool projectOnly)
        {
            Func<IType, bool> matcher = t => t.ClassType == ClassType.Class && AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_CLASS);
            return this.GetMatchingTypes(matcher, projectOnly);
        }

        private IEnumerable<IType> InternalGetAllProtocols (bool projectOnly)
        {
            Func<IType, bool> matcher = t => t.ClassType == ClassType.Interface && AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_PROTOCOL);
            return this.GetMatchingTypes(matcher, projectOnly);
        }

        private IEnumerable<IType> InternalGetEntryPoints()
        {
            // Collect types that have a Main static method
            Func<IType, bool> matcher = t => t.Methods.Where(m => m.IsStatic && m.Name == "Main").Count () > 0;
            return this.GetMatchingTypes(matcher, true);
        }

        private IMethod InternalGetMainMethod (IType type)
        {
            return type.Methods.SingleOrDefault (m => m.IsStatic && m.Name == "Main");
        }

        private IType InternalResolveType (Type type)
        {
            return new DomType(type.FullName);
        }

        private IType InternalResolveType (IType type)
        {
            return type;
        }

        private IType InternalResolvePartialType (String className)
        {
            Func<IType, bool> matcher, attributeMatcher;
            IEnumerable<IType> types;
            IType type = null;

            if (this.typeCache.TryGetValue(className, out type)) {
                return type;
            }

            if (type == null) {
                // Search for types matches in classes
                matcher = td => String.Equals (
                    td.Name,
                    className
                );
                types = GetMatchingTypes(matcher, false);
    
                switch (types.Count ()) {
                case 1:
                    type = types.First ();
                    break;
                default:
                    // If there is multiple match, filter by looking at the attributes
                    attributeMatcher = td => AttributeHelper.HasAttribute (
                        td,
                        AttributeHelper.OBJECTIVE_C_CLASS
                    );
                    types = GetMatchingTypes (types, attributeMatcher);
                    if (types.Count () == 1) {
                        type = types.First ();
                    }
                    break;
                }
            }

            if (type == null) {
                // Search for types matches in protocols
                matcher = td => String.Equals (
                    td.Name,
                   "I" + className
                );
                types = GetMatchingTypes(matcher, false);
    
                switch (types.Count ()) {
                case 1:
                    type = types.First ();
                    break;
                default:
                    // If there is multiple match, filter by looking at the attributes
                    attributeMatcher = td => AttributeHelper.HasAttribute (
                        td,
                        AttributeHelper.OBJECTIVE_C_PROTOCOL
                    );
                    types = GetMatchingTypes (types, attributeMatcher);
                    if (types.Count () == 1) {
                        type = types.First ();
                    }
                    break;
                }
            }

            if (type != null) {
                this.typeCache.Add(className, type);
            }

            return type;
        }

        private bool InternalIsInProject (IType type)
        {
            Func<IType, bool> matcher = t => String.Equals(type.FullName, t.FullName);
            return GetMatchingTypes(this.ProjectDom, matcher).Count() > 0;
        }

        private ProjectDom ProjectDom
        {
            get {
                if (this.projectDom == null) {
                    this.projectDom = ProjectDomService.GetProjectDom (this.Project);
                }
                return this.projectDom;
            }
        }

        private IList<ProjectDom> ProjectDoms
        {
            get {
                if (this.projectDoms == null) {
                    this.projectDoms = new List<ProjectDom> ();
                    CollectReferences(this.projectDoms, this.ProjectDom);
                }
                return this.projectDoms;
            }
        }

        private static void CollectReferences (ICollection<ProjectDom> result, ProjectDom dom)
        {
            if (dom == null) {
                return;
            }
            if (result.Contains (dom)) {
                return;
            }
            result.Add (dom);
            foreach (ProjectDom reference in dom.References) {
                CollectReferences (result, reference);
            }
        }
        
        private static IEnumerable<IType> GetMatchingTypes(IEnumerable<IType> typeDefinitions, Func<IType, bool> matcher)
        {
            return typeDefinitions.Where(t => matcher(t));
        }

        private static IEnumerable<IType> GetMatchingTypes(ProjectDom dom, Func<IType, bool> matcher)
        {
            IEnumerable<IType> types = dom.Types;
            return GetMatchingTypes (types, matcher);
        }

        private IEnumerable<IType> GetMatchingTypes (Func<IType, bool> matcher, bool projectOnly)
        {
            // Search only in the project dom
            if (projectOnly && this.ProjectDom != null) {
                this.ProjectDom.ForceUpdate (true);
                return GetMatchingTypes(this.ProjectDom, matcher);
            }
            
            // Search in all the dom (project + references)
            List<IType> result = new List<IType> ();
            foreach (ProjectDom dom in this.ProjectDoms) {
                dom.ForceUpdate (true);
                result.AddRange(GetMatchingTypes(dom, matcher));
            }
            return result;
        }
    }
}
