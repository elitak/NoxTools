using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using NoxShared;

namespace NoxMapEditor
{
	/// <summary>
	/// Summary description for MapView.
	/// </summary>
	public class MapView : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected static int squareSize = 23;
		protected int objectSelectionRadius = 7;
		protected Button currentButton;
		public Map.Object SelectedObject = new Map.Object();

		public enum Mode
		{
			MAKE_WALL,
			MAKE_OBJECT,
			SELECT,			MAKE_WINDOW,			MAKE_DESTRUCTABLE,			MAKE_FLOOR
		};
		public Mode CurrentMode;
		private FlickerFreePanel mapPanel;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Button newObjectButton;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem contextMenuDelete;
		private System.Windows.Forms.MenuItem contextMenuProperties;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Panel panel2;
		private NoxMapEditor.WallSelector wallSelector1;
		private System.Windows.Forms.Button windowsButton;
		private System.Windows.Forms.Button destructableButton;
		private System.Windows.Forms.Button floorButton;
		private System.Windows.Forms.ComboBox lTileGraphic;
		private System.Windows.Forms.ComboBox rTileGraphic;
		private System.Windows.Forms.TextBox lTileVar;
		private System.Windows.Forms.TextBox rTileVar;

		public Map Map;

		public MapView()
		{
			InitializeComponent();
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			//SetStyle(ControlStyles.DoubleBuffer, true);
			wallSelector1.Parent = this;

			hScrollBar1.Value = (hScrollBar1.Maximum - hScrollBar1.Minimum) / 2;
			vScrollBar1.Value = (vScrollBar1.Maximum - vScrollBar1.Minimum) / 2;

			lTileGraphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			rTileGraphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			//set default values
			lTileGraphic.SelectedIndex = rTileGraphic.SelectedIndex = 0;
			lTileVar.Text = rTileVar.Text = "0";

			Map = new Map();//dummy map
			currentButton = selectButton;
			CurrentMode = Mode.SELECT;
		}

