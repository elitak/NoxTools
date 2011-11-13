using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for ObjectListDialog.
	/// </summary>
	public class ObjectListDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		protected DataTable objList;
		public Map.ObjectTable objTable
		{
			set
			{
				objList = new DataTable("objList");
				objList.Columns.Add("Extent",Type.GetType("System.UInt32"));
				objList.Columns.Add("X-Coor.",Type.GetType("System.Single"));
				objList.Columns.Add("Y-Coor.",Type.GetType("System.Single"));
				objList.Columns.Add("Name",Type.GetType("System.String"));
				foreach (Map.Object obj in value)
					objList.Rows.Add(new Object[] {obj.Extent,obj.Location.X,obj.Location.Y,obj.Name});
				dataGrid1.DataSource = objList;
			}
		}

		public ObjectListDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		
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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.AllowNavigation = false;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(424, 269);
			this.dataGrid1.TabIndex = 0;
			// 
			// ObjectListDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 269);
			this.Controls.Add(this.dataGrid1);
			this.Name = "ObjectListDialog";
			this.ShowInTaskbar = false;
			this.Text = "Object List";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
