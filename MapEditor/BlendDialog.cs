using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using NoxShared;

namespace NoxMapEditor
{
	public class BlendDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox tile1Graphic;
		private System.Windows.Forms.ComboBox tile1Var;
		private System.Windows.Forms.ComboBox tile2Var;
		private System.Windows.Forms.ComboBox tile2Graphic;
		private System.Windows.Forms.ComboBox tile4Var;
		private System.Windows.Forms.ComboBox tile4Graphic;
		private System.Windows.Forms.ComboBox tile3Var;
		private System.Windows.Forms.ComboBox tile3Graphic;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox tile4Dir;
		private System.Windows.Forms.ComboBox tile3Dir;
		private System.Windows.Forms.ComboBox tile2Dir;
		private System.Windows.Forms.ComboBox tile1Dir;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox tile1Enabled;
		private System.Windows.Forms.CheckBox tile2Enabled;
		private System.Windows.Forms.CheckBox tile3Enabled;
		private System.Windows.Forms.CheckBox tile4Enabled;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox tile1BlendType;
		private System.Windows.Forms.ComboBox tile2BlendType;
		private System.Windows.Forms.ComboBox tile3BlendType;
		private System.Windows.Forms.ComboBox tile4BlendType;

		public ArrayList Blends = new ArrayList();
		
		public BlendDialog()
		{
			InitializeComponent();

			tile1Graphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			tile2Graphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			tile3Graphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			tile4Graphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());

			tile1Dir.Items.AddRange(Enum.GetNames(typeof(Map.Tile.EdgeTile.Direction)));
			tile2Dir.Items.AddRange(Enum.GetNames(typeof(Map.Tile.EdgeTile.Direction)));
			tile3Dir.Items.AddRange(Enum.GetNames(typeof(Map.Tile.EdgeTile.Direction)));
			tile4Dir.Items.AddRange(Enum.GetNames(typeof(Map.Tile.EdgeTile.Direction)));