		private void mapPanel_Paint(object sender, PaintEventArgs e)
		{
			if (Map == null)
				return;

			Size size = mapPanel.Size;
			e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), size));			if (CurrentMode == Mode.MAKE_FLOOR)//only draw the floor when editing it
			{
				foreach (Map.TilePair tilePair in Map.FloorMap.Values)				{					Point pt = tilePair.Location;					Pen pen = new Pen(Color.Yellow, 1);					PointF nwCorner, neCorner, seCorner, swCorner, center = new PointF(0, 0);					int x = pt.X * squareSize * 2 - hScrollBar1.Value;					int y = pt.Y * squareSize * 2 - vScrollBar1.Value;					if (x >= 0 && x < size.Width && y >= 0 && y < size.Height)						{						center = new PointF(x + squareSize/2f, y + (3/2f)*squareSize);						nwCorner = new PointF(x - squareSize/2f, y + (3/2f)*squareSize);						neCorner = new PointF(nwCorner.X + 2*squareSize, nwCorner.Y - 2*squareSize);						if(tilePair.OneTileOnly)						{							if (tilePair.Left != null)							{								neCorner.X += -squareSize;								neCorner.Y += squareSize;							}							else							{								center.X += squareSize;								center.Y += -squareSize;								nwCorner.X += squareSize;								nwCorner.Y += -squareSize;							}							}						swCorner = new PointF(nwCorner.X + squareSize, nwCorner.Y + squareSize);						seCorner = new PointF(neCorner.X + squareSize, neCorner.Y + squareSize);						e.Graphics.DrawPolygon(pen, new PointF[] {nwCorner, neCorner, seCorner, swCorner});						e.Graphics.DrawEllipse(pen, center.X, center.Y, 2, 2);					}				}			}
			foreach (Map.Wall wall in Map.WallMap.Values)
			{				Point pt = wall.Location;				if (pt.X*squareSize >= hScrollBar1.Value && pt.X*squareSize < size.Width+hScrollBar1.Value && pt.Y*squareSize >= vScrollBar1.Value && pt.Y*squareSize < size.Height+vScrollBar1.Value)					{					Pen pen;					int x = pt.X * squareSize - hScrollBar1.Value, y = pt.Y * squareSize - vScrollBar1.Value;					if (wall.Destructable)						pen = new Pen(Color.Red, 1);										else if (wall.Window)						pen = new Pen(Color.Orange, 1);										else						pen = new Pen(Color.White, 1);					PointF center = new PointF(x + squareSize/2f, y + squareSize/2f);					Point nCorner = new Point(x, y);					Point sCorner = new Point(x + squareSize, y + squareSize);					Point wCorner = new Point(x + squareSize, y);					Point eCorner = new Point(x, y + squareSize);					switch ((Map.Wall.WallFacing) ((int) wall.Facing & 0x7F))//FIXME, TODO: sign bit seems to signify "internal" wall (not on the border of the map), however, there are seemingly random exceptions to this					{						case Map.Wall.WallFacing.NORTH:							e.Graphics.DrawLine(pen, wCorner, eCorner);							break;						case Map.Wall.WallFacing.WEST:							e.Graphics.DrawLine(pen, nCorner, sCorner);							break;
						case Map.Wall.WallFacing.CROSS:							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall							e.Graphics.DrawLine(pen, nCorner, sCorner);//south wall							break;
						case Map.Wall.WallFacing.NORTH_T:							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall							e.Graphics.DrawLine(pen, center, sCorner);//tail towards south							break;
						case Map.Wall.WallFacing.SOUTH_T:							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall							e.Graphics.DrawLine(pen, center, nCorner);//tail towards north							break;
						case Map.Wall.WallFacing.WEST_T:							e.Graphics.DrawLine(pen, nCorner, sCorner);//west wall							e.Graphics.DrawLine(pen, center, eCorner);//tail towards east							break;
						case Map.Wall.WallFacing.EAST_T:							e.Graphics.DrawLine(pen, nCorner, sCorner);//west wall							e.Graphics.DrawLine(pen, center, wCorner);//tail towards west							break;
						case Map.Wall.WallFacing.NE_CORNER:							e.Graphics.DrawLine(pen, center, eCorner);							e.Graphics.DrawLine(pen, center, sCorner);							break;
						case Map.Wall.WallFacing.NW_CORNER:							e.Graphics.DrawLine(pen, center, wCorner);							e.Graphics.DrawLine(pen, center, sCorner);							break;
						case Map.Wall.WallFacing.SW_CORNER:							e.Graphics.DrawLine(pen, center, nCorner);							e.Graphics.DrawLine(pen, center, wCorner);							break;
						case Map.Wall.WallFacing.SE_CORNER:							e.Graphics.DrawLine(pen, center, nCorner);							e.Graphics.DrawLine(pen, center, eCorner);							break;
						default:							e.Graphics.DrawRectangle(pen, x, y, squareSize, squareSize);							e.Graphics.DrawString("?", new Font("Arial", 12), new SolidBrush(Color.Red), nCorner);							break;					}				}			}
			foreach (Map.Object oe in Map.Objects)			{				PointF ptf = oe.Location;				if (ptf.X >= hScrollBar1.Value && ptf.X < size.Width+hScrollBar1.Value && ptf.Y >= vScrollBar1.Value && ptf.Y < size.Height+vScrollBar1.Value)					{					Pen pen;					float x = ptf.X - hScrollBar1.Value, y = ptf.Y - vScrollBar1.Value;										PointF center = new PointF(x, y);					PointF topLeft = new PointF(center.X - objectSelectionRadius, center.Y - objectSelectionRadius);					if (SelectedObject != null && ((Map.Object) SelectedObject).Location.Equals(oe.Location))						pen = new Pen(Color.Green, 1);
					else						pen = new Pen(Color.Blue, 1);
					e.Graphics.DrawEllipse(pen, new RectangleF(topLeft, new Size(2*objectSelectionRadius, 2*objectSelectionRadius)));					e.Graphics.DrawString(oe.Extent.ToString(),new Font("Arial", 10), new SolidBrush(Color.Purple), topLeft);				}			}		} 
		private void ScrollBarChanged(object sender, System.EventArgs e)
		{
			mapPanel.Invalidate();
		}

		private void mapPanel_MouseDown(object sender, MouseEventArgs e)
		{
			Point pointClicked = new Point(hScrollBar1.Value + e.X, vScrollBar1.Value + e.Y);
			if (e.Button.Equals(MouseButtons.Left) && e.Clicks == 1)//if single left click
			{
				if (CurrentMode == Mode.SELECT)
				{
					//dragging = Map.SelectObject(pointClicked) == SelectedObject;//only start "dragging" if this object has already been selected
					if (SelectObject(pointClicked) == SelectedObject)
						dragging = true;
					else
						SelectedObject = SelectObject(pointClicked);
				}
				else if (CurrentMode == Mode.MAKE_WALL)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					if(Map.WallMap[pt] == null)
					{
						Map.Wall newWall = new Map.Wall(pt, wallSelector1.SelectedFacing, wallSelector1.SelectedMaterial);
						Map.WallMap.Add(pt,newWall);
					}
					else
					{
						Map.WallMap.Remove(pt);
					}
				}
				else if (CurrentMode == Mode.MAKE_OBJECT)
				{
					Map.Object obj = new Map.Object();
					obj.Location = pointClicked;
					Map.Objects.Add(obj);
				}
				else if (CurrentMode == Mode.MAKE_WINDOW)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					Map.Wall wall = (Map.Wall)Map.WallMap[pt];
					if(wall != null)
					{
						wall.Window = !wall.Window;
						if(wall.Window)
							Map.num_Windows++;
						else
							Map.num_Windows--;
					}
				}
				else if (CurrentMode == Mode.MAKE_DESTRUCTABLE)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					Map.Wall wall = (Map.Wall)Map.WallMap[pt];
					if(wall != null)
					{
						wall.Destructable = !wall.Destructable;
						if(wall.Destructable)
							Map.num_Destructable++;
						else
							Map.num_Destructable--;
					}
				}
				else if (CurrentMode == Mode.MAKE_FLOOR)//TODO: allow addition of single tile tilepairs
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/(squareSize*2),(e.Y+vScrollBar1.Value)/(squareSize*2));
					Map.TilePair tilePair = (Map.TilePair)Map.FloorMap[pt];
					if(tilePair == null)
					{
						tilePair = new Map.TilePair(
							(byte)pt.X,
							(byte)pt.Y,
							new Map.TilePair.Tile((byte) lTileGraphic.SelectedIndex, Byte.Parse(lTileVar.Text,System.Globalization.NumberStyles.HexNumber)),
							new Map.TilePair.Tile((byte) rTileGraphic.SelectedIndex, Byte.Parse(rTileVar.Text,System.Globalization.NumberStyles.HexNumber))
							);
						Map.FloorMap.Add(pt, tilePair);
					}
					else
					{
						Map.FloorMap.Remove(pt);
					}
				}
				mapPanel.Invalidate();
			}
		}

		private void mapPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Map.Wall wall = (Map.Wall)Map.WallMap[new Point((e.X+hScrollBar1.Value)/23,(e.Y+vScrollBar1.Value)/23)];
			Map.TilePair tilePair = (Map.TilePair)Map.FloorMap[new Point((e.X+hScrollBar1.Value)/(squareSize*2),(e.Y+vScrollBar1.Value)/(squareSize*2))];

			statusBar1.Text = "";
			statusBar1.Text += String.Format("X={0} Y={1}", e.X + hScrollBar1.Value, e.Y + vScrollBar1.Value);

			if (wall != null)
				statusBar1.Text += String.Format("  Wall Material=\"{0}\"", wall.Material);
			if (tilePair != null && CurrentMode == Mode.MAKE_FLOOR)
			{
				//TODO: show the info on the single tile that is selected, not both in its corresponding pair
				if (tilePair.Left != null)
				{
					Map.TilePair.Tile tile = tilePair.Left;
					statusBar1.Text += "  LEFT:";
					statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}", tile.Graphic, tile.Variation);
					if (tile.EdgeTiles.Count > 0)
					{
						statusBar1.Text += String.Format(" EdgeTiles({0}):", tile.EdgeTiles.Count);
						foreach (Map.TilePair.Tile.EdgeTile eTile in tile.EdgeTiles)
							statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}-{2}", eTile.Graphic, eTile.Variation, eTile.Direction);
					}
				}
				if (tilePair.Right != null)
				{
					Map.TilePair.Tile tile = tilePair.Right;
					statusBar1.Text += "  RIGHT:";
					statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}", tile.Graphic, tile.Variation);
					if (tile.EdgeTiles.Count > 0)
					{
						statusBar1.Text += String.Format(" EdgeTiles({0}):", tile.EdgeTiles.Count);
						foreach (Map.TilePair.Tile.EdgeTile eTile in tile.EdgeTiles)
							statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}-{2}", eTile.Graphic, eTile.Variation, eTile.Direction);
					}
				}
			}
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
		protected ObjectPropertiesDialog propDlg = new ObjectPropertiesDialog();
		private void contextMenuProperties_Click(object sender, EventArgs e)
		{
			propDlg.Object = SelectedObject;
			propDlg.ShowDialog();
		}

		private bool dragging;
		private void mapPanel_MouseUp(object sender, MouseEventArgs e)
		{
			Point pointClicked = new Point(hScrollBar1.Value+e.X, vScrollBar1.Value+e.Y);
			if (dragging && SelectedObject != null)
			{
				SelectedObject.Location = pointClicked;
				dragging = false;
				
				/*if (SelectedObject != null)
					SelectedObject = null;*/
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

		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{			this.mapPanel = new FlickerFreePanel();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.contextMenuDelete = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.contextMenuProperties = new System.Windows.Forms.MenuItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rTileVar = new System.Windows.Forms.TextBox();
			this.lTileVar = new System.Windows.Forms.TextBox();
			this.rTileGraphic = new System.Windows.Forms.ComboBox();
			this.lTileGraphic = new System.Windows.Forms.ComboBox();
			this.floorButton = new System.Windows.Forms.Button();
			this.destructableButton = new System.Windows.Forms.Button();
			this.windowsButton = new System.Windows.Forms.Button();
			this.selectButton = new System.Windows.Forms.Button();
			this.newObjectButton = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.wallSelector1 = new NoxMapEditor.WallSelector();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mapPanel
			// 
			this.mapPanel.ContextMenu = this.contextMenu1;
			this.mapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapPanel.Location = new System.Drawing.Point(0, 0);
			this.mapPanel.Name = "mapPanel";
			this.mapPanel.Size = new System.Drawing.Size(1024, 768);
			this.mapPanel.TabIndex = 0;
			this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseUp);
			this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
			this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseMove);
			this.mapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDown);
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
			// statusBar1
			// 
			this.statusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.statusBar1.Location = new System.Drawing.Point(0, 746);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(1024, 22);
			this.statusBar1.SizingGrip = false;
			this.statusBar1.TabIndex = 1;
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hScrollBar1.LargeChange = 230;
			this.hScrollBar1.Location = new System.Drawing.Point(88, 730);
			this.hScrollBar1.Maximum = 5888;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(920, 16);
			this.hScrollBar1.SmallChange = 23;
			this.hScrollBar1.TabIndex = 2;
			this.hScrollBar1.ValueChanged += new System.EventHandler(this.ScrollBarChanged);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.LargeChange = 230;
			this.vScrollBar1.Location = new System.Drawing.Point(1008, 0);
			this.vScrollBar1.Maximum = 5888;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(16, 746);
			this.vScrollBar1.SmallChange = 23;
			this.vScrollBar1.TabIndex = 3;
			this.vScrollBar1.ValueChanged += new System.EventHandler(this.ScrollBarChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rTileVar);
			this.groupBox1.Controls.Add(this.lTileVar);
			this.groupBox1.Controls.Add(this.rTileGraphic);
			this.groupBox1.Controls.Add(this.lTileGraphic);
			this.groupBox1.Controls.Add(this.floorButton);
			this.groupBox1.Controls.Add(this.destructableButton);
			this.groupBox1.Controls.Add(this.windowsButton);
			this.groupBox1.Controls.Add(this.selectButton);
			this.groupBox1.Controls.Add(this.newObjectButton);
			this.groupBox1.Controls.Add(this.panel2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(88, 746);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tools";
			// 
			// rTileVar
			// 
			this.rTileVar.Location = new System.Drawing.Point(48, 448);
			this.rTileVar.Name = "rTileVar";
			this.rTileVar.Size = new System.Drawing.Size(24, 20);
			this.rTileVar.TabIndex = 17;
			this.rTileVar.Text = "";
			// 
			// lTileVar
			// 
			this.lTileVar.Location = new System.Drawing.Point(48, 400);
			this.lTileVar.Name = "lTileVar";
			this.lTileVar.Size = new System.Drawing.Size(24, 20);
			this.lTileVar.TabIndex = 16;
			this.lTileVar.Text = "";
			// 
			// rTileGraphic
			// 
			this.rTileGraphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.rTileGraphic.DropDownWidth = 200;
			this.rTileGraphic.Location = new System.Drawing.Point(8, 424);
			this.rTileGraphic.Name = "rTileGraphic";
			this.rTileGraphic.Size = new System.Drawing.Size(72, 21);
			this.rTileGraphic.TabIndex = 15;
			// 
			// lTileGraphic
			// 
			this.lTileGraphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lTileGraphic.DropDownWidth = 200;
			this.lTileGraphic.Location = new System.Drawing.Point(8, 376);
			this.lTileGraphic.Name = "lTileGraphic";
			this.lTileGraphic.Size = new System.Drawing.Size(72, 21);
			this.lTileGraphic.TabIndex = 13;
			// 
			// floorButton
			// 
			this.floorButton.Location = new System.Drawing.Point(8, 344);
			this.floorButton.Name = "floorButton";
			this.floorButton.Size = new System.Drawing.Size(72, 23);
			this.floorButton.TabIndex = 8;
			this.floorButton.Text = "Floor";
			this.floorButton.Click += new System.EventHandler(this.floorButton_Click);
			// 
			// destructableButton
			// 
			this.destructableButton.Location = new System.Drawing.Point(8, 312);
			this.destructableButton.Name = "destructableButton";
			this.destructableButton.Size = new System.Drawing.Size(72, 23);
			this.destructableButton.TabIndex = 7;
			this.destructableButton.Text = "Destruct";
			this.destructableButton.Click += new System.EventHandler(this.destructableButton_Click);
			// 
			// windowsButton
			// 
			this.windowsButton.Location = new System.Drawing.Point(8, 280);
			this.windowsButton.Name = "windowsButton";
			this.windowsButton.Size = new System.Drawing.Size(72, 23);
			this.windowsButton.TabIndex = 6;
			this.windowsButton.Text = "Windows";
			this.windowsButton.Click += new System.EventHandler(this.windowsButton_Click);
			// 
			// selectButton
			// 
			this.selectButton.Location = new System.Drawing.Point(8, 248);
			this.selectButton.Name = "selectButton";
			this.selectButton.Size = new System.Drawing.Size(72, 23);
			this.selectButton.TabIndex = 0;
			this.selectButton.Text = "Sel./Move";
			this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
			// 
			// newObjectButton
			// 
			this.newObjectButton.Location = new System.Drawing.Point(8, 216);
			this.newObjectButton.Name = "newObjectButton";
			this.newObjectButton.Size = new System.Drawing.Size(72, 23);
			this.newObjectButton.TabIndex = 1;
			this.newObjectButton.Text = "New Object";
			this.newObjectButton.Click += new System.EventHandler(this.newObjectButton_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.wallSelector1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(82, 192);
			this.panel2.TabIndex = 5;
			// 
			// wallSelector1
			// 
			this.wallSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wallSelector1.Location = new System.Drawing.Point(0, 0);
			this.wallSelector1.Name = "wallSelector1";
			this.wallSelector1.Size = new System.Drawing.Size(82, 192);
			this.wallSelector1.TabIndex = 0;
			// 
			// MapView
			// 
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.mapPanel);
			this.Name = "MapView";
			this.Size = new System.Drawing.Size(1024, 768);
			this.groupBox1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
