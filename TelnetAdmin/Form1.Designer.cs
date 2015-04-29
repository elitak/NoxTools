namespace TelnetAdmin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabAdmin = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtlock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtsysop = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPlLimit = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listSpells = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtcmd = new System.Windows.Forms.TextBox();
            this.cmdline = new System.Windows.Forms.RichTextBox();
            this.timRecv = new System.Windows.Forms.Timer(this.components);
            this.tabAdmin.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAdmin
            // 
            this.tabAdmin.Controls.Add(this.tabPage1);
            this.tabAdmin.Controls.Add(this.tabPage2);
            this.tabAdmin.Controls.Add(this.tabPage3);
            this.tabAdmin.Location = new System.Drawing.Point(3, 1);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.SelectedIndex = 0;
            this.tabAdmin.Size = new System.Drawing.Size(523, 204);
            this.tabAdmin.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.listUsers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(515, 178);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connect/Ban";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtlock);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtsysop);
            this.groupBox1.Controls.Add(this.connect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Location = new System.Drawing.Point(6, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 135);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(116, 101);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 19;
            this.button4.Text = "Disconnect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Lock Pass";
            // 
            // txtlock
            // 
            this.txtlock.Location = new System.Drawing.Point(116, 70);
            this.txtlock.Name = "txtlock";
            this.txtlock.Size = new System.Drawing.Size(100, 20);
            this.txtlock.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sysop";
            // 
            // txtsysop
            // 
            this.txtsysop.Location = new System.Drawing.Point(10, 70);
            this.txtsysop.Name = "txtsysop";
            this.txtsysop.Size = new System.Drawing.Size(100, 20);
            this.txtsysop.TabIndex = 15;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(35, 101);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 14;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(116, 32);
            this.txtPort.MaxLength = 5;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 11;
            this.txtPort.Text = "18500";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(10, 32);
            this.txtIP.MaxLength = 15;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(457, 150);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 22);
            this.button3.TabIndex = 13;
            this.button3.Text = "Ban";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(399, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 22);
            this.button2.TabIndex = 12;
            this.button2.Text = "Kick";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listUsers
            // 
            this.listUsers.BackColor = System.Drawing.Color.Black;
            this.listUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.listUsers.FormattingEnabled = true;
            this.listUsers.Location = new System.Drawing.Point(248, 10);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(261, 134);
            this.listUsers.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.cboPlLimit);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.listSpells);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(515, 178);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Spells/Items";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Player Limit";
            // 
            // cboPlLimit
            // 
            this.cboPlLimit.FormattingEnabled = true;
            this.cboPlLimit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.cboPlLimit.Location = new System.Drawing.Point(377, 19);
            this.cboPlLimit.Name = "cboPlLimit";
            this.cboPlLimit.Size = new System.Drawing.Size(121, 21);
            this.cboPlLimit.TabIndex = 16;
            this.cboPlLimit.SelectedIndexChanged += new System.EventHandler(this.cboPlLimit_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(241, 67);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 2;
            this.button6.Text = "Allow";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(241, 96);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Ban";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listSpells
            // 
            this.listSpells.FormattingEnabled = true;
            this.listSpells.Items.AddRange(new object[] {
            "SPELL_ANCHOR",
            "SPELL_BLINK",
            "SPELL_BURN",
            "SPELL_CANCEL",
            "SPELL_CHAIN_LIGHTNING",
            "SPELL_CHAIN_LIGHTNING_BOLT",
            "SPELL_CHANNEL_LIFE",
            "SPELL_CHARM",
            "SPELL_CLEANSING_FLAME",
            "SPELL_CLEANSING_MANA_FLAME",
            "SPELL_CONFUSE",
            "SPELL_COUNTER_SPELL",
            "SPELL_CURE_POISON",
            "SPELL_DEATH",
            "SPELL_DEATH_RAY",
            "SPELL_DETECT_MAGIC",
            "SPELL_DETONATE",
            "SPELL_DETONATE GLYPHS",
            "SPELL_DISENCHANT_ALL",
            "SPELL_DRAIN_MANA",
            "SPELL_EARTHQUAKE",
            "SPELL_EXPLOSION",
            "SPELL_FEAR",
            "SPELL_FIREBALL",
            "SPELL_FIREWALK",
            "SPELL_FIST",
            "SPELL_FORCE_FIELD",
            "SPELL_FORCE_OF_NATURE",
            "SPELL_FREEZE",
            "SPELL_FUMBLE",
            "SPELL_GLYPH",
            "SPELL_GREATER_HEAL",
            "SPELL_HASTE",
            "SPELL_INFRAVISION",
            "SPELL_INVERSION",
            "SPELL_INVISIBILITY",
            "SPELL_INVULNERABILITY",
            "SPELL_LESSER_HEAL",
            "SPELL_LIGHT",
            "SPELL_LIGHTNING",
            "SPELL_LOCK",
            "SPELL_MAGIC_MISSLE",
            "SPELL_MANA_BOMB",
            "SPELL_MARK",
            "SPELL_MARK_1",
            "SPELL_MARK_2",
            "SPELL_MARK_3",
            "SPELL_MARK_4",
            "SPELL_MARK_LOCATION",
            "SPELL_MECHANICAL_GOLEM",
            "SPELL_METEOR",
            "SPELL_METEOR_SHOWER",
            "SPELL_MOONGLOW",
            "SPELL_NULLIFY",
            "SPELL_OVAL_SHIELD",
            "SPELL_PHANTOM",
            "SPELL_PIXIE_SWARM",
            "SPELL_PLASMA",
            "SPELL_POISON",
            "SPELL_PROTECTION_FROM_ELECTRICITY",
            "SPELL_PROTECTION_FROM_FIRE",
            "SPELL_PROTECTION_FROM_MAGIC",
            "SPELL_PROTECTION_FROM_POISON",
            "SPELL_PULL",
            "SPELL_PUSH",
            "SPELL_RESTORE_HEALTH",
            "SPELL_RESTORE_MANA",
            "SPELL_RUN",
            "SPELL_SHIELD",
            "SPELL_SHOCK",
            "SPELL_SLOW",
            "SPELL_SMALL_ZAP",
            "SPELL_STUN",
            "SPELL_SWAP",
            "SPELL_TAG",
            "SPELL_TELEKINESIS",
            "SPELL_TELEPORT_POP",
            "SPELL_TELEPORT_TO_TARGET",
            "SPELL_TOXIC_CLOUD",
            "SPELL_TRIGGER_GLYPH",
            "SPELL_TURN_UNDEAD",
            "SPELL_VAMPIRISM",
            "SPELL_VILLAIN",
            "SPELL_WALL",
            "SPELL_WINK"});
            this.listSpells.Location = new System.Drawing.Point(3, 3);
            this.listSpells.Name = "listSpells";
            this.listSpells.Size = new System.Drawing.Size(232, 173);
            this.listSpells.Sorted = true;
            this.listSpells.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(515, 178);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "General";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtcmd
            // 
            this.txtcmd.Location = new System.Drawing.Point(3, 364);
            this.txtcmd.Name = "txtcmd";
            this.txtcmd.Size = new System.Drawing.Size(523, 20);
            this.txtcmd.TabIndex = 14;
            this.txtcmd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtcmd_KeyUp);
            // 
            // cmdline
            // 
            this.cmdline.BackColor = System.Drawing.Color.Black;
            this.cmdline.ForeColor = System.Drawing.Color.Green;
            this.cmdline.Location = new System.Drawing.Point(3, 207);
            this.cmdline.Name = "cmdline";
            this.cmdline.Size = new System.Drawing.Size(523, 151);
            this.cmdline.TabIndex = 10;
            this.cmdline.Text = "";
            // 
            // timRecv
            // 
            this.timRecv.Interval = 1000;
            this.timRecv.Tick += new System.EventHandler(this.timRecv_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 386);
            this.Controls.Add(this.tabAdmin);
            this.Controls.Add(this.txtcmd);
            this.Controls.Add(this.cmdline);
            this.Name = "Form1";
            this.Text = "Telnet Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabAdmin.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer timRecv;
        private System.Windows.Forms.RichTextBox cmdline;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtcmd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtlock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtsysop;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPort;
        public System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listSpells;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPlLimit;
        private System.Windows.Forms.TabPage tabPage3;
    }
}

