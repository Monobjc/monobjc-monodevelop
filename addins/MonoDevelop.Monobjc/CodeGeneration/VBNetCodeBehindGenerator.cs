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

#if MD_2_6 || MD_2_8
using MonoDevelop.Projects.Dom;
#endif
#if MD_3_0
using ICSharpCode.NRefactory.TypeSystem;
#endif

namespace MonoDevelop.Monobjc.CodeGeneration
{
	/// <summary>
	///   VBNet implementation for a code-behind generator.
	/// </summary>
	public class VBNetCodeBehindGenerator : BaseCodeBehindGenerator
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
			if (start) {
				return (line.Contains ("#Region") && line.Contains ("Monobjc Generated Code"));
			} else {
				return (line.Contains ("#End Region"));
			}
		}

		/// <summary>
		///   Generates the framework loading code which is language specific.
		/// </summary>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>A list of lines for the code.</returns>
		protected override IEnumerable<String> GenerateFrameworkLoadingcode (String[] frameworks)
		{
			List<String> lines = new List<String> ();

			lines.Add ("			'#Region \"--- Monobjc Generated Code ---\"");
			lines.Add ("			'");
			lines.Add ("			' DO NOT ALTER OR REMOVE");
			lines.Add ("			'");
			foreach (String framework in frameworks) {
				lines.Add ("			ObjectiveCRuntime.LoadFramework(\"" + framework + "\")");
			}
			lines.Add ("			'#End Region");

			return lines;
		}

		/// <summary>
		///   Generates the partial method for an action.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "argumentType">Type of the argument.</param>
		/// <returns>The type member.</returns>
		protected override CodeTypeMember GenerateActionPartialMethod (string message, IType argumentType)
		{
			String selector = message;
			String name = GenerateMethodName (selector);

			// Partial method are only possible by using a snippet of code as CodeDom does not handle them
			String content = String.Format ("Partial Private Sub {0}({1} sender){2}End Sub{2}", name, argumentType.Name, Environment.NewLine);
			CodeSnippetTypeMember method = new CodeSnippetTypeMember (content);

			return method;
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
			String content = String.Format ("Return Me.GetInstanceVariable(Of {0})(\"{1}\")", typeRef.BaseType, nameRef.Value);
			CodeSnippetStatement statement = new CodeSnippetStatement (content);
			return statement;
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
			String content = String.Format ("Me.SetInstanceVariable(Of {0})(\"{1}\", value)", typeRef.BaseType, nameRef.Value);
			CodeSnippetExpression expression = new CodeSnippetExpression(content);
			return expression;
		}
	}
}