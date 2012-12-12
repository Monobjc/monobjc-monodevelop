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
using System.Text;
using Gtk;
using MonoDevelop.Refactoring;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace MonoDevelop.Monobjc.Gui
{
	public class BaseRefactoringDialog : Dialog
	{
		public BaseRefactoringDialog (RefactoringOperation refactoring, RefactoringOptions options, MonobjcProject project)
		{
			this.Refactoring = refactoring;
			this.Options = options;
			this.Project = project;
		}

		protected RefactoringOperation Refactoring { get; private set; }
		protected RefactoringOptions Options { get; private set; }
		protected MonobjcProject Project { get; private set; }
		
		protected AstType Shorten (IType type)
        {
            return this.Options.CreateShortType(type);
        }

        protected ICSharpCode.NRefactory.CSharp.Attribute GetAttribute(String attributeType, String parameter)
        {
            var attribute = new ICSharpCode.NRefactory.CSharp.Attribute();
            attribute.Type = new SimpleType(attributeType);
            if (parameter != null) {
                attribute.Arguments.Add(new PrimitiveExpression(parameter));
            }
            return attribute;
        }
        
        protected PropertyDeclaration GetPropertyDeclaration(String propertyName, AstType propertyType, AttributeSection attributeSection)
        {
            var propertyDeclaration = new PropertyDeclaration();
            propertyDeclaration.Modifiers = Modifiers.Public | Modifiers.Virtual;
            propertyDeclaration.Name = propertyName;
            propertyDeclaration.ReturnType = propertyType;
            propertyDeclaration.Attributes.Add(attributeSection);
            return propertyDeclaration;
        }
        
        protected ThrowStatement GetThrowStatement(String exceptionName)
        {
            return new ThrowStatement (new ObjectCreateExpression (new SimpleType (exceptionName), new List<Expression> ()));
        }

        protected String Indent(String content, String indent)
        {
            StringBuilder result = new StringBuilder();
            foreach(String line in content.Split('\n')) {
                result.AppendFormat("{0}{1}", indent, line);
                result.AppendLine();
            }
            return result.ToString();
        }
	}
}
