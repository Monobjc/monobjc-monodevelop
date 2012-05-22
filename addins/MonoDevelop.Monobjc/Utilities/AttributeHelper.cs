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
using System.Collections;
using System.Linq;
using Mono.Cecil;
using Mono.Collections.Generic;
using MonoDevelop.Core;

#if MD_2_6 || MD_2_8
using MonoDevelop.Projects.Dom;
using AttributeHolder = MonoDevelop.Projects.Dom.IMember;
#endif
#if MD_3_0
using ICSharpCode.NRefactory.TypeSystem;
using AttributeHolder = ICSharpCode.NRefactory.TypeSystem.IEntity;
#endif

namespace MonoDevelop.Monobjc.Utilities
{
	/// <summary>
	///   Helper class for attribute.
	/// </summary>
	public static partial class AttributeHelper
	{
		public const String IBACTION = "Monobjc.IBActionAttribute";

		public const String IBOUTLET = "Monobjc.IBOutletAttribute";

		public const String OBJECTIVE_C_CLASS = "Monobjc.ObjectiveCClassAttribute";

		public const String OBJECTIVE_C_PROTOCOL = "Monobjc.ObjectiveCProtocolAttribute";

		public const String OBJECTIVE_C_MESSAGE = "Monobjc.ObjectiveCMessageAttribute";

		public const String OBJECTIVE_C_IVAR = "Monobjc.ObjectiveCIVarAttribute";

		public const String OBJECTIVE_C_FRAMEWORK = "Monobjc.ObjectiveCFrameworkAttribute";

		/// <summary>
		///   Returns the attribute with the given full name if it exists.
		/// </summary>
		public static IAttribute GetAttribute (AttributeHolder holder, String attributeFullName)
		{
			if (holder.Attributes == null) {
				return null;
			}
			return holder.Attributes.FirstOrDefault (a => String.Equals (a.AttributeType.FullName, attributeFullName));
		}

		/// <summary>
		///   Checks if the attribute with the given full name exists.
		/// </summary>
		public static bool HasAttribute (AttributeHolder holder, String attributeFullName)
		{
			if (holder.Attributes == null) {
				return false;
			}
			return holder.Attributes.Any (a => String.Equals (a.AttributeType.FullName, attributeFullName));
		}

		/// <summary>
		///   Returns the value of the attribute with the given full name if it exists.
		/// </summary>
		public static String GetAttributeValue (AttributeHolder holder, String attributeFullName)
		{
			IAttribute attribute = GetAttribute (holder, attributeFullName);
			if (attribute == null) {
				return null;
			}
#if MD_2_6 || MD_2_8
			var expression = attribute.PositionalArguments.FirstOrDefault (pa => typeof(CodePrimitiveExpression).IsAssignableFrom (pa.GetType ())) as CodePrimitiveExpression;
			if (expression == null) {
				return null;
			}
			return expression.Value.ToString ();
#endif
#if MD_3_0
			var expression = attribute.PositionalArguments.FirstOrDefault(pa => pa.IsCompileTimeConstant);
			if (expression == null) {
				return null;
			}
			return expression.ConstantValue.ToString ();
#endif
		}

		/// <summary>
		/// Determines whether this assembly is a wrapping framework.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this assembly is a wrapping framework; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="assemblyPath">
		/// If set to <c>true</c> assembly path.
		/// </param>
		/// <param name="systemFramework">
		/// If set to <c>true</c> system framework.
		/// </param>
		public static bool IsWrappingFramework(String assemblyPath, out bool systemFramework)
        {
            systemFramework = false;
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyPath);

            // Balk if the assembly has no custom attribute
            if (!assemblyDefinition.HasCustomAttributes)
            {
                return false;
            }

            CustomAttribute frameworkAttribute = null;
            Collection<CustomAttribute> attributes = assemblyDefinition.CustomAttributes;
            foreach (CustomAttribute attribute in attributes)
            {
                String fullType = attribute.Constructor.DeclaringType.ToString();
                if (String.Equals(fullType, OBJECTIVE_C_FRAMEWORK))
                {
                    frameworkAttribute = attribute;
                    break;
                }
            }
			
            // Return false if no framework attribute is found
            if (frameworkAttribute == null)
            {
                return false;
            }

            // Retrieve the attribute parameters
            Collection<CustomAttributeArgument> list = frameworkAttribute.ConstructorArguments;
            if (list.Count == 0)
            {
                return false;
            }

            // Set if this assembly wraps a system framework
            String value = list[0].Value.ToString();
            systemFramework = Boolean.Parse(value);

            return true;
        }
	}
}
