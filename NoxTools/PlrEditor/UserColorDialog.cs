using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NoxShared.NoxType;

namespace PlrEditor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class UserColorDialog : Form
	{
		protected TextBox[] boxes;
		public Color Color;

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UserColorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			boxes = new TextBox[UserColor.Colors.Count];
			int row = 0, col = 0;

			for (int ndx = 0; ndx < boxes.Length; ndx++)
			{
				boxes[ndx] = new TextBox();
				boxes[ndx].BackColor = (Color) UserColor.Colors[ndx];
				boxes[ndx].Size = new Size(20, 20);
				boxes[ndx].Location = new Point(8 + 24*col, 32 + 24*row);
				boxes[ndx].Click += new EventHandler(UserColorDialog_Click);
				boxes[ndx].Cursor = this.Cursor;
				boxes[ndx].ReadOnly = true;
				Controls.Add(boxes[ndx]);
				if (ndx == 31)//special case for #33 so it lines up with the other grays
				{
					col = 7;
					row++;
				}
				else if (++col == 8)
				{
					col = 0;
					row++;
				}
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(20, 164);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(104, 164);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Colors:";
			// 
			// UserColorDialog
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(204, 191);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserColorDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Color";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			Dispose();
		}

		private void UserColorDialog_Click(object sender, EventArgs e)
		{
			Color = ((Control) sender).BackColor;
		}
	}
}
