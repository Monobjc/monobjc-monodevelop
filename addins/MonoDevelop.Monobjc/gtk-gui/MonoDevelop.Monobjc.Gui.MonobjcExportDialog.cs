
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.Monobjc.Gui
{
	public partial class MonobjcExportDialog
	{
		private global::Gtk.VBox vbox;
		private global::Gtk.RadioButton radiobuttonManaged;
		private global::Gtk.Label labelManaged;
		private global::Gtk.RadioButton radiobuttonNative;
		private global::Gtk.Label labelNative;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label labelOutput;
		private global::Gtk.FileChooserButton filechooserbuttonOutput;
		private global::Gtk.HSeparator hseparator2;
		private global::Gtk.ProgressBar progressbar;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.Monobjc.Gui.MonobjcExportDialog
			this.Name = "MonoDevelop.Monobjc.Gui.MonobjcExportDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Export Application");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("monobjc-32.png");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(1));
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			this.BorderWidth = ((uint)(6));
			this.Resizable = false;
			this.AllowGrow = false;
			this.DefaultWidth = 500;
			this.DefaultHeight = 400;
			// Internal child MonoDevelop.Monobjc.Gui.MonobjcExportDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.Spacing = 3;
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox = new global::Gtk.VBox ();
			this.vbox.Name = "vbox";
			this.vbox.Spacing = 6;
			this.vbox.BorderWidth = ((uint)(6));
			// Container child vbox.Gtk.Box+BoxChild
			this.radiobuttonManaged = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Export As Managed Application"));
			this.radiobuttonManaged.CanFocus = true;
			this.radiobuttonManaged.Name = "radiobuttonManaged";
			this.radiobuttonManaged.Active = true;
			this.radiobuttonManaged.DrawIndicator = true;
			this.radiobuttonManaged.UseUnderline = true;
			this.radiobuttonManaged.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.vbox.Add (this.radiobuttonManaged);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox [this.radiobuttonManaged]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.labelManaged = new global::Gtk.Label ();
			this.labelManaged.Name = "labelManaged";
			this.labelManaged.Xalign = 0F;
			this.labelManaged.LabelProp = global::Mono.Unix.Catalog.GetString ("A managed application is a lightweight application. It requires an installed Mono runtime on the target machines in order to be run.");
			this.labelManaged.Wrap = true;
			this.labelManaged.Justify = ((global::Gtk.Justification)(3));
			this.vbox.Add (this.labelManaged);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox [this.labelManaged]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.radiobuttonNative = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Export As Native Application"));
			this.radiobuttonNative.CanFocus = true;
			this.radiobuttonNative.Name = "radiobuttonNative";
			this.radiobuttonNative.DrawIndicator = true;
			this.radiobuttonNative.UseUnderline = true;
			this.radiobuttonNative.Group = this.radiobuttonManaged.Group;
			this.vbox.Add (this.radiobuttonNative);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox [this.radiobuttonNative]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.labelNative = new global::Gtk.Label ();
			this.labelNative.Name = "labelNative";
			this.labelNative.Xalign = 0F;
			this.labelNative.LabelProp = global::Mono.Unix.Catalog.GetString ("A native application is a self contained application. You can distribute it without the need to install the Mono runtime on the target machines.");
			this.labelNative.Wrap = true;
			this.labelNative.Justify = ((global::Gtk.Justification)(3));
			this.vbox.Add (this.labelNative);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox [this.labelNative]));
			w5.Position = 3;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox [this.hseparator1]));
			w6.Position = 4;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.labelOutput = new global::Gtk.Label ();
			this.labelOutput.Name = "labelOutput";
			this.labelOutput.Xalign = 1F;
			this.labelOutput.LabelProp = global::Mono.Unix.Catalog.GetString ("Output Folder:");
			this.hbox2.Add (this.labelOutput);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.labelOutput]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.filechooserbuttonOutput = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select The Destination Folder"), ((global::Gtk.FileChooserAction)(2)));
			this.filechooserbuttonOutput.Name = "filechooserbuttonOutput";
			this.hbox2.Add (this.filechooserbuttonOutput);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.filechooserbuttonOutput]));
			w8.Position = 1;
			this.vbox.Add (this.hbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox [this.hbox2]));
			w9.Position = 5;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator ();
			this.hseparator2.Name = "hseparator2";
			this.vbox.Add (this.hseparator2);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox [this.hseparator2]));
			w10.Position = 6;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox.Gtk.Box+BoxChild
			this.progressbar = new global::Gtk.ProgressBar ();
			this.progressbar.Name = "progressbar";
			this.progressbar.Fraction = 0.5;
			this.vbox.Add (this.progressbar);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox [this.progressbar]));
			w11.Position = 7;
			w11.Expand = false;
			w11.Fill = false;
			w1.Add (this.vbox);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Internal child MonoDevelop.Monobjc.Gui.MonobjcExportDialog.ActionArea
			global::Gtk.HButtonBox w13 = this.ActionArea;
			w13.Name = "dialog1_ActionArea";
			w13.Spacing = 10;
			w13.BorderWidth = ((uint)(5));
			w13.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-close";
			this.AddActionWidget (this.buttonCancel, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w14 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w13 [this.buttonCancel]));
			w14.Expand = false;
			w14.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			// Container child buttonOk.Gtk.Container+ContainerChild
			global::Gtk.Alignment w15 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w16 = new global::Gtk.HBox ();
			w16.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w17 = new global::Gtk.Image ();
			w17.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-ok", global::Gtk.IconSize.Menu);
			w16.Add (w17);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w19 = new global::Gtk.Label ();
			w19.LabelProp = global::Mono.Unix.Catalog.GetString ("Create");
			w16.Add (w19);
			w15.Add (w16);
			this.buttonOk.Add (w15);
			w13.Add (this.buttonOk);
			global::Gtk.ButtonBox.ButtonBoxChild w23 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w13 [this.buttonOk]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnExport);
		}
	}
}
