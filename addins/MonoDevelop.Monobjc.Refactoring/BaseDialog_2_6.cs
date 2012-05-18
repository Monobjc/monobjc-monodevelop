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
using Gtk;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.Ast;

namespace MonoDevelop.Monobjc.Refactoring
{
	partial class BaseDialog
	{
		protected TypeReference Shorten (IType declaringType, IReturnType type)
		{
			return this.options.Document.CompilationUnit.ShortenTypeName (type, declaringType.Location).ConvertToTypeReference ();
		}
		
		protected ICSharpCode.NRefactory.Ast.Attribute GetAttribute(String attributeType, String parameter)
		{
			if (parameter != null) {
				return new ICSharpCode.NRefactory.Ast.Attribute (attributeType, new List<Expression> { new PrimitiveExpression (parameter) }, null);
			}
			return new ICSharpCode.NRefactory.Ast.Attribute (attributeType, null, null);
		}
		
		protected PropertyDeclaration GetPropertyDeclaration(String propertyName, TypeReference propertyType, AttributeSection attributeSection)
		{
			var modifiers = ICSharpCode.NRefactory.Ast.Modifiers.Public | ICSharpCode.NRefactory.Ast.Modifiers.Virtual;
			List<AttributeSection> attributes = null;
			if (attributeSection != null) {
				attributes = new List<AttributeSection> { attributeSection };
			}
			var propertyDeclaration = new PropertyDeclaration (modifiers, attributes, propertyName, null);
			propertyDeclaration.TypeReference = propertyType;
			return propertyDeclaration;
		}
		
		protected ThrowStatement GetThrowStatement(String exceptionName)
		{
			return new ThrowStatement (new ObjectCreateExpression (new TypeReference (exceptionName), new List<Expression> ()));
		}
	}
}
