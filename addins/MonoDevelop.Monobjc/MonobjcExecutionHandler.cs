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
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.Monobjc.Utilities;

namespace MonoDevelop.Monobjc
{
	/// <summary>
	/// A Monobjc's version of a execution handler.
	/// </summary>
	public class MonobjcExecutionHandler : IExecutionHandler
	{
		/// <summary>
		///   Determines whether this instance can execute the specified command.
		/// </summary>
		/// <param name = "command">The command.</param>
		/// <returns>
		///   <c>true</c> if this instance can execute the specified command; otherwise, <c>false</c>.
		/// </returns>
		public bool CanExecute (ExecutionCommand command)
		{
			return (command is MonobjcExecutionCommand);
		}

		/// <summary>
		///   Executes the specified command.
		/// </summary>
		/// <param name = "command">The command.</param>
		/// <param name = "console">The console.</param>
		/// <returns></returns>
		public IProcessAsyncOperation Execute (ExecutionCommand command, IConsole console)
		{
			MonobjcExecutionCommand executionCommand = (MonobjcExecutionCommand)command;
			
			IProcessAsyncOperation operation = Runtime.ProcessService.StartConsoleProcess (executionCommand.CommandString, executionCommand.CommandLineParameters, null, executionCommand.EnvironmentVariables, console, null);

			// Make sure the process is the front application
			LoggingService.LogInfo ("Running application (pid={0})", operation.ProcessId);
			PSNHelper.SetFront (operation.ProcessId);
			
			return operation;
		}
	}
}
