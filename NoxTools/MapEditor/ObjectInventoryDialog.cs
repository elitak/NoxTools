using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for ObjectInventoryDialog.
	/// </summary>
	public class ObjectInventoryDialog : System.Windows.Forms.Form
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
				UpdateList();
				if(obj.inven == 0)
					obj.childObjects = new ArrayList();
			}
		}
		private System.Windows.Forms.ListBox objectsList;
		private System.Windows.Forms.Button addButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ObjectInventoryDialog()
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
			this.objectsList = new System.Windows.Forms.ListBox();
			this.addButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// objectsList
			// 
			this.objectsList.Location = new System.Drawing.Point(8, 8);
			this.objectsList.Name = "objectsList";
			this.objectsList.Size = new System.Drawing.Size(176, 251);
			this.objectsList.TabIndex = 0;
			this.objectsList.DoubleClick += new System.EventHandler(this.objectsList_DoubleClick);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(200, 8);
			this.addButton.Name = "addButton";
			this.addButton.TabIndex = 15;
			this.addButton.Text = "Add";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// ObjectInventoryDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 269);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.objectsList);
			this.Name = "ObjectInventoryDialog";
			this.Text = "ObjectInventoryDialog";
			this.ResumeLayout(false);

		}
		#endregion

		private void addButton_Click(object sender, System.EventArgs e)
		{
			Map.Object o = new Map.Object();
			ObjectPropertiesDialog propDlg = new ObjectPropertiesDialog();
			propDlg.Object = o;
			propDlg.ShowDialog();
			obj.childObjects.Add(o);
			obj.inven++;
			UpdateList();
		}

		private void objectsList_DoubleClick(object sender, System.EventArgs e)
		{
			if(objectsList.SelectedItem != null)
			{
				String[] strs = ((String)objectsList.SelectedItem).Split(' ');	
				foreach(Map.Object o in obj.childObjects)
					if(strs[0] == o.Name && strs[1] == o.Extent.ToString())
					{
						ObjectPropertiesDialog propDlg = new ObjectPropertiesDialog();
						propDlg.Object = o;
						propDlg.ShowDialog();
					}
			}
		}

		private void UpdateList()
		{
			objectsList.Items.Clear();
			if(obj.inven > 0)
				foreach(Map.Object o in obj.childObjects)
					objectsList.Items.Add(o.Name + " " + o.Extent.ToString());
		}
	}
}
