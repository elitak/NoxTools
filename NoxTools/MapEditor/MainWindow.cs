using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using NoxShared;

namespace NoxMapEditor
{
	public class MainWindow : Form
	{
		private System.Windows.Forms.TabPage WallViewer;
		private System.Windows.Forms.TabPage largeMap;
		private NoxMapEditor.MapView mapView1;
		private System.Windows.Forms.Panel MinimapPanel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox mapSummary;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox mapDescription;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox mapAuthor;
		private System.Windows.Forms.TextBox mapEmail;
		private System.Windows.Forms.TextBox mapEmail2;
		private System.Windows.Forms.TextBox mapAuthor2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox mapDate;
		private System.Windows.Forms.TextBox mapVersion;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.Label minRecLbl;
		private System.Windows.Forms.Label maxRecLbl;
		private System.Windows.Forms.Label recommendedLbl;
		private System.Windows.Forms.Label mapTypeLbl;
		private System.Windows.Forms.TextBox mapMinRec;
		private System.Windows.Forms.TextBox mapMaxRec;
		private System.Windows.Forms.ComboBox mapType;
		private System.Windows.Forms.MenuItem itemSave;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.MenuItem menuItemSaveAs;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem viewObjects;
		private System.Windows.Forms.TextBox mapCopyright;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItemGrid;
		private System.Windows.Forms.MenuItem menuItemNxz;

		protected Map map = new Map();

