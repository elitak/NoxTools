using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using NoxShared;

namespace NoxMapEditor
{
	public class ObjectPropertiesDialog : System.Windows.Forms.Form
	{
		protected Map.Object obj;
		public Map.Object Object
		{
			get
			{
				return obj;
			}
			set
			{
				obj = value;
				nameBox.Text = obj.Name;
				xBox.Text = obj.Location.X.ToString();
				yBox.Text = obj.Location.Y.ToString();
				extentBox.Text = obj.Extent.ToString();
				teamBox.Text = obj.Team.ToString();
				scrNameBox.Text = obj.Scr_Name;
				//print out the bytes in hex
				boxMod.Text = "";
				if (obj.modbuf != null)
				{
					System.IO.BinaryReader rdr = new System.IO.BinaryReader(new System.IO.MemoryStream(obj.modbuf));
					while (rdr.BaseStream.Position < rdr.BaseStream.Length)
						boxMod.Text += String.Format("{0:x2} ", rdr.ReadByte());
				}
				if(((ThingDb.Thing)ThingDb.Things[obj.Name]).Init=="ModifierInit")
					enchantButton.Enabled = true;
				else if((((ThingDb.Thing)ThingDb.Things[obj.Name]).Class & ThingDb.Thing.ClassFlags.DOOR)==ThingDb.Thing.ClassFlags.DOOR)
					lockButton.Enabled = true;
			}
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox xBox;
		private System.Windows.Forms.TextBox yBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox extentBox;
		private System.Windows.Forms.ComboBox nameBox;
		private System.Windows.Forms.Button enchantButton;
		private System.Windows.Forms.Button lockButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox teamBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox scrNameBox;
		private System.Windows.Forms.CheckBox xtraBox;
		private System.Windows.Forms.TextBox boxMod;

		public ObjectPropertiesDialog()
		{
			InitializeComponent();
			nameBox.Items.AddRange(new ArrayList(ThingDb.Things.Keys).ToArray());
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.xBox = new System.Windows.Forms.TextBox();
			this.yBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.extentBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.ComboBox();
			this.boxMod = new System.Windows.Forms.TextBox();
			this.enchantButton = new System.Windows.Forms.Button();
			this.lockButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.teamBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.scrNameBox = new System.Windows.Forms.TextBox();
			this.xtraBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label1.Location = new System.Drawing.Point(16, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(12, 216);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(100, 216);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			// 
			// label2
			// 
			this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label2.Location = new System.Drawing.Point(40, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "X";
			// 
			// label3
			// 
			this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label3.Location = new System.Drawing.Point(104, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Y";
			// 
			// xBox
			// 
			this.xBox.Location = new System.Drawing.Point(56, 64);
			this.xBox.Name = "xBox";
			this.xBox.Size = new System.Drawing.Size(40, 20);
			this.xBox.TabIndex = 6;
			this.xBox.Text = "";
			// 
			// yBox
			// 
			this.yBox.Location = new System.Drawing.Point(120, 64);
			this.yBox.Name = "yBox";
			this.yBox.Size = new System.Drawing.Size(40, 20);
			this.yBox.TabIndex = 7;
			this.yBox.Text = "";
			// 
			// label4
			// 
			this.label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label4.Location = new System.Drawing.Point(16, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = "Extent";
			// 
			// extentBox
			// 
			this.extentBox.Location = new System.Drawing.Point(56, 88);
			this.extentBox.Name = "extentBox";
			this.extentBox.Size = new System.Drawing.Size(40, 20);
			this.extentBox.TabIndex = 9;
			this.extentBox.Text = "";
			// 
			// nameBox
			// 
			this.nameBox.DropDownWidth = 200;
			this.nameBox.Location = new System.Drawing.Point(56, 16);
			this.nameBox.MaxDropDownItems = 16;
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(120, 21);
			this.nameBox.TabIndex = 11;
			this.nameBox.SelectedIndexChanged += new System.EventHandler(this.nameBox_SelectedIndexChanged);
			// 
			// boxMod
			// 
			this.boxMod.Location = new System.Drawing.Point(16, 112);
			this.boxMod.Multiline = true;
			this.boxMod.Name = "boxMod";
			this.boxMod.Size = new System.Drawing.Size(160, 88);
			this.boxMod.TabIndex = 12;
			this.boxMod.Text = "";
			// 
			// enchantButton
			// 
			this.enchantButton.Enabled = false;
			this.enchantButton.Location = new System.Drawing.Point(192, 16);
			this.enchantButton.Name = "enchantButton";
			this.enchantButton.TabIndex = 13;
			this.enchantButton.Text = "Enchants";
			this.enchantButton.Click += new System.EventHandler(this.enchantButton_Click);
			// 
			// lockButton
			// 
			this.lockButton.Enabled = false;
			this.lockButton.Location = new System.Drawing.Point(192, 48);
			this.lockButton.Name = "lockButton";
			this.lockButton.TabIndex = 14;
			this.lockButton.Text = "Doors";
			this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(104, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 16);
			this.label5.TabIndex = 17;
			this.label5.Text = "Team";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teamBox
			// 
			this.teamBox.Location = new System.Drawing.Point(144, 88);
			this.teamBox.MaxLength = 1;
			this.teamBox.Name = "teamBox";
			this.teamBox.Size = new System.Drawing.Size(24, 20);
			this.teamBox.TabIndex = 16;
			this.teamBox.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 16);
			this.label6.TabIndex = 18;
			this.label6.Text = "Script Name";
			// 
			// scrNameBox
			// 
			this.scrNameBox.Location = new System.Drawing.Point(88, 40);
			this.scrNameBox.Name = "scrNameBox";
			this.scrNameBox.Size = new System.Drawing.Size(88, 20);
			this.scrNameBox.TabIndex = 19;
			this.scrNameBox.Text = "";
			// 
			// xtraBox
			// 
			this.xtraBox.Location = new System.Drawing.Point(184, 80);
			this.xtraBox.Name = "xtraBox";
			this.xtraBox.Size = new System.Drawing.Size(80, 16);
			this.xtraBox.TabIndex = 20;
			this.xtraBox.Text = "Xtra Bytes";
			// 
			// ObjectPropertiesDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(274, 247);
			this.Controls.Add(this.xtraBox);
			this.Controls.Add(this.scrNameBox);
			this.Controls.Add(this.teamBox);
			this.Controls.Add(this.boxMod);
			this.Controls.Add(this.extentBox);
			this.Controls.Add(this.yBox);
			this.Controls.Add(this.xBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lockButton);
			this.Controls.Add(this.enchantButton);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ObjectPropertiesDialog";
			this.Text = "Object Properites";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			//verify that we have valid input
			if (ThingDb.GetThing(nameBox.Text) == null)
			{
				MessageBox.Show("Invalid object name.", "Error");
				return;
			}
			//commit the changes
			obj.Name = nameBox.Text;
			obj.Location.X = Single.Parse(xBox.Text);
			obj.Location.Y = Single.Parse(yBox.Text);
			obj.Extent = Int32.Parse(extentBox.Text);
			obj.Terminator = (byte)(xtraBox.Checked==true? 0xFF : 0x00);
			
			if(teamBox.Text != "")
			{
				obj.Team = Byte.Parse(teamBox.Text);
				obj.Scr_Name = scrNameBox.Text;
			}
			//get the contents of the box and parse it to turn it into a byte[] and use it as the modbuf
			if (boxMod.Text.Length > 0)
			{
				System.IO.MemoryStream stream = new System.IO.MemoryStream();
				System.IO.BinaryWriter wtr = new System.IO.BinaryWriter(stream);
				Regex bytes = new Regex("[0-9|a-f|A-F]{2}");
				foreach (Match match in bytes.Matches(boxMod.Text))
					wtr.Write(Convert.ToByte(match.Value, 16));
				obj.modbuf = stream.ToArray();
			}
			else
				obj.modbuf = null;
			this.Visible = false;
		}

		private void enchantButton_Click(object sender, System.EventArgs e)
		{
			ObjectEnchantDialog enchantDlg = new ObjectEnchantDialog();
			enchantDlg.Object = obj;
			enchantDlg.ShowDialog();
			boxMod.Clear();
			if (obj.modbuf != null)
			{
				System.IO.BinaryReader rdr = new System.IO.BinaryReader(new System.IO.MemoryStream(obj.modbuf));
				while (rdr.BaseStream.Position < rdr.BaseStream.Length)
					boxMod.Text += String.Format("{0:x2} ", rdr.ReadByte());
			}
		}

		private void lockButton_Click(object sender, System.EventArgs e)
		{
			DoorProperties doorDlg = new DoorProperties();
			doorDlg.Object = obj;
			doorDlg.ShowDialog();
			boxMod.Clear();
			if (obj.modbuf != null)
			{
				System.IO.BinaryReader rdr = new System.IO.BinaryReader(new System.IO.MemoryStream(obj.modbuf));
				while (rdr.BaseStream.Position < rdr.BaseStream.Length)
					boxMod.Text += String.Format("{0:x2} ", rdr.ReadByte());
			}
		}

		private void nameBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(((ThingDb.Thing)ThingDb.Things[nameBox.Text]).Init=="ModifierInit")
				enchantButton.Enabled = true;
			else if((((ThingDb.Thing)ThingDb.Things[nameBox.Text]).Class & ThingDb.Thing.ClassFlags.DOOR)==ThingDb.Thing.ClassFlags.DOOR)
				lockButton.Enabled = true;
		}
	}
}
