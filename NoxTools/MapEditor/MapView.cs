using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	public class MapView : System.Windows.Forms.UserControl
	{
		public Map Map;
		protected static int squareSize = 23;
		protected int objectSelectionRadius = 7;
		protected Button currentButton;
		public Map.Object SelectedObject = new Map.Object();
		public bool DrawGrid;
		protected const int wallThickness = 2;
		protected const int gridThickness = 1;
		protected Map.Object DefaultObject = new Map.Object();
		protected PolygonDialog polyDlg = new PolygonDialog();

		public enum Mode
		{
			MAKE_WALL,
			MAKE_OBJECT,
			SELECT,
			MAKE_WINDOW,
			MAKE_DESTRUCTABLE,
			MAKE_SECRET,
			MAKE_FLOOR
		};
		public Mode CurrentMode;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Button newObjectButton;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem contextMenuDelete;
		private System.Windows.Forms.MenuItem contextMenuProperties;
		private System.Windows.Forms.MenuItem menuItem3;
		private NoxMapEditor.WallSelector wallSelector1;
		private System.Windows.Forms.Button windowsButton;
		private System.Windows.Forms.Button destructableButton;
		private System.Windows.Forms.Button floorButton;
		private System.Windows.Forms.ComboBox tileGraphic;
		private System.Windows.Forms.Button buttonSecret;
		
		private System.Windows.Forms.Button buttonBlend;
		private System.Windows.Forms.ComboBox tileVar;
		private System.Windows.Forms.GroupBox floorGroup;
		private System.Windows.Forms.GroupBox wallGroup;
		private System.Windows.Forms.GroupBox objectGroup;
		private System.Windows.Forms.CheckBox threeFloorBox;
		private System.Windows.Forms.Button defaultButt;
		private System.Windows.Forms.GroupBox groupPolygons;
		private System.Windows.Forms.Button buttonPoints;
		private System.Windows.Forms.Button buttonEditPolygon;
		private System.Windows.Forms.Button buttonPolygonNew;
		private System.Windows.Forms.ComboBox listPolygons;
		private System.Windows.Forms.Button buttonPolygonDelete;
		private System.Windows.Forms.StatusBarPanel statusWall;
		private System.Windows.Forms.StatusBarPanel statusTile;
		private System.Windows.Forms.StatusBarPanel statusObject;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel statusLocation;
		private System.Windows.Forms.Panel scrollPanel;
		private System.Windows.Forms.Panel wallSelectPanel;

		protected class FlickerFreePanel : Panel {public FlickerFreePanel() : base() {SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);}}
		private System.Windows.Forms.Panel mapPanel;

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.contextMenuDelete = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.contextMenuProperties = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusLocation = new System.Windows.Forms.StatusBarPanel();
			this.statusWall = new System.Windows.Forms.StatusBarPanel();
			this.statusTile = new System.Windows.Forms.StatusBarPanel();
			this.statusObject = new System.Windows.Forms.StatusBarPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.objectGroup = new System.Windows.Forms.GroupBox();
			this.selectButton = new System.Windows.Forms.Button();
			this.newObjectButton = new System.Windows.Forms.Button();
			this.defaultButt = new System.Windows.Forms.Button();
			this.floorGroup = new System.Windows.Forms.GroupBox();
			this.threeFloorBox = new System.Windows.Forms.CheckBox();
			this.buttonBlend = new System.Windows.Forms.Button();
			this.tileVar = new System.Windows.Forms.ComboBox();
			this.tileGraphic = new System.Windows.Forms.ComboBox();
			this.floorButton = new System.Windows.Forms.Button();
			this.wallGroup = new System.Windows.Forms.GroupBox();
			this.wallSelectPanel = new System.Windows.Forms.Panel();
			this.buttonSecret = new System.Windows.Forms.Button();
			this.destructableButton = new System.Windows.Forms.Button();
			this.windowsButton = new System.Windows.Forms.Button();
			this.groupPolygons = new System.Windows.Forms.GroupBox();
			this.buttonPolygonDelete = new System.Windows.Forms.Button();
			this.buttonPolygonNew = new System.Windows.Forms.Button();
			this.buttonEditPolygon = new System.Windows.Forms.Button();
			this.buttonPoints = new System.Windows.Forms.Button();
			this.listPolygons = new System.Windows.Forms.ComboBox();
			this.mapPanel = new FlickerFreePanel();
			this.scrollPanel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.statusLocation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusWall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusTile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusObject)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.objectGroup.SuspendLayout();
			this.floorGroup.SuspendLayout();
			this.wallGroup.SuspendLayout();
			this.groupPolygons.SuspendLayout();
			this.scrollPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.contextMenuDelete,
																						 this.menuItem3,
																						 this.contextMenuProperties});
			// 
			// contextMenuDelete
			// 
			this.contextMenuDelete.Index = 0;
			this.contextMenuDelete.Text = "Delete";
			this.contextMenuDelete.Click += new System.EventHandler(this.contextMenuDelete_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// contextMenuProperties
			// 
			this.contextMenuProperties.Index = 2;
			this.contextMenuProperties.Text = "Properties";
			this.contextMenuProperties.Click += new System.EventHandler(this.contextMenuProperties_Click);
			// 
			// statusBar
			// 
			this.statusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.statusBar.Location = new System.Drawing.Point(0, 578);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.statusLocation,
																						 this.statusWall,
																						 this.statusTile,
																						 this.statusObject});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(800, 22);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 1;
			// 
			// statusLocation
			// 
			this.statusLocation.MinWidth = 0;
			this.statusLocation.ToolTipText = "Location";
			// 
			// statusWall
			// 
			this.statusWall.MinWidth = 0;
			this.statusWall.ToolTipText = "Wall Info";
			// 
			// statusTile
			// 
			this.statusTile.MinWidth = 0;
			this.statusTile.ToolTipText = "Tile Info";
			this.statusTile.Width = 400;
			// 
			// statusObject
			// 
			this.statusObject.MinWidth = 0;
			this.statusObject.ToolTipText = "Object Info";
			this.statusObject.Width = 300;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.objectGroup);
			this.groupBox1.Controls.Add(this.floorGroup);
			this.groupBox1.Controls.Add(this.wallGroup);
			this.groupBox1.Controls.Add(this.groupPolygons);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 578);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tools";
			// 
			// objectGroup
			// 
			this.objectGroup.Controls.Add(this.selectButton);
			this.objectGroup.Controls.Add(this.newObjectButton);
			this.objectGroup.Controls.Add(this.defaultButt);
			this.objectGroup.Location = new System.Drawing.Point(128, 168);
			this.objectGroup.Name = "objectGroup";
			this.objectGroup.Size = new System.Drawing.Size(112, 96);
			this.objectGroup.TabIndex = 24;
			this.objectGroup.TabStop = false;
			this.objectGroup.Text = "Objects";
			// 
			// selectButton
			// 
			this.selectButton.Location = new System.Drawing.Point(24, 64);
			this.selectButton.Name = "selectButton";
			this.selectButton.Size = new System.Drawing.Size(56, 23);
			this.selectButton.TabIndex = 0;
			this.selectButton.Text = "Select";
			this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
			// 
			// newObjectButton
			// 
			this.newObjectButton.Location = new System.Drawing.Point(24, 16);
			this.newObjectButton.Name = "newObjectButton";
			this.newObjectButton.Size = new System.Drawing.Size(56, 23);
			this.newObjectButton.TabIndex = 1;
			this.newObjectButton.Text = "Create";
			this.newObjectButton.Click += new System.EventHandler(this.newObjectButton_Click);
			// 
			// defaultButt
			// 
			this.defaultButt.Location = new System.Drawing.Point(24, 40);
			this.defaultButt.Name = "defaultButt";
			this.defaultButt.Size = new System.Drawing.Size(56, 23);
			this.defaultButt.TabIndex = 25;
			this.defaultButt.Text = "Defaults";
			this.defaultButt.Click += new System.EventHandler(this.defaultButt_Click);
			// 
			// floorGroup
			// 
			this.floorGroup.Controls.Add(this.threeFloorBox);
			this.floorGroup.Controls.Add(this.buttonBlend);
			this.floorGroup.Controls.Add(this.tileVar);
			this.floorGroup.Controls.Add(this.tileGraphic);
			this.floorGroup.Controls.Add(this.floorButton);
			this.floorGroup.Location = new System.Drawing.Point(128, 16);
			this.floorGroup.Name = "floorGroup";
			this.floorGroup.Size = new System.Drawing.Size(112, 144);
			this.floorGroup.TabIndex = 22;
			this.floorGroup.TabStop = false;
			this.floorGroup.Text = "Tiles";
			// 
			// threeFloorBox
			// 
			this.threeFloorBox.Location = new System.Drawing.Point(72, 16);
			this.threeFloorBox.Name = "threeFloorBox";
			this.threeFloorBox.Size = new System.Drawing.Size(48, 24);
			this.threeFloorBox.TabIndex = 22;
			this.threeFloorBox.Text = "3x3";
			// 
			// buttonBlend
			// 
			this.buttonBlend.Location = new System.Drawing.Point(24, 112);
			this.buttonBlend.Name = "buttonBlend";
			this.buttonBlend.Size = new System.Drawing.Size(56, 24);
			this.buttonBlend.TabIndex = 19;
			this.buttonBlend.Text = "Edges";
			this.buttonBlend.Click += new System.EventHandler(this.buttonBlend_Click);
			// 
			// tileVar
			// 
			this.tileVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tileVar.DropDownWidth = 40;
			this.tileVar.Location = new System.Drawing.Point(8, 80);
			this.tileVar.MaxDropDownItems = 10;
			this.tileVar.Name = "tileVar";
			this.tileVar.Size = new System.Drawing.Size(64, 21);
			this.tileVar.TabIndex = 21;
			// 
			// tileGraphic
			// 
			this.tileGraphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tileGraphic.DropDownWidth = 180;
			this.tileGraphic.Location = new System.Drawing.Point(8, 48);
			this.tileGraphic.Name = "tileGraphic";
			this.tileGraphic.Size = new System.Drawing.Size(96, 21);
			this.tileGraphic.TabIndex = 13;
			this.tileGraphic.SelectedIndexChanged += new System.EventHandler(this.tileGraphic_SelectedIndexChanged);
			// 
			// floorButton
			// 
			this.floorButton.Location = new System.Drawing.Point(8, 16);
			this.floorButton.Name = "floorButton";
			this.floorButton.Size = new System.Drawing.Size(56, 23);
			this.floorButton.TabIndex = 8;
			this.floorButton.Text = "Create";
			this.floorButton.Click += new System.EventHandler(this.floorButton_Click);
			// 
			// wallGroup
			// 
			this.wallGroup.Controls.Add(this.wallSelectPanel);
			this.wallGroup.Controls.Add(this.buttonSecret);
			this.wallGroup.Controls.Add(this.destructableButton);
			this.wallGroup.Controls.Add(this.windowsButton);
			this.wallGroup.Location = new System.Drawing.Point(8, 16);
			this.wallGroup.Name = "wallGroup";
			this.wallGroup.Size = new System.Drawing.Size(112, 336);
			this.wallGroup.TabIndex = 23;
			this.wallGroup.TabStop = false;
			this.wallGroup.Text = "Walls";
			// 
			// wallSelectPanel
			// 
			this.wallSelectPanel.Location = new System.Drawing.Point(16, 16);
			this.wallSelectPanel.Name = "wallSelectPanel";
			this.wallSelectPanel.Size = new System.Drawing.Size(80, 240);
			this.wallSelectPanel.TabIndex = 19;
			// 
			// buttonSecret
			// 
			this.buttonSecret.Location = new System.Drawing.Point(24, 304);
			this.buttonSecret.Name = "buttonSecret";
			this.buttonSecret.Size = new System.Drawing.Size(56, 23);
			this.buttonSecret.TabIndex = 18;
			this.buttonSecret.Text = "Secret";
			this.buttonSecret.Click += new System.EventHandler(this.buttonSecret_Click);
			// 
			// destructableButton
			// 
			this.destructableButton.Location = new System.Drawing.Point(24, 280);
			this.destructableButton.Name = "destructableButton";
			this.destructableButton.Size = new System.Drawing.Size(56, 23);
			this.destructableButton.TabIndex = 7;
			this.destructableButton.Text = "Destruct";
			this.destructableButton.Click += new System.EventHandler(this.destructableButton_Click);
			// 
			// windowsButton
			// 
			this.windowsButton.Location = new System.Drawing.Point(24, 256);
			this.windowsButton.Name = "windowsButton";
			this.windowsButton.Size = new System.Drawing.Size(56, 23);
			this.windowsButton.TabIndex = 6;
			this.windowsButton.Text = "Windows";
			this.windowsButton.Click += new System.EventHandler(this.windowsButton_Click);
			// 
			// groupPolygons
			// 
			this.groupPolygons.Controls.Add(this.buttonPolygonDelete);
			this.groupPolygons.Controls.Add(this.buttonPolygonNew);
			this.groupPolygons.Controls.Add(this.buttonEditPolygon);
			this.groupPolygons.Controls.Add(this.buttonPoints);
			this.groupPolygons.Controls.Add(this.listPolygons);
			this.groupPolygons.Location = new System.Drawing.Point(128, 272);
			this.groupPolygons.Name = "groupPolygons";
			this.groupPolygons.Size = new System.Drawing.Size(112, 104);
			this.groupPolygons.TabIndex = 5;
			this.groupPolygons.TabStop = false;
			this.groupPolygons.Text = "Polygons";
			// 
			// buttonPolygonDelete
			// 
			this.buttonPolygonDelete.Location = new System.Drawing.Point(72, 72);
			this.buttonPolygonDelete.Name = "buttonPolygonDelete";
			this.buttonPolygonDelete.Size = new System.Drawing.Size(32, 24);
			this.buttonPolygonDelete.TabIndex = 25;
			this.buttonPolygonDelete.Text = "Del";
			this.buttonPolygonDelete.Click += new System.EventHandler(this.buttonPolygonDelete_Click);
			// 
			// buttonPolygonNew
			// 
			this.buttonPolygonNew.Location = new System.Drawing.Point(16, 72);
			this.buttonPolygonNew.Name = "buttonPolygonNew";
			this.buttonPolygonNew.Size = new System.Drawing.Size(40, 23);
			this.buttonPolygonNew.TabIndex = 24;
			this.buttonPolygonNew.Text = "New";
			this.buttonPolygonNew.Click += new System.EventHandler(this.buttonPolygonNew_Click);
			// 
			// buttonEditPolygon
			// 
			this.buttonEditPolygon.Location = new System.Drawing.Point(72, 48);
			this.buttonEditPolygon.Name = "buttonEditPolygon";
			this.buttonEditPolygon.Size = new System.Drawing.Size(32, 23);
			this.buttonEditPolygon.TabIndex = 6;
			this.buttonEditPolygon.Text = "Edit";
			this.buttonEditPolygon.Click += new System.EventHandler(this.buttonEditPolygon_Click);
			// 
			// buttonPoints
			// 
			this.buttonPoints.Enabled = false;
			this.buttonPoints.Location = new System.Drawing.Point(24, 24);
			this.buttonPoints.Name = "buttonPoints";
			this.buttonPoints.Size = new System.Drawing.Size(64, 23);
			this.buttonPoints.TabIndex = 5;
			this.buttonPoints.Text = "Points";
			// 
			// listPolygons
			// 
			this.listPolygons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.listPolygons.DropDownWidth = 120;
			this.listPolygons.Location = new System.Drawing.Point(8, 48);
			this.listPolygons.MaxDropDownItems = 10;
			this.listPolygons.Name = "listPolygons";
			this.listPolygons.Size = new System.Drawing.Size(64, 21);
			this.listPolygons.TabIndex = 23;
			this.listPolygons.Click += new System.EventHandler(this.listPolygons_Click);
			// 
			// mapPanel
			// 
			this.mapPanel.Location = new System.Drawing.Point(0, 0);
			this.mapPanel.Name = "mapPanel";
			this.mapPanel.Size = new System.Drawing.Size(5888, 5888);
			this.mapPanel.TabIndex = 5;
			this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseUp);
			this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
			this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseMove);
			this.mapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDown);
			// 
			// scrollPanel
			// 
			this.scrollPanel.AutoScroll = true;
			this.scrollPanel.Controls.Add(this.mapPanel);
			this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrollPanel.Location = new System.Drawing.Point(248, 0);
			this.scrollPanel.Name = "scrollPanel";
			this.scrollPanel.Size = new System.Drawing.Size(552, 578);
			this.scrollPanel.TabIndex = 6;
			// 
			// MapView
			// 
			this.Controls.Add(this.scrollPanel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusBar);
			this.Name = "MapView";
			this.Size = new System.Drawing.Size(800, 600);
			((System.ComponentModel.ISupportInitialize)(this.statusLocation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusWall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusTile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusObject)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.objectGroup.ResumeLayout(false);
			this.floorGroup.ResumeLayout(false);
			this.wallGroup.ResumeLayout(false);
			this.groupPolygons.ResumeLayout(false);
			this.scrollPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public MapView()
		{
			InitializeComponent();

			//form designer doesn't like my flickerfreepanel, so put initialization here instead
			/*mapPanel = new FlickerFreePanel();
			mapPanel.BorderStyle = BorderStyle.Fixed3D;
			mapPanel.ContextMenu = contextMenu1;
			mapPanel.Dock = DockStyle.Fill;
			mapPanel.Location = new Point(0, 0);
			mapPanel.Name = "mapPanel";
			mapPanel.Size = new Size(1024, 768);
			mapPanel.TabIndex = 0;
			mapPanel.MouseUp += new MouseEventHandler(this.mapPanel_MouseUp);
			mapPanel.Paint += new PaintEventHandler(this.mapPanel_Paint);
			mapPanel.MouseMove += new MouseEventHandler(this.mapPanel_MouseMove);
			mapPanel.MouseDown += new MouseEventHandler(this.mapPanel_MouseDown);
			Controls.Add(mapPanel);*/

			wallSelector1 = new WallSelector();
			wallSelectPanel.Controls.Add(wallSelector1);
			wallSelector1.Parent = this;

			tileGraphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			//set default values
			tileGraphic.SelectedIndex = 0;
			tileVar.SelectedIndex = 0;

			Map = new Map();//dummy map
			currentButton = selectButton;
			CurrentMode = Mode.SELECT;
		}

		private void ScrollBarChanged(object sender, System.EventArgs e)
		{
			mapPanel.Invalidate();
		}

		private void mapPanel_MouseDown(object sender, MouseEventArgs e)
		{
			Point pt = new Point(e.X, e.Y);
			Point tilePt = GetNearestTilePoint(pt);
			if (e.Button.Equals(MouseButtons.Left) && e.Clicks == 1)//if single left click
			{
				if (CurrentMode == Mode.SELECT)
				{
					//dragging = Map.SelectObject(pointClicked) == SelectedObject;//only start "dragging" if this object has already been selected
					if (SelectObject(pt) == SelectedObject)
						dragging = true;
					else
						SelectedObject = SelectObject(pt);
				}
				else if (CurrentMode == Mode.MAKE_WALL)
				{
					if(Map.WallMap[tilePt] == null)
					{
						Map.WallMap.Add(tilePt, wallSelector1.NewWall(tilePt));
					}
					else
					{
						Map.WallMap.Remove(tilePt);
					}
				}
				else if (CurrentMode == Mode.MAKE_OBJECT)
				{
					Map.Object obj = (NoxShared.Map.Object) DefaultObject.Clone();
					obj.Location = pt;
					obj.Extent = DefaultObject.Extent + 1;
					while(Map.Objects.extents.Contains(obj.Extent))
						obj.Extent++;
					Map.Objects.Add(obj);
				}
				else if (CurrentMode == Mode.MAKE_WINDOW)
				{
					Map.Wall wall = (Map.Wall)Map.WallMap[tilePt];
					if(wall != null)
						wall.Window = !wall.Window;
				}
				else if (CurrentMode == Mode.MAKE_DESTRUCTABLE)
				{
					Map.Wall wall = (Map.Wall)Map.WallMap[tilePt];
					if(wall != null)
						wall.Destructable = !wall.Destructable;
				}

				else if (CurrentMode == Mode.MAKE_SECRET)
				{
					Map.Wall wall = (Map.Wall)Map.WallMap[tilePt];
					if(wall != null)
						wall.Secret = !wall.Secret;
				}
				else if (CurrentMode == Mode.MAKE_FLOOR)
				{
					tilePt.Offset(0, -1);
					Map.Tile tile = (Map.Tile) Map.FloorMap[tilePt];
					if(tile == null && !threeFloorBox.Checked)
					{
						tile = new Map.Tile(
							tilePt,
							(byte) tileGraphic.SelectedIndex,
							GetVariation(tileVar),
							blendDialog.Blends
							);
						Map.FloorMap.Add(tilePt, tile);
					}
					else if(tile == null)
					{
						tile = new Map.Tile(
							new Point(tilePt.X-1,tilePt.Y-1),
							(byte) tileGraphic.SelectedIndex,
							3,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X-2,tilePt.Y),
							(byte) tileGraphic.SelectedIndex,
							6,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X-1,tilePt.Y+1),
							(byte) tileGraphic.SelectedIndex,
							7,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X,tilePt.Y-2),
							(byte) tileGraphic.SelectedIndex,
							0,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							tilePt,
							(byte) tileGraphic.SelectedIndex,
							4,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X,tilePt.Y+2),
							(byte) tileGraphic.SelectedIndex,
							8,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X+1,tilePt.Y-1),
							(byte) tileGraphic.SelectedIndex,
							1,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X+2,tilePt.Y),
							(byte) tileGraphic.SelectedIndex,
							2,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(tilePt.X+1,tilePt.Y+1),
							(byte) tileGraphic.SelectedIndex,
							5,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
					}
					else
					{
						Map.FloorMap.Remove(tilePt);
						if (threeFloorBox.Checked)
						{
							Map.FloorMap.Remove(new Point(tilePt.X - 1, tilePt.Y - 1));
							Map.FloorMap.Remove(new Point(tilePt.X - 2, tilePt.Y));
							Map.FloorMap.Remove(new Point(tilePt.X - 1, tilePt.Y + 1));
							Map.FloorMap.Remove(new Point(tilePt.X, tilePt.Y - 2));
							Map.FloorMap.Remove(new Point(tilePt.X, tilePt.Y + 2));
							Map.FloorMap.Remove(new Point(tilePt.X + 1, tilePt.Y - 1));
							Map.FloorMap.Remove(new Point(tilePt.X + 2, tilePt.Y));
							Map.FloorMap.Remove(new Point(tilePt.X + 1, tilePt.Y + 1));
						}
					}
				}
				mapPanel.Invalidate();
			}
		}

		private void mapPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point pt = GetNearestTilePoint(new Point(e.X, e.Y));
			Map.Wall wall = (Map.Wall) Map.WallMap[pt];
			Map.Tile tile = (Map.Tile) Map.FloorMap[new Point(pt.X, pt.Y - 1)];

			statusWall.Text = statusTile.Text = statusObject.Text = "";
			statusLocation.Text = String.Format("X={0} Y={1}", e.X, e.Y);

			if (wall != null)
				statusWall.Text = String.Format("{0}", wall.Material);

			if (tile != null)
			{
				statusTile.Text += String.Format("{0}-0x{1:x2}", tile.Graphic, tile.Variation);
				if (tile.EdgeTiles.Count > 0)
				{
					statusTile.Text += String.Format(" Edges({0}):", tile.EdgeTiles.Count);
					foreach (Map.Tile.EdgeTile edge in tile.EdgeTiles)
						statusTile.Text += String.Format(" {0}-0x{1:x2}-{2}-{3}", ThingDb.FloorTileNames[edge.Graphic], edge.Variation, edge.Dir, ThingDb.EdgeTileNames[edge.Edge]);
				}
			}

			if (SelectedObject != null)
				statusObject.Text = String.Format("{0}", SelectedObject.Name);
		}

		private void selectButton_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.SELECT;//set new mode
			currentButton = (Button) sender;//update current button
			mapPanel.Invalidate();
		}
		private void newObjectButton_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.MAKE_OBJECT;//set new mode
			currentButton = (Button) sender;//update current button
			mapPanel.Invalidate();
		}

		private void destructableButton_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.MAKE_DESTRUCTABLE;//set new mode
			currentButton = (Button) sender;//update current button	
			mapPanel.Invalidate();
		}

		private void windowsButton_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.MAKE_WINDOW;//set new mode
			currentButton = (Button) sender;//update current button	
			mapPanel.Invalidate();
		}

		private void buttonSecret_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.MAKE_SECRET;//set new mode
			currentButton = (Button) sender;//update current button	
			mapPanel.Invalidate();
		}

		private void floorButton_Click(object sender, System.EventArgs e)
		{
			CurrentMode = Mode.MAKE_FLOOR;//set new mode
			currentButton = (Button) sender;//update current button	
			mapPanel.Invalidate();
		}

		private void contextMenuDelete_Click(object sender, System.EventArgs e)
		{
			if(SelectedObject != null)
			{
				Map.Objects.Remove(SelectedObject);
				mapPanel.Invalidate();
			}
		}
		protected ObjectPropertiesDialog propDlg;
		protected ObjectEnchantDialog enchantDlg;
		protected DoorProperties doorDlg;
		private void contextMenuProperties_Click(object sender, EventArgs e)
		{
			propDlg = new ObjectPropertiesDialog();
			propDlg.Object = SelectedObject;
			if (propDlg.ShowDialog() == DialogResult.OK)//modifications will be effected when ok is pressed
				mapPanel.Invalidate();
		}

		private bool dragging;
		private void mapPanel_MouseUp(object sender, MouseEventArgs e)
		{
			Point pointClicked = new Point(e.X, e.Y);
			
			if(DrawGrid)
				pointClicked = new Point((int)Math.Round((decimal)(pointClicked.X/squareSize))*squareSize,(int)Math.Round((decimal)(pointClicked.Y/squareSize))*23);
			
			if (dragging && SelectedObject != null)
			{
				SelectedObject.Location = pointClicked;
				dragging = false;
			}

			mapPanel.Invalidate();
		}

		public Map.Object SelectObject(Point pt)
		{
			double closestDistance = Double.MaxValue;
			Map.Object closest = null;

			foreach (Map.Object obj in Map.Objects)
			{
				double distance = Math.Sqrt(Math.Pow(pt.X - obj.Location.X, 2) + Math.Pow(pt.Y - obj.Location.Y, 2));

				if (distance < (double) objectSelectionRadius && distance < closestDistance)
				{
					closestDistance = distance;
					closest = obj;
				}
			}

			return closest;
		}

		private void checkboxGrid_CheckedChanged(object sender, System.EventArgs e)
		{
			mapPanel.Invalidate();
		}

		protected BlendDialog blendDialog = new BlendDialog();
		private void buttonBlend_Click(object sender, System.EventArgs e)
		{
			blendDialog.ShowDialog();
		}

		private byte GetVariation(ComboBox box)
		{
			return box.SelectedIndex == 0 ? (byte) new Random().Next(((ThingDb.Tile) ThingDb.FloorTiles[box.SelectedIndex]).Variations) : Convert.ToByte(box.Text);
		}

		private void repopulateVariations(ComboBox box, int variations)
		{
			int oldNdx = box.SelectedIndex;
			box.Items.Clear();
			box.Items.Add("Random");
			for (int i = 0; i < variations; i++)
				box.Items.Add(String.Format("{0}", i));
			if (oldNdx < box.Items.Count)
				box.SelectedIndex = oldNdx;
		}

		private void tileGraphic_SelectedIndexChanged(object sender, EventArgs e)
		{
			repopulateVariations(tileVar, ((ThingDb.Tile) ThingDb.FloorTiles[((ComboBox) sender).SelectedIndex]).Variations);
		}

		private void defaultButt_Click(object sender, System.EventArgs e)
		{
			propDlg = new ObjectPropertiesDialog();
			propDlg.Object = DefaultObject;
			propDlg.ShowDialog();
		}


		private void buttonPolygonNew_Click(object sender, System.EventArgs e)
		{
			polyDlg.Polygon = null;
			if (polyDlg.ShowDialog() == DialogResult.OK && polyDlg.Polygon != null)
			{
				Map.Polygons.Add(polyDlg.Polygon);
				listPolygons.Items.Add(polyDlg.Polygon.Name);
				mapPanel.Invalidate();
			}
		}

		private void buttonEditPolygon_Click(object sender, System.EventArgs e)
		{
			polyDlg.Polygon = (Map.Polygon) Map.Polygons[listPolygons.SelectedIndex];
			if (polyDlg.ShowDialog() == DialogResult.OK && polyDlg.Polygon != null)
			{
				Map.Polygons.RemoveAt(listPolygons.SelectedIndex);
				Map.Polygons.Insert(listPolygons.SelectedIndex, polyDlg.Polygon);
				mapPanel.Invalidate();
			}
		}

		private void buttonPolygonDelete_Click(object sender, System.EventArgs e)
		{
			Map.Polygons.RemoveAt(listPolygons.SelectedIndex);
			mapPanel.Invalidate();
		}

		private void listPolygons_Click(object sender, System.EventArgs e)
		{
			listPolygons.Items.Clear();
			foreach (Map.Polygon poly in Map.Polygons)
				listPolygons.Items.Add(poly.Name);
		}

		//FIXME: use e.ClipRectangle to limit drawing
		private void mapPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (Map == null)
				return;

			Graphics g = e.Graphics;

			//black out the panel to start out
			Size size = mapPanel.Size;
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), size));

			//draw grid
			if (DrawGrid)
			{
				Pen pen = new Pen(Color.Gray, gridThickness);
				//draw the grid sloppily (an extra screen's worth of lines along either axis)
				for (int x = -squareSize*(size.Width/squareSize) - squareSize/2 % (2*squareSize); x < 2*size.Width; x += 2*squareSize)
				{
					int y = -3*squareSize/2 % (2*squareSize);
					g.DrawLine(pen, new Point(x, y), new Point(y, x));
					g.DrawLine(pen, new Point(x, y), new Point(size.Width + x, size.Width + y));
				}
			}

			//draw floor
			if (CurrentMode == Mode.MAKE_FLOOR)//only draw the floor when editing it
			{
				foreach (Map.Tile tile in Map.FloorMap.Values)
				{
					Pen pen = new Pen(Color.Yellow, 1);
					int x = tile.Location.X * squareSize;
					int y = tile.Location.Y * squareSize;
					if (x >= e.ClipRectangle.Left && x < e.ClipRectangle.Right
						&& y >= e.ClipRectangle.Top && y < e.ClipRectangle.Bottom)	
					{
						PointF nwCorner, neCorner, seCorner, swCorner, center;
						center = new PointF(x + squareSize/2f, y + (3/2f)*squareSize);
						nwCorner = new PointF(x - squareSize/2f, y + (3/2f)*squareSize);
						neCorner = new PointF(nwCorner.X + squareSize, nwCorner.Y - squareSize);
						swCorner = new PointF(nwCorner.X + squareSize, nwCorner.Y + squareSize);
						seCorner = new PointF(neCorner.X + squareSize, neCorner.Y + squareSize);
						g.DrawPolygon(pen, new PointF[] {nwCorner, neCorner, seCorner, swCorner});
						g.DrawEllipse(pen, center.X, center.Y, 2, 2);
					}
				}
			}

			//draw walls
			foreach (Map.Wall wall in Map.WallMap.Values)
			{
				Point pt = wall.Location;
				Pen pen;
				int x = pt.X * squareSize, y = pt.Y * squareSize;
				if (wall.Destructable)
					pen = new Pen(Color.Red, wallThickness);
				else if (wall.Window)
					pen = new Pen(Color.Orange, wallThickness);
				else if (wall.Secret)
					pen = new Pen(Color.Green, wallThickness);
				else
					pen = new Pen(Color.White, wallThickness);
				PointF center = new PointF(x + squareSize/2f, y + squareSize/2f);
				Point nCorner = new Point(x, y);
				Point sCorner = new Point(x + squareSize, y + squareSize);
				Point wCorner = new Point(x + squareSize, y);
				Point eCorner = new Point(x, y + squareSize);
				switch (wall.Facing)
				{
					case Map.Wall.WallFacing.NORTH:
						g.DrawLine(pen, wCorner, eCorner);
						break;
					case Map.Wall.WallFacing.WEST:
						g.DrawLine(pen, nCorner, sCorner);
						break;
					case Map.Wall.WallFacing.CROSS:
						g.DrawLine(pen, wCorner, eCorner);//north wall
						g.DrawLine(pen, nCorner, sCorner);//south wall
						break;
					case Map.Wall.WallFacing.NORTH_T:
						g.DrawLine(pen, wCorner, eCorner);//north wall
						g.DrawLine(pen, center, sCorner);//tail towards south
						break;
					case Map.Wall.WallFacing.SOUTH_T:
						g.DrawLine(pen, wCorner, eCorner);//north wall
						g.DrawLine(pen, center, nCorner);//tail towards north
						break;
					case Map.Wall.WallFacing.WEST_T:
						g.DrawLine(pen, nCorner, sCorner);//west wall
						g.DrawLine(pen, center, eCorner);//tail towards east
						break;
					case Map.Wall.WallFacing.EAST_T:
						g.DrawLine(pen, nCorner, sCorner);//west wall
						g.DrawLine(pen, center, wCorner);//tail towards west
						break;
					case Map.Wall.WallFacing.NE_CORNER:
						g.DrawLine(pen, center, eCorner);
						g.DrawLine(pen, center, sCorner);
						break;
					case Map.Wall.WallFacing.NW_CORNER:
						g.DrawLine(pen, center, wCorner);
						g.DrawLine(pen, center, sCorner);
						break;
					case Map.Wall.WallFacing.SW_CORNER:
						g.DrawLine(pen, center, nCorner);
						g.DrawLine(pen, center, wCorner);
						break;
					case Map.Wall.WallFacing.SE_CORNER:
						g.DrawLine(pen, center, nCorner);
						g.DrawLine(pen, center, eCorner);
						break;
					default:
						g.DrawRectangle(pen, x, y, squareSize, squareSize);
						g.DrawString("?", new Font("Arial", 12), new SolidBrush(Color.Red), nCorner);
						break;
				}

				g.DrawString(wall.Minimap.ToString(), new Font("Arial", 10), new SolidBrush(Color.Red), x, y);
			}

			//draw objects
			foreach (Map.Object oe in Map.Objects)
			{
				PointF ptf = oe.Location;
				Pen pen;
				float x = ptf.X, y = ptf.Y;
				
				PointF center = new PointF(x, y);
				PointF topLeft = new PointF(center.X - objectSelectionRadius, center.Y - objectSelectionRadius);
				if (SelectedObject != null && ((Map.Object) SelectedObject).Location.Equals(oe.Location))
					pen = new Pen(Color.Green, 1);
				else
					pen = new Pen(Color.Blue, 1);
				g.DrawEllipse(pen, new RectangleF(topLeft, new Size(2*objectSelectionRadius, 2*objectSelectionRadius)));
				g.DrawString(oe.Extent.ToString(),new Font("Arial", 10), new SolidBrush(Color.Purple), topLeft);
			}

			//draw polygons
			foreach (Map.Polygon poly in Map.Polygons)
			{
				Pen pen = new Pen(Color.PaleGreen, 1);
				ArrayList points = new ArrayList();
				foreach (PointF pt in poly.Points)
					points.Add(new PointF(pt.X, pt.Y));
				points.Add(points[0]);//complete the loop
				g.DrawLines(pen, (PointF[]) points.ToArray(typeof(PointF)));
			}
		}

		private Point GetNearestTilePoint(Point pt)
		{
			//pt.Offset(0, -squareSize);
			Point tl = new Point((pt.X/squareSize)*squareSize, (pt.Y/squareSize)*squareSize);
			if (tl.X/squareSize % 2 == tl.Y/squareSize % 2)
				return new Point(tl.X/squareSize, tl.Y/squareSize);
			else
			{
				Point left = new Point(tl.X, tl.Y + squareSize/2);
				Point right = new Point(tl.X + squareSize, tl.Y + squareSize/2);
				Point top = new Point(tl.X + squareSize/2, tl.Y);
				Point bottom = new Point(tl.X + squareSize/2, tl.Y + squareSize);
				Point closest = left;
				foreach (Point point in new Point[] {left, right, top, bottom})
					if (Distance(point, pt) < Distance(closest, pt))
						closest = point;

				if (closest == left)
					return new Point(tl.X/squareSize - 1, tl.Y/squareSize);
				else if (closest == right)
					return new Point(tl.X/squareSize + 1, tl.Y/squareSize);
				else if (closest == top)
					return new Point(tl.X/squareSize, tl.Y/squareSize - 1);
				else
					return new Point(tl.X/squareSize, tl.Y/squareSize + 1);

				/*
				Point tr = new Point(tl.X + squareSize, tl.Y);
				Point bl = new Point(tl.X, tl.Y + squareSize);
				if (Math.Sqrt(Math.Pow(pt.X - tr.X, 2) + Math.Pow(pt.Y - tr.Y, 2))
					< Math.Sqrt(Math.Pow(pt.X - bl.X, 2) + Math.Pow(pt.Y - bl.Y, 2)))
					return new Point(tr.X/squareSize, tr.Y/squareSize);
				else
					return new Point(bl.X/squareSize, bl.Y/squareSize);*/
			}
		}

		private int Distance(Point a, Point b)
		{
			return (int) Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
		}
	}
}
