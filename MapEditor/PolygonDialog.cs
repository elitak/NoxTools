using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
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
        private Label labelOnEnter;
        private TextBox onEnterBox;
        //MapView mapview;

        private System.Windows.Forms.TextBox boxPoints;

		public PolygonDialog(/*MapView view*/)
		{
            //mapview = view;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.boxPoints = new System.Windows.Forms.TextBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mmGroup = new System.Windows.Forms.TextBox();
            this.labelOnEnter = new System.Windows.Forms.Label();
            this.onEnterBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
// 
// label1
// 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
// 
// boxPoints
// 
            resources.ApplyResources(this.boxPoints, "boxPoints");
            this.boxPoints.Name = "boxPoints";
// 
// colorButton
// 
            resources.ApplyResources(this.colorButton, "colorButton");
            this.colorButton.Name = "colorButton";
            this.colorButton.Click += new System.EventHandler(this.button1_Click);
// 
// buttonOK
// 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
// 
// buttonCancel
// 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
// 
// name
// 
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
// 
// label2
// 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
// 
// label3
// 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
// 
// mmGroup
// 
            resources.ApplyResources(this.mmGroup, "mmGroup");
            this.mmGroup.Name = "mmGroup";
// 
// labelOnEnter
// 
            resources.ApplyResources(this.labelOnEnter, "labelOnEnter");
            this.labelOnEnter.Name = "labelOnEnter";
// 
// onEnterBox
// 
            resources.ApplyResources(this.onEnterBox, "onEnterBox");
            this.onEnterBox.Name = "onEnterBox";
// 
// PolygonDialog
// 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.onEnterBox);
            this.Controls.Add(this.labelOnEnter);
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
            this.ResumeLayout(false);
            this.PerformLayout();

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
                List<PointF> points = new List<PointF>();
                Regex regex = new Regex("\\((?<xval>[0-9]+), (?<yval>[0-9]+)\\)");
				foreach (Match match in regex.Matches(boxPoints.Text))
					points.Add(new PointF(Convert.ToSingle(match.Groups["xval"].Value), Convert.ToSingle(match.Groups["yval"].Value)));

				if (polygon == null)
				{
					return new Map.Polygon(name.Text, colorButton.BackColor, Convert.ToByte(mmGroup.Text), points, onEnterBox.Text);
				}
				else
				{
					polygon.Name = name.Text;
					polygon.AmbientLightColor = colorButton.BackColor;
					polygon.MinimapGroup = Convert.ToByte(mmGroup.Text);
					polygon.Points = points;
                    polygon.EnterFunc = onEnterBox.Text;

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
                    onEnterBox.Text = polygon.EnterFunc;
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
