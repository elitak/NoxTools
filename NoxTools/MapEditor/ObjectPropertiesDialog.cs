using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for ObjectPropertiesDialog.
	/// </summary>
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
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox nameBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ObjectPropertiesDialog()
		{
			InitializeComponent();

			//ArrayList nameList = new ArrayList(ThingDb.Things.Keys);
			//nameList.Sort();
			//nameBox.Items.AddRange(nameList.ToArray());
			nameBox.Items.AddRange(new ArrayList(ThingDb.Things.Keys).ToArray());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();
			base.Dispose(disposing);
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
			this.label5 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.ComboBox();
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
			this.buttonOK.Location = new System.Drawing.Point(12, 192);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(100, 192);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			// 
			// label2
			// 
			this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label2.Location = new System.Drawing.Point(40, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "X";
			// 
			// label3
			// 
			this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label3.Location = new System.Drawing.Point(104, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Y";
			// 
			// xBox
			// 
			this.xBox.Location = new System.Drawing.Point(56, 40);
			this.xBox.Name = "xBox";
			this.xBox.Size = new System.Drawing.Size(40, 20);
			this.xBox.TabIndex = 6;
			this.xBox.Text = "";
			// 
			// yBox
			// 
			this.yBox.Location = new System.Drawing.Point(120, 40);
			this.yBox.Name = "yBox";
			this.yBox.Size = new System.Drawing.Size(40, 20);
			this.yBox.TabIndex = 7;
			this.yBox.Text = "";
			// 
			// label4
			// 
			this.label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label4.Location = new System.Drawing.Point(16, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = "Extent";
			// 
			// extentBox
			// 
			this.extentBox.Location = new System.Drawing.Point(56, 64);
			this.extentBox.Name = "extentBox";
			this.extentBox.Size = new System.Drawing.Size(40, 20);
			this.extentBox.TabIndex = 9;
			this.extentBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(168, 80);
			this.label5.TabIndex = 10;
			this.label5.Text = "Note: Only enter names of objects that are IMMOBILE. Moveable/pickupable objects " +
				"are not yet supported. The map will not load if you create any objects of these " +
				"types.";
			// 
			// nameBox
			// 
			this.nameBox.DropDownWidth = 200;
			this.nameBox.Location = new System.Drawing.Point(56, 16);
			this.nameBox.MaxDropDownItems = 16;
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(120, 21);
			this.nameBox.TabIndex = 11;
			// 
			// ObjectPropertiesDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(186, 223);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.extentBox);
			this.Controls.Add(this.yBox);
			this.Controls.Add(this.xBox);
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
			this.Visible = false;
		}
	}
}
