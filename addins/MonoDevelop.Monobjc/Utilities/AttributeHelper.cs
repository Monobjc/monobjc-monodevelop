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
using System.Linq;
using MonoDevelop.Projects.Dom;

namespace MonoDevelop.Monobjc.Utilities
{
    /// <summary>
    ///   Helper class for attribute.
    /// </summary>
    public static class AttributeHelper
    {
        public const String OBJECTIVE_C_CLASS = "Monobjc.ObjectiveCClassAttribute";

        public const String OBJECTIVE_C_PROTOCOL = "Monobjc.ObjectiveCProtocolAttribute";

        public const String OBJECTIVE_C_MESSAGE = "Monobjc.ObjectiveCMessageAttribute";

        public const String OBJECTIVE_C_IVAR = "Monobjc.ObjectiveCIVarAttribute";

        /// <summary>
        ///   Returns the attribute with the given full name if it exists.
        /// </summary>
        /// <param name = "member">
        ///   A <see cref = "IMember" /> where the attribute is
        /// </param>
        /// <param name = "attributeFullName">
        ///   The attribute fullname.
        /// </param>
        /// <returns>
        ///   A <see cref = "IAttribute" /> if it is found; <code>null</code> otherwise.
        /// </returns>
        public static IAttribute GetAttribute(IMember member, String attributeFullName)
        {
            return member.Attributes.FirstOrDefault(a => String.Equals(a.AttributeType.FullName, attributeFullName));
        }

        /// <summary>
        ///   Checks if the attribute with the given full name exists.
        /// </summary>
        /// <param name = "member">
        ///   A <see cref = "IMember" /> where the attribute is
        /// </param>
        /// <param name = "attributeFullName">
        ///   The attribute fullname.
        /// </param>
        /// <returns>
        ///   <code>true</code> if it is found; <code>false</code> otherwise.
        /// </returns>
        public static bool HetAttribute(IMember member, String attributeFullName)
        {
            return member.Attributes.Any(a => String.Equals(a.AttributeType.FullName, attributeFullName));
        }

        /// <summary>
        ///   Returns the value of the attribute with the given full name if it exists.
        /// </summary>
        /// <param name = "member">
        ///   A <see cref = "IMember" />
        /// </param>
        /// <param name = "attributeFullName">
        ///   The attribute fullname.
        /// </param>
        /// <returns>
        ///   The value if the attribute is found; <code>null</code> otherwise.
        /// </returns>
        public static String GetAttributeValue(IMember member, String attributeFullName)
        {
            IAttribute attribute = GetAttribute(member, attributeFullName);
            if (attribute == null)
            {
                return null;
            }
            CodePrimitiveExpression expression = attribute.PositionalArguments.FirstOrDefault(pa => typeof (CodePrimitiveExpression).IsAssignableFrom(pa.GetType())) as CodePrimitiveExpression;
            if (expression == null)
            {
                return null;
            }
            return expression.Value.ToString();
        }
    }
}