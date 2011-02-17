
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcGeneralOptionsWidget
	{
		private global::Gtk.Table table1;

		private global::Gtk.ComboBox comboboxVersion;

		private global::Gtk.FileChooserButton filechooserbuttonBundleIcon;

		private global::Gtk.FileChooserButton filechooserbuttonMainNib;

		private global::Gtk.Label labelBundleIcon;

		private global::Gtk.Label labelFrameworks;

		private global::Gtk.Label labelMainNib;

		private global::Gtk.Label labelVersion;

		private global::Gtk.ScrolledWindow scrolledwindowFrameworks;

		private global::Gtk.TreeView treeviewFrameworks;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.Monobjc.Gui.MonobjcGeneralOptionsWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MonoDevelop.Monobjc.Gui.MonobjcGeneralOptionsWidget";
			// Container child MonoDevelop.Monobjc.Gui.MonobjcGeneralOptionsWidget.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(6)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.comboboxVersion = global::Gtk.ComboBox.NewText ();
			this.comboboxVersion.Name = "comboboxVersion";
			this.table1.Add (this.comboboxVersion);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.comboboxVersion]));
			w1.TopAttach = ((uint)(2));
			w1.BottomAttach = ((uint)(3));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.filechooserbuttonBundleIcon = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select Bundle Icon"), ((global::Gtk.FileChooserAction)(0)));
			this.filechooserbuttonBundleIcon.Name = "filechooserbuttonBundleIcon";
			this.table1.Add (this.filechooserbuttonBundleIcon);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.filechooserbuttonBundleIcon]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.filechooserbuttonMainNib = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select Main NIB File"), ((global::Gtk.FileChooserAction)(0)));
			this.filechooserbuttonMainNib.Name = "filechooserbuttonMainNib";
			this.table1.Add (this.filechooserbuttonMainNib);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.filechooserbuttonMainNib]));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelBundleIcon = new global::Gtk.Label ();
			this.labelBundleIcon.Name = "labelBundleIcon";
			this.labelBundleIcon.Xalign = 1f;
			this.labelBundleIcon.LabelProp = global::Mono.Unix.Catalog.GetString ("Bundle Icon:");
			this.table1.Add (this.labelBundleIcon);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.labelBundleIcon]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelFrameworks = new global::Gtk.Label ();
			this.labelFrameworks.Name = "labelFrameworks";
			this.labelFrameworks.Xalign = 0f;
			this.labelFrameworks.LabelProp = global::Mono.Unix.Catalog.GetString ("Frameworks:");
			this.table1.Add (this.labelFrameworks);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.labelFrameworks]));
			w5.TopAttach = ((uint)(3));
			w5.BottomAttach = ((uint)(4));
			w5.RightAttach = ((uint)(2));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelMainNib = new global::Gtk.Label ();
			this.labelMainNib.Name = "labelMainNib";
			this.labelMainNib.Xalign = 1f;
			this.labelMainNib.LabelProp = global::Mono.Unix.Catalog.GetString ("Main NIB File:");
			this.table1.Add (this.labelMainNib);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.labelMainNib]));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelVersion = new global::Gtk.Label ();
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Xalign = 1f;
			this.labelVersion.LabelProp = global::Mono.Unix.Catalog.GetString ("Target Mac OS Version:");
			this.table1.Add (this.labelVersion);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.labelVersion]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.scrolledwindowFrameworks = new global::Gtk.ScrolledWindow ();
			this.scrolledwindowFrameworks.CanFocus = true;
			this.scrolledwindowFrameworks.Name = "scrolledwindowFrameworks";
			this.scrolledwindowFrameworks.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindowFrameworks.Gtk.Container+ContainerChild
			this.treeviewFrameworks = new global::Gtk.TreeView ();
			this.treeviewFrameworks.CanFocus = true;
			this.treeviewFrameworks.Name = "treeviewFrameworks";
			this.scrolledwindowFrameworks.Add (this.treeviewFrameworks);
			this.table1.Add (this.scrolledwindowFrameworks);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.scrolledwindowFrameworks]));
			w9.TopAttach = ((uint)(4));
			w9.BottomAttach = ((uint)(5));
			w9.RightAttach = ((uint)(2));
			this.Add (this.table1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
