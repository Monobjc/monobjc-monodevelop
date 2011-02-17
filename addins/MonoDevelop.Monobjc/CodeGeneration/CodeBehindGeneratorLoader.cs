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
using System.Collections.Generic;
using System;
using Mono.Addins;

namespace MonoDevelop.Monobjc.CodeGeneration
{
    /// <summary>
    ///   A loader for the extension codons of code-behind implementations.
    /// </summary>
    public static class CodeBehindGeneratorLoader
    {
        private static readonly IDictionary<string, ICodeBehindGenerator> generators = new Dictionary<string, ICodeBehindGenerator>();

        /// <summary>
        ///   Inits this class.
        /// </summary>
        public static void Init()
        {
            AddinManager.AddExtensionNodeHandler("/Monobjc/CodeBehindGeneration", OnCodeBehindGenerationChanged);
        }

        /// <summary>
        ///   Gets the generator for the given language.
        /// </summary>
        /// <param name = "languageName">The language name.</param>
        /// <returns>A code generator.</returns>
        public static ICodeBehindGenerator getGenerator(String languageName)
        {
            if (generators.ContainsKey(languageName))
            {
                return generators[languageName];
            }
            throw new InvalidOperationException("No generator found for language " + languageName);
        }

        /// <summary>
        ///   Handler for codon addition/removal.
        /// </summary>
        private static void OnCodeBehindGenerationChanged(object sender, ExtensionNodeEventArgs args)
        {
            CodeBehindGeneratorCodon codon = (CodeBehindGeneratorCodon) args.ExtensionNode;
            if (args.Change == ExtensionChange.Add)
            {
                generators.Add(codon.Id, codon.Generator);
            }
            else
            {
                generators.Remove(codon.Id);
            }
        }
    }
}