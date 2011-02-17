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
using Mono.Debugging.Client;
using MonoDevelop.Monobjc;
using MonoDevelop.Core.Execution;

namespace MonoDevelop.Debugger.Soft.Monobjc
{
	/// <summary>
	/// </summary>
	public class MonobjcSoftDebuggerEngine : IDebuggerEngine
	{
		/// <summary>
		///   Determines whether this instance can debug the specified command.
		/// </summary>
		/// <param name = "command">The command.</param>
		/// <returns>
		///   <c>true</c> if this instance can debug the specified command; otherwise, <c>false</c>.
		/// </returns>
		public bool CanDebugCommand (ExecutionCommand command)
		{
			MonobjcExecutionCommand executionCommand = command as MonobjcExecutionCommand;
			return executionCommand != null && executionCommand.DebugMode;
		}

		/// <summary>
		///   Creates the debugger start info.
		/// </summary>
		/// <param name = "command">The command.</param>
		/// <returns></returns>
		public DebuggerStartInfo CreateDebuggerStartInfo (ExecutionCommand command)
		{
			MonobjcExecutionCommand executionCommand = (MonobjcExecutionCommand)command;
			
			MonobjcDebuggerStartInfo startInfo = new MonobjcDebuggerStartInfo (executionCommand);
			startInfo.SetUserAssemblies (executionCommand.UserAssemblyPaths);
			
			return startInfo;
		}

		/// <summary>
		///   Creates the session.
		/// </summary>
		/// <returns></returns>
		public DebuggerSession CreateSession ()
		{
			return new MonobjcDebuggerSession ();
		}

		/// <summary>
		///   Gets the attachable processes.
		/// </summary>
		/// <returns></returns>
		public ProcessInfo[] GetAttachableProcesses ()
		{
			// TODO: Scan process for debuggable processes
			return new ProcessInfo[0];
		}
	}
}
