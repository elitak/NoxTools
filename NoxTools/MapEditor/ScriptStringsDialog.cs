using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for ScriptStringsDialog.
	/// </summary>
	public class ScriptStringsDialog : System.Windows.Forms.Form
	{
		protected Map.ScriptObject sct;
		public Map.ScriptObject Scripts
		{
			get
			{
				return sct;
			}
			set
			{
				sct = value;
				foreach(String s in sct.SctStr.Values)
					stringsList.Items.Add(s);
			}
		}
		private System.Windows.Forms.ListBox stringsList;
		private System.Windows.Forms.Button listUpButton;
		private System.Windows.Forms.Button listDownButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox stringBox;
		private System.Windows.Forms.Button textOkButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ScriptStringsDialog()
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
			this.stringsList = new System.Windows.Forms.ListBox();
			this.listUpButton = new System.Windows.Forms.Button();
			this.listDownButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.stringBox = new System.Windows.Forms.TextBox();
			this.textOkButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// stringsList
			// 
			this.stringsList.Dock = System.Windows.Forms.DockStyle.Left;
			this.stringsList.Location = new System.Drawing.Point(0, 0);
			this.stringsList.Name = "stringsList";
			this.stringsList.Size = new System.Drawing.Size(144, 264);
			this.stringsList.TabIndex = 0;
			this.stringsList.SelectedIndexChanged += new System.EventHandler(this.stringsList_SelectedIndexChanged);
			// 
			// listUpButton
			// 
			this.listUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listUpButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listUpButton.Location = new System.Drawing.Point(152, 24);
			this.listUpButton.Name = "listUpButton";
			this.listUpButton.Size = new System.Drawing.Size(24, 23);
			this.listUpButton.TabIndex = 1;
			this.listUpButton.Text = "^";
			this.listUpButton.Click += new System.EventHandler(this.listUpButton_Click);
			// 
			// listDownButton
			// 
			this.listDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listDownButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listDownButton.Location = new System.Drawing.Point(152, 56);
			this.listDownButton.Name = "listDownButton";
			this.listDownButton.Size = new System.Drawing.Size(24, 24);
			this.listDownButton.TabIndex = 2;
			this.listDownButton.Text = "v";
			this.listDownButton.Click += new System.EventHandler(this.listDownButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(152, 232);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Close";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// stringBox
			// 
			this.stringBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.stringBox.Location = new System.Drawing.Point(144, 0);
			this.stringBox.Name = "stringBox";
			this.stringBox.Size = new System.Drawing.Size(88, 20);
			this.stringBox.TabIndex = 5;
			this.stringBox.Text = "";
			// 
			// textOkButton
			// 
			this.textOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textOkButton.Location = new System.Drawing.Point(200, 24);
			this.textOkButton.Name = "textOkButton";
			this.textOkButton.Size = new System.Drawing.Size(32, 23);
			this.textOkButton.TabIndex = 6;
			this.textOkButton.Text = "OK";
			this.textOkButton.Click += new System.EventHandler(this.textOkButton_Click);
			// 
			// ScriptStringsDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 269);
			this.Controls.Add(this.textOkButton);
			this.Controls.Add(this.stringBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.listDownButton);
			this.Controls.Add(this.listUpButton);
			this.Controls.Add(this.stringsList);
			this.Name = "ScriptStringsDialog";
			this.Text = "ScriptStringsDialog";
			this.ResumeLayout(false);

		}
		#endregion

		private void listUpButton_Click(object sender, System.EventArgs e)
		{
			int selected = stringsList.SelectedIndex;
			if(selected > -1 && (selected-1) >= 0)
			{
				SortedList slKeys = new SortedList(); 
				foreach(int i in sct.SctStr.Keys)
				{
					if(i == selected-1)
						slKeys.Add(i+1,sct.SctStr[i]);
					else if(i == selected)
						slKeys.Add(i-1,sct.SctStr[i]);
					else
						slKeys.Add(i,sct.SctStr[i]);
				}

				sct.SctStr = slKeys;
			}
			stringsList.Items.Clear();
			foreach(String s in sct.SctStr.Values)
				stringsList.Items.Add(s);
			stringsList.SelectedIndex = (selected-1) >= 0 ? selected-1 : selected;
		}

		private void listDownButton_Click(object sender, System.EventArgs e)
		{
			int selected = stringsList.SelectedIndex;
			if(selected > -1 && ((selected+1) < stringsList.Items.Count))
			{
				SortedList slKeys = new SortedList(); 
				foreach(int i in sct.SctStr.Keys)
				{
					if(i == selected+1)
						slKeys.Add(i-1,sct.SctStr[i]);
					else if(i == selected)
						slKeys.Add(i+1,sct.SctStr[i]);
					else
						slKeys.Add(i,sct.SctStr[i]);
				}

				sct.SctStr = slKeys;
			}
			stringsList.Items.Clear();
			foreach(String s in sct.SctStr.Values)
				stringsList.Items.Add(s);
			stringsList.SelectedIndex = ((selected+1) < stringsList.Items.Count) ? selected+1 : selected;
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
		}

		private void stringsList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			stringBox.Text = (String)stringsList.SelectedItem;
		}

		private void textOkButton_Click(object sender, System.EventArgs e)
		{
			if(stringsList.SelectedIndex > -1)
			{
				sct.SctStr[stringsList.SelectedIndex] = stringBox.Text;
				stringsList.Items.Clear();
				foreach(String s in sct.SctStr.Values)
					stringsList.Items.Add(s);
			}
		}

	}
}
