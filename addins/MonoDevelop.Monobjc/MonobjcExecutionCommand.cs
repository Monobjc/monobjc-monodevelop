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
using MonoDevelop.Core.Execution;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	///   A Monobjc's version of a execution command.
	/// </summary>
	public class MonobjcExecutionCommand : ExecutionCommand
	{
		private readonly MonobjcProjectConfiguration configuration;

		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcExecutionCommand" /> class.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		public MonobjcExecutionCommand (MonobjcProjectConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		///   Gets or sets the user assembly paths.
		/// </summary>
		/// <value>The user assembly paths.</value>
		public IList<string> UserAssemblyPaths { get; set; }

		/// <summary>
		///   Gets the command string.
		/// </summary>
		/// <value>The command string.</value>
#if MD_3_0
        public override string CommandString {
            get { return this.configuration.Runtime; }
        }
#endif
#if MD_4_0
        public string CommandString {
            get { return this.configuration.Runtime; }
        }
#endif

        /// <summary>
		///   Gets the name of the application.
		/// </summary>
		/// <value>The name of the application.</value>
		public string ApplicationName {
			get { return this.configuration.ApplicationName; }
		}

		/// <summary>
		///   Gets the command line parameters.
		/// </summary>
		/// <value>The command line parameters.</value>
		public string CommandLineParameters {
			get { return this.configuration.CommandLineParameters; }
		}

		/// <summary>
		///   Gets the environment variables.
		/// </summary>
		/// <value>The environment variables.</value>
		public Dictionary<string, string> EnvironmentVariables {
			get { return this.configuration.EnvironmentVariables; }
		}

		/// <summary>
		///   Gets a value indicating whether this command runs in debug mode.
		/// </summary>
		/// <value><c>true</c> if this command runs in debug mode; otherwise, <c>false</c>.</value>
		public bool DebugMode {
			get { return this.configuration.DebugMode; }
		}
	}
}