using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using NoxShared;

namespace NoxMapEditor
{
	public class PolygonDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;

		private ColorDialog clr = new ColorDialog();
		private System.Windows.Forms.Button colorButton;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox mmGroup;
		private System.Windows.Forms.TextBox boxPoints;

		public PolygonDialog()
		{
			InitializeComponent();
			colorButton.BackColor = Color.White;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.boxPoints = new System.Windows.Forms.TextBox();
			this.colorButton = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.name = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.mmGroup = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Points";
			// 
			// boxPoints
			// 
			this.boxPoints.Location = new System.Drawing.Point(8, 56);
			this.boxPoints.Multiline = true;
			this.boxPoints.Name = "boxPoints";
			this.boxPoints.Size = new System.Drawing.Size(168, 56);
			this.boxPoints.TabIndex = 1;
			this.boxPoints.Text = "";
			// 
			// colorButton
			// 
			this.colorButton.Location = new System.Drawing.Point(8, 128);
			this.colorButton.Name = "colorButton";
			this.colorButton.Size = new System.Drawing.Size(112, 23);
			this.colorButton.TabIndex = 2;
			this.colorButton.Text = "Ambient Light Color";
			this.colorButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(24, 168);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(112, 168);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			// 
			// name
			// 
			this.name.Location = new System.Drawing.Point(48, 8);
			this.name.Name = "name";
			this.name.TabIndex = 5;
			this.name.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Name";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(128, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 32);
			this.label3.TabIndex = 7;
			this.label3.Text = "Minimap Group";
			// 
			// mmGroup
			// 
			this.mmGroup.Location = new System.Drawing.Point(176, 128);
			this.mmGroup.Name = "mmGroup";
			this.mmGroup.Size = new System.Drawing.Size(32, 20);
			this.mmGroup.TabIndex = 8;
			this.mmGroup.Text = "100";
			// 
			// PolygonDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(210, 199);
			this.Controls.Add(this.mmGroup);
			this.Controls.Add(this.name);
			this.Controls.Add(this.boxPoints);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.colorButton);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PolygonDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Polygon";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (clr.ShowDialog() == DialogResult.OK)
				colorButton.BackColor = clr.Color;
		}


		protected Map.Polygon polygon;
		public Map.Polygon Polygon
		{
			get
			{
				ArrayList points = new ArrayList();
				Regex regex = new Regex("\\((?<xval>[0-9]+), (?<yval>[0-9]+)\\)");
				foreach (Match match in regex.Matches(boxPoints.Text))
					points.Add(new PointF(Convert.ToSingle(match.Groups["xval"].Value), Convert.ToSingle(match.Groups["yval"].Value)));

				if (polygon == null)
				{
					return new Map.Polygon(name.Text, colorButton.BackColor, Convert.ToByte(mmGroup.Text), points);
				}
				else
				{
					polygon.Name = name.Text;
					polygon.AmbientLightColor = colorButton.BackColor;
					polygon.MinimapGroup = Convert.ToByte(mmGroup.Text);
					polygon.Points = points;
					return polygon;
				}
			}
			set
			{
				polygon = value;
				if (polygon == null)
				{
					name.Text = "";
					colorButton.BackColor = Color.White;
					boxPoints.Text = "";
				}
				else
				{
					name.Text = polygon.Name;
					colorButton.BackColor = polygon.AmbientLightColor;
					mmGroup.Text = polygon.MinimapGroup.ToString();
					boxPoints.Text = "";
					foreach (PointF pt in polygon.Points)
						boxPoints.Text += String.Format("({0}, {1}) ", pt.X, pt.Y);
				}
			}
		}
	}
}
