using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;



using NoxShared;

namespace NoxTrainer
{
	public class MainWindow : System.Windows.Forms.Form
	{


        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs,
                                           int cbSize);
        bool CreateAccount = false;
        bool Enabled = false;
        public static string filepath;
        public static string arguments;
        public static int menuState = 0;
        string port = "";
        const int MOUSEEVENTF_ABSOLUTE = 32768;
        const int MOUSEEVENTF_MOVE = 1;
        const int MOUSEEVENTF_LEFTDOWN = 2;
        const int MOUSEEVENTF_LEFTUP = 4;
        const int INPUT_MOUSE = 0;

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabTeams;
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
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TabPage tabPlayers;
		private System.Windows.Forms.TextBox console;
		private System.Windows.Forms.TextBox consoleInput;
		private NoxTrainer.PlayerList playerList2;
		private NoxTrainer.TeamList teamList2;
        private IContainer components;
        private TabPage tabAdmin;
        private Button button1;
        private Label label9;
        private TextBox argtxt;
        private CheckBox chkteams;
        private ComboBox cbotype;
        private Label label11;
        private Process proc1;
        private Timer timer1;
        private Timer timer2;
        private CheckBox checkAccount;
        private Button buttongame;
        private Timer timer3;
        private CheckBox checkQuest;
        private Button button3;
        private CheckBox chkAdv;
        private CheckBox chkClickServer;
        private TextBox textBox1;

