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
		private static IDictionary<MonobjcProject, ProjectTypeCache> caches = new Dictionary<MonobjcProject, ProjectTypeCache> ();
		private TypeSystemService.ProjectContentWrapper projectWrapper;
		private List<TypeSystemService.ProjectContentWrapper> projectWrappers;
		private IDictionary<String, IType> typeCache = new Dictionary<String, IType> ();

		static ProjectTypeCache ()
		{
			TypeSystemService.ProjectUnloaded += HandleProjectUnloaded;
		}
		
		/// <summary>
		/// Get the type cache for the given project.
		/// </summary>
		public static ProjectTypeCache Get (MonobjcProject project)
		{
			ProjectTypeCache cache;
			if (!caches.TryGetValue (project, out cache)) {
				cache = new ProjectTypeCache (project);
				caches.Add (project, cache);
			}
			return cache;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoDevelop.Monobjc.Utilities.ProjectTypeCache"/> class.
		/// </summary>
		private ProjectTypeCache (MonobjcProject project)
		{
			this.Project = project;
		}

		/// <summary>
		/// Gets the project.
		/// </summary>
		public MonobjcProject Project { get; private set; }
		
		/// <summary>
		/// Clears the cache.
		/// </summary>
		public void ClearCache ()
		{
			this.typeCache.Clear ();
		}
		
		/// <summary>
		/// Recomputes the reference types.
		/// </summary>
		public void RecomputeReferences ()
		{
			this.typeCache.Clear ();
			this.projectWrappers = null;
		}
		
		/// <summary>
		/// Gets the main file for the type
		/// </summary>
		public FilePath GetMainFile (IType type)
		{
			if (type == null) {
				return FilePath.Null;
			}
			ITypeDefinition definition = type.GetDefinition ();
			IEnumerable<IUnresolvedTypeDefinition> parts = definition.Parts;
			IEnumerable<String> files = parts.Select (td => td.Region.FileName).OrderBy (s => s.Length);
			return (FilePath)files.FirstOrDefault ();
		}
		
		/// <summary>
		///   Gets all the known classes.
		/// </summary>
		public IEnumerable<IType> GetAllClasses (bool projectOnly)
		{
			Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Class && AttributeHelper.HasAttribute (td, AttributeHelper.OBJECTIVE_C_CLASS);
			IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, projectOnly);
			return ConvertTo (typeDefinitions);
		}
		
		/// <summary>
		///   Gets all the known protocols.
		/// </summary>
		public IEnumerable<IType> GetAllProtocols (bool projectOnly)
		{
			Func<ITypeDefinition, bool> matcher = td => td.Kind == TypeKind.Interface && AttributeHelper.HasAttribute (td, AttributeHelper.OBJECTIVE_C_PROTOCOL);
			IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, projectOnly);
			return ConvertTo (typeDefinitions);
		}
		
		/// <summary>
		///   Resolves the entry points of the project.
		/// </summary>
		public IEnumerable<IType> GetEntryPoints ()
		{
			// Collect types that have a Main static method
			Func<ITypeDefinition, bool> matcher = td => td.GetMethods (m => m.IsStatic && m.Name == "Main", GetMemberOptions.ReturnMemberDefinitions).Count () > 0;
			IEnumerable<ITypeDefinition> typeDefinitions = this.GetMatchingTypeDefinitions (matcher, true);
			return ConvertTo (typeDefinitions);
		}
		
		/// <summary>
		/// Gets the main method of the given type.
		/// </summary>
		public IMethod GetMainMethod (IType type)
		{
			return type.GetMethods (m => m.IsStatic && m.Name == "Main").SingleOrDefault ();
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType (Type type)
		{
			ITypeReference typeReference = ReflectionHelper.ToTypeReference (type);
			return typeReference.Resolve (this.ProjectWrapper.Compilation);
		}
		
		/// <summary>
		/// Gets the DOM type of the type.
		/// </summary>
		public IType ResolveType (IType type)
		{
			return type;
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
			
			Func<ITypeDefinition, bool> matcher, attributeMatcher;
			IEnumerable<ITypeDefinition> typeDefinitions;
			ITypeDefinition typeDefinition = null;
			IType type = null;
			
			if (this.typeCache.TryGetValue (className, out type)) {
				return type;
			}
			
			if (typeDefinition == null) {
				// Search for types matches in classes
				matcher = td => String.Equals (
					td.Name,
					className
				);
				typeDefinitions = GetMatchingTypeDefinitions (matcher, false);
				
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
				typeDefinitions = GetMatchingTypeDefinitions (matcher, false);
				
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
				this.typeCache.Add (className, type);
			}
			
			return type;
		}
		
		/// <summary>
		/// Check if the type is defined in a project.
		/// </summary>
		public bool IsInProject (IType type)
		{
			ITypeDefinition definition = type.GetDefinition ();
			if (definition == null) {
				return false;
			}
			return definition.ParentAssembly.Equals (this.ProjectWrapper.Compilation.MainAssembly);
		}

		private static void HandleProjectUnloaded (object sender, ProjectUnloadEventArgs e)
		{
			MonobjcProject project = e.Project as MonobjcProject;
			if (project != null) {
				caches.Remove (project);
			}
		}

		private TypeSystemService.ProjectContentWrapper ProjectWrapper {
			get {
				if (this.projectWrapper == null) {
					this.projectWrapper = TypeSystemService.GetProjectContentWrapper (this.Project);
				}
				return this.projectWrapper;
			}
		}

		private IList<TypeSystemService.ProjectContentWrapper> ProjectWrappers {
			get {
				if (this.projectWrappers == null) {
					this.projectWrappers = new List<TypeSystemService.ProjectContentWrapper> ();
					CollectReferences (this.projectWrappers, this.ProjectWrapper);
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
			IEnumerable<ITypeDefinition> typeDefinitions = this.ProjectWrapper.Compilation.GetAllTypeDefinitions ();
			return GetMatchingTypeDefinitions (typeDefinitions, matcher);
		}
	}
}
