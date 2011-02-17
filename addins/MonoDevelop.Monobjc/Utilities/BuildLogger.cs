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
using Monobjc.Tools.Utilities;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Utilities
{
	/// <summary>
	/// An implementation of <see cref="IExecutionLogger"/> for MonoDevelop.
	/// </summary>
	public class BuildLogger : IExecutionLogger
	{
		private readonly FilePath projectFile;
		private readonly IProgressMonitor monitor;
		private readonly BuildResult result;

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildLogger"/> class.
		/// </summary>
		/// <param name="monitor">The monitor.</param>
		/// <param name="result">The result.</param>
		public BuildLogger (IProgressMonitor monitor, BuildResult result) : this(FilePath.Null, monitor, result)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildLogger"/> class.
		/// </summary>
		/// <param name="projectFile">The project file.</param>
		/// <param name="monitor">The monitor.</param>
		/// <param name="result">The result.</param>
		public BuildLogger (FilePath projectFile, IProgressMonitor monitor, BuildResult result)
		{
			this.projectFile = projectFile;
			this.monitor = monitor;
			this.result = result;
		}

		/// <summary>
		/// Logs the debug message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void LogDebug (string message)
		{
			this.monitor.Log.WriteLine (message);
		}

		/// <summary>
		/// Logs the info message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void LogInfo (string message)
		{
			this.monitor.Log.WriteLine (message);
		}

		/// <summary>
		/// Logs the warning message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void LogWarning (string message)
		{
			this.monitor.Log.WriteLine (message);
			if (this.projectFile != FilePath.Null) {
				this.result.AddWarning (this.projectFile, 0, 0, "0", message);
			} else {
				this.result.AddWarning (message);
			}
		}

		/// <summary>
		/// Logs the error message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void LogError (string message)
		{
			this.monitor.Log.WriteLine (message);
			if (this.projectFile != FilePath.Null) {
				this.result.AddError (this.projectFile, 0, 0, "0", message);
			} else {
				this.result.AddError (message);
			}
		}
	}
}
