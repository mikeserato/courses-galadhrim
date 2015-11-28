using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MNM
{
	public partial class Gimmeh : Gtk.Window
	{
		String variable_name;

		public Gimmeh () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		private void inputButton_Click(object sender, EventArgs e)
		{
			//if (String.IsNullOrEmpty(inputText.Text))
			//{
			//	MessageBox.Show("Error: You cannot pass an empty value.");
			//}
		}
	}
}