		public MainWindow()
		{
			InitializeComponent();

			mapType.Items.AddRange(new ArrayList(Map.MapInfo.MapTypeNames.Values).ToArray());
			mapType.SelectedIndex = 3;//arena by default

			//This init code never seems to stay after you edit it with VS.NET
			mapView1 = new MapView();
			largeMap.Controls.Add(mapView1);
			mapView1.Dock = System.Windows.Forms.DockStyle.Fill;
			mapView1.Location = new System.Drawing.Point(0, 0);
			mapView1.Size = new System.Drawing.Size(1008, 695);
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
			this.menuItemNew = new System.Windows.Forms.MenuItem();
			this.menuItemOpen = new System.Windows.Forms.MenuItem();
			this.menuItemSave = new System.Windows.Forms.MenuItem();
			this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.viewObjects = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemGrid = new System.Windows.Forms.MenuItem();
			this.menuItemNxz = new System.Windows.Forms.MenuItem();
			this.itemSave = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.largeMap = new System.Windows.Forms.TabPage();
			this.WallViewer = new System.Windows.Forms.TabPage();
			this.MinimapPanel = new System.Windows.Forms.Panel();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mapType = new System.Windows.Forms.ComboBox();
			this.mapTypeLbl = new System.Windows.Forms.Label();
			this.recommendedLbl = new System.Windows.Forms.Label();
			this.maxRecLbl = new System.Windows.Forms.Label();
			this.minRecLbl = new System.Windows.Forms.Label();
			this.mapMaxRec = new System.Windows.Forms.TextBox();
			this.mapMinRec = new System.Windows.Forms.TextBox();
			this.mapCopyright = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.mapVersion = new System.Windows.Forms.TextBox();
			this.mapDate = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.mapAuthor2 = new System.Windows.Forms.TextBox();
			this.mapEmail2 = new System.Windows.Forms.TextBox();
			this.mapEmail = new System.Windows.Forms.TextBox();
			this.mapAuthor = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.mapDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mapSummary = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.WallViewer.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem4,
																					  this.itemSave});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemNew,
																					  this.menuItemOpen,
																					  this.menuItemSave,
																					  this.menuItemSaveAs,
																					  this.menuItem3,
																					  this.menuItemExit});
			this.menuItem1.Text = "File";
			// 
			// menuItemNew
			// 
			this.menuItemNew.Index = 0;
			this.menuItemNew.Text = "New";
			this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
			// 
			// menuItemOpen
			// 
			this.menuItemOpen.Index = 1;
			this.menuItemOpen.Text = "Open";
			this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
			// 
			// menuItemSave
			// 
			this.menuItemSave.Index = 2;
			this.menuItemSave.Text = "Save";
			this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
			// 
			// menuItemSaveAs
			// 
			this.menuItemSaveAs.Index = 3;
			this.menuItemSaveAs.Text = "Save As...";
			this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 5;
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.viewObjects});
			this.menuItem2.Text = "Map";
			// 
			// viewObjects
			// 
			this.viewObjects.Index = 0;
			this.viewObjects.Text = "List Objects";
			this.viewObjects.Click += new System.EventHandler(this.viewObjects_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemGrid,
																					  this.menuItemNxz});
			this.menuItem4.Text = "Options";
			// 
			// menuItemGrid
			// 
			this.menuItemGrid.Index = 0;
			this.menuItemGrid.Text = "Grid";
			this.menuItemGrid.Click += new System.EventHandler(this.menuItemGrid_Click);
			// 
			// menuItemNxz
			// 
			this.menuItemNxz.Checked = true;
			this.menuItemNxz.Index = 1;
			this.menuItemNxz.Text = "Save .nxz";
			this.menuItemNxz.Click += new System.EventHandler(this.menuItemNxz_Click);
			// 
			// itemSave
			// 
			this.itemSave.Index = 3;
			this.itemSave.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemAbout});
			this.itemSave.Text = "Help";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 0;
			this.menuItemAbout.Text = "About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.largeMap);
			this.tabControl1.Controls.Add(this.WallViewer);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1272, 913);
			this.tabControl1.TabIndex = 0;
			// 
			// largeMap
			// 
			this.largeMap.Location = new System.Drawing.Point(4, 22);
			this.largeMap.Name = "largeMap";
			this.largeMap.Size = new System.Drawing.Size(1264, 887);
			this.largeMap.TabIndex = 0;
			this.largeMap.Text = "Large Map";
			// 
			// WallViewer
			// 
			this.WallViewer.Controls.Add(this.MinimapPanel);
			this.WallViewer.Location = new System.Drawing.Point(4, 22);
			this.WallViewer.Name = "WallViewer";
			this.WallViewer.Size = new System.Drawing.Size(1208, 845);
			this.WallViewer.TabIndex = 0;
			this.WallViewer.Text = "Mini Map";
			// 
			// MinimapPanel
			// 
			this.MinimapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MinimapPanel.Location = new System.Drawing.Point(0, 0);
			this.MinimapPanel.Name = "MinimapPanel";
			this.MinimapPanel.Size = new System.Drawing.Size(1208, 845);
			this.MinimapPanel.TabIndex = 0;
			this.MinimapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MinimapPanel_Paint);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(1208, 845);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Map Info";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mapType);
			this.groupBox1.Controls.Add(this.mapTypeLbl);
			this.groupBox1.Controls.Add(this.recommendedLbl);
			this.groupBox1.Controls.Add(this.maxRecLbl);
			this.groupBox1.Controls.Add(this.minRecLbl);
			this.groupBox1.Controls.Add(this.mapMaxRec);
			this.groupBox1.Controls.Add(this.mapMinRec);
			this.groupBox1.Controls.Add(this.mapCopyright);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.mapVersion);
			this.groupBox1.Controls.Add(this.mapDate);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.mapAuthor2);
			this.groupBox1.Controls.Add(this.mapEmail2);
			this.groupBox1.Controls.Add(this.mapEmail);
			this.groupBox1.Controls.Add(this.mapAuthor);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.mapDescription);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.mapSummary);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1208, 560);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// mapType
			// 
			this.mapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mapType.Location = new System.Drawing.Point(88, 24);
			this.mapType.Name = "mapType";
			this.mapType.Size = new System.Drawing.Size(88, 21);
			this.mapType.TabIndex = 26;
			// 
			// mapTypeLbl
			// 
			this.mapTypeLbl.Location = new System.Drawing.Point(24, 24);
			this.mapTypeLbl.Name = "mapTypeLbl";
			this.mapTypeLbl.Size = new System.Drawing.Size(64, 24);
			this.mapTypeLbl.TabIndex = 25;
			this.mapTypeLbl.Text = "Map Type";
			this.mapTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// recommendedLbl
			// 
			this.recommendedLbl.Location = new System.Drawing.Point(256, 264);
			this.recommendedLbl.Name = "recommendedLbl";
			this.recommendedLbl.Size = new System.Drawing.Size(184, 24);
			this.recommendedLbl.TabIndex = 24;
			this.recommendedLbl.Text = "Recommended Number of Players";
			this.recommendedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// maxRecLbl
			// 
			this.maxRecLbl.Location = new System.Drawing.Point(328, 288);
			this.maxRecLbl.Name = "maxRecLbl";
			this.maxRecLbl.Size = new System.Drawing.Size(32, 24);
			this.maxRecLbl.TabIndex = 23;
			this.maxRecLbl.Text = "Max";
			this.maxRecLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// minRecLbl
			// 
			this.minRecLbl.Location = new System.Drawing.Point(256, 288);
			this.minRecLbl.Name = "minRecLbl";
			this.minRecLbl.Size = new System.Drawing.Size(32, 24);
			this.minRecLbl.TabIndex = 22;
			this.minRecLbl.Text = "Min";
			this.minRecLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mapMaxRec
			// 
			this.mapMaxRec.Location = new System.Drawing.Point(360, 288);
			this.mapMaxRec.Name = "mapMaxRec";
			this.mapMaxRec.Size = new System.Drawing.Size(32, 20);
			this.mapMaxRec.TabIndex = 21;
			this.mapMaxRec.Text = "";
			// 
			// mapMinRec
			// 
			this.mapMinRec.Location = new System.Drawing.Point(288, 288);
			this.mapMinRec.Name = "mapMinRec";
			this.mapMinRec.Size = new System.Drawing.Size(32, 20);
			this.mapMinRec.TabIndex = 20;
			this.mapMinRec.Text = "";
			// 
			// mapCopyright
			// 
			this.mapCopyright.Location = new System.Drawing.Point(88, 264);
			this.mapCopyright.Name = "mapCopyright";
			this.mapCopyright.Size = new System.Drawing.Size(128, 20);
			this.mapCopyright.TabIndex = 17;
			this.mapCopyright.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 264);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 24);
			this.label9.TabIndex = 16;
			this.label9.Text = "Copyright";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mapVersion
			// 
			this.mapVersion.Location = new System.Drawing.Point(88, 288);
			this.mapVersion.Name = "mapVersion";
			this.mapVersion.Size = new System.Drawing.Size(128, 20);
			this.mapVersion.TabIndex = 15;
			this.mapVersion.Text = "";
			// 
			// mapDate
			// 
			this.mapDate.Location = new System.Drawing.Point(88, 312);
			this.mapDate.Name = "mapDate";
			this.mapDate.Size = new System.Drawing.Size(128, 20);
			this.mapDate.TabIndex = 14;
			this.mapDate.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 312);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 24);
			this.label8.TabIndex = 13;
			this.label8.Text = "Date";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mapAuthor2
			// 
			this.mapAuthor2.Location = new System.Drawing.Point(88, 224);
			this.mapAuthor2.Name = "mapAuthor2";
			this.mapAuthor2.Size = new System.Drawing.Size(128, 20);
			this.mapAuthor2.TabIndex = 12;
			this.mapAuthor2.Text = "";
			// 
			// mapEmail2
			// 
			this.mapEmail2.Location = new System.Drawing.Point(288, 224);
			this.mapEmail2.Name = "mapEmail2";
			this.mapEmail2.Size = new System.Drawing.Size(160, 20);
			this.mapEmail2.TabIndex = 11;
			this.mapEmail2.Text = "";
			// 
			// mapEmail
			// 
			this.mapEmail.Location = new System.Drawing.Point(288, 192);
			this.mapEmail.Name = "mapEmail";
			this.mapEmail.Size = new System.Drawing.Size(160, 20);
			this.mapEmail.TabIndex = 10;
			this.mapEmail.Text = "";
			// 
			// mapAuthor
			// 
			this.mapAuthor.Location = new System.Drawing.Point(88, 192);
			this.mapAuthor.Name = "mapAuthor";
			this.mapAuthor.Size = new System.Drawing.Size(128, 20);
			this.mapAuthor.TabIndex = 9;
			this.mapAuthor.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(248, 224);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 24);
			this.label7.TabIndex = 8;
			this.label7.Text = "Email";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(248, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 24);
			this.label6.TabIndex = 7;
			this.label6.Text = "Email";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 224);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 24);
			this.label5.TabIndex = 6;
			this.label5.Text = "Secondary Author";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 24);
			this.label4.TabIndex = 5;
			this.label4.Text = "Author";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 288);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "Version";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mapDescription
			// 
			this.mapDescription.Location = new System.Drawing.Point(88, 88);
			this.mapDescription.Multiline = true;
			this.mapDescription.Name = "mapDescription";
			this.mapDescription.Size = new System.Drawing.Size(360, 88);
			this.mapDescription.TabIndex = 3;
			this.mapDescription.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title/Summary";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mapSummary
			// 
			this.mapSummary.Location = new System.Drawing.Point(88, 56);
			this.mapSummary.Name = "mapSummary";
			this.mapSummary.Size = new System.Drawing.Size(360, 20);
			this.mapSummary.TabIndex = 1;
			this.mapSummary.Text = "";
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1272, 913);
			this.Controls.Add(this.tabControl1);
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(526, 582);
			this.Name = "MainWindow";
			this.Text = "Nox Map Editor";
			this.tabControl1.ResumeLayout(false);
			this.WallViewer.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main()
		{
			try
			{
				Application.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
				Application.Run(new MainWindow());
			}
			catch (Exception ex)
			{
				new ExceptionDialog(ex).ShowDialog();
				Environment.Exit(1);
			}
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fd = new OpenFileDialog();

			fd.Filter = "Nox Map Files (*.map)|*.map";

			if (fd.ShowDialog() == DialogResult.OK && System.IO.File.Exists(fd.FileName))
			{
				//TODO: check for changes and prompt to save before opening another map
				map = new Map(fd.FileName);

				mapView1.Map = map;
				mapType.SelectedIndex = Map.MapInfo.MapTypeNames.IndexOfKey(map.Info.Type);
				mapSummary.Text = map.Info.Summary;
				mapDescription.Text = map.Info.Description;

				mapAuthor.Text = map.Info.Author;
				mapEmail.Text = map.Info.Email;
				mapAuthor2.Text = map.Info.Author2;
				mapEmail2.Text = map.Info.Email2;

				mapVersion.Text = map.Info.Version;
				mapCopyright.Text = map.Info.Copyright;
				mapDate.Text = map.Info.Date;

				mapMinRec.Text = String.Format("{0}", map.Info.RecommendedMin);
				mapMaxRec.Text = String.Format("{0}", map.Info.RecommendedMax);

				mapView1.SelectedObject = null;

				Invalidate(true);
			}
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			//do any necessary prompts or cleanup...

			//then exit
			Environment.Exit(0);
		}

		private void MinimapPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (map == null)
				return;
			int mapZoom = 2, mapDimension = 256;
			e.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, mapDimension*mapZoom, mapDimension*mapZoom);

			foreach (Point pt in map.WallMap.Keys)
				e.Graphics.DrawRectangle(new Pen(Color.White, 1), pt.X*mapZoom, pt.Y*mapZoom, mapZoom/2, mapZoom/2);
		}

		private void menuItemSave_Click(object sender, System.EventArgs e)
		{
			if(map == null)
				return;
			else if (map.FileName == null)
			{
				menuItemSaveAs_Click(sender, e);//FIXME, fire event instead
				return;
			}

			//TODO: check lengths for each to make sure they aren't too long
			map.Info.Type = (Map.MapInfo.MapType) Map.MapInfo.MapTypeNames.GetKey(mapType.SelectedIndex);//FIXME: default to something if unspecified
			map.Info.Summary = mapSummary.Text;
			map.Info.Description = mapDescription.Text;

			map.Info.Author = mapAuthor.Text;
			map.Info.Email = mapEmail.Text;
			map.Info.Author2 = mapAuthor2.Text;
			map.Info.Email2 = mapEmail2.Text;

			map.Info.Version = mapVersion.Text;
			map.Info.Copyright = mapCopyright.Text;
			map.Info.Date = mapDate.Text;
			map.Info.RecommendedMin = mapMinRec.Text.Length == 0 ? (byte) 0 : Convert.ToByte(mapMinRec.Text);
			map.Info.RecommendedMax = mapMaxRec.Text.Length == 0 ? (byte) 0 : Convert.ToByte(mapMaxRec.Text);

			map.WriteMap();
			if (menuItemNxz.Checked)
				map.WriteNxz();
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutDialog dlg = new AboutDialog();
			dlg.ShowDialog();
		}

		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			map = new Map();
		}

		private void menuItemSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog fd = new SaveFileDialog();
			fd.Filter = "Nox Map Files (*.map)|*.map";

			if (fd.ShowDialog() == DialogResult.OK)//&& fd.FileName)
			{
				map.FileName = fd.FileName;
				this.menuItemSave_Click(sender, e);//FIXME HOWTO? fire the event instead
			}
		}

		private void viewObjects_Click(object sender, System.EventArgs e)
		{
			ObjectListDialog objLd = new ObjectListDialog();
			objLd.objTable = map.Objects;
			objLd.Show();
			objLd.Owner = this;
		}

		private void menuItemGrid_Click(object sender, System.EventArgs e)
		{
			menuItemGrid.Checked = !menuItemGrid.Checked;
			mapView1.DrawGrid = menuItemGrid.Checked;
			Invalidate(true);
		}

		private void menuItemNxz_Click(object sender, System.EventArgs e)
		{
			menuItemNxz.Checked = !menuItemNxz.Checked;
		}
	}
}




