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
using MonoDevelop.Projects.Dom;

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
        protected override bool IsDesignerRegionDelimiter(String line, bool start)
        {
            if (start)
            {
                return (line.Contains("#region") && line.Contains("Monobjc Generated Code"));
            }
            else
            {
                return (line.Contains("#endregion"));
            }
        }

        /// <summary>
        ///   Generates the framework loading code which is language specific.
        /// </summary>
        /// <param name = "frameworks">The frameworks.</param>
        /// <returns>A list of lines for the code.</returns>
        protected override IEnumerable<String> GenerateFrameworkLoadingcode(String[] frameworks)
        {
            List<String> lines = new List<String>();

            lines.Add("			#region \"Monobjc Generated Code\"");
            lines.Add("			'");
            lines.Add("			' DO NOT ALTER OR REMOVE");
            lines.Add("			'");
            foreach (String framework in frameworks)
            {
                lines.Add("			ObjectiveCRuntime.LoadFramework(\"" + framework + "\");");
            }
            lines.Add("			#endregion");

            return lines;
        }

        /// <summary>
        ///   Generates the partial method for an action.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "argumentType">Type of the argument.</param>
        /// <returns>The type member.</returns>
        protected override CodeTypeMember GenerateActionPartialMethod(string message, IType argumentType)
        {
            String selector = message;
            String name = GenerateMethodName(selector);

            // Partial method are only possible by using a snippet of code as CodeDom does not handle them
            CodeSnippetTypeMember method = new CodeSnippetTypeMember("Partial Private Sub " + name + "(" + argumentType.Name + " sender)" + Environment.NewLine + "End Sub" + Environment.NewLine);

            return method;
        }
    }
}