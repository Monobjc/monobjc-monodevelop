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
using System.IO;
using MonoDevelop.Core;

namespace MonoDevelop.Monobjc.Utilities
{
    /// <summary>
    ///   A construct that holds a source and a destination file.
    /// </summary>
    public class FilePair
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "FilePair" /> struct.
        /// </summary>
        /// <param name = "source">The source.</param>
        /// <param name = "output">The destination.</param>
        public FilePair(FilePath source, FilePath destination)
        {
            this.Source = source;
            this.Destination = destination;
        }

        /// <summary>
        ///   Gets or sets the input file.
        /// </summary>
        /// <value>The input file.</value>
        public FilePath Source { get; private set; }

        /// <summary>
        ///   Gets or sets the output file.
        /// </summary>
        /// <value>The output file.</value>
        public FilePath Destination { get; private set; }
		
		/// <summary>
		/// Gets the destination directory.
		/// </summary>
		/// <value>The destination directory.</value>
		public FilePath DestinationDir
		{
			get { return this.Destination.ParentDirectory; }	
		}
		
        /// <summary>
        ///   Returns whether this file pair needs building.
        /// </summary>
        /// <returns></returns>
        public bool NeedsBuilding
        {
            get { return !File.Exists(this.Destination) || File.GetLastWriteTime(this.Source) > File.GetLastWriteTime(this.Destination); }
        }

        /// <summary>
        ///   Ensures the output directory exists by creating it if needed..
        /// </summary>
        public void EnsureOutputDirectory()
        {
            if (!Directory.Exists(this.Destination.ParentDirectory))
            {
                Directory.CreateDirectory(this.Destination.ParentDirectory);
            }
        }

        /// <summary>
        ///   Copies the specified pair.
        /// </summary>
        /// <param name = "allowsOverride">if set to <c>true</c> allows override.</param>
        public void Copy(bool allowsOverride)
        {
            if (this.NeedsBuilding || allowsOverride)
            {
                this.EnsureOutputDirectory();
                File.Copy(this.Source, this.Destination, true);
            }
        }
    }
}