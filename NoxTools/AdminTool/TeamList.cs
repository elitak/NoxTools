using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using NoxShared;

namespace NoxTrainer
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class TeamList : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label labelTeam1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.ComboBox comboBox5;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.ComboBox comboBox6;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.ComboBox comboBox7;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.ComboBox comboBox8;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.TextBox textBox8;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ArrayList names;
		private ArrayList colors;
		private ArrayList enabled;

		public TeamList()
		{

			InitializeComponent();

			names = new ArrayList();
			names.Add(textBox1);
			names.Add(textBox2);
			names.Add(textBox3);
			names.Add(textBox4);
			names.Add(textBox5);
			names.Add(textBox6);
			names.Add(textBox7);
			names.Add(textBox8);

			foreach (TextBox box in names)
			{
				box.TextChanged += new EventHandler(nameBox_TextChanged);
			}

			colors = new ArrayList();
			colors.Add(comboBox1);
			colors.Add(comboBox2);
			colors.Add(comboBox3);
			colors.Add(comboBox4);
			colors.Add(comboBox5);
			colors.Add(comboBox6);
			colors.Add(comboBox7);
			colors.Add(comboBox8);

			foreach (ComboBox box in colors)
			{
				box.SelectedIndexChanged  +=new EventHandler(box_SelectedIndexChanged);
			}

			enabled = new ArrayList();
			enabled.Add(checkBox1);
			enabled.Add(checkBox2);
			enabled.Add(checkBox3);
			enabled.Add(checkBox4);
			enabled.Add(checkBox5);
			enabled.Add(checkBox6);
			enabled.Add(checkBox7);
			enabled.Add(checkBox8);

			foreach (CheckBox box in enabled)
			{
				box.CheckedChanged += new EventHandler(box_CheckedChanged);
			}

			SetEnabledAllFields(false);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonWrite = new System.Windows.Forms.Button();
			this.buttonRead = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.labelTeam1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.comboBox5 = new System.Windows.Forms.ComboBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.comboBox7 = new System.Windows.Forms.ComboBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.comboBox8 = new System.Windows.Forms.ComboBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(152, 256);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(72, 24);
			this.buttonWrite.TabIndex = 15;
			this.buttonWrite.Text = "Save";
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(64, 256);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(72, 24);
			this.buttonRead.TabIndex = 14;
			this.buttonRead.Text = "Update";
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(232, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 13;
			this.label2.Text = "Enabled";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(136, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 12;
			this.label1.Text = "Color";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox1.Location = new System.Drawing.Point(136, 40);
			this.comboBox1.MaxDropDownItems = 9;
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(80, 21);
			this.comboBox1.TabIndex = 11;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(232, 40);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(16, 20);
			this.checkBox1.TabIndex = 10;
			// 
			// labelTeam1
			// 
			this.labelTeam1.Location = new System.Drawing.Point(16, 16);
			this.labelTeam1.Name = "labelTeam1";
			this.labelTeam1.Size = new System.Drawing.Size(48, 16);
			this.labelTeam1.TabIndex = 9;
			this.labelTeam1.Text = "Name";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(104, 20);
			this.textBox1.TabIndex = 8;
			this.textBox1.Text = "";
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox2.Location = new System.Drawing.Point(136, 64);
			this.comboBox2.MaxDropDownItems = 9;
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(80, 21);
			this.comboBox2.TabIndex = 18;
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(232, 64);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(16, 20);
			this.checkBox2.TabIndex = 17;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(16, 64);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(104, 20);
			this.textBox2.TabIndex = 16;
			this.textBox2.Text = "";
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox3.Location = new System.Drawing.Point(136, 88);
			this.comboBox3.MaxDropDownItems = 9;
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(80, 21);
			this.comboBox3.TabIndex = 21;
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(232, 88);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(16, 20);
			this.checkBox3.TabIndex = 20;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(16, 88);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(104, 20);
			this.textBox3.TabIndex = 19;
			this.textBox3.Text = "";
			// 
			// comboBox4
			// 
			this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox4.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox4.Location = new System.Drawing.Point(136, 112);
			this.comboBox4.MaxDropDownItems = 9;
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(80, 21);
			this.comboBox4.TabIndex = 24;
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(232, 112);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(16, 20);
			this.checkBox4.TabIndex = 23;
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(16, 112);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(104, 20);
			this.textBox4.TabIndex = 22;
			this.textBox4.Text = "";
			// 
			// comboBox5
			// 
			this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox5.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox5.Location = new System.Drawing.Point(136, 136);
			this.comboBox5.MaxDropDownItems = 9;
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new System.Drawing.Size(80, 21);
			this.comboBox5.TabIndex = 27;
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(232, 136);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(16, 20);
			this.checkBox5.TabIndex = 26;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(16, 136);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(104, 20);
			this.textBox5.TabIndex = 25;
			this.textBox5.Text = "";
			// 
			// comboBox6
			// 
			this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox6.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox6.Location = new System.Drawing.Point(136, 160);
			this.comboBox6.MaxDropDownItems = 9;
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(80, 21);
			this.comboBox6.TabIndex = 30;
			// 
			// checkBox6
			// 
			this.checkBox6.Location = new System.Drawing.Point(232, 160);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(16, 20);
			this.checkBox6.TabIndex = 29;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(16, 160);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(104, 20);
			this.textBox6.TabIndex = 28;
			this.textBox6.Text = "";
			// 
			// comboBox7
			// 
			this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox7.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox7.Location = new System.Drawing.Point(136, 184);
			this.comboBox7.MaxDropDownItems = 9;
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new System.Drawing.Size(80, 21);
			this.comboBox7.TabIndex = 33;
			// 
			// checkBox7
			// 
			this.checkBox7.Location = new System.Drawing.Point(232, 184);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(16, 20);
			this.checkBox7.TabIndex = 32;
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(16, 184);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(104, 20);
			this.textBox7.TabIndex = 31;
			this.textBox7.Text = "";
			// 
			// comboBox8
			// 
			this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox8.Items.AddRange(new object[] {
														   "Red",
														   "Blue",
														   "Green",
														   "Dark Blue",
														   "Yellow",
														   "Dark Red",
														   "Black",
														   "White",
														   "Orange"});
			this.comboBox8.Location = new System.Drawing.Point(136, 208);
			this.comboBox8.MaxDropDownItems = 9;
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new System.Drawing.Size(80, 21);
			this.comboBox8.TabIndex = 36;
			// 
			// checkBox8
			// 
			this.checkBox8.Location = new System.Drawing.Point(232, 208);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(16, 20);
			this.checkBox8.TabIndex = 35;
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(16, 208);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(104, 20);
			this.textBox8.TabIndex = 34;
			this.textBox8.Text = "";
			// 
			// TeamList
			// 
			this.Controls.Add(this.comboBox8);
			this.Controls.Add(this.checkBox8);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.comboBox7);
			this.Controls.Add(this.checkBox7);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.comboBox6);
			this.Controls.Add(this.checkBox6);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.comboBox5);
			this.Controls.Add(this.checkBox5);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.comboBox4);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.buttonWrite);
			this.Controls.Add(this.buttonRead);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.labelTeam1);
			this.Controls.Add(this.textBox1);
			this.Name = "TeamList";
			this.Size = new System.Drawing.Size(280, 296);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void buttonRead_Click(object sender, System.EventArgs e)
		{
			try
			{
				NoxMemoryHack.Teams.Refresh();
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Could not access Nox's memory.", "Error");
			}

			for (int ndx = 0; ndx < NoxMemoryHack.Teams.TeamList.Count && ndx < names.Count; ndx++)//assumes that number of names = colors = enabled
			{
				NoxMemoryHack.Teams.Team team = (NoxMemoryHack.Teams.Team) NoxMemoryHack.Teams.TeamList[ndx];
				((TextBox) names[ndx]).Text = team.Name;
				((TextBox) names[ndx]).Enabled = true;
				((ComboBox) colors[ndx]).SelectedIndex = new ArrayList(NoxMemoryHack.Teams.Team.TeamColor).IndexOf(team.Color);
				((ComboBox) colors[ndx]).Enabled = true;
				((CheckBox) enabled[ndx]).Checked = team.Enabled;
			}

			foreach (CheckBox box in enabled)
				box.Enabled = true;
		}

		private void nameBox_TextChanged(object sender, EventArgs e)
		{
			TextBox box = (TextBox) sender;
			int ndx = names.IndexOf(box);
			((NoxMemoryHack.Teams.Team) NoxMemoryHack.Teams.TeamList[ndx]).Name = box.Text;
		}

		private void box_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox box = (ComboBox) sender;
			((NoxMemoryHack.Teams.Team) NoxMemoryHack.Teams.TeamList[colors.IndexOf(box)]).Color = NoxMemoryHack.Teams.Team.TeamColor[box.SelectedIndex];
		}

		private void box_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = (CheckBox) sender;
			int ndx = enabled.IndexOf(box);
			if (ndx > NoxMemoryHack.Teams.TeamList.Count - 1)
				NoxMemoryHack.Teams.AddTeam();
			((NoxMemoryHack.Teams.Team) NoxMemoryHack.Teams.TeamList[ndx]).Enabled = box.Checked;
			((TextBox) names[ndx]).Enabled = box.Checked;
			((ComboBox) colors[ndx]).Enabled = box.Checked;
		}

		private void buttonWrite_Click(object sender, System.EventArgs e)
		{
			for (int ndx = 0; ndx < NoxMemoryHack.Teams.TeamList.Count && ndx < names.Count; ndx++)
			{
				NoxMemoryHack.Teams.Team team = (NoxMemoryHack.Teams.Team) NoxMemoryHack.Teams.TeamList[ndx];
				((TextBox) names[ndx]).Text = team.Name;

				((ComboBox) colors[ndx]).SelectedIndex = new ArrayList(NoxMemoryHack.Teams.Team.TeamColor).IndexOf(team.Color);

				((CheckBox) enabled[ndx]).Checked = team.Enabled;
			}
			NoxMemoryHack.Teams.Write();
		}

		private void SetEnabledAllFields(bool enable)
		{
			foreach (TextBox box in names)
			{
				box.Enabled = enable;
			}
			foreach (ComboBox box in colors)
			{
				box.Enabled = enable;
			}
			foreach (CheckBox box in enabled)
			{
				box.Enabled = enable;
			}
		}
	}
}
