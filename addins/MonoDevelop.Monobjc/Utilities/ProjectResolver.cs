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
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Utilities
{
	/// <summary>
	///   Type resolver for CodeBehind generation.
	/// </summary>
	public class ProjectResolver
	{
		private readonly List<ProjectDom> projectDoms;

		/// <summary>
		///   Initializes a new instance of the <see cref = "CodeBehindResolver" /> class.
		/// </summary>
		/// <param name = "project">The project.</param>
		public ProjectResolver (MonobjcProject project)
		{
			this.Project = project;
			ProjectDom projectDom = ProjectDomService.GetProjectDom (this.Project);
			this.projectDoms = new List<ProjectDom> ();
			CollectReference (this.projectDoms, projectDom);
		}

		/// <summary>
		///   Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		public MonobjcProject Project { get; private set; }

		/// <summary>
		/// Gets all the known classes.
		/// </summary>
		/// <returns>
		/// A <see cref="IEnumerable<IType>"/> list.
		/// </returns>
		public IEnumerable<IType> GetAllClasses ()
		{
			List<IType> result = new List<IType> ();
			foreach (ProjectDom projectDom in this.projectDoms) {
				IEnumerable<IType> types = projectDom.Types.Where (t => t.ClassType == ClassType.Class && t.Attributes != null && t.Attributes.Any (a => String.Equals ("Monobjc.ObjectiveCClassAttribute", a.AttributeType.FullName)));
				if (types != null) {
					result.AddRange (types);
				}
			}
			return result;
		}

		/// <summary>
		/// Gets all the known protocol interfaces.
		/// </summary>
		/// <returns>
		/// A <see cref="IEnumerable<IType>"/> list.
		/// </returns>
		public IEnumerable<IType> GetAllProtocols ()
		{
			List<IType> result = new List<IType> ();
			foreach (ProjectDom projectDom in this.projectDoms) {
				IEnumerable<IType> types = projectDom.Types.Where (t => t.ClassType == ClassType.Interface && t.Attributes != null && t.Attributes.Any (a => String.Equals ("Monobjc.ObjectiveCProtocolAttribute", a.AttributeType.FullName)));
				if (types != null) {
					result.AddRange (types);
				}
			}
			return result;
		}

		/// <summary>
		///   Resolves the specified class name.
		/// </summary>
		/// <param name = "className">Name of the class.</param>
		/// <returns></returns>
		public IType ResolvePartialType (String className)
		{
			// Make sure that the first character is upper-case
			if (String.Equals (className, "id", StringComparison.OrdinalIgnoreCase)) {
				className = "Id";
			}
			
			// Search for types matches in classes
			IEnumerable<IType> types = this.InternalResolve (className);
			switch (types.Count ()) {
			case 1:
				return types.First ();
			default:
				// If there is multiple match, filter by looking at the attributes
				IEnumerable<IType> candidates = types.Where (t => t.Attributes != null && t.Attributes.Any (a => String.Equals ("Monobjc.ObjectiveCClassAttribute", a.AttributeType.FullName)));
				if (candidates.Count () == 1) {
					return candidates.First ();
				}
				break;
			}
			
			// Search for types matches in protocols
			types = this.InternalResolve ("I" + className);
			switch (types.Count ()) {
			case 1:
				return types.First ();
			default:
				// If there is multiple match, filter by looking at the attributes
				IEnumerable<IType> candidates = types.Where (t => t.Attributes != null && t.Attributes.Any (a => String.Equals ("Monobjc.ObjectiveCProtocolAttribute", a.AttributeType.FullName)));
				if (candidates.Count () == 1) {
					return candidates.First ();
				}
				break;
			}
			
			return null;
		}

		/// <summary>
		///   Resolves the entry points of the project.
		/// </summary>
		/// <returns>A List of types.</returns>
		public IEnumerable<IType> ResolveEntryPoints ()
		{
			List<IType> result = new List<IType> ();
			if (this.projectDoms.Count > 0) {
				// Note: The first ProjectDOM is the project one
				ProjectDom projectDom = this.projectDoms[0];
				
				// Collect types that have a Main static method
				IEnumerable<IType> types = from t in projectDom.Types
					where (from m in t.Methods
						where m.IsStatic && m.Name == "Main"
						select m).Count () > 0
					select t;
				if (types != null) {
					result.AddRange (types);
				}
			}
			return result;
		}

		/// <summary>
		///   Search for a type that has the given name.
		/// </summary>
		/// <param name = "className">Name of the class.</param>
		/// <returns>A list of candidates.</returns>
		private IEnumerable<IType> InternalResolve (String className)
		{
			List<IType> result = new List<IType> ();
			foreach (ProjectDom projectDom in this.projectDoms) {
				IEnumerable<IType> types = projectDom.Types.Where (t => t.Name.Equals (className));
				if (types != null) {
					result.AddRange (types);
				}
			}
			return result;
		}

		/// <summary>
		///   Collects the references of a project DOM.
		/// </summary>
		/// <param name = "result">The result list.</param>
		/// <param name = "projectDom">The project DOM.</param>
		private static void CollectReference (List<ProjectDom> result, ProjectDom projectDom)
		{
			if (projectDom == null) {
				return;
			}
			if (result.Contains (projectDom)) {
				return;
			}
			result.Add (projectDom);
			foreach (ProjectDom reference in projectDom.References) {
				CollectReference (result, reference);
			}
		}
	}
}
