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
using System.Linq;
using System.Text;
using MonoDevelop.Monobjc.Utilities;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Monobjc.Tracking
{
	public class HeaderGenerator
	{
		public static IList<String> GenerateHeaders(MonobjcProject project, String folder)
		{
			IList<String> result = new List<String>();
			ProjectResolver resolver = new ProjectResolver(project);
			IEnumerable<IType> types = resolver.GetAllClasses(true);
			foreach(IType type in types)
			{
				String file = GenerateHeader(project, type, folder);
				result.Add(file);
			}
			return result;
		}
	
		public static String GenerateHeader(MonobjcProject project, IType type, String folder)
		{
			// 1. Create the filename
			
			StringBuilder builder = new StringBuilder();
			
			// 2. Add framework imports
            String[] frameworks = project.OSFrameworks.Split(';');
			foreach(String framework in frameworks)
			{
				builder.appendFormat("#import <{0}/{0}.h>", framework);
			}
			
			// 3. Collect outlets/actions
			IEnumerable<IMethod> properties = (from p in type.Properties
			                                   where p.IsPublic
											   && AttributeHelper.HasAttribute(p, AttributeHelper.OBJECTIVE_C_IVAR)
											   select p);
			IEnumerable<IMethod> methods = (from m in type.Methods
			                                where m.IsPublic
											&& AttributeHelper.HasAttribute(m, AttributeHelper.OBJECTIVE_C_MESSAGE)
											select m);
			
			// 3. Add user-type imports (collect property types and methods parameters/return type)
			//IEnumerable<IReturnType> propertyTypes = from p in properties select p.ReturnType;
			//IEnumerable<IReturnType> methodsTypes = from m in methods select m.Parameters[0].ReturnType;
			
			// 4. Output the interface declaration
			//builder.appendFormat("@interface {0} : {1} {", type, type);
			builder.append("{");
			
			// 4.a Output the outlets
			
			builder.append("}");
			
			// 4.b Output the actions

			builder.append("@end");
			
			return null;
		}
	}
}