		protected ArrayList argBoxes;
		public MainWindow()
		{
			InitializeComponent();
			argBoxes = new ArrayList(new TextBox[] {arg1, arg2, arg3, arg4, arg5});
			AppConsole.LineWritten += new AppConsole.ConsoleEvent(Console_LineWritten);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPlayers = new System.Windows.Forms.TabPage();
            this.playerList2 = new NoxTrainer.PlayerList();
            this.tabTeams = new System.Windows.Forms.TabPage();
            this.teamList2 = new NoxTrainer.TeamList();
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConsoleText = new System.Windows.Forms.Button();
            this.boxConsoleColor = new System.Windows.Forms.TextBox();
            this.boxConsoleText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabAdmin = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkClickServer = new System.Windows.Forms.CheckBox();
            this.chkAdv = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkQuest = new System.Windows.Forms.CheckBox();
            this.buttongame = new System.Windows.Forms.Button();
            this.checkAccount = new System.Windows.Forms.CheckBox();
            this.chkteams = new System.Windows.Forms.CheckBox();
            this.cbotype = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.argtxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.TextBox();
            this.consoleInput = new System.Windows.Forms.TextBox();
            this.proc1 = new System.Diagnostics.Process();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPlayers.SuspendLayout();
            this.tabTeams.SuspendLayout();
            this.tabFuncs.SuspendLayout();
            this.tabAdmin.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPlayers);
            this.tabControl1.Controls.Add(this.tabTeams);
            this.tabControl1.Controls.Add(this.tabFuncs);
            this.tabControl1.Controls.Add(this.tabAdmin);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(304, 360);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPlayers
            // 
            this.tabPlayers.Controls.Add(this.playerList2);
            this.tabPlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPlayers.Name = "tabPlayers";
            this.tabPlayers.Size = new System.Drawing.Size(296, 334);
            this.tabPlayers.TabIndex = 2;
            this.tabPlayers.Text = "Players";
            this.tabPlayers.UseVisualStyleBackColor = true;
            // 
            // playerList2
            // 
            this.playerList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerList2.Location = new System.Drawing.Point(0, 0);
            this.playerList2.Name = "playerList2";
            this.playerList2.Size = new System.Drawing.Size(296, 334);
            this.playerList2.TabIndex = 0;
            // 
            // tabTeams
            // 
            this.tabTeams.Controls.Add(this.teamList2);
            this.tabTeams.Location = new System.Drawing.Point(4, 22);
            this.tabTeams.Name = "tabTeams";
            this.tabTeams.Size = new System.Drawing.Size(296, 334);
            this.tabTeams.TabIndex = 0;
            this.tabTeams.Text = "Teams";
            this.tabTeams.UseVisualStyleBackColor = true;
            // 
            // teamList2
            // 
            this.teamList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamList2.Location = new System.Drawing.Point(0, 0);
            this.teamList2.Name = "teamList2";
            this.teamList2.Size = new System.Drawing.Size(296, 334);
            this.teamList2.TabIndex = 0;
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
            this.tabFuncs.Size = new System.Drawing.Size(296, 334);
            this.tabFuncs.TabIndex = 1;
            this.tabFuncs.Text = "Functions";
            this.tabFuncs.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Note: Color should be a decimal number from 0 to 16. ";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(256, 80);
            this.label7.TabIndex = 16;
            this.label7.Text = resources.GetString("label7.Text");
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
            // 
            // arg4
            // 
            this.arg4.Location = new System.Drawing.Point(200, 176);
            this.arg4.Name = "arg4";
            this.arg4.Size = new System.Drawing.Size(40, 20);
            this.arg4.TabIndex = 13;
            // 
            // arg3
            // 
            this.arg3.Location = new System.Drawing.Point(152, 176);
            this.arg3.Name = "arg3";
            this.arg3.Size = new System.Drawing.Size(40, 20);
            this.arg3.TabIndex = 12;
            // 
            // arg2
            // 
            this.arg2.Location = new System.Drawing.Point(104, 176);
            this.arg2.Name = "arg2";
            this.arg2.Size = new System.Drawing.Size(40, 20);
            this.arg2.TabIndex = 11;
            // 
            // arg1
            // 
            this.arg1.Location = new System.Drawing.Point(56, 176);
            this.arg1.Name = "arg1";
            this.arg1.Size = new System.Drawing.Size(40, 20);
            this.arg1.TabIndex = 10;
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
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
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
            this.boxAddress.Size = new System.Drawing.Size(100, 20);
            this.boxAddress.TabIndex = 6;
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
            // 
            // boxConsoleText
            // 
            this.boxConsoleText.Location = new System.Drawing.Point(72, 32);
            this.boxConsoleText.Name = "boxConsoleText";
            this.boxConsoleText.Size = new System.Drawing.Size(176, 20);
            this.boxConsoleText.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(104, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Print to Console";
            // 
            // tabAdmin
            // 
            this.tabAdmin.Controls.Add(this.textBox1);
            this.tabAdmin.Controls.Add(this.chkClickServer);
            this.tabAdmin.Controls.Add(this.chkAdv);
            this.tabAdmin.Controls.Add(this.button3);
            this.tabAdmin.Controls.Add(this.checkQuest);
            this.tabAdmin.Controls.Add(this.buttongame);
            this.tabAdmin.Controls.Add(this.checkAccount);
            this.tabAdmin.Controls.Add(this.chkteams);
            this.tabAdmin.Controls.Add(this.cbotype);
            this.tabAdmin.Controls.Add(this.label11);
            this.tabAdmin.Controls.Add(this.label9);
            this.tabAdmin.Controls.Add(this.argtxt);
            this.tabAdmin.Controls.Add(this.button1);
            this.tabAdmin.Location = new System.Drawing.Point(4, 22);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdmin.Size = new System.Drawing.Size(296, 334);
            this.tabAdmin.TabIndex = 3;
            this.tabAdmin.Text = "AutoAdmin";
            this.tabAdmin.UseVisualStyleBackColor = true;
            this.tabAdmin.Click += new System.EventHandler(this.tabAdmin_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(32, 92);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 93);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "18590 - KOTR\r\n18591 - Ladder\r\n18592 - CTF\r\n18593 - Elimination\r\n18594 - Quest\r\n18" +
                "595 - Flagball\r\n";
            // 
            // chkClickServer
            // 
            this.chkClickServer.AutoSize = true;
            this.chkClickServer.Checked = true;
            this.chkClickServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClickServer.Location = new System.Drawing.Point(131, 63);
            this.chkClickServer.Name = "chkClickServer";
            this.chkClickServer.Size = new System.Drawing.Size(109, 17);
            this.chkClickServer.TabIndex = 14;
            this.chkClickServer.Text = "Must Click Server";
            this.chkClickServer.UseVisualStyleBackColor = true;
            // 
            // chkAdv
            // 
            this.chkAdv.AutoSize = true;
            this.chkAdv.Location = new System.Drawing.Point(147, 191);
            this.chkAdv.Name = "chkAdv";
            this.chkAdv.Size = new System.Drawing.Size(136, 17);
            this.chkAdv.TabIndex = 13;
            this.chkAdv.Text = "Use Advanced Options";
            this.chkAdv.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(147, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Ctrl,Alt,Del";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkQuest
            // 
            this.checkQuest.AutoSize = true;
            this.checkQuest.Location = new System.Drawing.Point(147, 142);
            this.checkQuest.Name = "checkQuest";
            this.checkQuest.Size = new System.Drawing.Size(88, 17);
            this.checkQuest.TabIndex = 11;
            this.checkQuest.Text = "Create Quest";
            this.checkQuest.UseVisualStyleBackColor = true;
            // 
            // buttongame
            // 
            this.buttongame.Location = new System.Drawing.Point(109, 264);
            this.buttongame.Name = "buttongame";
            this.buttongame.Size = new System.Drawing.Size(75, 23);
            this.buttongame.TabIndex = 10;
            this.buttongame.Text = "Start Server";
            this.buttongame.UseVisualStyleBackColor = true;
            this.buttongame.Click += new System.EventHandler(this.buttongame_Click);
            // 
            // checkAccount
            // 
            this.checkAccount.AutoSize = true;
            this.checkAccount.Location = new System.Drawing.Point(147, 119);
            this.checkAccount.Name = "checkAccount";
            this.checkAccount.Size = new System.Drawing.Size(97, 17);
            this.checkAccount.TabIndex = 3;
            this.checkAccount.Text = "CreateAccount";
            this.checkAccount.UseVisualStyleBackColor = true;
            // 
            // chkteams
            // 
            this.chkteams.AutoSize = true;
            this.chkteams.Location = new System.Drawing.Point(89, 241);
            this.chkteams.Name = "chkteams";
            this.chkteams.Size = new System.Drawing.Size(80, 17);
            this.chkteams.TabIndex = 8;
            this.chkteams.Text = "Use Teams";
            this.chkteams.UseVisualStyleBackColor = true;
            // 
            // cbotype
            // 
            this.cbotype.FormattingEnabled = true;
            this.cbotype.Items.AddRange(new object[] {
            "CTF",
            "Arena",
            "Elimination",
            "KOTR",
            "FlagBall"});
            this.cbotype.Location = new System.Drawing.Point(89, 214);
            this.cbotype.Name = "cbotype";
            this.cbotype.Size = new System.Drawing.Size(132, 21);
            this.cbotype.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 220);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Game Type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(90, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Command Arguments";
            // 
            // argtxt
            // 
            this.argtxt.Location = new System.Drawing.Point(55, 37);
            this.argtxt.Name = "argtxt";
            this.argtxt.Size = new System.Drawing.Size(181, 20);
            this.argtxt.TabIndex = 1;
            this.argtxt.Text = " -swindow -serveronly -port 18591";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Startup";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(0, 360);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(304, 88);
            this.console.TabIndex = 1;
            // 
            // consoleInput
            // 
            this.consoleInput.Location = new System.Drawing.Point(0, 448);
            this.consoleInput.Name = "consoleInput";
            this.consoleInput.Size = new System.Drawing.Size(304, 20);
            this.consoleInput.TabIndex = 2;
            // 
            // proc1
            // 
            this.proc1.EnableRaisingEvents = true;
            this.proc1.StartInfo.Domain = "";
            this.proc1.StartInfo.LoadUserProfile = false;
            this.proc1.StartInfo.Password = null;
            this.proc1.StartInfo.StandardErrorEncoding = null;
            this.proc1.StartInfo.StandardOutputEncoding = null;
            this.proc1.StartInfo.UserName = "";
            this.proc1.SynchronizingObject = this;
            this.proc1.Exited += new System.EventHandler(this.proc1_Exited);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 3000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(304, 474);
            this.Controls.Add(this.consoleInput);
            this.Controls.Add(this.console);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "Nox Admin Tool";
            this.tabControl1.ResumeLayout(false);
            this.tabPlayers.ResumeLayout(false);
            this.tabTeams.ResumeLayout(false);
            this.tabFuncs.ResumeLayout(false);
            this.tabFuncs.PerformLayout();
            this.tabAdmin.ResumeLayout(false);
            this.tabAdmin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			/*PlayerDb.FromOdbc(
					"Driver={MySQL ODBC 3.51 Driver};"
					+ "Server=192.168.1.2;"
					+ "Database=AdminTool;"
					+ "Uid=AdminTool;"
					+ "Pwd=atool;");*/
			try
			{
				Debug.Listeners.Add(new TextWriterTraceListener("Debug.log"));
				Debug.WriteLine(String.Format("Started at {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
				Application.Run(new MainWindow());
			}
			catch (Exception ex)
			{
				new ExceptionDialog(ex).ShowDialog();
				Environment.Exit(1);
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Environment.Exit(0);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Environment.Exit(0);
		}

		private void buttonConsoleText_Click(object sender, System.EventArgs e)
		{
			NoxMemoryHack.PrintToConsole(boxConsoleText.Text, (NoxMemoryHack.ConsoleColor) Byte.Parse(boxConsoleColor.Text));
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

		private void Console_LineWritten(object sender, AppConsole.ConsoleEventArgs e)
		{
			console.AppendText((console.Text.EndsWith("\r\n") ? "" : "\r\n") + e.Line);
			if (console.Lines.Length > 200)
				console.Text = console.Text.Substring(console.Text.IndexOf("\n")+1);
			console.Select(console.Text.Length, 0);
			console.ScrollToCaret();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            			OpenFileDialog fd = new OpenFileDialog();

			fd.Filter = "Exe (*.exe)|*.exe";
			//fd.ReadOnlyChecked = true;
			fd.ShowDialog();

            if (System.IO.File.Exists(fd.FileName))
            {
                // TRY SENDING A set mode "mode WITH CONSOLE
                // THIS COULD ALLOW FOR RANDOM SERVER TYPE SWAPPING
                // ALSO ALLOW FOR THE CONFIG.ini FILE IN NOX TO BE ALTERED
                // SO YOU CAN MAKE GAME NAMES AND SET SETTINGS AND EVEN
                // HAVE THE NAME SWAP DEPENDING ON THE GAME TYPE


                // GO BACK TO PLAYER LOADING FILE AND ADD CLOTHING COLORS
                // MOD ATUOBAN TO INCLUDE CRASH CHARS / MOD THEIR COLORS
                // MAKE SURE THERE ISN'T ANOTHER Sleep(1000) TIMER TO MAKE 100 
                // IMPLEMENT AN AUTO MAP CYCLE FEATURE
                // ADD MORE TEAM COLORS

                //System.IO.File.Delete("c:\\Westwood\\Nox\\Server.rul");
                //System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\Westwood\\Nox\\Server.rul");
                //sw.WriteLine("Racoiaws");
                //sw.WriteLine("Racoiaws");
                //sw.Close();

                //Process proc1 = new Process();



                filepath = fd.FileName;
                arguments = argtxt.Text;
                // Right here, load the server config and change based on argtxt.text
                port = argtxt.Text.Substring(argtxt.Text.Length - 5, 5);
                FileStream file = new FileStream("server.rul", FileMode.Truncate);

                StreamWriter filewrt = new StreamWriter(file);
                switch (port)
                {
                    case "18590":
                        filewrt.WriteLine("set mode kotr");
                        filewrt.WriteLine("set team off");
                        filewrt.WriteLine("set name NoXForum KOTR");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 12");
                        filewrt.WriteLine("telnet on 18090");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load bunker");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    case "18591":
                        filewrt.WriteLine("set mode arena");
                        filewrt.WriteLine("set team off");
                        filewrt.WriteLine("set name NoXForum Ladder");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 12");
                        filewrt.WriteLine("telnet on 18091");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load bunker");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    case "18592":
                        filewrt.WriteLine("set mode ctf");
                        filewrt.WriteLine("set team on");
                        filewrt.WriteLine("set name NoXForum CTF");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 4");
                        filewrt.WriteLine("telnet on 18092");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load capflag");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    case "18593":
                        filewrt.WriteLine("set mode Elimination");
                        filewrt.WriteLine("set team off");
                        filewrt.WriteLine("set name NoXForum Elim");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 12");
                        filewrt.WriteLine("telnet on 18093");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load bunker");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    case "18594":
                        filewrt.WriteLine("set mode quest");
                        filewrt.WriteLine("set team on");
                        filewrt.WriteLine("set name NoXForum Quest");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 4");
                        filewrt.WriteLine("telnet on 18094");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load ");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    case "18595":
                        filewrt.WriteLine("set mode flagball");
                        filewrt.WriteLine("set team on");
                        filewrt.WriteLine("set name NoXForum FB");
                        filewrt.WriteLine("set players 24");
                        filewrt.WriteLine("set lessons 5");
                        filewrt.WriteLine("telnet on 18095");
                        filewrt.WriteLine("set sysop m0rden1al");
                        filewrt.WriteLine("set sysog m0rden1al");
                        filewrt.WriteLine("load flagball");
                        filewrt.WriteLine("lock m0rden1al");
                        break;
                    default: break;
                };
                filewrt.Close();
                file.Close();

                proc1.StartInfo.Arguments = argtxt.Text;
                //proc1.StartInfo.ErrorDialog = true;
                proc1.StartInfo.FileName = fd.FileName;
                //proc1.StartInfo.WorkingDirectory = "c:\\Windows";
                proc1.Start();
               

                timer1.Enabled = true;
                // i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
                // SendInput(1, ref i, Marshal.SizeOf(i));

                //DO NOT USE
                //System.Windows.Forms.SendKeys.Send(
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkAccount.Checked)
            {
                switch (menuState)
                {
                    case 0:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 1;
                        break;
                    case 1:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 2;
                        break;
                    case 2:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 3;
                        break;
                    case 3:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(220, 170, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 4;
                        break;
                    case 4:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(260, 190, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 5;
                        break;
                       case 5: // Login
                      SendMouse(-1000, -1000, false, false);
                        SendMouse(142, 170, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 9000;
                        menuState = 6;
                        break;
                      case 6: // Password
                      SendMouse(-1000, -1000, false, false);
                        SendMouse(128, 200, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 9000;
                        menuState = 7;
                        break;
                      case 7:
                      SendMouse(-1000, -1000, false, false);
                        SendMouse(49, 219, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 8;
                        break;
                    case 8: // Choose xwis
                        if (chkClickServer.Checked)
                        {
                            SendMouse(-1000, -1000, false, false);
                            SendMouse(234, 332, false, false);
                            SendMouse(0, 0, true, false);
                            timer1.Interval = 3000;
                        }
                        menuState = 9;
                        break;
                    case 9: // Accept region
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(126, 391, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 10;
                        break;
                    case 10: // Click GO
                      SendMouse(-1000, -1000, false, false);
                        SendMouse(350, 296, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 11;
                        break;

                        case 11:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(232, 330, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 12;
                        break;
                    case 12:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(534, 435, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 13;
                        break;
                    case 13:
                        SendMouse(-1000, -1000, false, false);
                        if (checkQuest.Checked)
                        {
                            SendMouse(111, 125, false, false);
                        }
                        else
                        {
                            
                            SendMouse(111, 64, false, false);
                       
                        }
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 14;
                        break;
                    case 14:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(323, 379, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 15;
                        break;
                    /*case 5:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(126, 391, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 6;
                        break;
                    case 6:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(232, 330, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 7;
                        break;
                    case 7:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(534, 435, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 8;
                        break;
                    case 8:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(111, 64, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 9;
                        break;
                    case 9:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(323, 379, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 10;
                        break;*/
                    default:
                        timer1.Enabled = false;
                        menuState = 0;
                        if (chkAdv.Checked)
                            timer3.Enabled = true;
                        //proc1.StartInfo.FileName = "C:/Program Files/Nox/GAME.exe";
                        //proc1.Start();
                        //Enabled = true;
                        break;
            }
            }
            else
            {
                switch (menuState)
                {
                    case 0:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 1;
                        break;
                    case 1:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 2;
                        break;
                    case 2:
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 3;
                        break;
                    case 3:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(220, 170, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 4;
                        break;
                    case 4:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(260, 190, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 5;
                        break;
                    case 5:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(142, 170, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 9000;
                        menuState = 6;
                        break;
                    case 6:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(128, 200, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 9000;
                        menuState = 7;
                        break;
                    case 7: // Choose xwis
                        if (chkClickServer.Checked)
                        {
                            SendMouse(-1000, -1000, false, false);
                            SendMouse(234, 332, false, false);
                            SendMouse(0, 0, true, false);
                            timer1.Interval = 3000;
                        }
                        menuState = 8;
                        break;
                    case 8:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(126, 391, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 9;
                        break;
                    case 9:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(232, 330, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 10;
                        break;
                    case 10:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(534, 435, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 4000;
                        menuState = 11;
                        break;
                    case 11:
                        SendMouse(-1000, -1000, false, false);
                        if (checkQuest.Checked)
                        {
                            SendMouse(111, 125, false, false);
                        }
                        else
                        {

                            SendMouse(111, 64, false, false);

                        }
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 2000;
                        menuState = 12;
                        break;
                    case 12:
                        SendMouse(-1000, -1000, false, false);
                        SendMouse(323, 379, false, false);
                        SendMouse(0, 0, true, false);
                        timer1.Interval = 3000;
                        menuState = 13;
                        break;
                    default:
                        timer1.Enabled = false;
                        menuState = 0;
                        if (chkAdv.Checked)
                            timer3.Enabled = true;
                        //proc1.StartInfo.FileName = "C:/Program Files/Nox/GAME.exe";
                        //proc1.Start();
                        //Enabled = true;
                        break;
                }
            }
            
            
        }
        public void SendMouse(int x, int y, bool click, bool isAbs)
        {
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = x;
            i.mi.dy = y;

            if( x>0 && y>0 || !click )
                  i.mi.dwFlags |= MOUSEEVENTF_MOVE;
            if( click )
                i.mi.dwFlags |= MOUSEEVENTF_LEFTDOWN;
            if( isAbs )
                  i.mi.dwFlags |= MOUSEEVENTF_ABSOLUTE;

            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0; // Set it to 100 to start Screen saver
            int val = SendInput(1, ref i, Marshal.SizeOf(i));
            if( click )
                timer2.Enabled = true;
            //timer1.Enabled = false;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;

            i.mi.dwFlags |= MOUSEEVENTF_LEFTUP;

            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0; // Set it to 100 to start Screen saver
            int val = SendInput(1, ref i, Marshal.SizeOf(i));
            timer2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //proc1.WaitForExit();
            //timer1.Enabled = true;
        }

        private void tabAdmin_Click(object sender, EventArgs e)
        {

        }

        private void proc1_Exited(object sender, EventArgs e)
        {
            //proc1.StartInfo.Arguments = argtxt.Text;
            //proc1.StartInfo.ErrorDialog = true;
            //proc1.StartInfo.FileName = fd.FileName;
            //proc1.StartInfo.WorkingDirectory = "c:\\Windows";
           // if (Enabled)
           // {
             //   proc1.Start();
              //  timer1.Enabled = true;
           // }
        }

        private void buttongame_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            switch (menuState)
            {
                case 0:
                    timer3.Interval = 9000;
                    menuState = 1; break;

                case 1: 
                    SendMouse(-1000, -1000, false, false);
                    SendMouse(316, 127, false, false);
                    SendMouse(0, 0, true, false);
                    timer3.Interval = 2000;
                    menuState = 2; break;

                case 2:
                    SendMouse(-1000, -1000, false, false);
                    SendMouse(558, 99, false, false);
                    SendMouse(0, 0, true, false);
                    timer3.Interval = 2000;
                    menuState = 3; break;

                case 3:
                    SendMouse(-1000, -1000, false, false);
                    SendMouse(538, 90+(cbotype.TabIndex*8), false, false);
                    SendMouse(0, 0, true, false);
                    timer3.Interval = 2000;
                    menuState = 4; break;

                default:
                    timer3.Enabled = false;
                    menuState = 0; break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            string path = "";
            proc1.StartInfo.FileName = path + "taskmgr.exe";
            proc1.StartInfo.WorkingDirectory = "c:\\Windows";
            proc1.Start();
        }
	}
}
