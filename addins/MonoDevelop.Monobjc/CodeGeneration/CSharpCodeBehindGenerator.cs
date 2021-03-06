﻿//
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
	///   C# implementation for a code-behind generator.
	/// </summary>
	public class CSharpCodeBehindGenerator : BaseCodeBehindGenerator
	{
		/// <summary>
		/// Gets a value indicating whether this generator supports partial classes.
		/// </summary>
		/// <value>
		/// <c>true</c> if support partial classes; otherwise, <c>false</c>.
		/// </value>
		public override bool SupportPartialClasses {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this generator support partial methods.
		/// </summary>
		/// <value>
		/// <c>true</c> if support partial methods; otherwise, <c>false</c>.
		/// </value>
		public override bool SupportPartialMethods {
			get {
				return true;
			}
		}

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
				return (line.Contains ("#region") && line.Contains ("Monobjc Generated Code"));
			}
			return (line.Contains ("#endregion"));
		}

		/// <summary>
		///   Generates the framework loading code which is language specific.
		/// </summary>
		/// <param name = "frameworks">The frameworks.</param>
		/// <returns>A list of lines for the code.</returns>
		protected override IEnumerable<String> GenerateFrameworkLoadingcode (String[] frameworks)
		{
			List<String> lines = new List<String> ();

			lines.Add ("			#region --- Monobjc Generated Code ---");
			lines.Add ("			//");
			lines.Add ("			// DO NOT ALTER OR REMOVE");
			lines.Add ("			//");
			foreach (String framework in frameworks) {
				lines.Add ("			ObjectiveCRuntime.LoadFramework(\"" + framework + "\");");
			}
			lines.Add ("			#endregion");

			return lines;
		}

		/// <summary>
		/// Generates the partial method for an action.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "argumentType">Type of the argument.</param>
		/// <returns>The type member.</returns>
		protected override CodeTypeMember GenerateActionPartialMethod (string message, IType argumentType)
		{
			String selector = message;
			String name = GenerateMethodName (selector);

			// Partial method are only possible by using a snippet of code as CodeDom does not handle them
			CodeSnippetTypeMember method = new CodeSnippetTypeMember ("partial void " + name + "(" + argumentType.Name + " sender);" + Environment.NewLine);

			return method;
		}
	}
}
