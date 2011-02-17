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
using Gtk;
using MonoDevelop.Ide.Gui.Dialogs;

namespace MonoDevelop.Monobjc.Gui
{
    /// <summary>
    /// The options panel for general.
    /// </summary>
    public class MonobjcGeneralOptionsPanel : ItemOptionsPanel
    {
        private MonobjcGeneralOptionsWidget widget;

        /// <summary>
        ///   Creates the panel widget.
        /// </summary>
        /// <returns></returns>
        public override Widget CreatePanelWidget()
        {
            if (this.widget == null)
            {
                this.widget = new MonobjcGeneralOptionsWidget();
            }
            this.widget.Load(this.ConfiguredProject as MonobjcProject);
            return this.widget;
        }

        /// <summary>
        ///   Applies the changes.
        /// </summary>
        public override void ApplyChanges()
        {
            this.widget.Save(this.ConfiguredProject as MonobjcProject);
        }
    }
}