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

namespace MonoDevelop.Monobjc.Tracking
{
	abstract class ProjectHandler : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectHandler"/> class.
		/// </summary>
		/// <param name="project">The project.</param>
		protected ProjectHandler (MonobjcProject project)
		{
			this.Project = project;

			String sourceFile = this.Project.LanguageBinding.GetFileName ("ABC");
			this.SourceExtension = sourceFile.Substring (3);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public virtual void Dispose ()
		{
		}

		/// <summary>
		/// Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		protected MonobjcProject Project { get; private set; }
		
		/// <summary>
		/// Gets or sets the source extension.
		/// </summary>
		protected String SourceExtension { get; set; }
	}
}
