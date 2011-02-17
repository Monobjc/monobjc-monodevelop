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
using Monobjc.Tools.InterfaceBuilder;
using MonoDevelop.Core;
using MonoDevelop.DesignerSupport;
using MonoDevelop.Monobjc.Utilities;

namespace MonoDevelop.Monobjc.CodeGeneration
{
	/// <summary>
	///   Base contract for a code behind generator.
	/// </summary>
	public interface ICodeBehindGenerator
	{
		/// <summary>
		///   Generates the design code for framework loading.
		/// </summary>
		/// <param name = "resolver">The type resolver.</param>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>The path to the designer file.</returns>
		FilePath GenerateDesignCodeForFrameworks (ProjectResolver resolver, String[] frameworks);

		/// <summary>
		///   Generates the design code for an Interface Builder file.
		/// </summary>
		/// <param name = "resolver">The type resolver.</param>
		/// <param name = "writer">The writer.</param>
		/// <param name = "className">Name of the class.</param>
		/// <param name = "enumerable">The class descriptions.</param>
		/// <returns>The path to the designer file.</returns>
		FilePath GenerateDesignCodeForIB (ProjectResolver resolver, CodeBehindWriter writer, String className, IEnumerable<IBPartialClassDescription> enumerable);
	}
}
