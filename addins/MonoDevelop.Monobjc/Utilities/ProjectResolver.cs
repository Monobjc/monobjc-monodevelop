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
	///   Type resolver for a Monobjc project.
	/// </summary>
	public class ProjectResolver
	{
		private ProjectDom projectDom;
		private List<ProjectDom> projectDoms;

		/// <summary>
		///   Initializes a new instance of the <see cref = "ProjectResolver" /> class.
		/// </summary>
		/// <param name = "project">The project.</param>
		public ProjectResolver (MonobjcProject project)
		{
			this.Project = project;
			this.projectDom = ProjectDomService.GetProjectDom (this.Project);
			this.projectDoms = new List<ProjectDom> ();
			CollectReference (this.projectDoms, this.projectDom);
		}

		/// <summary>
		///   Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		public MonobjcProject Project { get; private set; }

		/// <summary>
		///   Gets all the known classes.
		/// </summary>
		public IEnumerable<IType> GetAllClasses ()
		{
			return this.GetAllClasses (false);
		}

		/// <summary>
		///   Gets all the known protocol interfaces.
		/// </summary>
		public IEnumerable<IType> GetAllProtocols ()
		{
			return GetAllProtocols (false);
		}

		/// <summary>
		///   Gets all the known classes.
		/// </summary>
		public IEnumerable<IType> GetAllClasses (bool projectOnly)
		{
			Func<ProjectDom, IEnumerable<IType>> matcher = dom => from t in dom.Types
				where t.ClassType == ClassType.Class && t.Attributes != null && t.Attributes.Any (a => String.Equals (AttributeHelper.OBJECTIVE_C_CLASS, a.AttributeType.FullName))
				select t;
			return GetMatchingTypes (matcher, projectOnly);
		}

		/// <summary>
		///   Gets all the known protocol interfaces.
		/// </summary>
		public IEnumerable<IType> GetAllProtocols (bool projectOnly)
		{
			Func<ProjectDom, IEnumerable<IType>> matcher = dom => from t in dom.Types
				where t.ClassType == ClassType.Interface && t.Attributes != null && t.Attributes.Any (a => String.Equals (AttributeHelper.OBJECTIVE_C_PROTOCOL, a.AttributeType.FullName))
				select t;
			return GetMatchingTypes (matcher, projectOnly);
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
				IEnumerable<IType> candidates = from t in types
					where t.Attributes != null && t.Attributes.Any (a => String.Equals (AttributeHelper.OBJECTIVE_C_CLASS, a.AttributeType.FullName))
					select t;
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
				IEnumerable<IType> candidates = from t in types
					where t.Attributes != null && t.Attributes.Any (a => String.Equals (AttributeHelper.OBJECTIVE_C_PROTOCOL, a.AttributeType.FullName))
					select t;
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
			// Collect types that have a Main static method
			this.projectDom.ForceUpdate (true);
			IEnumerable<IType> types = from t in this.projectDom.Types
				where (from m in t.Methods
					where m.IsStatic && m.Name == "Main"
					select m).Count () > 0
				select t;
			return types;
		}

		/// <summary>
		///   Search for a type that has the given name.
		/// </summary>
		/// <param name = "className">Name of the class.</param>
		/// <returns>A list of candidates.</returns>
		private IEnumerable<IType> InternalResolve (String className)
		{
			List<IType> result = new List<IType> ();
			foreach (ProjectDom dom in this.projectDoms) {
				dom.ForceUpdate (true);
				IEnumerable<IType> types = from t in dom.Types
					where String.Equals (t.Name, className)
					select t;
				result.AddRange (types);
			}
			return result;
		}

		/// <summary>
		///   Collects the references of a project DOM.
		/// </summary>
		/// <param name = "result">The result list.</param>
		/// <param name = "dom">The project DOM.</param>
		private static void CollectReference (ICollection<ProjectDom> result, ProjectDom dom)
		{
			if (dom == null) {
				return;
			}
			if (result.Contains (dom)) {
				return;
			}
			result.Add (dom);
			foreach (ProjectDom reference in dom.References) {
				CollectReference (result, reference);
			}
		}

		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private IEnumerable<IType> GetMatchingTypes (Func<ProjectDom, IEnumerable<IType>> matcher, bool projectOnly)
		{
			// Search only in the project dom
			if (projectOnly && this.projectDom != null) {
				return matcher (this.projectDom);
			}
			
			// Search in all the dom (project + references)
			List<IType> result = new List<IType> ();
			foreach (ProjectDom dom in this.projectDoms) {
				IEnumerable<IType> types = matcher (dom);
				result.AddRange (types);
			}
			return result;
		}
	}
}