			tile1BlendType.Items.AddRange(ThingDb.EdgeTileNames.ToArray());
			tile2BlendType.Items.AddRange(ThingDb.EdgeTileNames.ToArray());
			tile3BlendType.Items.AddRange(ThingDb.EdgeTileNames.ToArray());
			tile4BlendType.Items.AddRange(ThingDb.EdgeTileNames.ToArray());
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tile1Enabled = new System.Windows.Forms.CheckBox();
			this.tile2Enabled = new System.Windows.Forms.CheckBox();
			this.tile3Enabled = new System.Windows.Forms.CheckBox();
			this.tile4Enabled = new System.Windows.Forms.CheckBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tile1Graphic = new System.Windows.Forms.ComboBox();
			this.tile1Var = new System.Windows.Forms.ComboBox();
			this.tile2Var = new System.Windows.Forms.ComboBox();
			this.tile2Graphic = new System.Windows.Forms.ComboBox();
			this.tile4Var = new System.Windows.Forms.ComboBox();
			this.tile4Graphic = new System.Windows.Forms.ComboBox();
			this.tile3Var = new System.Windows.Forms.ComboBox();
			this.tile3Graphic = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tile4Dir = new System.Windows.Forms.ComboBox();
			this.tile3Dir = new System.Windows.Forms.ComboBox();
			this.tile2Dir = new System.Windows.Forms.ComboBox();
			this.tile1Dir = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.tile1BlendType = new System.Windows.Forms.ComboBox();
			this.tile2BlendType = new System.Windows.Forms.ComboBox();
			this.tile3BlendType = new System.Windows.Forms.ComboBox();
			this.tile4BlendType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// tile1Enabled
			// 
			this.tile1Enabled.Location = new System.Drawing.Point(24, 40);
			this.tile1Enabled.Name = "tile1Enabled";
			this.tile1Enabled.Size = new System.Drawing.Size(16, 24);
			this.tile1Enabled.TabIndex = 0;
			// 
			// tile2Enabled
			// 
			this.tile2Enabled.Location = new System.Drawing.Point(24, 80);
			this.tile2Enabled.Name = "tile2Enabled";
			this.tile2Enabled.Size = new System.Drawing.Size(16, 24);
			this.tile2Enabled.TabIndex = 1;
			// 
			// tile3Enabled
			// 
			this.tile3Enabled.Location = new System.Drawing.Point(24, 120);
			this.tile3Enabled.Name = "tile3Enabled";
			this.tile3Enabled.Size = new System.Drawing.Size(16, 24);
			this.tile3Enabled.TabIndex = 2;
			// 
			// tile4Enabled
			// 
			this.tile4Enabled.Location = new System.Drawing.Point(24, 160);
			this.tile4Enabled.Name = "tile4Enabled";
			this.tile4Enabled.Size = new System.Drawing.Size(16, 24);
			this.tile4Enabled.TabIndex = 3;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(104, 192);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// tile1Graphic
			// 
			this.tile1Graphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile1Graphic.DropDownWidth = 180;
			this.tile1Graphic.Location = new System.Drawing.Point(64, 40);
			this.tile1Graphic.Name = "tile1Graphic";
			this.tile1Graphic.Size = new System.Drawing.Size(80, 21);
			this.tile1Graphic.TabIndex = 5;
			this.tile1Graphic.SelectedIndexChanged += new System.EventHandler(this.tile1Graphic_SelectedIndexChanged);
			// 
			// tile1Var
			// 
			this.tile1Var.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile1Var.Location = new System.Drawing.Point(160, 40);
			this.tile1Var.MaxDropDownItems = 10;
			this.tile1Var.Name = "tile1Var";
			this.tile1Var.Size = new System.Drawing.Size(48, 21);
			this.tile1Var.TabIndex = 6;
			// 
			// tile2Var
			// 
			this.tile2Var.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile2Var.Location = new System.Drawing.Point(160, 80);
			this.tile2Var.MaxDropDownItems = 10;
			this.tile2Var.Name = "tile2Var";
			this.tile2Var.Size = new System.Drawing.Size(48, 21);
			this.tile2Var.TabIndex = 8;
			// 
			// tile2Graphic
			// 
			this.tile2Graphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile2Graphic.DropDownWidth = 180;
			this.tile2Graphic.Location = new System.Drawing.Point(64, 80);
			this.tile2Graphic.Name = "tile2Graphic";
			this.tile2Graphic.Size = new System.Drawing.Size(80, 21);
			this.tile2Graphic.TabIndex = 7;
			this.tile2Graphic.SelectedIndexChanged += new System.EventHandler(this.tile2Graphic_SelectedIndexChanged);
			// 
			// tile4Var
			// 
			this.tile4Var.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile4Var.Location = new System.Drawing.Point(160, 160);
			this.tile4Var.MaxDropDownItems = 10;
			this.tile4Var.Name = "tile4Var";
			this.tile4Var.Size = new System.Drawing.Size(48, 21);
			this.tile4Var.TabIndex = 12;
			// 
			// tile4Graphic
			// 
			this.tile4Graphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile4Graphic.DropDownWidth = 180;
			this.tile4Graphic.Location = new System.Drawing.Point(64, 160);
			this.tile4Graphic.Name = "tile4Graphic";
			this.tile4Graphic.Size = new System.Drawing.Size(80, 21);
			this.tile4Graphic.TabIndex = 11;
			this.tile4Graphic.SelectedIndexChanged += new System.EventHandler(this.tile4Graphic_SelectedIndexChanged);
			// 
			// tile3Var
			// 
			this.tile3Var.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile3Var.Location = new System.Drawing.Point(160, 120);
			this.tile3Var.MaxDropDownItems = 10;
			this.tile3Var.Name = "tile3Var";
			this.tile3Var.Size = new System.Drawing.Size(48, 21);
			this.tile3Var.TabIndex = 10;
			// 
			// tile3Graphic
			// 
			this.tile3Graphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile3Graphic.DropDownWidth = 180;
			this.tile3Graphic.Location = new System.Drawing.Point(64, 120);
			this.tile3Graphic.Name = "tile3Graphic";
			this.tile3Graphic.Size = new System.Drawing.Size(80, 21);
			this.tile3Graphic.TabIndex = 9;
			this.tile3Graphic.SelectedIndexChanged += new System.EventHandler(this.tile3Graphic_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Enable";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(64, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Tile";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(160, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 15;
			this.label3.Text = "Variation";
			// 
			// tile4Dir
			// 
			this.tile4Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile4Dir.DropDownWidth = 128;
			this.tile4Dir.Location = new System.Drawing.Point(224, 160);
			this.tile4Dir.Name = "tile4Dir";
			this.tile4Dir.Size = new System.Drawing.Size(64, 21);
			this.tile4Dir.TabIndex = 19;
			// 
			// tile3Dir
			// 
			this.tile3Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile3Dir.DropDownWidth = 128;
			this.tile3Dir.Location = new System.Drawing.Point(224, 120);
			this.tile3Dir.Name = "tile3Dir";
			this.tile3Dir.Size = new System.Drawing.Size(64, 21);
			this.tile3Dir.TabIndex = 18;
			// 
			// tile2Dir
			// 
			this.tile2Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile2Dir.DropDownWidth = 128;
			this.tile2Dir.Location = new System.Drawing.Point(224, 80);
			this.tile2Dir.Name = "tile2Dir";
			this.tile2Dir.Size = new System.Drawing.Size(64, 21);
			this.tile2Dir.TabIndex = 17;
			// 
			// tile1Dir
			// 
			this.tile1Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile1Dir.DropDownWidth = 128;
			this.tile1Dir.Location = new System.Drawing.Point(224, 40);
			this.tile1Dir.Name = "tile1Dir";
			this.tile1Dir.Size = new System.Drawing.Size(64, 21);
			this.tile1Dir.TabIndex = 16;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(224, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 20;
			this.label4.Text = "Direction";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(200, 192);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 21;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(304, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 22;
			this.label5.Text = "Edge";
			// 
			// tile1BlendType
			// 
			this.tile1BlendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile1BlendType.DropDownWidth = 180;
			this.tile1BlendType.Location = new System.Drawing.Point(304, 40);
			this.tile1BlendType.Name = "tile1BlendType";
			this.tile1BlendType.Size = new System.Drawing.Size(64, 21);
			this.tile1BlendType.TabIndex = 23;
			// 
			// tile2BlendType
			// 
			this.tile2BlendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile2BlendType.DropDownWidth = 180;
			this.tile2BlendType.Location = new System.Drawing.Point(304, 80);
			this.tile2BlendType.Name = "tile2BlendType";
			this.tile2BlendType.Size = new System.Drawing.Size(64, 21);
			this.tile2BlendType.TabIndex = 24;
			// 
			// tile3BlendType
			// 
			this.tile3BlendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile3BlendType.DropDownWidth = 180;
			this.tile3BlendType.Location = new System.Drawing.Point(304, 120);
			this.tile3BlendType.Name = "tile3BlendType";
			this.tile3BlendType.Size = new System.Drawing.Size(64, 21);
			this.tile3BlendType.TabIndex = 25;
			// 
			// tile4BlendType
			// 
			this.tile4BlendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tile4BlendType.DropDownWidth = 180;
			this.tile4BlendType.Location = new System.Drawing.Point(304, 160);
			this.tile4BlendType.Name = "tile4BlendType";
			this.tile4BlendType.Size = new System.Drawing.Size(64, 21);
			this.tile4BlendType.TabIndex = 26;
			// 
			// BlendDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(378, 223);
			this.Controls.Add(this.tile4BlendType);
			this.Controls.Add(this.tile3BlendType);
			this.Controls.Add(this.tile2BlendType);
			this.Controls.Add(this.tile1BlendType);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tile4Dir);
			this.Controls.Add(this.tile3Dir);
			this.Controls.Add(this.tile2Dir);
			this.Controls.Add(this.tile1Dir);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tile4Var);
			this.Controls.Add(this.tile4Graphic);
			this.Controls.Add(this.tile3Var);
			this.Controls.Add(this.tile3Graphic);
			this.Controls.Add(this.tile2Var);
			this.Controls.Add(this.tile2Graphic);
			this.Controls.Add(this.tile1Var);
			this.Controls.Add(this.tile1Graphic);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.tile4Enabled);
			this.Controls.Add(this.tile3Enabled);
			this.Controls.Add(this.tile2Enabled);
			this.Controls.Add(this.tile1Enabled);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BlendDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tile Edges";
			this.ResumeLayout(false);

		}
		#endregion

