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
#if MD_3_0
using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Core;
using MonoDevelop.Projects;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using MonoDevelop.Ide.TypeSystem;
using ProjectDom = MonoDevelop.Ide.TypeSystem.TypeSystemService.ProjectContentWrapper;

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
		/// Initializes the <see cref="ProjectResolver"/> class.
		/// </summary>
		static ProjectResolver() {
			// TODO
		}
		
		/// <summary>
		///   Initializes a new instance of the <see cref = "ProjectResolver" /> class.
		/// </summary>
		/// <param name = "project">The project.</param>
		public ProjectResolver (MonobjcProject project)
		{
			this.Project = project;
			this.projectDom = TypeSystemService.GetProjectContentWrapper (this.Project);
			this.projectDoms = new List<ProjectDom> ();
			CollectReference (this.projectDoms, this.projectDom);
		}

		/// <summary>
		///   Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		public MonobjcProject Project { get; private set; }
		
		/// <summary>
		/// Occurs when types updated.
		/// </summary>
		public static event EventHandler<TypesUpdatedEventArgs> TypesUpdated;
		
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
			Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Class && AttributeHelper.HasAttribute(td, AttributeHelper.OBJECTIVE_C_CLASS);
			IEnumerable<ITypeDefinition> typeDefinitions =  this.GetMatchingTypeDefinitions(matcher, projectOnly);
			return ConvertTo(typeDefinitions);
		}

		/// <summary>
		///   Gets all the known protocol interfaces.
		/// </summary>
		public IEnumerable<IType> GetAllProtocols (bool projectOnly)
		{
			Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Interface && AttributeHelper.HasAttribute(td, AttributeHelper.OBJECTIVE_C_PROTOCOL);
			IEnumerable<ITypeDefinition> typeDefinitions =  this.GetMatchingTypeDefinitions(matcher, projectOnly);
			return ConvertTo(typeDefinitions);
		}

		/// <summary>
		///   Resolves the entry points of the project.
		/// </summary>
		/// <returns>A List of types.</returns>
		public IEnumerable<IType> GetEntryPoints ()
		{
			// Collect types that have a Main static method
			Func<ITypeDefinition, bool> matcher = td => td.GetMethods(m => m.IsStatic && m.Name == "Main", GetMemberOptions.ReturnMemberDefinitions).Count () > 0;
			IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions(matcher, false);
			return ConvertTo(typeDefinitions);
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
				Func<ITypeDefinition, bool> matcher = td => AttributeHelper.HasAttribute(td, AttributeHelper.OBJECTIVE_C_CLASS);
				IEnumerable<ITypeDefinition> typeDefinitions = ConvertTo(types);
				typeDefinitions = GetMatchingTypeDefinitions(typeDefinitions, matcher);
				IEnumerable<IType> candidates = ConvertTo(typeDefinitions);
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
				Func<ITypeDefinition, bool> matcher = td => AttributeHelper.HasAttribute(td, AttributeHelper.OBJECTIVE_C_PROTOCOL);
				IEnumerable<ITypeDefinition> typeDefinitions = ConvertTo(types);
				typeDefinitions = GetMatchingTypeDefinitions(typeDefinitions, matcher);
				IEnumerable<IType> candidates = ConvertTo(typeDefinitions);
				if (candidates.Count () == 1) {
					return candidates.First ();
				}
				break;
			}
			
			return null;
		}

		/// <summary>
		/// Gets the main method of the given type.
		/// </summary>
		public IMethod GetMainMethod(IType type)
		{
			return type.GetMethods(m => m.IsStatic && m.Name == "Main").SingleOrDefault();
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType(Type type)
		{
			if (type == typeof(IntPtr)) {
				ITypeReference typeReference = new GetClassTypeReference("System", "IntPtr");
				return typeReference.Resolve(this.projectDom.Compilation);
			}
			return null;
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType(ITypeReference type)
		{
			return type.Resolve(this.projectDom.Compilation);
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType(IType type)
		{
			return type;
		}
		
		/// <summary>
		/// Check if the type is defined in a project.
		/// </summary>
		public bool IsInProject(ITypeReference typeReference)
		{
			IType type = this.ResolveType(typeReference);
			return this.IsInProject(type);
		}
		
		/// <summary>
		/// Check if the type is defined in a project.
		/// </summary>
		public bool IsInProject(IType type)
		{
			Func<ITypeDefinition, bool> matcher = td => String.Equals(type.FullName, td.FullName);
			return GetMatchingTypeDefinitions(this.projectDom, matcher).Count() > 0;
		}
		
		/// <summary>
		///   Search for a type that has the given name.
		/// </summary>
		/// <param name = "className">Name of the class.</param>
		/// <returns>A list of candidates.</returns>
		private IEnumerable<IType> InternalResolve (String className)
		{
			Func<ITypeDefinition, bool> matcher = td => String.Equals(td.Name, className);
			IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions(matcher, false);
			return ConvertTo(typeDefinitions);
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
			foreach (Project project in dom.ReferencedProjects) {
				ProjectDom reference = TypeSystemService.GetProjectContentWrapper (project);
				CollectReference (result, reference);
			}
		}

		/// <summary>
		/// Converts from type defintions to resolved types.
		/// </summary>
		private IEnumerable<IType> ConvertTo(IEnumerable<ITypeDefinition> typeDefinitions)
		{
			return typeDefinitions.Select(td => td.ToTypeReference().Resolve(this.projectDom.Compilation));
		}
		
		/// <summary>
		/// Converts from type defintions to resolved types.
		/// </summary>
		private IEnumerable<ITypeDefinition> ConvertTo(IEnumerable<IType> types)
		{
			return types.Select(t => t.GetDefinition());
		}
		
		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private static IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions(IEnumerable<ITypeDefinition> typeDefinitions, Func<ITypeDefinition, bool> matcher)
		{
			return typeDefinitions.Where(td => matcher(td));
		}

		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private static IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions(ProjectDom dom, Func<ITypeDefinition, bool> matcher)
		{
			IEnumerable<ITypeDefinition> typeDefinitions = dom.Compilation.GetAllTypeDefinitions();
			return GetMatchingTypeDefinitions (typeDefinitions, matcher);
		}

		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private IEnumerable<ITypeDefinition> GetMatchingTypeDefinitions (Func<ITypeDefinition, bool> matcher, bool projectOnly)
		{
			// Search only in the project dom
			if (projectOnly && this.projectDom != null) {
				TypeSystemService.ForceUpdate(this.projectDom);
				return GetMatchingTypeDefinitions(this.projectDom, matcher);
			}
			
			// Search in all the dom (project + references)
			List<ITypeDefinition> result = new List<ITypeDefinition> ();
			foreach (ProjectDom dom in this.projectDoms) {
				TypeSystemService.ForceUpdate(dom);
				result.AddRange(GetMatchingTypeDefinitions(dom, matcher));
			}
			return result;
		}
	}
}
#endif
