using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using NoxShared;
using NoxShared.NoxType;

namespace PlrEditor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainWindow : System.Windows.Forms.Form
	{
		protected PlayerFile player;

		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MainMenu menuBar;
		private System.Windows.Forms.MenuItem menuItemExitSeparator;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.TextBox textBox14;
		private System.Windows.Forms.TextBox textBox15;
		private System.Windows.Forms.TextBox textBox16;
		private System.Windows.Forms.TextBox textBox17;
		private System.Windows.Forms.TextBox textBox18;
		private System.Windows.Forms.TextBox textBox19;
		private System.Windows.Forms.TextBox textBox20;
		private System.Windows.Forms.TextBox textBox21;
		private System.Windows.Forms.TextBox textBox22;
		private System.Windows.Forms.TextBox textBox23;
		private System.Windows.Forms.TextBox textBox24;
		private System.Windows.Forms.TextBox textBox25;
		private System.Windows.Forms.TextBox textBox26;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label colorBoxMustache;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label colorBoxPants;
		private System.Windows.Forms.Label colorBoxShirt;
		private System.Windows.Forms.Label colorBoxShirtTrim;
		private System.Windows.Forms.Label colorBoxShoes;
		private System.Windows.Forms.Label colorBoxShoesTrim;
		private System.Windows.Forms.Label colorBoxHair;
		private System.Windows.Forms.Label colorBoxSkin;
		private System.Windows.Forms.Label colorBoxBeard;
		private System.Windows.Forms.Label colorBoxSideburns;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Label classLabel;
		private System.Windows.Forms.DomainUpDown classBox;
		private System.Windows.Forms.Label nameLengthLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainWindow()
		{
			player = new PlayerFile();
			InitializeComponent();
			MyInitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainWindow));
			this.menuBar = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuItemOpen = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemExitSeparator = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.classBox = new System.Windows.Forms.DomainUpDown();
			this.classLabel = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.colorBoxPants = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.colorBoxShirt = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.colorBoxShirtTrim = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.colorBoxShoes = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.colorBoxShoesTrim = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.colorBoxHair = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.colorBoxSkin = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.colorBoxMustache = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.colorBoxBeard = new System.Windows.Forms.Label();
			this.colorBoxSideburns = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBox22 = new System.Windows.Forms.TextBox();
			this.textBox23 = new System.Windows.Forms.TextBox();
			this.textBox24 = new System.Windows.Forms.TextBox();
			this.textBox25 = new System.Windows.Forms.TextBox();
			this.textBox26 = new System.Windows.Forms.TextBox();
			this.textBox17 = new System.Windows.Forms.TextBox();
			this.textBox18 = new System.Windows.Forms.TextBox();
			this.textBox19 = new System.Windows.Forms.TextBox();
			this.textBox20 = new System.Windows.Forms.TextBox();
			this.textBox21 = new System.Windows.Forms.TextBox();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.textBox14 = new System.Windows.Forms.TextBox();
			this.textBox15 = new System.Windows.Forms.TextBox();
			this.textBox16 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nameLengthLabel = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuBar
			// 
			this.menuBar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.menuFile,
																					this.menuItem1});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemOpen,
																					 this.menuItem3,
																					 this.menuItemExitSeparator,
																					 this.menuItemExit});
			this.menuFile.Text = "File";
			// 
			// menuItemOpen
			// 
			this.menuItemOpen.Index = 0;
			this.menuItemOpen.Text = "Open";
			this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Save";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItemExitSeparator
			// 
			this.menuItemExitSeparator.Index = 2;
			this.menuItemExitSeparator.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 3;
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "Help";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "About";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nameBox
			// 
			this.nameBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nameBox.Location = new System.Drawing.Point(48, 24);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(176, 18);
			this.nameBox.TabIndex = 1;
			this.nameBox.Text = "";
			this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(4, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(336, 504);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 478);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Character Info";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.nameLengthLabel);
			this.groupBox3.Controls.Add(this.classBox);
			this.groupBox3.Controls.Add(this.classLabel);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.nameBox);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(312, 464);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			// 
			// classBox
			// 
			this.classBox.BackColor = System.Drawing.SystemColors.Window;
			this.classBox.Items.Add("Warrior");
			this.classBox.Items.Add("Wizard");
			this.classBox.Items.Add("Conjurer");
			this.classBox.Location = new System.Drawing.Point(48, 48);
			this.classBox.Name = "classBox";
			this.classBox.ReadOnly = true;
			this.classBox.TabIndex = 4;
			// 
			// classLabel
			// 
			this.classLabel.Location = new System.Drawing.Point(8, 48);
			this.classLabel.Name = "classLabel";
			this.classLabel.Size = new System.Drawing.Size(40, 24);
			this.classLabel.TabIndex = 3;
			this.classLabel.Text = "Class";
			this.classLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox2);
			this.tabPage3.Controls.Add(this.groupBox1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(328, 478);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Colors";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.colorBoxPants);
			this.groupBox2.Controls.Add(this.label22);
			this.groupBox2.Controls.Add(this.colorBoxShirt);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.colorBoxShirtTrim);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.colorBoxShoes);
			this.groupBox2.Controls.Add(this.label26);
			this.groupBox2.Controls.Add(this.colorBoxShoesTrim);
			this.groupBox2.Location = new System.Drawing.Point(8, 240);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(312, 232);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Clothes";
			// 
			// colorBoxPants
			// 
			this.colorBoxPants.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxPants.Location = new System.Drawing.Point(72, 24);
			this.colorBoxPants.Name = "colorBoxPants";
			this.colorBoxPants.Size = new System.Drawing.Size(56, 24);
			this.colorBoxPants.TabIndex = 5;
			this.colorBoxPants.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 24);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(56, 24);
			this.label22.TabIndex = 15;
			this.label22.Text = "Pants";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxShirt
			// 
			this.colorBoxShirt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxShirt.Location = new System.Drawing.Point(72, 64);
			this.colorBoxShirt.Name = "colorBoxShirt";
			this.colorBoxShirt.Size = new System.Drawing.Size(56, 24);
			this.colorBoxShirt.TabIndex = 6;
			this.colorBoxShirt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(8, 64);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(56, 24);
			this.label23.TabIndex = 16;
			this.label23.Text = "Shirt";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(8, 104);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(56, 24);
			this.label24.TabIndex = 17;
			this.label24.Text = "Shirt (trim)";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxShirtTrim
			// 
			this.colorBoxShirtTrim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxShirtTrim.Location = new System.Drawing.Point(72, 104);
			this.colorBoxShirtTrim.Name = "colorBoxShirtTrim";
			this.colorBoxShirtTrim.Size = new System.Drawing.Size(56, 24);
			this.colorBoxShirtTrim.TabIndex = 7;
			this.colorBoxShirtTrim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8, 144);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(56, 24);
			this.label25.TabIndex = 18;
			this.label25.Text = "Shoes";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxShoes
			// 
			this.colorBoxShoes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxShoes.Location = new System.Drawing.Point(72, 144);
			this.colorBoxShoes.Name = "colorBoxShoes";
			this.colorBoxShoes.Size = new System.Drawing.Size(56, 24);
			this.colorBoxShoes.TabIndex = 8;
			this.colorBoxShoes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(8, 184);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(48, 24);
			this.label26.TabIndex = 19;
			this.label26.Text = "Shoes (trim)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxShoesTrim
			// 
			this.colorBoxShoesTrim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxShoesTrim.Location = new System.Drawing.Point(72, 184);
			this.colorBoxShoesTrim.Name = "colorBoxShoesTrim";
			this.colorBoxShoesTrim.Size = new System.Drawing.Size(56, 24);
			this.colorBoxShoesTrim.TabIndex = 9;
			this.colorBoxShoesTrim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.colorBoxHair);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.colorBoxSkin);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.colorBoxMustache);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.colorBoxBeard);
			this.groupBox1.Controls.Add(this.colorBoxSideburns);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 224);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Appearance";
			// 
			// colorBoxHair
			// 
			this.colorBoxHair.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxHair.Location = new System.Drawing.Point(72, 24);
			this.colorBoxHair.Name = "colorBoxHair";
			this.colorBoxHair.Size = new System.Drawing.Size(56, 24);
			this.colorBoxHair.TabIndex = 0;
			this.colorBoxHair.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 24);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(56, 24);
			this.label17.TabIndex = 10;
			this.label17.Text = "Hair";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(56, 24);
			this.label18.TabIndex = 11;
			this.label18.Text = "Skin";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxSkin
			// 
			this.colorBoxSkin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxSkin.Location = new System.Drawing.Point(72, 64);
			this.colorBoxSkin.Name = "colorBoxSkin";
			this.colorBoxSkin.Size = new System.Drawing.Size(56, 24);
			this.colorBoxSkin.TabIndex = 1;
			this.colorBoxSkin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 104);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(56, 24);
			this.label19.TabIndex = 12;
			this.label19.Text = "Mustache";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxMustache
			// 
			this.colorBoxMustache.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxMustache.Location = new System.Drawing.Point(72, 104);
			this.colorBoxMustache.Name = "colorBoxMustache";
			this.colorBoxMustache.Size = new System.Drawing.Size(56, 24);
			this.colorBoxMustache.TabIndex = 3;
			this.colorBoxMustache.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 144);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(56, 24);
			this.label20.TabIndex = 13;
			this.label20.Text = "Beard";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 184);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(56, 24);
			this.label21.TabIndex = 14;
			this.label21.Text = "Sideburns";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxBeard
			// 
			this.colorBoxBeard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxBeard.Location = new System.Drawing.Point(72, 144);
			this.colorBoxBeard.Name = "colorBoxBeard";
			this.colorBoxBeard.Size = new System.Drawing.Size(56, 24);
			this.colorBoxBeard.TabIndex = 2;
			this.colorBoxBeard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// colorBoxSideburns
			// 
			this.colorBoxSideburns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBoxSideburns.Location = new System.Drawing.Point(72, 184);
			this.colorBoxSideburns.Name = "colorBoxSideburns";
			this.colorBoxSideburns.Size = new System.Drawing.Size(56, 24);
			this.colorBoxSideburns.TabIndex = 4;
			this.colorBoxSideburns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.textBox22);
			this.tabPage2.Controls.Add(this.textBox23);
			this.tabPage2.Controls.Add(this.textBox24);
			this.tabPage2.Controls.Add(this.textBox25);
			this.tabPage2.Controls.Add(this.textBox26);
			this.tabPage2.Controls.Add(this.textBox17);
			this.tabPage2.Controls.Add(this.textBox18);
			this.tabPage2.Controls.Add(this.textBox19);
			this.tabPage2.Controls.Add(this.textBox20);
			this.tabPage2.Controls.Add(this.textBox21);
			this.tabPage2.Controls.Add(this.textBox12);
			this.tabPage2.Controls.Add(this.textBox13);
			this.tabPage2.Controls.Add(this.textBox14);
			this.tabPage2.Controls.Add(this.textBox15);
			this.tabPage2.Controls.Add(this.textBox16);
			this.tabPage2.Controls.Add(this.textBox7);
			this.tabPage2.Controls.Add(this.textBox8);
			this.tabPage2.Controls.Add(this.textBox9);
			this.tabPage2.Controls.Add(this.textBox10);
			this.tabPage2.Controls.Add(this.textBox11);
			this.tabPage2.Controls.Add(this.textBox6);
			this.tabPage2.Controls.Add(this.textBox5);
			this.tabPage2.Controls.Add(this.textBox4);
			this.tabPage2.Controls.Add(this.textBox2);
			this.tabPage2.Controls.Add(this.textBox3);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(328, 478);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Spell Sets";
			// 
			// textBox22
			// 
			this.textBox22.Location = new System.Drawing.Point(264, 176);
			this.textBox22.Name = "textBox22";
			this.textBox22.ReadOnly = true;
			this.textBox22.Size = new System.Drawing.Size(56, 20);
			this.textBox22.TabIndex = 30;
			this.textBox22.Text = "";
			// 
			// textBox23
			// 
			this.textBox23.Location = new System.Drawing.Point(200, 176);
			this.textBox23.Name = "textBox23";
			this.textBox23.ReadOnly = true;
			this.textBox23.Size = new System.Drawing.Size(56, 20);
			this.textBox23.TabIndex = 29;
			this.textBox23.Text = "";
			// 
			// textBox24
			// 
			this.textBox24.Location = new System.Drawing.Point(136, 176);
			this.textBox24.Name = "textBox24";
			this.textBox24.ReadOnly = true;
			this.textBox24.Size = new System.Drawing.Size(56, 20);
			this.textBox24.TabIndex = 28;
			this.textBox24.Text = "";
			// 
			// textBox25
			// 
			this.textBox25.Location = new System.Drawing.Point(8, 176);
			this.textBox25.Name = "textBox25";
			this.textBox25.ReadOnly = true;
			this.textBox25.Size = new System.Drawing.Size(56, 20);
			this.textBox25.TabIndex = 27;
			this.textBox25.Text = "";
			// 
			// textBox26
			// 
			this.textBox26.Location = new System.Drawing.Point(72, 176);
			this.textBox26.Name = "textBox26";
			this.textBox26.ReadOnly = true;
			this.textBox26.Size = new System.Drawing.Size(56, 20);
			this.textBox26.TabIndex = 26;
			this.textBox26.Text = "";
			// 
			// textBox17
			// 
			this.textBox17.Location = new System.Drawing.Point(264, 144);
			this.textBox17.Name = "textBox17";
			this.textBox17.ReadOnly = true;
			this.textBox17.Size = new System.Drawing.Size(56, 20);
			this.textBox17.TabIndex = 25;
			this.textBox17.Text = "";
			// 
			// textBox18
			// 
			this.textBox18.Location = new System.Drawing.Point(200, 144);
			this.textBox18.Name = "textBox18";
			this.textBox18.ReadOnly = true;
			this.textBox18.Size = new System.Drawing.Size(56, 20);
			this.textBox18.TabIndex = 24;
			this.textBox18.Text = "";
			// 
			// textBox19
			// 
			this.textBox19.Location = new System.Drawing.Point(136, 144);
			this.textBox19.Name = "textBox19";
			this.textBox19.ReadOnly = true;
			this.textBox19.Size = new System.Drawing.Size(56, 20);
			this.textBox19.TabIndex = 23;
			this.textBox19.Text = "";
			// 
			// textBox20
			// 
			this.textBox20.Location = new System.Drawing.Point(8, 144);
			this.textBox20.Name = "textBox20";
			this.textBox20.ReadOnly = true;
			this.textBox20.Size = new System.Drawing.Size(56, 20);
			this.textBox20.TabIndex = 22;
			this.textBox20.Text = "";
			// 
			// textBox21
			// 
			this.textBox21.Location = new System.Drawing.Point(72, 144);
			this.textBox21.Name = "textBox21";
			this.textBox21.ReadOnly = true;
			this.textBox21.Size = new System.Drawing.Size(56, 20);
			this.textBox21.TabIndex = 21;
			this.textBox21.Text = "";
			// 
			// textBox12
			// 
			this.textBox12.Location = new System.Drawing.Point(264, 112);
			this.textBox12.Name = "textBox12";
			this.textBox12.ReadOnly = true;
			this.textBox12.Size = new System.Drawing.Size(56, 20);
			this.textBox12.TabIndex = 20;
			this.textBox12.Text = "";
			// 
			// textBox13
			// 
			this.textBox13.Location = new System.Drawing.Point(200, 112);
			this.textBox13.Name = "textBox13";
			this.textBox13.ReadOnly = true;
			this.textBox13.Size = new System.Drawing.Size(56, 20);
			this.textBox13.TabIndex = 19;
			this.textBox13.Text = "";
			// 
			// textBox14
			// 
			this.textBox14.Location = new System.Drawing.Point(136, 112);
			this.textBox14.Name = "textBox14";
			this.textBox14.ReadOnly = true;
			this.textBox14.Size = new System.Drawing.Size(56, 20);
			this.textBox14.TabIndex = 18;
			this.textBox14.Text = "";
			// 
			// textBox15
			// 
			this.textBox15.Location = new System.Drawing.Point(8, 112);
			this.textBox15.Name = "textBox15";
			this.textBox15.ReadOnly = true;
			this.textBox15.Size = new System.Drawing.Size(56, 20);
			this.textBox15.TabIndex = 17;
			this.textBox15.Text = "";
			// 
			// textBox16
			// 
			this.textBox16.Location = new System.Drawing.Point(72, 112);
			this.textBox16.Name = "textBox16";
			this.textBox16.ReadOnly = true;
			this.textBox16.Size = new System.Drawing.Size(56, 20);
			this.textBox16.TabIndex = 16;
			this.textBox16.Text = "";
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(264, 80);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(56, 20);
			this.textBox7.TabIndex = 15;
			this.textBox7.Text = "";
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(200, 80);
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new System.Drawing.Size(56, 20);
			this.textBox8.TabIndex = 14;
			this.textBox8.Text = "";
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(136, 80);
			this.textBox9.Name = "textBox9";
			this.textBox9.ReadOnly = true;
			this.textBox9.Size = new System.Drawing.Size(56, 20);
			this.textBox9.TabIndex = 13;
			this.textBox9.Text = "";
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(8, 80);
			this.textBox10.Name = "textBox10";
			this.textBox10.ReadOnly = true;
			this.textBox10.Size = new System.Drawing.Size(56, 20);
			this.textBox10.TabIndex = 12;
			this.textBox10.Text = "";
			// 
			// textBox11
			// 
			this.textBox11.Location = new System.Drawing.Point(72, 80);
			this.textBox11.Name = "textBox11";
			this.textBox11.ReadOnly = true;
			this.textBox11.Size = new System.Drawing.Size(56, 20);
			this.textBox11.TabIndex = 11;
			this.textBox11.Text = "";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(264, 48);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(56, 20);
			this.textBox6.TabIndex = 10;
			this.textBox6.Text = "";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(200, 48);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(56, 20);
			this.textBox5.TabIndex = 9;
			this.textBox5.Text = "";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(136, 48);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(56, 20);
			this.textBox4.TabIndex = 8;
			this.textBox4.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(8, 48);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(56, 20);
			this.textBox2.TabIndex = 7;
			this.textBox2.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(72, 48);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(56, 20);
			this.textBox3.TabIndex = 6;
			this.textBox3.Text = "";
			// 
			// label6
			// 
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Location = new System.Drawing.Point(264, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "G";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Location = new System.Drawing.Point(200, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "F";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Location = new System.Drawing.Point(136, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "D";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Location = new System.Drawing.Point(72, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "S";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "A";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nameLengthLabel
			// 
			this.nameLengthLabel.Location = new System.Drawing.Point(232, 24);
			this.nameLengthLabel.Name = "nameLengthLabel";
			this.nameLengthLabel.Size = new System.Drawing.Size(72, 16);
			this.nameLengthLabel.TabIndex = 5;
			this.nameLengthLabel.Text = "Length: ";
			this.nameLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 515);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.menuBar;
			this.Name = "MainWindow";
			this.Text = "Nox Player Editor";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void MyInitializeComponent()
		{
			//color labels
			colorBoxHair.Click += new EventHandler(ChangeColor);
			colorBoxSkin.Click += new EventHandler(ChangeColor);
			colorBoxBeard.Click += new EventHandler(ChangeColor);
			colorBoxMustache.Click += new EventHandler(ChangeColor);
			colorBoxSideburns.Click += new EventHandler(ChangeColor);
			colorBoxPants.Click += new EventHandler(ChangeUserColor);
			colorBoxShirt.Click += new EventHandler(ChangeUserColor);
			colorBoxShirtTrim.Click += new EventHandler(ChangeUserColor);
			colorBoxShoes.Click += new EventHandler(ChangeUserColor);
			colorBoxShoesTrim.Click += new EventHandler(ChangeUserColor);
		}

		protected ColorDialog colorDialog = new ColorDialog();
		protected void ChangeColor(object sender, EventArgs e)
		{
			colorDialog.Color = ((Label) sender).BackColor;
			if (colorDialog.ShowDialog() == DialogResult.OK)
				((Label) sender).BackColor = colorDialog.Color;
		}
		
		protected void ChangeUserColor(object sender, EventArgs e)
		{
			UserColorDialog dlg = new UserColorDialog();
			//if (dlg.ShowDialog() == DialogResult.OK)//FIXME
			//HACK next 2 lines should be the commented out one above
			dlg.Color = ((Label) sender).BackColor;
			dlg.ShowDialog();
			((Label) sender).BackColor = dlg.Color;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainWindow());
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fd = new OpenFileDialog();

			fd.Filter = "Nox Player Files (*.plr)|*.plr";
			//fd.ReadOnlyChecked = true;
			fd.ShowDialog();

			if (System.IO.File.Exists(fd.FileName))
			{
				player = new PlayerFile();
				player.Load(fd.FileName);

				//Update the GUI
				nameBox.Text = player.Name;

				//colors
				colorBoxHair.BackColor = player.HairColor;
				colorBoxSkin.BackColor = player.SkinColor;
				colorBoxBeard.BackColor = player.BeardColor;
				colorBoxMustache.BackColor = player.MustacheColor;
				colorBoxSideburns.BackColor = player.SideburnsColor;

				colorBoxPants.BackColor = player.PantsColor;
				colorBoxShirt.BackColor = player.ShirtColor;
				colorBoxShirtTrim.BackColor = player.ShirtTrimColor;
				colorBoxShoes.BackColor = player.ShoesColor;
				colorBoxShoesTrim.BackColor = player.ShoesTrimColor;

				classBox.SelectedIndex = (int) player.Class;
			}
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			//Dispose();
			System.Environment.Exit(0);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Form dlg = new AboutDialog();
			dlg.ShowDialog(this);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			player.HairColor = colorBoxHair.BackColor;
			player.SkinColor = colorBoxSkin.BackColor;
			player.MustacheColor = colorBoxMustache.BackColor;
			player.BeardColor = colorBoxBeard.BackColor;
			player.SideburnsColor = colorBoxSideburns.BackColor;
			player.PantsColor = (UserColor) colorBoxPants.BackColor;
			player.ShirtColor = (UserColor) colorBoxShirt.BackColor;
			player.ShirtTrimColor = (UserColor) colorBoxShirtTrim.BackColor;
			player.ShoesColor = (UserColor) colorBoxShoes.BackColor;
			player.ShoesTrimColor = (UserColor) colorBoxShoesTrim.BackColor;

			player.Name = nameBox.Text;
			player.Class = (PlayerFile.CharacterClass) classBox.SelectedIndex;

			player.WriteFile();
		}

		private bool nameWarned;
		private void nameBox_TextChanged(object sender, EventArgs e)
		{
			nameLengthLabel.Text = "Length: " + ((TextBox) sender).Text.Length;
			if (((TextBox) sender).Text.Length > 24)
				nameLengthLabel.ForeColor = Color.Red;
			else
				nameLengthLabel.ForeColor = Color.FromKnownColor(KnownColor.WindowText);

			if (((TextBox) sender).Text.Length > 24 && !nameWarned)
			{
				MessageBox.Show("Names longer than 24 characters will not show correctly in the game.", "Warning");
				nameWarned = true;
			}
		}
	}
}
