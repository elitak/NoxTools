using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for DoorProperties.
	/// </summary>
	public class DoorProperties : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox dirBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox lockBox;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;

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
				if (obj.modbuf != null)
				{
					System.IO.BinaryReader rdr = new System.IO.BinaryReader(new System.IO.MemoryStream(obj.modbuf));
					dirBox.Text = ((DOORS_DIR)rdr.ReadInt32()).ToString();
					lockBox.Text = ((DOORS_LOCK)rdr.ReadInt32()).ToString();
				}
			}
		}

		public enum DOORS_DIR : int
		{
			South = 00,
			North = 0x10,
			East = 0x18,
			West = 0x08
		}

		public enum DOORS_LOCK : int
		{
			None = 0,
			Silver = 1,
			Gold = 2,
			Ruby = 3,
			Saphire = 4,
			Mechanism = 5
		}

		public DoorProperties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			dirBox.Items.AddRange(Enum.GetNames(typeof(DOORS_DIR)));
			lockBox.Items.AddRange(Enum.GetNames(typeof(DOORS_LOCK)));
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
			this.dirBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lockBox = new System.Windows.Forms.ComboBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dirBox
			// 
			this.dirBox.Location = new System.Drawing.Point(8, 32);
			this.dirBox.Name = "dirBox";
			this.dirBox.Size = new System.Drawing.Size(88, 21);
			this.dirBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Direction";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(104, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Lock-Style";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lockBox
			// 
			this.lockBox.Location = new System.Drawing.Point(104, 32);
			this.lockBox.Name = "lockBox";
			this.lockBox.Size = new System.Drawing.Size(88, 21);
			this.lockBox.TabIndex = 2;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(104, 64);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(16, 64);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// DoorProperties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(200, 93);
			this.ControlBox = false;
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lockBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dirBox);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(208, 184);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(208, 120);
			this.Name = "DoorProperties";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Door Properties";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			System.IO.BinaryWriter wtr = new System.IO.BinaryWriter(stream);
			wtr.Write((int)Enum.Parse(typeof(DOORS_DIR),dirBox.Text));
			wtr.Write((int)Enum.Parse(typeof(DOORS_LOCK),lockBox.Text));
			wtr.Write((int)Enum.Parse(typeof(DOORS_DIR),dirBox.Text));
			obj.modbuf = stream.ToArray();
			Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
