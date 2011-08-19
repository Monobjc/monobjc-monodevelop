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
using System.Diagnostics;
using Mono.Debugging.Client;
using Mono.Unix;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Monobjc;
using MonoDevelop.Monobjc.Utilities;

#if MD_2_6 || MD_2_8
using Mono.Debugging.Soft;
#endif

namespace MonoDevelop.Debugger.Soft.Monobjc
{
	/// <summary>
	/// </summary>
	public class MonobjcDebuggerSession : 
#if MD_2_4
		RemoteSoftDebuggerSession
#endif
#if MD_2_6 || MD_2_8
		SoftDebuggerSession
#endif
	{
		private Process process;

		/// <summary>
		///   Called when debugger runs.
		/// </summary>
		/// <param name = "startInfo">The start info.</param>
		protected override void OnRun (DebuggerStartInfo startInfo)
		{
#if MD_2_4
			MonobjcDebuggerStartInfo dsi = (MonobjcDebuggerStartInfo)startInfo;
			MonobjcExecutionCommand command = dsi.ExecutionCommand;
			
			this.StartListening (dsi);
			
			// Create the start information
			ProcessStartInfo psi = new ProcessStartInfo (command.CommandString) { Arguments = command.CommandLineParameters, RedirectStandardOutput = true, RedirectStandardError = true, RedirectStandardInput = true, UseShellExecute = false };
			psi.EnvironmentVariables["MONO_OPTIONS"] = string.Format ("--debug --debugger-agent=transport=dt_socket,address={0}:{1}", dsi.Address, dsi.DebugPort);
#endif
#if MD_2_6 || MD_2_8
			MonobjcDebuggerStartInfo dsi = (MonobjcDebuggerStartInfo) startInfo;
			SoftDebuggerRemoteArgs startArgs = (SoftDebuggerRemoteArgs) dsi.StartArgs;
			MonobjcExecutionCommand command = dsi.ExecutionCommand;		
			
			int assignedPort;
			this.StartListening(dsi, out assignedPort);
			
			// Create the start information
			ProcessStartInfo psi = new ProcessStartInfo (command.CommandString) { Arguments = command.CommandLineParameters, RedirectStandardOutput = true, RedirectStandardError = true, RedirectStandardInput = true, UseShellExecute = false };
			psi.EnvironmentVariables["MONO_OPTIONS"] = string.Format ("--debug --debugger-agent=transport=dt_socket,address={0}:{1}", startArgs.Address, assignedPort);
#endif
			// Try to start the process
			this.process = Process.Start (psi);
			if (this.process == null) {
				this.EndSession ();
				return;
			}			
			
			// Connect the stdout and stderr to the MonoDevelop's output
			this.ConnectOutput (this.process.StandardOutput, false);
			this.ConnectOutput (this.process.StandardError, true);
			
			// When process exits, end the session
			this.process.EnableRaisingEvents = true;
			this.process.Exited += delegate {
				this.EndSession ();
				this.process = null;
			};
			
			DispatchService.GuiDispatch(() => {
				// Make sure the process is the front application
				MonoDevelop.Core.LoggingService.LogInfo ("Debuggin application (pid={0})", this.process.Id);
				PSNHelper.SetFront (this.process.Id);
			});
		}

		/// <summary>
		///   Ensures that the process has exited.
		/// </summary>
		protected override void EnsureExited ()
		{
			try {
				if (this.process != null && !this.process.HasExited) {
					this.process.Kill ();
				}
			} catch (Exception ex) {
				MonoDevelop.Core.LoggingService.LogError (GettextCatalog.GetString ("Error force-terminating soft debugger process"), ex);
			}
		}
		
#if MD_2_4
		/// <summary>
		/// Get the waiting message.
		/// </summary>
		/// <param name="dsi">The start info.</param>
		/// <returns>The message.</returns>
		protected override string GetListenMessage (RemoteDebuggerStartInfo dsi)
		{
			return GettextCatalog.GetString ("Waiting for app to connect to: {0}:{1}", dsi.Address, dsi.DebugPort);
		}
#endif
	}
}
