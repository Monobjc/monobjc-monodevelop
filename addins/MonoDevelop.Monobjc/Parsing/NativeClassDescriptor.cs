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
using System.Collections.Generic;
using System.Diagnostics;
using Monobjc.Tools.InterfaceBuilder;
using System.Linq;

namespace MonobjcDevelop.Monobjc.Parsing
{
    public class NativeClassDescriptor : IIBClassDescriptor
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="MonobjcDevelop.Monobjc.Parsing.NativeClassDescriptor"/> class.
        /// </summary>
		public NativeClassDescriptor () : this(null, null)
		{
			this.Methods = new List<NativeMethodDescriptor> ();
			this.InstanceVariables = new List<NativeInstanceVariableDescriptor> ();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MonobjcDevelop.Monobjc.Parsing.NativeClassDescriptor"/> class.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <param name="superClassName">The super-class name.</param>
		public NativeClassDescriptor (String className, String superClassName)
		{
			this.ClassName = ClassName;
			this.SuperClassName = SuperClassName;
		}

		/// <summary>
		///   Gets or sets the name of the class.
		/// </summary>
		/// <value>The name of the class.</value>
		public String ClassName { get; set; }

		/// <summary>
		///   Gets or sets the name of the super class.
		/// </summary>
		/// <value>The name of the super class.</value>
		public String SuperClassName { get; set; }

		/// <summary>
		///   Gets or sets the actions.
		/// </summary>
		/// <value>The actions.</value>
		public IList<NativeMethodDescriptor> Methods { get; private set; }

		/// <summary>
		///   Gets or sets the outlets.
		/// </summary>
		/// <value>The outlets.</value>
		public IList<NativeInstanceVariableDescriptor> InstanceVariables { get; private set; }

        /// <summary>
        ///   Gets the actions.
        /// </summary>
        /// <value>The actions.</value>
        public IEnumerable<IBActionDescriptor> Actions {
            get {
                foreach (NativeMethodDescriptor method in this.Methods) {
                    if (!method.IBAction) {
                        continue;
                    }
                    yield return new IBActionDescriptor(method.Selector, method.Parameters[0].Type);
                }
            }
        }

        /// <summary>
        ///   Gets the outlets.
        /// </summary>
        /// <value>The outlets.</value>
        public IEnumerable<IBOutletDescriptor> Outlets {
            get {
                foreach (NativeInstanceVariableDescriptor variable in this.InstanceVariables) {
                    if (!variable.IBOutlet) {
                        continue;
                    }
                    yield return new IBOutletDescriptor(variable.Name, variable.Type);
                }
            }
        }
	}
}
