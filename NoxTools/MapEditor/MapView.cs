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

		protected class FlickerFreePanel : Panel
		{
			public FlickerFreePanel() : base()
			{
				SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			}
		}

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
		private NoxMapEditor.WallSelector wallSelector1;
		private System.Windows.Forms.Button windowsButton;
		private System.Windows.Forms.Button destructableButton;
		private System.Windows.Forms.Button floorButton;
		private System.Windows.Forms.ComboBox tileGraphic;
		private System.Windows.Forms.Button buttonSecret;
		private NoxMapEditor.MapView.FlickerFreePanel mapPanel;
		private System.Windows.Forms.Button buttonBlend;
		private System.Windows.Forms.ComboBox tileVar;
		private System.Windows.Forms.GroupBox floorGroup;
		private System.Windows.Forms.GroupBox wallGroup;
		private System.Windows.Forms.GroupBox objectGroup;
		private System.Windows.Forms.CheckBox threeFloorBox;
		private System.Windows.Forms.Button defaultButt;
		private System.Windows.Forms.Panel wallSelectPanel;

		public MapView()
		{
			InitializeComponent();

			//form designer doesn't like my flickerfreepanel, so put initialization here instead
			mapPanel = new FlickerFreePanel();
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
			Controls.Add(mapPanel);

			wallSelector1 = new WallSelector();
			wallSelectPanel.Controls.Add(wallSelector1);
			wallSelector1.Parent = this;

			hScrollBar1.Value = (hScrollBar1.Maximum - hScrollBar1.Minimum) / 2;
			vScrollBar1.Value = (vScrollBar1.Maximum - vScrollBar1.Minimum) / 2;

			tileGraphic.Items.AddRange(ThingDb.FloorTileNames.ToArray());
			//set default values
			tileGraphic.SelectedIndex = 0;
			tileVar.SelectedIndex = 0;

			Map = new Map();//dummy map
			currentButton = selectButton;
			CurrentMode = Mode.SELECT;
		}

		private void mapPanel_Paint(object sender, PaintEventArgs e)
		{
			if (Map == null)
				return;

			//black out the panel to start out
			Size size = mapPanel.Size;
			e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), size));
			Point pos = new Point(hScrollBar1.Value, vScrollBar1.Value);
			//draw grid
			if (DrawGrid)
			{
				Pen pen = new Pen(Color.Gray, gridThickness);
				//draw the grid sloppily (an extra screen's worth of lines along either axis)
				for (int x = -squareSize*(size.Width/squareSize) - squareSize/2 - pos.X % (2*squareSize); x < 2*size.Width; x += 2*squareSize)
				{
					int y = -3*squareSize/2 - pos.Y % (2*squareSize);
					e.Graphics.DrawLine(pen, new Point(x, y), new Point(y, x));
					e.Graphics.DrawLine(pen, new Point(x, y), new Point(size.Width + x, size.Width + y));
				}
			}
			if (CurrentMode == Mode.MAKE_FLOOR)//only draw the floor when editing it
			{
				//draw floor
				foreach (Map.Tile tile in Map.FloorMap.Values)
				{
					Pen pen = new Pen(Color.Yellow, 1);
					int x = tile.Location.X * squareSize - hScrollBar1.Value;
					int y = tile.Location.Y * squareSize - vScrollBar1.Value;
					if (x >= 0 && x < size.Width && y >= 0 && y < size.Height)	
					{
						PointF nwCorner, neCorner, seCorner, swCorner, center;
						center = new PointF(x + squareSize/2f, y + (3/2f)*squareSize);
						nwCorner = new PointF(x - squareSize/2f, y + (3/2f)*squareSize);
						neCorner = new PointF(nwCorner.X + squareSize, nwCorner.Y - squareSize);
						swCorner = new PointF(nwCorner.X + squareSize, nwCorner.Y + squareSize);
						seCorner = new PointF(neCorner.X + squareSize, neCorner.Y + squareSize);
						e.Graphics.DrawPolygon(pen, new PointF[] {nwCorner, neCorner, seCorner, swCorner});
						e.Graphics.DrawEllipse(pen, center.X, center.Y, 2, 2);
					}
				}
			}

			//draw walls
			foreach (Map.Wall wall in Map.WallMap.Values)
			{
				Point pt = wall.Location;
				if (pt.X*squareSize >= hScrollBar1.Value && pt.X*squareSize < size.Width+hScrollBar1.Value && pt.Y*squareSize >= vScrollBar1.Value && pt.Y*squareSize < size.Height+vScrollBar1.Value)	
				{
					Pen pen;
					int x = pt.X * squareSize - hScrollBar1.Value, y = pt.Y * squareSize - vScrollBar1.Value;
					if (wall.Destructable)
						pen = new Pen(Color.Red, wallThickness);
					else if (wall.Window)
						pen = new Pen(Color.Orange, wallThickness);
					else if (wall.Secret)
						pen = new Pen(Color.Green, wallThickness);
					else if (wall.Internal)
						pen = new Pen(Color.Cyan, wallThickness);
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
							e.Graphics.DrawLine(pen, wCorner, eCorner);
							break;
						case Map.Wall.WallFacing.WEST:
							e.Graphics.DrawLine(pen, nCorner, sCorner);
							break;
						case Map.Wall.WallFacing.CROSS:
							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall
							e.Graphics.DrawLine(pen, nCorner, sCorner);//south wall
							break;
						case Map.Wall.WallFacing.NORTH_T:
							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall
							e.Graphics.DrawLine(pen, center, sCorner);//tail towards south
							break;
						case Map.Wall.WallFacing.SOUTH_T:
							e.Graphics.DrawLine(pen, wCorner, eCorner);//north wall
							e.Graphics.DrawLine(pen, center, nCorner);//tail towards north
							break;
						case Map.Wall.WallFacing.WEST_T:
							e.Graphics.DrawLine(pen, nCorner, sCorner);//west wall
							e.Graphics.DrawLine(pen, center, eCorner);//tail towards east
							break;
						case Map.Wall.WallFacing.EAST_T:
							e.Graphics.DrawLine(pen, nCorner, sCorner);//west wall
							e.Graphics.DrawLine(pen, center, wCorner);//tail towards west
							break;
						case Map.Wall.WallFacing.NE_CORNER:
							e.Graphics.DrawLine(pen, center, eCorner);
							e.Graphics.DrawLine(pen, center, sCorner);
							break;
						case Map.Wall.WallFacing.NW_CORNER:
							e.Graphics.DrawLine(pen, center, wCorner);
							e.Graphics.DrawLine(pen, center, sCorner);
							break;
						case Map.Wall.WallFacing.SW_CORNER:
							e.Graphics.DrawLine(pen, center, nCorner);
							e.Graphics.DrawLine(pen, center, wCorner);
							break;
						case Map.Wall.WallFacing.SE_CORNER:
							e.Graphics.DrawLine(pen, center, nCorner);
							e.Graphics.DrawLine(pen, center, eCorner);
							break;
						default:
							e.Graphics.DrawRectangle(pen, x, y, squareSize, squareSize);
							e.Graphics.DrawString("?", new Font("Arial", 12), new SolidBrush(Color.Red), nCorner);
							break;
					}

					e.Graphics.DrawString(wall.Minimap.ToString(), new Font("Arial", 10), new SolidBrush(Color.Red), x, y);
				}
			}

			//draw objects
			foreach (Map.Object oe in Map.Objects)
			{
				PointF ptf = oe.Location;
				if (ptf.X >= hScrollBar1.Value && ptf.X < size.Width+hScrollBar1.Value && ptf.Y >= vScrollBar1.Value && ptf.Y < size.Height+vScrollBar1.Value)	
				{
					Pen pen;
					float x = ptf.X - hScrollBar1.Value, y = ptf.Y - vScrollBar1.Value;
					
					PointF center = new PointF(x, y);
					PointF topLeft = new PointF(center.X - objectSelectionRadius, center.Y - objectSelectionRadius);
					if (SelectedObject != null && ((Map.Object) SelectedObject).Location.Equals(oe.Location))
						pen = new Pen(Color.Green, 1);
					else
						pen = new Pen(Color.Blue, 1);
					e.Graphics.DrawEllipse(pen, new RectangleF(topLeft, new Size(2*objectSelectionRadius, 2*objectSelectionRadius)));
					e.Graphics.DrawString(oe.Extent.ToString(),new Font("Arial", 10), new SolidBrush(Color.Purple), topLeft);
				}
			}
			//draw polygons
			foreach (Map.Polygon poly in Map.Polygons)
			{
				Pen pen = new Pen(Color.PaleGreen, 1);
				ArrayList points = new ArrayList();
				foreach (PointF pt in poly.Points)
					points.Add(new PointF(pt.X-hScrollBar1.Value, pt.Y - vScrollBar1.Value));
				points.Add(points[0]);//complete the loop
				e.Graphics.DrawLines(pen, (PointF[]) points.ToArray(typeof(PointF)));
			}
		} 
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
					if(pt.X % 2 == pt.Y % 2 && Map.WallMap[pt] == null)
					{
						Map.WallMap.Add(pt, wallSelector1.NewWall(pt));
					}
					else
					{
						Map.WallMap.Remove(pt);
					}
				}
				else if (CurrentMode == Mode.MAKE_OBJECT)
				{
					Map.Object obj = new Map.Object();
					DefaultObject.CopyTo(obj);
					obj.Location = pointClicked;
					obj.Extent = DefaultObject.Extent + 1;
					while(Map.Objects.extents.Contains(obj.Extent))
						obj.Extent++;
					Map.Objects.Add(obj);
				}
				else if (CurrentMode == Mode.MAKE_WINDOW)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					Map.Wall wall = (Map.Wall)Map.WallMap[pt];
					if(wall != null)
						wall.Window = !wall.Window;
				}
				else if (CurrentMode == Mode.MAKE_DESTRUCTABLE)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					Map.Wall wall = (Map.Wall)Map.WallMap[pt];
					if(wall != null)
						wall.Destructable = !wall.Destructable;
				}

				else if (CurrentMode == Mode.MAKE_SECRET)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize);
					Map.Wall wall = (Map.Wall)Map.WallMap[pt];
					if(wall != null)
						wall.Secret = !wall.Secret;
				}
				else if (CurrentMode == Mode.MAKE_FLOOR)
				{
					Point pt = new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value-squareSize)/squareSize);
					Map.Tile tile = (Map.Tile) Map.FloorMap[pt];
					if(tile == null && pt.X % 2 == pt.Y % 2 && !threeFloorBox.Checked)
					{
						tile = new Map.Tile(
							pt,
							(byte) tileGraphic.SelectedIndex,
							GetVariation(tileVar),
							blendDialog.Blends
							);
						Map.FloorMap.Add(pt, tile);
					}
					else if(tile==null && pt.X % 2 == pt.Y % 2)
					{
						tile = new Map.Tile(
							new Point(pt.X-1,pt.Y-1),
							(byte) tileGraphic.SelectedIndex,
							3,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X-2,pt.Y),
							(byte) tileGraphic.SelectedIndex,
							6,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X-1,pt.Y+1),
							(byte) tileGraphic.SelectedIndex,
							7,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X,pt.Y-2),
							(byte) tileGraphic.SelectedIndex,
							0,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							pt,
							(byte) tileGraphic.SelectedIndex,
							4,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X,pt.Y+2),
							(byte) tileGraphic.SelectedIndex,
							8,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X+1,pt.Y-1),
							(byte) tileGraphic.SelectedIndex,
							1,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X+2,pt.Y),
							(byte) tileGraphic.SelectedIndex,
							2,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
						tile = new Map.Tile(
							new Point(pt.X+1,pt.Y+1),
							(byte) tileGraphic.SelectedIndex,
							5,
							blendDialog.Blends
							);
						if((Map.Tile) Map.FloorMap[tile.Location]==null)
							Map.FloorMap.Add(tile.Location, tile);
					}
					else
					{
						Map.FloorMap.Remove(pt);
						if (threeFloorBox.Checked)
						{
							Map.FloorMap.Remove(new Point(pt.X - 1, pt.Y - 1));
							Map.FloorMap.Remove(new Point(pt.X - 2, pt.Y));
							Map.FloorMap.Remove(new Point(pt.X - 1, pt.Y + 1));
							Map.FloorMap.Remove(new Point(pt.X, pt.Y - 2));
							Map.FloorMap.Remove(new Point(pt.X, pt.Y + 2));
							Map.FloorMap.Remove(new Point(pt.X + 1, pt.Y - 1));
							Map.FloorMap.Remove(new Point(pt.X + 2, pt.Y));
							Map.FloorMap.Remove(new Point(pt.X + 1, pt.Y + 1));
						}
					}
				}
				mapPanel.Invalidate();
			}
		}

		//TODO: fix status bar so it doesnt truncate...
		private void mapPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Map.Wall wall = (Map.Wall)Map.WallMap[new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value)/squareSize)];
			Map.Tile tile = (Map.Tile)Map.FloorMap[new Point((e.X+hScrollBar1.Value)/squareSize,(e.Y+vScrollBar1.Value-squareSize)/squareSize)];

			statusBar1.Text = "";
			statusBar1.Text += String.Format("X={0} Y={1}", e.X + hScrollBar1.Value, e.Y + vScrollBar1.Value);

			if (wall != null)
				statusBar1.Text += String.Format("  Wall Material=\"{0}\"", wall.Material);
			if (tile != null && CurrentMode == Mode.MAKE_FLOOR)
			{
				statusBar1.Text += "  Tile:";
				statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}", tile.Graphic, tile.Variation);
				if (tile.EdgeTiles.Count > 0)
				{
					statusBar1.Text += String.Format(" Edges({0}):", tile.EdgeTiles.Count);
					foreach (Map.Tile.EdgeTile edge in tile.EdgeTiles)
						statusBar1.Text += String.Format(" \"{0}\"-0x{1:x2}-{2}-{3}", ThingDb.FloorTileNames[edge.Graphic], edge.Variation, edge.Dir, ThingDb.EdgeTileNames[edge.Edge]);
				}
			}

			if (SelectedObject != null && CurrentMode == Mode.SELECT)
				statusBar1.Text += String.Format(" SelectedObject={0}", SelectedObject.Name);
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
			propDlg.ShowDialog();
			mapPanel.Invalidate();
		}

		private bool dragging;
		private void mapPanel_MouseUp(object sender, MouseEventArgs e)
		{
			Point pointClicked = new Point(hScrollBar1.Value+e.X, vScrollBar1.Value+e.Y);
			
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
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
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
			this.groupBox1.SuspendLayout();
			this.objectGroup.SuspendLayout();
			this.floorGroup.SuspendLayout();
			this.wallGroup.SuspendLayout();
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
			this.hScrollBar1.Location = new System.Drawing.Point(128, 730);
			this.hScrollBar1.Maximum = 5888;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(880, 16);
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
			this.groupBox1.Controls.Add(this.objectGroup);
			this.groupBox1.Controls.Add(this.floorGroup);
			this.groupBox1.Controls.Add(this.wallGroup);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(128, 746);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tools";
			// 
			// objectGroup
			// 
			this.objectGroup.Controls.Add(this.selectButton);
			this.objectGroup.Controls.Add(this.newObjectButton);
			this.objectGroup.Controls.Add(this.defaultButt);
			this.objectGroup.Location = new System.Drawing.Point(8, 512);
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
			this.floorGroup.Location = new System.Drawing.Point(8, 360);
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
			// MapView
			// 
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusBar1);
			this.Name = "MapView";
			this.Size = new System.Drawing.Size(1024, 768);
			this.groupBox1.ResumeLayout(false);
			this.objectGroup.ResumeLayout(false);
			this.floorGroup.ResumeLayout(false);
			this.wallGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

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
	}
}
