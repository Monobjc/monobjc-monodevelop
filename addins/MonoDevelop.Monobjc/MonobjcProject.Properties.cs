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
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects;
using MonoDevelop.Monobjc.Tracking;

namespace MonoDevelop.Monobjc
{
	public partial class MonobjcProject
	{
		private IEnumerable<SystemAssembly> monobjcAssemblies;

		/// <summary>
		///   Gets or sets the code behind tracker.
		/// </summary>
		internal ResolverProjectTracker ResolverTracker { get; private set; }

		/// <summary>
		///   Gets or sets the code behind tracker.
		/// </summary>
		//internal CodeBehindProjectTracker CodeBehindTracker { get; private set; }

		/// <summary>
		///   Gets or sets the dependency tracker.
		/// </summary>
		internal DependencyProjectTracker DependencyTracker { get; private set; }
		
		/// <summary>
		///   Gets or sets the embedding tracker.
		/// </summary>
		//internal EmbeddingProjectTracker EmbeddingTracker { get; private set; }

		/// <summary>
		///   Gets or sets the xcode tracker.
		/// </summary>
		//internal XcodeProjectTracker XcodeTracker { get; private set; }

		/// <summary>
		///   Gets the project Monobjc assemblies.
		/// </summary>
		internal IEnumerable<ProjectReference> ProjectMonobjcAssemblies {
			get { return this.References.Where (BuildHelper.IsMonobjcReference); }
		}

		/// <summary>
		///   Returns all the registered Monobjc assemblies.
		/// </summary>
		internal IEnumerable<SystemAssembly> EveryMonobjcAssemblies {
			get { return this.monobjcAssemblies ?? (this.monobjcAssemblies = this.AssemblyContext.GetAssemblies ().Where (BuildHelper.IsMonobjcReference)); }
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		internal void Initialize ()
		{
			IDELogger.Log ("MonobjcProject::Initialize");

			// Set default values
			// TODO: Add more defaults
			if (String.IsNullOrEmpty (this.OSFrameworks)) {
				this.OSFrameworks = "Foundation;AppKit";
			}
			if (this.TargetOSVersion == MacOSVersion.None) {
				this.TargetOSVersion = MacOSVersion.MacOS106;
			}
			if (this.TargetOSArch == MacOSArchitecture.None) {
				this.TargetOSArch = MacOSArchitecture.X86;
			}
			if (String.IsNullOrEmpty (this.DevelopmentRegion)) {
				this.DevelopmentRegion = "en";
			}

			// Create the trackers
			this.ResolverTracker = new ResolverProjectTracker (this);
			this.DependencyTracker = new DependencyProjectTracker (this);
			//this.CodeBehindTracker = new CodeBehindProjectTracker(this);
			//this.XcodeTracker = new XcodeProjectTracker(this);
			//this.EmbeddingTracker = new EmbeddingProjectTracker(this);
		}
	}
}
