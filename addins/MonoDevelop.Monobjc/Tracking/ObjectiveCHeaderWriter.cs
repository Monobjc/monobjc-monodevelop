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
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Tracking
{
	public class ObjectiveCHeaderWriter : ObjectiveCWriter
	{
		private ProjectResolver resolver;
		
		public ObjectiveCHeaderWriter (MonobjcProject project) : base(project)
		{
			this.resolver = new ProjectResolver(project);
		}
		
		protected override void WritePrologue (TextWriter writer, string name, string baseName)
		{
			writer.WriteLine ("@interface {0} : {1}", name, baseName);
		}

		protected override void WriteProperties (TextWriter writer, IType type)
		{
			writer.WriteLine ("{");
			
			foreach (IProperty property in this.GetProperties(type)) {
				writer.WriteLine ("\tIBOutlet {0} *{1};", property.ReturnType.Name, property.Name);
			}
			
			writer.WriteLine ("}");
		}

		protected override void WriteMethods (TextWriter writer, IType type)
		{
			foreach (IMethod method in this.GetMethods(type)) {
				String selector = AttributeHelper.GetAttributeValue (method, AttributeHelper.OBJECTIVE_C_MESSAGE);
				writer.WriteLine ();
				writer.WriteLine ("{0}(IBAction) {1}(id) sender;", method.IsStatic ? "+" : "-", selector);
			}
		}

		protected override void WriteEpilogue (TextWriter writer, string name, string baseName)
		{
			writer.WriteLine ();
			writer.WriteLine ("@end");
		}		
		
		protected override IEnumerable<String> GetOtherImports (IType type)
		{
			foreach (IProperty property in this.GetProperties(type)) {
				ProjectDom dom = this.resolver.GetOwnerDom(property.ReturnType);
				Project project = dom != null ? dom.Project : null;
				if (dom.Project != null) {
					yield return String.Format("#import \"{0}.h\"", property.ReturnType.Name);
				}
			}
		}
	}
}
