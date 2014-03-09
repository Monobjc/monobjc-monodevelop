//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2013 - Laurent Etiemble
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
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MonobjcDevelop.Monobjc.Parsing
{
	public class NativeMethodDescriptor
	{
		public bool Static { get; set; }

		public String ReturnType { get; set; }

		public String Selector { get; set; }

		public IList<NativeMethodDescriptor.Parameter> Parameters { get; set; }

		public bool IBAction { get; set; }

		public override string ToString ()
		{
			return string.Format ("[IBMethodDescriptor: Static={0}, ReturnType={1}, Selector={2}, Parameters={3}, IBAction={4}]", Static, ReturnType, Selector, String.Join (";", Parameters.Select (p => p.Type + " " + p.Name)), IBAction);
		}

		public class Parameter
		{
			public String Type { get; set; }

			public String Name { get; set; }
		}
	}
}
