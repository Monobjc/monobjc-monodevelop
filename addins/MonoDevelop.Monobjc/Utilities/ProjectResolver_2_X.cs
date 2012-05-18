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
	/// <summary>
	/// Event arguments for type update/deletion.
	/// </summary>
	
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
			ProjectDomService.TypesUpdated += HandleProjectDomServiceTypesUpdated;
		}
		
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
			Func<IType, bool> matcher = t => t.ClassType == ClassType.Class && AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_CLASS);
			return this.GetMatchingTypes(matcher, projectOnly);
		}

		/// <summary>
		///   Gets all the known protocol interfaces.
		/// </summary>
		public IEnumerable<IType> GetAllProtocols (bool projectOnly)
		{
			Func<IType, bool> matcher = t => t.ClassType == ClassType.Interface && AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_PROTOCOL);
			return this.GetMatchingTypes(matcher, projectOnly);
		}

		/// <summary>
		///   Resolves the entry points of the project.
		/// </summary>
		/// <returns>A List of types.</returns>
		public IEnumerable<IType> GetEntryPoints ()
		{
			// Collect types that have a Main static method
			Func<IType, bool> matcher = t => t.Methods.Where(m => m.IsStatic && m.Name == "Main").Count () > 0;
			return this.GetMatchingTypes(matcher, true);
		}
		
		/// <summary>
		/// Gets the main file for the type
		/// </summary>
		public FilePath GetMainFile(IType type)
		{
			LoggingService.LogInfo ("GetMainFile '" + type + "'");
			if (type.HasParts) {
				IEnumerable<IType> parts = type.Parts;
				IEnumerable<FilePath> files = parts.Where(t => t.CompilationUnit != null).Select(t => t.CompilationUnit.FileName).OrderBy(f => f.FileName.Length);
				foreach(FilePath file in files) {
					LoggingService.LogInfo ("GetMainFile => '" + file + "'");
				}
				return files.FirstOrDefault();
			} else {
				ICompilationUnit unit = type.CompilationUnit;
				if (unit == null) {
					return FilePath.Null;
				}
				LoggingService.LogInfo ("GetMainFile => " + unit.FileName);
				return unit.FileName;
			}
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
				Func<IType, bool> matcher = t => AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_CLASS);
				IEnumerable<IType> candidates = GetMatchingTypes(types, matcher);
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
				Func<IType, bool> matcher = t => AttributeHelper.HasAttribute(t, AttributeHelper.OBJECTIVE_C_PROTOCOL);
				IEnumerable<IType> candidates = GetMatchingTypes(types, matcher);
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
			return type.Methods.SingleOrDefault (m => m.IsStatic && m.Name == "Main");
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType(Type type)
		{
			if (type == typeof(IntPtr)) {
				return new DomType("System.IntPtr");
			}
			return null;
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType(IReturnType type)
		{
			return this.projectDom.GetType(type);
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
		public bool IsInProject(IReturnType type)
		{
			Func<IType, bool> matcher = t => String.Equals(type.FullName, t.FullName);
			return GetMatchingTypes(this.projectDom, matcher).Count() > 0;
		}
		
		/// <summary>
		/// Check if the type is defined in a project.
		/// </summary>
		public bool IsInProject(IType type)
		{
			Func<IType, bool> matcher = t => String.Equals(type.FullName, t.FullName);
			return GetMatchingTypes(this.projectDom, matcher).Count() > 0;
		}
		
		/// <summary>
		///   Search for a type that has the given name.
		/// </summary>
		/// <param name = "className">Name of the class.</param>
		/// <returns>A list of candidates.</returns>
		private IEnumerable<IType> InternalResolve (String className)
		{
			Func<IType, bool> matcher = td => String.Equals(td.Name, className);
			return this.GetMatchingTypes(matcher, false);
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
		private static IEnumerable<IType> GetMatchingTypes(IEnumerable<IType> typeDefinitions, Func<IType, bool> matcher)
		{
			return typeDefinitions.Where(t => matcher(t));
		}

		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private static IEnumerable<IType> GetMatchingTypes(ProjectDom dom, Func<IType, bool> matcher)
		{
			IEnumerable<IType> types = dom.Types;
			return GetMatchingTypes (types, matcher);
		}

		/// <summary>
		/// Gets the matching types.
		/// </summary>
		private IEnumerable<IType> GetMatchingTypes (Func<IType, bool> matcher, bool projectOnly)
		{
			// Search only in the project dom
			if (projectOnly && this.projectDom != null) {
				this.projectDom.ForceUpdate (true);
				return GetMatchingTypes(this.projectDom, matcher);
			}
			
			// Search in all the dom (project + references)
			List<IType> result = new List<IType> ();
			foreach (ProjectDom dom in this.projectDoms) {
				dom.ForceUpdate (true);
				result.AddRange(GetMatchingTypes(dom, matcher));
			}
			return result;
		}
		
		
		/// <summary>
		/// Called when a project is updated.
		/// </summary>
		private static void HandleProjectDomServiceTypesUpdated (object sender, TypeUpdateInformationEventArgs e)
		{
			Project project = e.Project;
			
			IList<IType> typesUpdated = new List<IType> ();
			IList<IType> typesDeleted = new List<IType> ();
			
			foreach (IType type in e.TypeUpdateInformation.Added) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
	            LoggingService.LogInfo("Projectresolver::TypeAdded " + type.FullName);
				typesUpdated.Add (type);
			}
			foreach (IType type in e.TypeUpdateInformation.Modified) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
	            LoggingService.LogInfo("Projectresolver::Modified " + type.FullName);
				typesUpdated.Add (type);
			}
			foreach (IType type in e.TypeUpdateInformation.Removed) {
				if (!AttributeHelper.HasAttribute (type, AttributeHelper.OBJECTIVE_C_CLASS)) {
					continue;
				}
	            LoggingService.LogInfo("Projectresolver::Removed " + type.FullName);
				typesDeleted.Add (type);
			}
			
			if (TypesUpdated != null) {
				TypesUpdatedEventArgs args = new TypesUpdatedEventArgs(project, typesUpdated, typesDeleted);
				TypesUpdated(null, args);
			}
		}
	}
}
