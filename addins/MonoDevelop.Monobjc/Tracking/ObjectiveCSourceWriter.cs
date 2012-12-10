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
using System.IO;
using ICSharpCode.NRefactory.TypeSystem;
using MonoDevelop.Monobjc.Utilities;

namespace MonoDevelop.Monobjc.Tracking
{
	public class ObjectiveCSourceWriter : ObjectiveCWriter
	{
		public ObjectiveCSourceWriter (MonobjcProject project) : base(project)
		{
		}

		protected override void WriteIncludes (TextWriter writer, IType type)
		{
			writer.WriteLine ("#import \"{0}.h\"", type.Name);
			writer.WriteLine ();
		}

		protected override void WritePrologue (TextWriter writer, string name, string baseName)
		{
			writer.WriteLine ("@implementation {0}", name);
			writer.WriteLine ();
		}

		protected override void WriteProperties (TextWriter writer, IType type)
		{
		}

		protected override void WriteMethods (TextWriter writer, IType type)
		{
			foreach (IMethod method in this.GetMethods(type)) {
				String selector = AttributeHelper.GetAttributeValue (method, Constants.OBJECTIVE_C_MESSAGE);
				writer.WriteLine ("{0}(IBAction) {1}(id) sender {{ }}", method.IsStatic ? "+" : "-", selector);
			}
		}

		protected override void WriteEpilogue (TextWriter writer, string name, string baseName)
		{
			writer.WriteLine ();
			writer.WriteLine ("@end");
		}
	}
}
