using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	public class BlendDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.ComboBox comboBox5;
		private System.Windows.Forms.ComboBox comboBox6;
		private System.Windows.Forms.ComboBox comboBox7;
		private System.Windows.Forms.ComboBox comboBox8;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label3;

		public BlendDialog()
		{
			InitializeComponent();

			comboBox1.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			comboBox1.SelectedIndex = 0;
			comboBox4.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			comboBox4.SelectedIndex = 0;
			comboBox8.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			comboBox8.SelectedIndex = 0;
			comboBox6.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			comboBox6.SelectedIndex = 0;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.comboBox5 = new System.Windows.Forms.ComboBox();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.comboBox7 = new System.Windows.Forms.ComboBox();
			this.comboBox8 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(16, 40);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(64, 24);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Blend 1";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(16, 85);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(64, 24);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Blend 2";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(16, 130);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(64, 24);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "Blend 3";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(16, 175);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(64, 24);
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "Blend 4";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(109, 216);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(96, 40);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(80, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// comboBox2
			// 
			this.comboBox2.Location = new System.Drawing.Point(192, 40);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(80, 21);
			this.comboBox2.TabIndex = 6;
			// 
			// comboBox3
			// 
			this.comboBox3.Location = new System.Drawing.Point(192, 85);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(80, 21);
			this.comboBox3.TabIndex = 8;
			// 
			// comboBox4
			// 
			this.comboBox4.Location = new System.Drawing.Point(96, 85);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(80, 21);
			this.comboBox4.TabIndex = 7;
			// 
			// comboBox5
			// 
			this.comboBox5.Location = new System.Drawing.Point(192, 175);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new System.Drawing.Size(80, 21);
			this.comboBox5.TabIndex = 12;
			// 
			// comboBox6
			// 
			this.comboBox6.Location = new System.Drawing.Point(96, 175);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(80, 21);
			this.comboBox6.TabIndex = 11;
			// 
			// comboBox7
			// 
			this.comboBox7.Location = new System.Drawing.Point(192, 130);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new System.Drawing.Size(80, 21);
			this.comboBox7.TabIndex = 10;
			// 
			// comboBox8
			// 
			this.comboBox8.Location = new System.Drawing.Point(96, 130);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new System.Drawing.Size(80, 21);
			this.comboBox8.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Enable";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(96, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Tile";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 15;
			this.label3.Text = "Direction";
			// 
			// BlendDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 247);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox5);
			this.Controls.Add(this.comboBox6);
			this.Controls.Add(this.comboBox7);
			this.Controls.Add(this.comboBox8);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.comboBox4);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BlendDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tile Blending";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			Hide();
		}
	}
}
