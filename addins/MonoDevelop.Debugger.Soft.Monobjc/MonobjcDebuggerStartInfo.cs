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
using System.Net;
using Mono.Debugging.Soft;
using MonoDevelop.Monobjc;

namespace MonoDevelop.Debugger.Soft.Monobjc
{
	/// <summary>
	/// The debugging start info.
	/// </summary>
	public class MonobjcDebuggerStartInfo : SoftDebuggerStartInfo
	{
		/// <summary>
		///   Initializes a new instance of the <see cref = "MonobjcDebuggerStartInfo" /> class.
		/// </summary>
		/// <param name = "command">The CMD.</param>
		public MonobjcDebuggerStartInfo (MonobjcExecutionCommand command) : base (new SoftDebuggerListenArgs (command.ApplicationName, IPAddress.Loopback, 0))
		{
			this.ExecutionCommand = command;
		}

		/// <summary>
		///   Gets or sets the execution command.
		/// </summary>
		/// <value>The execution command.</value>
		public MonobjcExecutionCommand ExecutionCommand { get; private set; }
	}
}
