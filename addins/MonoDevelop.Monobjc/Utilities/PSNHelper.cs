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
using System.Runtime.InteropServices;

namespace MonoDevelop.Monobjc.Utilities
{
    /// <summary>
    ///   Process Serial Number helper class.
    /// </summary>
    public static class PSNHelper
    {
        private const uint kProcessTransformToForegroundApplication = 1;

        /// <summary>
        ///   Pass the application running as the given pid as a front application.
        /// </summary>
        /// <param name = "pid">
        ///   The process id to pass as foreground application.
        /// </param>
        public static void SetFront(int pid)
        {
            // Make sure the process is tranformed to UI on the foreground, even when launched from the command line.
            // See http://cocoadev.com/index.pl?TransformProcessType for details
            IntPtr psn = IntPtr.Zero;
            if (GetProcessForPID(pid, ref psn) != 0)
            {
                //return;
            }
            if (TransformProcessType(ref psn, kProcessTransformToForegroundApplication) != 0)
            {
                //return;
            }
            if (SetFrontProcess(ref psn) != 0)
            {
                //return;
            }
        }

        /// <summary>
        ///   Gets information about the current process, if any.
        ///   <para>Applications can use this function to find their own process serial number. Drivers can use this function to find the process serial number of the current process. You can use the returned process serial number in other Process Manager functions.</para>
        ///   <para>This function is named MacGetCurrentProcess on non Macintosh platforms and GetCurrentProcess on Macintosh computers. However, even Macintosh code can use the MacGetCurrentProcess name because a macro exists that automatically maps that call to GetCurrentProcess.</para>
        /// </summary>
        /// <param name = "psn">On output, a pointer to the process serial number of the current process, that is, the one currently accessing the CPU. This application can be running in either the foreground or the background.</param>
        /// <returns>A result code.</returns>
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern int GetProcessForPID(int pid, ref IntPtr psn);

        /// <summary>
        ///   Changes the type of the specified process.
        ///   <para>You can use this call to transform a background-only application into a foreground application. A foreground application appears in the Dock (and in the Force Quit dialog) and contains a menu bar. This function does not cause the application to be brought to the front; you must call SetFrontProcess to do so.</para>
        /// </summary>
        /// <param name = "psn">The serial number of the process you want to transform. You can also use the constant kCurrentProcess to refer to the current process. See ProcessSerialNumber for more information.</param>
        /// <param name = "type">A constant indicating the type of transformation you want. Currently you can pass only kProcessTransformToForegroundApplication.</param>
        /// <returns>A result code.</returns>
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern int TransformProcessType(ref IntPtr psn, uint type);

        /// <summary>
        ///   Moves a process to the foreground.
        ///   <para>The SetFrontProcess function moves the specified process to the foreground immediately.</para>
        ///   <para>If the specified process serial number is invalid or if the specified process is a background-only application, SetFrontProcess returns a nonzero result code and does not change the current foreground process.</para>
        /// </summary>
        /// <param name = "psn">A pointer to a valid process serial number. You can also pass a process serial number structure containing the constant kCurrentProcess to refer to the current process. See ProcessSerialNumber for more information.</param>
        /// <returns>A result code.</returns>
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern int SetFrontProcess(ref IntPtr psn);
    }
}