using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using NoxShared;

namespace NoxTrainer
{
	/// <summary>
	/// Summary description for MainWindow.
	/// </summary>
	public class MainWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabTeams;
		private NoxTrainer.TeamList teamList1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.TabPage tabFuncs;
		private System.Windows.Forms.TextBox boxConsoleText;
		private System.Windows.Forms.TextBox boxConsoleColor;
		private System.Windows.Forms.Button buttonConsoleText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox boxAddress;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox arg1;
		private System.Windows.Forms.TextBox arg2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox arg3;
		private System.Windows.Forms.TextBox arg4;
		private System.Windows.Forms.TextBox arg5;
		private System.Windows.Forms.Button buttonExecute;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;

		protected ArrayList argBoxes;
		public MainWindow()
		{
			InitializeComponent();
			argBoxes = new ArrayList(new TextBox[] {arg1, arg2, arg3, arg4, arg5});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabTeams = new System.Windows.Forms.TabPage();
			this.teamList1 = new NoxTrainer.TeamList();
			this.tabFuncs = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.arg5 = new System.Windows.Forms.TextBox();
			this.arg4 = new System.Windows.Forms.TextBox();
			this.arg3 = new System.Windows.Forms.TextBox();
			this.arg2 = new System.Windows.Forms.TextBox();
			this.arg1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonExecute = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.boxAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonConsoleText = new System.Windows.Forms.Button();
			this.boxConsoleColor = new System.Windows.Forms.TextBox();
			this.boxConsoleText = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabTeams.SuspendLayout();
			this.tabFuncs.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Exit";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabTeams);
			this.tabControl1.Controls.Add(this.tabFuncs);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(304, 323);
			this.tabControl1.TabIndex = 0;
			// 
			// tabTeams
			// 
			this.tabTeams.Controls.Add(this.teamList1);
			this.tabTeams.Location = new System.Drawing.Point(4, 22);
			this.tabTeams.Name = "tabTeams";
			this.tabTeams.Size = new System.Drawing.Size(296, 297);
			this.tabTeams.TabIndex = 0;
			this.tabTeams.Text = "Teams";
			// 
			// teamList1
			// 
			this.teamList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.teamList1.Location = new System.Drawing.Point(0, 0);
			this.teamList1.Name = "teamList1";
			this.teamList1.Size = new System.Drawing.Size(296, 297);
			this.teamList1.TabIndex = 0;
			// 
			// tabFuncs
			// 
			this.tabFuncs.Controls.Add(this.label8);
			this.tabFuncs.Controls.Add(this.label7);
			this.tabFuncs.Controls.Add(this.label6);
			this.tabFuncs.Controls.Add(this.arg5);
			this.tabFuncs.Controls.Add(this.arg4);
			this.tabFuncs.Controls.Add(this.arg3);
			this.tabFuncs.Controls.Add(this.arg2);
			this.tabFuncs.Controls.Add(this.arg1);
			this.tabFuncs.Controls.Add(this.label5);
			this.tabFuncs.Controls.Add(this.buttonExecute);
			this.tabFuncs.Controls.Add(this.label4);
			this.tabFuncs.Controls.Add(this.boxAddress);
			this.tabFuncs.Controls.Add(this.label2);
			this.tabFuncs.Controls.Add(this.label1);
			this.tabFuncs.Controls.Add(this.buttonConsoleText);
			this.tabFuncs.Controls.Add(this.boxConsoleColor);
			this.tabFuncs.Controls.Add(this.boxConsoleText);
			this.tabFuncs.Controls.Add(this.label3);
			this.tabFuncs.Location = new System.Drawing.Point(4, 22);
			this.tabFuncs.Name = "tabFuncs";
			this.tabFuncs.Size = new System.Drawing.Size(296, 297);
			this.tabFuncs.TabIndex = 1;
			this.tabFuncs.Text = "Functions";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(20, 96);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(256, 16);
			this.label8.TabIndex = 17;
			this.label8.Text = "Note: Color should be a hex number from 0 to 16. ";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(256, 80);
			this.label7.TabIndex = 16;
			this.label7.Text = "Note: Address should be in hex. Args can be hex numbers or strings that will be c" +
				"onverted to unicode. Example: Enter 450b90 in Address and a color code in the fi" +
				"rst arg box and some text in the second to get the same result as \"Print to Cons" +
				"ole\" above.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(112, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 16);
			this.label6.TabIndex = 15;
			this.label6.Text = "Call Function";
			// 
			// arg5
			// 
			this.arg5.Location = new System.Drawing.Point(248, 176);
			this.arg5.Name = "arg5";
			this.arg5.Size = new System.Drawing.Size(40, 20);
			this.arg5.TabIndex = 14;
			this.arg5.Text = "";
			// 
			// arg4
			// 
			this.arg4.Location = new System.Drawing.Point(200, 176);
			this.arg4.Name = "arg4";
			this.arg4.Size = new System.Drawing.Size(40, 20);
			this.arg4.TabIndex = 13;
			this.arg4.Text = "";
			// 
			// arg3
			// 
			this.arg3.Location = new System.Drawing.Point(152, 176);
			this.arg3.Name = "arg3";
			this.arg3.Size = new System.Drawing.Size(40, 20);
			this.arg3.TabIndex = 12;
			this.arg3.Text = "";
			// 
			// arg2
			// 
			this.arg2.Location = new System.Drawing.Point(104, 176);
			this.arg2.Name = "arg2";
			this.arg2.Size = new System.Drawing.Size(40, 20);
			this.arg2.TabIndex = 11;
			this.arg2.Text = "";
			// 
			// arg1
			// 
			this.arg1.Location = new System.Drawing.Point(56, 176);
			this.arg1.Name = "arg1";
			this.arg1.Size = new System.Drawing.Size(40, 20);
			this.arg1.TabIndex = 10;
			this.arg1.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 23);
			this.label5.TabIndex = 9;
			this.label5.Text = "Args";
			// 
			// buttonExecute
			// 
			this.buttonExecute.Location = new System.Drawing.Point(200, 144);
			this.buttonExecute.Name = "buttonExecute";
			this.buttonExecute.TabIndex = 8;
			this.buttonExecute.Text = "Execute";
			this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Address";
			// 
			// boxAddress
			// 
			this.boxAddress.Location = new System.Drawing.Point(56, 144);
			this.boxAddress.Name = "boxAddress";
			this.boxAddress.TabIndex = 6;
			this.boxAddress.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(104, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 24);
			this.label3.TabIndex = 5;
			this.label3.Text = "Print to Console";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Color";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Text";
			// 
			// buttonConsoleText
			// 
			this.buttonConsoleText.Location = new System.Drawing.Point(144, 64);
			this.buttonConsoleText.Name = "buttonConsoleText";
			this.buttonConsoleText.Size = new System.Drawing.Size(40, 23);
			this.buttonConsoleText.TabIndex = 2;
			this.buttonConsoleText.Text = "Print";
			this.buttonConsoleText.Click += new System.EventHandler(this.buttonConsoleText_Click);
			// 
			// boxConsoleColor
			// 
			this.boxConsoleColor.Location = new System.Drawing.Point(72, 64);
			this.boxConsoleColor.Name = "boxConsoleColor";
			this.boxConsoleColor.Size = new System.Drawing.Size(24, 20);
			this.boxConsoleColor.TabIndex = 1;
			this.boxConsoleColor.Text = "";
			// 
			// boxConsoleText
			// 
			this.boxConsoleText.Location = new System.Drawing.Point(72, 32);
			this.boxConsoleText.Name = "boxConsoleText";
			this.boxConsoleText.Size = new System.Drawing.Size(176, 20);
			this.boxConsoleText.TabIndex = 0;
			this.boxConsoleText.Text = "";
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(304, 323);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "MainWindow";
			this.Text = "Nox Trainer";
			this.tabControl1.ResumeLayout(false);
			this.tabTeams.ResumeLayout(false);
			this.tabFuncs.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainWindow());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Dispose();//does this call finalize on everything? does Exit()?
			Environment.Exit(0);
		}

		private void buttonConsoleText_Click(object sender, System.EventArgs e)
		{
			NoxMemoryHack.PrintToConsole(boxConsoleText.Text, (NoxMemoryHack.ConsoleColor) Byte.Parse(boxConsoleColor.Text));
			//NoxMemoryHack.PrintToScreen("string 1", "string 2");
		}

		private void buttonExecute_Click(object sender, System.EventArgs e)
		{
			ArrayList args = new ArrayList();
			foreach (TextBox box in argBoxes)
			{
				if (box.Text != "")
					try
					{
						args.Add(Convert.ToInt32(box.Text, 16));
					}
					catch (Exception)
					{
						args.Add(box.Text);
					}
			}
			NoxMemoryHack.Process.CallFunction((IntPtr) Convert.ToUInt32(boxAddress.Text, 16), args.ToArray());
		}
	}
}