		private byte GetVariation(ComboBox box)
		{
			return box.SelectedIndex == 0 ? (byte) new Random().Next(((ThingDb.Tile) ThingDb.FloorTiles[box.SelectedIndex]).Variations) : Convert.ToByte(box.Text);
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			Blends = new ArrayList();

			if (tile1Enabled.Checked)
				Blends.Add(new Map.Tile.EdgeTile(
					(byte) tile1Graphic.SelectedIndex,
					GetVariation(tile1Var),
					(Map.Tile.EdgeTile.Direction) Enum.GetValues(typeof(Map.Tile.EdgeTile.Direction)).GetValue(tile1Dir.SelectedIndex),
					(byte) tile1BlendType.SelectedIndex));
			if (tile2Enabled.Checked)
				Blends.Add(new Map.Tile.EdgeTile(
					(byte) tile2Graphic.SelectedIndex,
					GetVariation(tile2Var),
					(Map.Tile.EdgeTile.Direction) Enum.GetValues(typeof(Map.Tile.EdgeTile.Direction)).GetValue(tile2Dir.SelectedIndex),
					(byte) tile2BlendType.SelectedIndex));
			if (tile3Enabled.Checked)
				Blends.Add(new Map.Tile.EdgeTile(
					(byte) tile3Graphic.SelectedIndex,
					GetVariation(tile3Var),
					(Map.Tile.EdgeTile.Direction) Enum.GetValues(typeof(Map.Tile.EdgeTile.Direction)).GetValue(tile3Dir.SelectedIndex),
					(byte) tile3BlendType.SelectedIndex));
			if (tile4Enabled.Checked)
				Blends.Add(new Map.Tile.EdgeTile(
					(byte) tile4Graphic.SelectedIndex,
					GetVariation(tile4Var),
					(Map.Tile.EdgeTile.Direction) Enum.GetValues(typeof(Map.Tile.EdgeTile.Direction)).GetValue(tile4Dir.SelectedIndex),
					(byte) tile4BlendType.SelectedIndex));

			Hide();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void tile1Graphic_SelectedIndexChanged(object sender, EventArgs e)
		{
			repopulateVariations(tile1Var, ((ThingDb.Tile) ThingDb.FloorTiles[((ComboBox) sender).SelectedIndex]).Variations);
		}

		private void tile2Graphic_SelectedIndexChanged(object sender, EventArgs e)
		{
			repopulateVariations(tile2Var, ((ThingDb.Tile) ThingDb.FloorTiles[((ComboBox) sender).SelectedIndex]).Variations);
		}

		private void tile3Graphic_SelectedIndexChanged(object sender, EventArgs e)
		{
			repopulateVariations(tile3Var, ((ThingDb.Tile) ThingDb.FloorTiles[((ComboBox) sender).SelectedIndex]).Variations);
		}

		private void tile4Graphic_SelectedIndexChanged(object sender, EventArgs e)
		{
			repopulateVariations(tile4Var, ((ThingDb.Tile) ThingDb.FloorTiles[((ComboBox) sender).SelectedIndex]).Variations);
		}

		private void repopulateVariations(ComboBox box, int variations)
		{
			int oldNdx = box.SelectedIndex;
			box.Items.Clear();
			box.Items.Add("Random");
			for (int i = 0; i < variations; i++)
				box.Items.Add(String.Format("{0}", i));
			if (oldNdx < box.Items.Count)
				box.SelectedIndex = oldNdx;
		}
	}
}
