using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for WallSelector.
	/// </summary>
	public class WallSelector : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.ComboBox material;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox minimapGroup;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.IContainer components;

		public new MapView Parent;
		protected ArrayList wallFacingButtons;
		protected Map.Wall.WallFacing facing = Map.Wall.WallFacing.NORTH;

		public WallSelector()
		{
			InitializeComponent();

			wallFacingButtons = new ArrayList(new Button[] {button1, button8, button12, button10, button4, button6, button3, button5, button7, button2, button11});
			material.Items.AddRange(ThingDb.WallNames.ToArray());
			material.SelectedIndex = 0;
			minimapGroup.Text = "100";
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WallSelector));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.material = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.minimapGroup = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(4, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(16, 16);
			this.button1.TabIndex = 0;
			this.button1.Text = "/";
			this.button1.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.Location = new System.Drawing.Point(28, 48);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(16, 16);
			this.button2.TabIndex = 1;
			this.button2.Text = "^";
			this.button2.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button3.ImageIndex = 1;
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new System.Drawing.Point(40, 104);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(16, 16);
			this.button3.TabIndex = 2;
			this.button3.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button6.ImageIndex = 0;
			this.button6.ImageList = this.imageList1;
			this.button6.Location = new System.Drawing.Point(16, 104);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(16, 16);
			this.button6.TabIndex = 6;
			this.button6.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button7.Location = new System.Drawing.Point(12, 64);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(16, 16);
			this.button7.TabIndex = 5;
			this.button7.Text = "<";
			this.button7.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button8.Location = new System.Drawing.Point(28, 24);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(16, 16);
			this.button8.TabIndex = 4;
			this.button8.Text = "\\";
			this.button8.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button10.ImageIndex = 3;
			this.button10.ImageList = this.imageList1;
			this.button10.Location = new System.Drawing.Point(40, 128);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(16, 16);
			this.button10.TabIndex = 10;
			this.button10.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button11.Location = new System.Drawing.Point(44, 64);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(16, 16);
			this.button11.TabIndex = 9;
			this.button11.Text = ">";
			this.button11.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button12.Location = new System.Drawing.Point(52, 24);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(16, 16);
			this.button12.TabIndex = 8;
			this.button12.Text = "X";
			this.button12.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button4.ImageIndex = 2;
			this.button4.ImageList = this.imageList1;
			this.button4.Location = new System.Drawing.Point(16, 128);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(16, 16);
			this.button4.TabIndex = 12;
			this.button4.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button5.Location = new System.Drawing.Point(28, 80);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(16, 16);
			this.button5.TabIndex = 11;
			this.button5.Text = "v";
			this.button5.Click += new System.EventHandler(this.wallFacingButton_Click);
			// 
			// material
			// 
			this.material.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.material.DropDownWidth = 200;
			this.material.Location = new System.Drawing.Point(0, 168);
			this.material.Name = "material";
			this.material.Size = new System.Drawing.Size(80, 21);
			this.material.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(-4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Wall Facing";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "Wall Material";
			// 
			// minimapGroup
			// 
			this.minimapGroup.Location = new System.Drawing.Point(48, 200);
			this.minimapGroup.Name = "minimapGroup";
			this.minimapGroup.Size = new System.Drawing.Size(28, 20);
			this.minimapGroup.TabIndex = 15;
			this.minimapGroup.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 200);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 28);
			this.label4.TabIndex = 16;
			this.label4.Text = "Minimap Group";
			// 
			// WallSelector
			// 
			this.Controls.Add(this.minimapGroup);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.material);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.button11);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Name = "WallSelector";
			this.Size = new System.Drawing.Size(80, 236);
			this.ResumeLayout(false);

		}
		#endregion

		private void wallFacingButton_Click(object sender, EventArgs e)
		{
			facing = (Map.Wall.WallFacing) wallFacingButtons.IndexOf(sender);
			Parent.CurrentMode = MapView.Mode.MAKE_WALL;
		}

		public Map.Wall NewWall(Point location)
		{
			return new Map.Wall(location, facing, (byte) material.SelectedIndex, Convert.ToByte(minimapGroup.Text));
		}
	}
}
