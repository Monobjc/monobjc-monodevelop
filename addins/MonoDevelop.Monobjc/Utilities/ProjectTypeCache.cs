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
using MonoDevelop.Core;
using MonoDevelop.Projects;

#if MD_2_6 || MD_2_8
using MonoDevelop.Projects.Dom;
#endif
#if MD_3_0
using ICSharpCode.NRefactory.TypeSystem;
#endif

namespace MonoDevelop.Monobjc.Utilities
{
    public partial class ProjectTypeCache
    {
        private static IDictionary<MonobjcProject, ProjectTypeCache> caches = new Dictionary<MonobjcProject, ProjectTypeCache>();

        /// <summary>
        /// Get the type cache for the given project.
        /// </summary>
        public static ProjectTypeCache Get(MonobjcProject project)
        {
            ProjectTypeCache cache;
            if (!caches.TryGetValue(project, out cache)) {
                cache = new ProjectTypeCache(project);
                caches.Add(project, cache);
            }
            return cache;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Utilities.ProjectTypeCache"/> class.
        /// </summary>
        private ProjectTypeCache(MonobjcProject project)
        {
            this.Project = project;
        }

        public MonobjcProject Project { get; private set; }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            this.InternalClearCache();
        }

        /// <summary>
        /// Recomputes the reference types.
        /// </summary>
        public void RecomputeReferences()
        {
            this.InternalClearCache();
            this.InternalRecommputeReferences();
        }

        /// <summary>
        /// Gets the main file for the type
        /// </summary>
        public FilePath GetMainFile (IType type)
        {
            return this.InternalGetMainFile(type);
        }

        /// <summary>
        ///   Gets all the known classes.
        /// </summary>
        public IEnumerable<IType> GetAllClasses (bool projectOnly)
        {
            return this.InternalGetAllClasses(projectOnly);
        }

        /// <summary>
        ///   Gets all the known protocols.
        /// </summary>
        public IEnumerable<IType> GetAllProtocols (bool projectOnly)
        {
            return this.InternalGetAllProtocols(projectOnly);
        }

        /// <summary>
        ///   Resolves the entry points of the project.
        /// </summary>
        public IEnumerable<IType> GetEntryPoints()
        {
            return this.InternalGetEntryPoints();
        }

        /// <summary>
        /// Gets the main method of the given type.
        /// </summary>
        public IMethod GetMainMethod (IType type)
        {
            return this.InternalGetMainMethod(type);
        }

        /// <summary>
        /// Gets the DOM type of the type.
        /// </summary>
        public IType ResolveType (Type type)
        {
            return this.InternalResolveType(type);
        }

        /// <summary>
        /// Gets the DOM type of the type.
        /// </summary>
        public IType ResolveType (IType type)
        {
            return this.InternalResolveType(type);
        }

        /// <summary>
        ///   Resolves the specified class name.
        /// </summary>
        public IType ResolvePartialType (String className)
        {
            // Make sure that the first character is upper-case
            if (String.Equals (className, "id", StringComparison.OrdinalIgnoreCase)) {
                className = "Id";
            }

            return this.InternalResolvePartialType(className);
        }

        /// <summary>
        /// Check if the type is defined in a project.
        /// </summary>
        public bool IsInProject (IType type)
        {
            return this.InternalIsInProject(type);
        }
    }
}
