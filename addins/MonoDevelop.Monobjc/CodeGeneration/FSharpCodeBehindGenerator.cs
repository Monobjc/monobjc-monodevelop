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
using System.CodeDom;
using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;

namespace MonoDevelop.Monobjc.CodeGeneration
{
	/// <summary>
	///   VBNet implementation for a code-behind generator.
	/// </summary>
	public class FSharpCodeBehindGenerator : BaseCodeBehindGenerator
	{
		/// <summary>
		///   Determines whether a line is a region delimiter.
		/// </summary>
		/// <param name = "line">The line.</param>
		/// <param name = "start">if set to <c>true</c>, check for a region start.</param>
		/// <returns>
		///   <c>true</c> if the line is a region delimiter; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IsDesignerRegionDelimiter (String line, bool start)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///   Generates the framework loading code which is language specific.
		/// </summary>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>A list of lines for the code.</returns>
		protected override IEnumerable<String> GenerateFrameworkLoadingcode (String[] frameworks)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///   Generates the partial method for an action.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "argumentType">Type of the argument.</param>
		/// <returns>The type member.</returns>
		protected override CodeTypeMember GenerateActionPartialMethod (string message, IType argumentType)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Generates the "GetInstanceVariable" statement.
		/// </summary>
		/// <returns>The "GetInstanceVariable" statement.</returns>
		/// <param name="thisRef">This reference.</param>
		/// <param name="typeRef">Type reference.</param>
		/// <param name="nameRef">Name reference.</param>
		protected override CodeStatement GenerateGetInstanceVariableStatement (CodeThisReferenceExpression thisRef, CodeTypeReference typeRef, CodePrimitiveExpression nameRef)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Generates the "SetInstanceVariable" statement.
		/// </summary>
		/// <returns>The "SetInstanceVariable" statement.</returns>
		/// <param name="thisRef">This reference.</param>
		/// <param name="typeRef">Type reference.</param>
		/// <param name="nameRef">Name reference.</param>
		/// <param name="valueRef">Value reference</param>
		protected override CodeExpression GenerateSetInstanceVariableStatement (CodeThisReferenceExpression thisRef, CodeTypeReference typeRef, CodePrimitiveExpression nameRef, CodePropertySetValueReferenceExpression valueRef)
		{
			throw new NotImplementedException();
		}
	}
}