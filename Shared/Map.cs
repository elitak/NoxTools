using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Diagnostics;

using NoxShared.NoxType;

namespace NoxShared
{
	public class Map
	{
		// ----PUBLIC MEMBERS----
		public MapInfo Info;
		public SortedList WallMap;

		public bool Encrypted = true;//whether map will be encrypted upon saving
		//encrypt file unless this is explicitly set to false
		//  OR we find upon loading that the file was originally unencrypted

		public TileMap FloorMap;
		public ObjectTable Objects;//contains objects, why would we reference them by location? -- TODO: enforce uniqueness of PointF key? <-- rethink this. Should this be a map at all? if so, we need to allow for multiple objects at the same point(?)
		public PolygonList Polygons = new PolygonList();
		public ScriptObject Scripts;

		// ----PROTECTED MEMBERS----
		protected MapHeader Header;
		public string FileName;
		protected Hashtable Headers;//contains the headers for each section or the complete section

		#region Inner Classes and Enumerations

		public class PolygonList : ArrayList
		{
			public short TermCount;//seems to control amount of useless data at the end???
			public ArrayList Points = new ArrayList();

			public PolygonList() {}
			public PolygonList(Stream stream)
			{
				Read(stream);
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				TermCount = rdr.ReadInt16();
				
				int numPoints = rdr.ReadInt32();
				while (numPoints-- > 0)
				{
					rdr.ReadInt32();//the point number. NOTE: I'm assuming they are in ascending order!!
					Points.Add(new PointF(rdr.ReadSingle(), rdr.ReadSingle()));
				}

				int numPolygons = rdr.ReadInt32();
				while (numPolygons-- > 0)
					Add(new Polygon(rdr.BaseStream, this));

				Debug.Assert(rdr.BaseStream.Position == finish, "(Map, Polygons) Bad read length.");
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write("Polygons\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				wtr.Write((long) 0);//dummy length value
				long startPos = wtr.BaseStream.Position;
				
				wtr.Write((short) TermCount);

				//rebuild point list
				Points.Clear();
				foreach (Polygon poly in this)
					foreach (PointF pt in poly.Points)
						if (!Points.Contains(pt))
							Points.Add(pt);

				//TODO: sort this before writing?
				wtr.Write((int) Points.Count);
				foreach (PointF pt in Points)
				{
					wtr.Write((int) (Points.IndexOf(pt)+1));
					wtr.Write((float) pt.X);
					wtr.Write((float) pt.Y);
				}

				wtr.Write((int) this.Count);
				foreach (Polygon poly in this)
					poly.Write(wtr.BaseStream, this);

				//rewrite the length now that we can find it
				long length = wtr.BaseStream.Position - startPos;
				wtr.Seek((int) startPos - 8, SeekOrigin.Begin);
				wtr.Write((long) length);
				wtr.Seek((int) length, SeekOrigin.Current);
			}
		}

		public class Polygon
		{
			public string Name;
			public Color AmbientLightColor;//the area's ambient light color
			public byte MinimapGroup;//the visible wall group when in this area
			public ArrayList Points = new ArrayList();//the unindexed points that define the polygon
			protected byte[] endbuf;

			public Polygon(string name, Color ambient, byte mmGroup, IList points)
			{
				Name = name;
				AmbientLightColor = ambient;
				MinimapGroup = mmGroup;
				Points = new ArrayList(points);
				//N.B. that endbuf is left null here
			}

			public Polygon(Stream stream, PolygonList list)
			{
				Read(stream, list);
			}

			//FIXME, TODO: figure out what the "termCount" is about
			// and remove dependency on
			// the parent polygon list for reading and writing
			protected void Read(Stream stream, PolygonList list)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Name = rdr.ReadString();
				AmbientLightColor = Color.FromArgb(rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte());
				MinimapGroup = rdr.ReadByte();

				short ptCount = rdr.ReadInt16();
				while (ptCount-- > 0)
					Points.Add(list.Points[rdr.ReadInt32()-1]);

				//TODO: figure this out?? really weird...
				//  termCount of 0x0004 means we end with the normal unknown endbuf of the last polygon
				//  termCount of 0x0003 means we omit the last 4 (null) bytes.
				//always "01 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00" or 4 shorter?
				System.IO.MemoryStream nStream = new System.IO.MemoryStream();
				System.IO.BinaryWriter wtr = new System.IO.BinaryWriter(nStream);
				

				//some maps (solo) have strings in this area?
				//please document/comment this, Andrew
				wtr.Write(rdr.ReadInt16());
				string temp = new string(rdr.ReadChars(rdr.ReadInt32()));
				wtr.Write((int)temp.Length);
				wtr.Write(temp.ToCharArray());
				wtr.Write(rdr.ReadInt32());
				wtr.Write(rdr.ReadInt16());
				temp = new string(rdr.ReadChars(rdr.ReadInt32()));
				wtr.Write((int)temp.Length);
				wtr.Write(temp.ToCharArray());

				if (list.TermCount == 0x0003)
					wtr.Write(rdr.ReadBytes(4));
				else if (list.TermCount == 0x0004)
					wtr.Write(rdr.ReadBytes(8));
				else
					Debug.Assert(false, "(Map, Polygons) Unhandled terminal count.");
				endbuf = nStream.ToArray();
			}

			public void Write(Stream stream, PolygonList list)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write(Name);
				wtr.Write((byte) AmbientLightColor.R);
				wtr.Write((byte) AmbientLightColor.G);
				wtr.Write((byte) AmbientLightColor.B);
				wtr.Write((byte) MinimapGroup);
				wtr.Write((short) Points.Count);
				foreach (PointF pt in Points)
					wtr.Write((int) (list.Points.IndexOf(pt)+1));
				if (endbuf != null)
					wtr.Write(endbuf);
				else
				{
					if (list.TermCount == 0x0003)
						wtr.Write(new byte[] {01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00});
					else if (list.TermCount == 0x0004)
						wtr.Write(new byte[] {01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00});
					else
						Debug.Assert(false, "(Map, Polygons) Unhandled terminal count.");
				}
			}
		}

		public class Tile
		{
			public Point Location;
			protected byte graphicId;
			public byte Variation;
			public ArrayList EdgeTiles = new ArrayList();

			public string Graphic
			{
				get
				{
					return ((ThingDb.Tile) ThingDb.FloorTiles[graphicId]).Name;
				}
			}

			public int Variations
			{
				get
				{
					return ((ThingDb.Tile) ThingDb.FloorTiles[graphicId]).Variations;
				}
			}

			public Tile(Point loc, byte graphic, byte variation, ArrayList edgetiles)
			{
				Location = loc;
				graphicId = graphic; Variation = variation;
				EdgeTiles = edgetiles;
			}

			public Tile(Point loc, byte graphic, byte variation) : this(loc, graphic, variation, new ArrayList()) {}

			public Tile(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				graphicId = rdr.ReadByte();
				Variation = (byte) rdr.ReadByte();
				rdr.ReadBytes(3);//these are always null for first tilePair of a blending group (?)
				for (int numEdgeTiles = rdr.ReadByte(); numEdgeTiles > 0; numEdgeTiles--)
					EdgeTiles.Add(new EdgeTile(rdr.BaseStream));
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((byte )graphicId);
				wtr.Write((byte) Variation);
				wtr.Write(new byte[3]);//3 nulls
				wtr.Write((byte) EdgeTiles.Count);
				foreach(EdgeTile edge in EdgeTiles)
					edge.Write(stream);
			}

			public class EdgeTile//maybe derive from tile?
			{
				public byte Graphic;
				public byte Variation;
				public byte unknown1 = 0x00; //Always 00(?)
				public Direction Dir;
				public byte Edge;

				public enum Direction : byte
				{
					SW_Tip,//0x00
					West,
					West_02,
					West_03,
					NW_Tip,//0x04
					South,
					North,
					South_07,
					North_08,//0x08
					South_09,
					North_0A,
					SE_Tip,
					East,//0x0C
					East_D,
					East_E,
					NE_Tip,
					SW_Sides,//0x10
					NW_Sides,
					NE_Sides,
					SE_Sides//0x13
					//TODO: figure out what's up with the different directions
				}

				public EdgeTile(byte graphic, byte variation, Direction dir, byte edge)
				{
					Graphic = graphic; Variation = variation; Dir = dir; Edge = edge;
				}

				public EdgeTile(Stream stream)
				{
					Read(stream);
				}

				public void Read(Stream stream)
				{
					BinaryReader rdr = new BinaryReader(stream);

					Graphic = rdr.ReadByte();
					Variation = rdr.ReadByte();
					unknown1 = rdr.ReadByte();
					if (unknown1 != 0)
						Debug.WriteLine(String.Format("WARNING: tile unknown byte was not 0, it was {0}", unknown1), "MapRead");
					Edge = rdr.ReadByte();
					Dir = (Direction) rdr.ReadByte();
					if (!Enum.IsDefined(typeof(Direction), (byte) Dir))
						Debug.WriteLine(String.Format("WARNING: edgetile direction {0} is undefined", (byte) Dir), "MapRead");
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);

					wtr.Write((byte) Graphic);
					wtr.Write((byte) Variation);
					wtr.Write((byte) unknown1);
					wtr.Write((byte) Edge);
					wtr.Write((byte) Dir);
				}
			}
		}

		public class TileMap : SortedList
		{
			protected byte[] header;

			public TileMap() : base(new LocationComparer()) {}

			public TileMap(Stream stream) : this()
			{
				Read(stream);
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				ArrayList tilePairs = new ArrayList();
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				header = rdr.ReadBytes(0x12);

				while (true)//we'll get an 0xFF for both x and y to signal end of section
				{
					byte y = rdr.ReadByte();
					byte x = rdr.ReadByte();

					if (y == 0xFF && x == 0xFF)
						break;

					rdr.BaseStream.Seek(-2, SeekOrigin.Current);//rewind back to beginning of current entry
					TilePair tilePair = new TilePair(rdr.BaseStream);
					if (!tilePairs.Contains(tilePair))//do not add duplicates (there should not be duplicate entries in a file anyway)
						tilePairs.Add(tilePair);
				}

				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (FloorMap) ERROR: bad section length");

				foreach (TilePair tp in tilePairs)
				{
					if (tp.Left != null) this.Add(tp.Left.Location, tp.Left);
					if (tp.Right != null) this.Add(tp.Right.Location, tp.Right);
				}
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write("FloorMap\0");
				long length = 0;
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				long pos = wtr.BaseStream.Position;
				wtr.Write(length);
				wtr.Write(header);

				//generate the TilePairs...
				ArrayList tilePairs = new ArrayList();
				SortedList tiles = (SortedList) this.Clone();
				while (tiles.Count > 0)
				{
					Tile left = null, right = null;
					Tile tile1 = (Tile) tiles.GetByIndex(0);
					if (tile1.Location.X % 2 == 1)//we got a right tile. the right tile will always come before it's left tile
					{
						right = tile1;
						Tile tile2 = (Tile) tiles[new Point(tile1.Location.X - 1, tile1.Location.Y + 1)];
						if (tile2 != null)
							left = tile2;
						tilePairs.Add(new TilePair((byte) ((right.Location.X-1)/2), (byte) ((right.Location.Y+1)/2), left, right));
					}
					else //assume that this tile is a single since the ordering would have forced the right tile to be handled first
					{
						left = tile1;
						tilePairs.Add(new TilePair((byte) (left.Location.X/2), (byte) (left.Location.Y/2), left, right));
					}
					if (left != null) tiles.Remove(left.Location);
					if (right != null) tiles.Remove(right.Location);
				}

				//... and write them
				foreach (TilePair tilePair in tilePairs)
					tilePair.Write(wtr.BaseStream);

				wtr.Write((ushort) 0xFFFF);//terminating x and y
						
				//rewrite the length now that we can find it
				length = wtr.BaseStream.Position - (pos + 8);
				wtr.Seek((int) pos, SeekOrigin.Begin);
				wtr.Write(length);
				wtr.Seek((int) length, SeekOrigin.Current);
			}
		}

		protected class MapHeader
		{
			const uint LENGTH = 0x18;
			const uint FILE_ID = 0xFADEFACE;
			public uint CheckSum;//checksum for rest of file. determines whether download is necessary.
			public uint u1;//UNKNOWN
			public uint u2;//UNKNOWN

			public MapHeader(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				uint id = rdr.ReadUInt32();//first int is always FADEFACE
				Debug.Assert(id == FILE_ID);

				rdr.ReadInt32();//always null?
				CheckSum = rdr.ReadUInt32();
				rdr.ReadInt32();//always null?
				u1 = rdr.ReadUInt32();
				u2 = rdr.ReadUInt32();
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write(FILE_ID);
				wtr.Write((uint) 0);
				wtr.Write((uint) CheckSum);
				wtr.Write((uint) 0);
				wtr.Write((uint) u1);
				wtr.Write((uint) u2);
			}

			public void GenerateChecksum(byte[] data)
			{
				CheckSum = Crc32.Calculate(data);
			}
		}

		public class MapInfo
		{
			public short unknown;//usually 2 or 3, but changing it causes the map not to load sometimes?
			public string Summary;//the map's brief name
			public string Description;//the map's long description
			public string Author;
			public string Email;
			public string Author2;
			public string Email2;
			public string Version;//the map's current version
			public string Copyright;
			public string Date;
			public MapType Type;
			public byte RecommendedMin;
			public byte RecommendedMax;

			public MapInfo()
			{
				Type = MapType.ARENA;
			}

			public enum MapType : uint
			{
				SOLO = 0x00000001,
				QUEST = 0x00000002,
				ARENA = 0x00000034,
				CTF = 0x00000018,
				SOCIAL = 0x80000000,
				FLAGBALL = 0x00000040
			}

			public static SortedList MapTypeNames;

			static MapInfo()
			{
				MapTypeNames = new SortedList();

				MapTypeNames.Add(MapType.ARENA, "Arena");
				MapTypeNames.Add(MapType.CTF, "CTF");
				MapTypeNames.Add(MapType.SOCIAL, "Social");
				MapTypeNames.Add(MapType.QUEST, "Quest");
				MapTypeNames.Add(MapType.SOLO, "Solo");
				MapTypeNames.Add(MapType.FLAGBALL, "Flagball");
			}

			protected enum SectionLength
			{
				PREFIX = 0x02,
				TITLE = 0x40,
				DESCRIPTION = 0x200,
				VERSION = 0x10,
				AUTHOR = 0x40,
				EMAIL = 0xC0,
				EMPTY = 0x80,
				COPYRIGHT = 0x80,//only on very few maps
				DATE = 0x20,
				TYPE = 0x04,
				MINMAX = 0x02,
				TOTAL = PREFIX + TITLE + DESCRIPTION + VERSION + 2*(AUTHOR + EMAIL) + EMPTY + COPYRIGHT + DATE + TYPE + MINMAX
			};

			public void Read(Stream stream)
			{
				NoxBinaryReader rdr = new NoxBinaryReader(stream);
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;//order matters!

				unknown = rdr.ReadInt16();//dont know what this is for
				//the ReadStrings below are equivalent to lines like these two:
				//  Summary = new string(rdr.ReadChars((int) SectionLength.TITLE));
				//  Summary = Summary.Substring(0, Summary.IndexOf('\0'));
				Summary = rdr.ReadString((int) SectionLength.TITLE);
				Description = rdr.ReadString((int) SectionLength.DESCRIPTION);
				Version = rdr.ReadString((int) SectionLength.VERSION);
				Author = rdr.ReadString((int) SectionLength.AUTHOR);
				Email = rdr.ReadString((int) SectionLength.EMAIL);
				Author2 = rdr.ReadString((int) SectionLength.AUTHOR);
				Email2 = rdr.ReadString((int) SectionLength.EMAIL);
				rdr.ReadBytes((int) SectionLength.EMPTY);
				Copyright = rdr.ReadString((int) SectionLength.COPYRIGHT);
				Date = rdr.ReadString((int) SectionLength.DATE);
				//TODO: quest maps have an extra section after this part and no min/max recommended players
				//TODO/FIXME: mapinfo does not read map type properly for Conflict.map
				Type = (MapType) rdr.ReadUInt32();
				RecommendedMin = rdr.ReadByte();
				RecommendedMax = rdr.ReadByte();
				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (MapInfo) WARNING: section length is incorrect");
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write("MapInfo\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				wtr.Write((long) SectionLength.TOTAL);

				long finish = wtr.BaseStream.Position + (long) SectionLength.TOTAL;

				wtr.Write((short) unknown);

				wtr.Write(Summary.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.TITLE - Summary.Length, SeekOrigin.Current);

				wtr.Write(Description.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.DESCRIPTION - Description.Length, SeekOrigin.Current);

				wtr.Write(Version.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.VERSION - Version.Length, SeekOrigin.Current);

				wtr.Write(Author.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.AUTHOR - Author.Length, SeekOrigin.Current);

				wtr.Write(Email.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.EMAIL - Email.Length, SeekOrigin.Current);

				wtr.Write(Author2.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.AUTHOR - Author2.Length, SeekOrigin.Current);

				wtr.Write(Email2.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.EMAIL - Email2.Length, SeekOrigin.Current);

				wtr.BaseStream.Seek((int) SectionLength.EMPTY, SeekOrigin.Current);

				wtr.Write(Copyright.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.COPYRIGHT - Copyright.Length, SeekOrigin.Current);

				wtr.Write(Date.ToCharArray());
				wtr.BaseStream.Seek((int) SectionLength.DATE - Date.Length, SeekOrigin.Current);

				wtr.Write((int) Type);
				wtr.Write((byte) RecommendedMin);
				wtr.Write((byte) RecommendedMax);

				Debug.Assert(wtr.BaseStream.Position == finish, "NoxMap (MapInfo) ERROR: wrote wrong length");
			}
		}

		public class SectHeader
		{
			public string name;
			public long length;
			public byte[] header;
			public SectHeader(string n, int len, byte []h)
			{
				name = n; length = len; header = h;
			}
		}

		protected class LocationComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				Point lhs = (Point) x, rhs = (Point) y;
				if (lhs.Y != rhs.Y)
					return lhs.Y - rhs.Y;
				else
					return lhs.X - rhs.X;
			}
		}


		public class TilePair : IComparable
		{
			public Point Location;
			public bool OneTileOnly
			{
				get
				{
					return Left == null || Right == null;
				}
			}

			//set one of these to null if you want a single-tile entry
			public Tile Left;
			public Tile Right;

			public TilePair(byte x, byte y, Tile left, Tile right)
			{
				Location = new Point(x, y);
				Left = left;
				Right = right;
			}

			public TilePair(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				byte y = rdr.ReadByte(), x = rdr.ReadByte();
				Location = new Point(x & 0x7F, y & 0x7F);//ignore sign bits for coordinates

				if ((x & y & 0x80) == 0)//sign bit signifies whether only the left, right, or both tilePairs are listed in this entry
				{
					if ((y & 0x80) != 0)//if y has sign bit set, then the left tilePair is specified
						Left = new Tile(stream);
					else if ((x & 0x80) != 0)
						Right = new Tile(stream);
					else
						Debug.Assert(false, "invalid x,y for tilepair entry");
				}
				else //otherwise, read right then left
				{
					Right = new Tile(stream);
					Left = new Tile(stream);
				}
				
				if (Left != null) Left.Location = new Point(Location.X * 2, Location.Y * 2);
				if (Right != null) Right.Location = new Point(Location.X * 2 + 1, Location.Y * 2 - 1);
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				byte x = (byte) (Location.X | 0x80), y = (byte) (Location.Y | 0x80);

				if (OneTileOnly)
				{
					if (Left == null)
						y &= 0x7F;
					else
						x &= 0x7F;
				}

				wtr.Write((byte) y);
				wtr.Write((byte) x);

				//write the right one first
				if (Right != null)
					Right.Write(stream);
				if (Left != null)
					Left.Write(stream);
			}

			public int CompareTo(object obj)
			{
				TilePair rhs = (TilePair) obj;
				if (Location.Y != rhs.Location.Y)
					return Location.Y - rhs.Location.Y;
				else
					return Location.X - rhs.Location.X;
			}

			public override bool Equals(object obj)
			{
				return obj is TilePair && CompareTo(obj) == 0;
			}

			public override int GetHashCode()
			{
				return Location.GetHashCode();
			}
		}

		public class Wall : IComparable
		{
			public enum WallFacing : byte
			{
				NORTH,//same as SOUTH
				WEST,//same as EAST
				CROSS,
				
				SOUTH_T,
				EAST_T,//4
				NORTH_T,
				WEST_T,

				SW_CORNER,
				NW_CORNER,//8
				NE_CORNER,
				SE_CORNER//10
			}

			public Point Location;
			//TODO: Make a list of all types and have a string
			//perhaps add these in the NoxTypes namespace?
			public WallFacing Facing;
			protected byte matId;
			public byte Unknown1 = 0x00;
			public byte Minimap = 0x64;
			public byte Unknown2 = 0x01;
			public bool Destructable;
			public bool Secret;
			public bool Window;

			//these unknowns follow the wall's entry in the SecretWalls section
			// and usually (always?) have these values
			//    (initialized here so they are the default for new walls)
			public int Secret_u1 = 0x00000003;
			public int Secret_u2 = 0x00000106;

			public string Material
			{
				get
				{
					return ((ThingDb.Wall) ThingDb.Walls[matId]).Name;
				}
			}

			public Wall(Stream stream)
			{
				Read(stream);
			}

			public Wall(Point loc, byte facing, byte mat) : this(loc, (WallFacing) facing, mat)
			{
			}

			public Wall(Point loc, WallFacing facing, byte mat)
			{
				Location = loc;	Facing = facing; matId = mat;
			}

			public Wall(Point loc, WallFacing facing, byte mat, byte mmGroup)
			{
				Location = loc;	Facing = facing; matId = mat; Minimap = mmGroup;
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Location = new Point(rdr.ReadByte(), rdr.ReadByte());
				Facing = (WallFacing) (rdr.ReadByte() & 0x7F);//I'm almost certain the sign bit is just garbage and does not signify anything about the wall
				matId = rdr.ReadByte();
				Unknown1 = rdr.ReadByte();
				Minimap = rdr.ReadByte();
				Unknown2 = rdr.ReadByte();
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((byte) Location.X);
				wtr.Write((byte) Location.Y);
				wtr.Write((byte) Facing);
				wtr.Write((byte) matId);
				wtr.Write((byte) Unknown1);
				wtr.Write((byte) Minimap);
				wtr.Write((byte) Unknown2);
			}

			public int CompareTo(object obj)
			{
				Wall rhs = (Wall) obj;
				if (Location.Y != rhs.Location.Y)
					return Location.Y - rhs.Location.Y;
				else
					return Location.X - rhs.Location.X;
			}
		}

		public class ObjectTable : ArrayList
		{
			protected SortedList toc = new SortedList();
			protected short tocUnknown;
			protected short dataUnknown;

			public ArrayList extents = new ArrayList();

			/// <summary>
			/// Constructs the Object table using the given streams.
			/// </summary>
			/// <param name="toc">A stream containing the ObjectTOC, without header but with length.</param>
			/// <param name="data">A stream containing the ObjectData, without header but with length.</param>
			public ObjectTable(Stream toc, Stream data)
			{
				Read(toc, data);
			}

			public ObjectTable()
			{
			}

			public override int Add(object obj) 			{ 				extents.Add(((Object)obj).Extent); 				return base.Add (obj); 			} 

			public void Read(Stream toc, Stream data)
			{
				BinaryReader rdr;
				//first construct the toc
				rdr = new BinaryReader(toc);
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				tocUnknown = rdr.ReadInt16();//0x0001, unknown -- might be useful
				short numEntries = rdr.ReadInt16();

				while (rdr.BaseStream.Position < finish)
					this.toc.Add(rdr.ReadInt16(), rdr.ReadString());//map id to string

				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (ObjectTOC) ERROR: bad section length");
				Debug.Assert(this.toc.Count == numEntries, "NoxMap (ObjectTOC) ERROR: wrong number of object entries");

				//now read the table and construct its objects
				rdr = new BinaryReader(data);
				finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				dataUnknown = rdr.ReadInt16();//0x0001 -- useful?
				while (rdr.BaseStream.Position < finish)
				{
					if (rdr.ReadInt16() == 0)//the list is terminated by a null object Name
						break;        //the loop should break on this condition only
					else
						rdr.BaseStream.Seek(-2, SeekOrigin.Current);//roll back the short we just read
					Add(new Object(rdr.BaseStream, this.toc));
				}

				//check that length of section matches
				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (ObjectDATA) ERROR: bad section length");
			}

			/// <summary>
			/// Writes the ObjectTOC and ObjectData along prefixed by their section names to the given strea.
			/// </summary>
			/// <param name="stream">The stream to write to.</param>
			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				//before we start writing, we need to build a new toc
				Sort();
				toc = new SortedList();
				short id = 1;
				foreach (Object obj in this)
				{
					if (toc[obj.Name] == null)
						toc.Add(obj.Name, id++);
					if (obj.inven > 0) //If object has an inventory
						foreach (Object o in obj.childObjects)
							if (toc[o.Name] == null) //What if there are embedded inventories? That needs fixed.
								toc.Add(o.Name, id++);
				}

				//--write the ObjectTOC--
				wtr.Write("ObjectTOC\0");
				long length = 0;
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				long startPos = wtr.BaseStream.Position;
				wtr.Write((long) length);//dummy value
				wtr.Write((short) tocUnknown);

				wtr.Write((short) toc.Count);

				//and write them
				foreach (string key in toc.Keys)
				{
					wtr.Write((short) toc[key]);
					wtr.Write(key);
				}

				//rewrite the length
				length = wtr.BaseStream.Position - (startPos+8);
				wtr.Seek((int)startPos, SeekOrigin.Begin);
				wtr.Write(length);
				wtr.Seek(0,SeekOrigin.End);

				//--write the ObjectTable--
				wtr.Write("ObjectData\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				length = 0;
				startPos = wtr.BaseStream.Position;
				wtr.Write((long) length);//will be rewritten later
				wtr.Write((short) dataUnknown);

				foreach (Object obj in this)
					obj.Write(wtr.BaseStream, toc);

				wtr.Write((short) 0);//write the null Name terminator

				//rewrite the length now that we can find it
				length = wtr.BaseStream.Position - (startPos + 8);
				wtr.Seek((int) startPos,SeekOrigin.Begin);
				wtr.Write((long) length);
				wtr.Seek(0, SeekOrigin.End);
			}
		}

		public class Object : IComparable, ICloneable
		{
			public string Name;
			public Property Properties;
			public short Type2;
			public int Extent;//TODO:enforce uniqueness?
			public PointF Location;
			public int Unknown;//always null?
			public byte Terminator;//usually 0x00, sometimes 0xFF (e.g., Flag objects)
			//TODO//public ArrayList Modifiers = new ArrayList();//modifiers this object has (elements are of type 'class Modifier')
			public byte[] modbuf = new byte[0];
			public ArrayList enchants = new ArrayList();
			public byte Team;//Specified in the extra stuff that comes with 0xFF Terminator
			public string Scr_Name;//Name used in Script Section
			public byte inven; //Number of objects in inventory, important for object ordering
			public ArrayList childObjects = new ArrayList(); //Objects in its inventory
			public byte[] temp1 = new byte[0];//Temporary buffers for FF term. stuff, unknowns 
			public byte[] temp2 = new byte[0];//Temporary buffers for FF term. stuff, unknowns 

			//note: there's a good chance that these are not flags at all and
			// should be a regular enumeration
			/*[Flags] public enum PropertyFlags : short
			{
				Always = 0x003C,//these three bits always seem to be set
				HasMana = 0x0001,//this is misnamed because elevators and buttons have it. "interactable"?
				HasAmmo = 0x0002,//should be "pickupable"? in stronghd.map, all items have this but in others most items are 0x3c
				//wands always have 3e and bows too
				Enemy = 0x0040//this seems to be the only bit set
			}
			
			public bool HasMana
			{
				get {return (Properties & PropertyFlags.HasMana) != 0;}
				set {if (value) Properties |= PropertyFlags.HasMana; else Properties &= ~PropertyFlags.HasMana;}
			}

			public bool HasAmmo
			{
				get {return (Properties & PropertyFlags.HasAmmo) != 0;}
				set {if (value) Properties |= PropertyFlags.HasAmmo; else Properties &= ~PropertyFlags.HasAmmo;}
			}*/

			public enum Property : short
			{
				Normal = 0x003C,
				Interact = 0x003D,
				Pickup = 0x003E,
				Enemy = 0x0040
			}

			public Object()
			{
				//default values
				Name = "ExtentShortCylinderSmall";
				Extent = 0;
				Properties = Property.Normal;
				Type2 = 0x0040;//always??
				Location = new PointF(0, 0);
			}

			public Object(string name, PointF loc) : this()
			{
				Name = name;
				Location = loc;
			}

			public Object(Stream stream, IDictionary toc)//read an object from the stream, using the provided toc to identify the object
			{
				Read(stream, toc);
			}

			public void Read(Stream stream, IDictionary toc)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Name = (string) toc[rdr.ReadInt16()];
				rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				long endOfData = rdr.ReadInt64() + rdr.BaseStream.Position;
				Properties = (Property) rdr.ReadInt16();
				Debug.WriteLineIf(!Enum.IsDefined(typeof(Property), Properties), String.Format("object {0} has properties 0x{1:x}", Name, (short) Properties));
				Type2 = rdr.ReadInt16();
				Debug.WriteLineIf(Type2 != 0x40, String.Format("object {0} has type2 0x{1:x}", Name, Type2));
				Extent = rdr.ReadInt32();
				Unknown = rdr.ReadInt32();//always null?
				Debug.WriteLineIf(Unknown != 0, String.Format("object {0}'s Unknown was not null! it was 0x{1:x}", Name, Unknown));
				Unknown = 0;//FIXME? remove this Andrew unless it's here for a reason, if so, add a comment
				Location = new PointF(rdr.ReadSingle(), rdr.ReadSingle());//x then y
				if(Location.X > 5880 || Location.Y > 5880)
					Location = new PointF(5870,5870);
				Terminator = rdr.ReadByte();
				if(Terminator == 0xFF)
				{
					temp1 = rdr.ReadBytes(4);
					Scr_Name = rdr.ReadString();
					Team = rdr.ReadByte();
					inven = rdr.ReadByte();
					temp2 = rdr.ReadBytes(20);
				}
				if (rdr.BaseStream.Position < endOfData)
				{
					/*
					//TODO: Loop to get the modifiers
					Modifier mod;
					rdr.ReadInt32();
					rdr.ReadInt32();
					rdr.ReadInt32();
					long length = rdr.ReadInt64();
							if(rdr.PeekChar() == 0x00)
								break;
							rdr.BaseStream.Seek(1, SeekOrigin.Current);
							if(rdr.PeekChar() == 0x00)
								break;
							rdr.BaseStream.Seek(-1, SeekOrigin.Current);
							mod = new Modifier();
							mod.name = rdr.ReadString();
							entry.Modifiers.Add(mod);
					}
					//*/
					//For now, just skip them.
					modbuf = rdr.ReadBytes((int)(endOfData - rdr.BaseStream.Position));
				}

				//check that this entry's length matches
				Debug.Assert(rdr.BaseStream.Position == endOfData, "NoxMap (ObjectData) ERROR: bad entry length");
				if(inven > 0)
				{
					childObjects = new ArrayList(inven);
					for(int i = inven; i > 0; i--)
						childObjects.Add(new Object(stream,toc));
				}
			}
			/// <summary>
			/// Writes the object to the stream
			/// </summary>
			/// <param name="stream">The stream to write to</param>
			/// <param name="toc">A Mapping of string to short IDs</param>
			public void Write(Stream stream, IDictionary toc)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write((short) toc[Name]);
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				int xtraLength = 0;
				if (Terminator == 0xFF)
				{
					if(temp1 == null)
						temp1 = new Byte[] {0, 0, 0, 1};
					if(temp2 == null)
						temp2 = new Byte[] {00, 00,00, 00, 00, 00, 01, 00, 00,00,00,00,00,00,00,00,00,00,00,00};
					xtraLength = temp1.Length + temp2.Length + 3 + Scr_Name.Length;
				}
				long dataLength = 0x15 + modbuf.Length + xtraLength;//0x15 is the minumum length of an entry
				wtr.Write((long) dataLength);
				//the 0x15 is the length of these entries combined...
				wtr.Write((short) Properties);
				wtr.Write((short) Type2);
				wtr.Write((int) Extent);
				wtr.Write((int) Unknown);
				wtr.Write((float) Location.X);
				wtr.Write((float) Location.Y);
				wtr.Write((byte) Terminator);
				//... these entries make 0x15 bytes
				if (Terminator == 0xFF)
				{
					wtr.Write(temp1);
					wtr.Write(Scr_Name);
					wtr.Write(Team);
					wtr.Write(inven);
					wtr.Write(temp2);
				}
				if (modbuf != null)
					wtr.Write(modbuf);
				if(inven > 0)
					foreach(Object o in childObjects)
						o.Write(stream,toc);
			}

			public int CompareTo(object obj)
			{
				return Name.CompareTo(((Object) obj).Name);
			}


			public object Clone()
			{
				Object copy = (Object) MemberwiseClone();
				copy.modbuf = (byte[]) modbuf.Clone();
				copy.enchants = (ArrayList) enchants.Clone();
				copy.childObjects = (ArrayList) childObjects.Clone();
				copy.temp1 = (byte[]) temp1.Clone();
				copy.temp2 = (byte[]) temp2.Clone();
				return copy;
			}
		}
		
		public class ScriptObject
		{
			public SortedList SctStr;
			public byte[] rest; // CODE till DONE, hopefully	
		}
		public class Modifier
		{
			//TODO: is object to modifier a 1 to n relationship? or is it more like constructor info instead of modifiers?
		}

		// ---- ENUMS ----
		protected enum MapType
		{
			//SINGLE = //0x??,
			//MULTI = 0x??
			//QUEST = 0x??
		};
		#endregion

		// ----CONSTRUCTORS----
		public Map()
		{
			WallMap = new SortedList(new LocationComparer());
			FloorMap = new TileMap();
			Headers = new Hashtable();
			Info = new MapInfo();
			Objects = new ObjectTable();//dummy table
		}

		public Map(string filename) : this()
		{
			Load(filename);
		}

		public void Load(string filename)
		{
			FileName = filename;
      		ReadFile();
		}

		#region Reading Methods
		public void ReadFile()
		{
			Debug.WriteLine("Reading " + FileName, "MapRead");
			Debug.Indent();
			NoxBinaryReader rdr	= new NoxBinaryReader(File.Open(FileName, FileMode.Open), CryptApi.NoxCryptFormat.MAP);

			//check to see if the file is not encrypted
			if (rdr.ReadUInt32() != 0xFADEFACE)//all unencrypted maps start with this
			{
				rdr = new NoxBinaryReader(File.Open(FileName, FileMode.Open), CryptApi.NoxCryptFormat.NONE);
				Encrypted = false;
			}
			rdr.BaseStream.Seek(0, SeekOrigin.Begin);//reset to start

			Header = new MapHeader(rdr.BaseStream);
			
			while (rdr.BaseStream.Position < rdr.BaseStream.Length)
			{
				//I don't know if the map format allows sections out of order, but this app supports it...
				string section = rdr.ReadString();
				rdr.SkipToNextBoundary();
				switch (section)
				{
					case "MapInfo":
						Info.Read(rdr.BaseStream);
						break;
					case "WallMap":
						ReadWallMap(rdr);
						break;
					case "FloorMap":
						FloorMap = new TileMap(rdr.BaseStream);
						break;
					case "SecretWalls":
						ReadSecretWalls(rdr);
						break;
					case "DestructableWalls":
						ReadDestructableWalls(rdr);
						break;
					case "WayPoints":
						//TODO
						SkipSection(rdr,"WayPoints");
						break;
					case "DebugData":
						//TODO
						SkipSection(rdr,"DebugData");
						break;
					case "WindowWalls":
						//TODO
						ReadWindowWalls(rdr);
						break;
					case "GroupData":
						//TODO
						SkipSection(rdr,"GroupData");
						break;
					case "ScriptObject":
						//TODO
						//SkipSection(rdr,"ScriptObject");
						ReadScriptObject(rdr);
						break;
					case "AmbientData":
						//TODO
						SkipSection(rdr,"AmbientData");
						break;
					case "Polygons":
						Polygons = new PolygonList(rdr.BaseStream);
						break;
					case "MapIntro":
						//TODO
						SkipSection(rdr,"MapIntro");
						break;
					case "ScriptData":
						//TODO
						SkipSection(rdr,"ScriptData");
						break;
					case "ObjectTOC":
						ReadObjectToc(rdr);
						break;
					case "ObjectData":
						ReadObjectData(rdr);
						break;
					default:
						Debug.WriteLine("unhanled section");
						break;
				}
			}

			rdr.Close();
			Debug.Unindent();
			Debug.WriteLine("Read successful", "MapRead");
		}

		private Stream tocStream;
		private Stream dataStream;
		protected void ReadObjectToc(NoxBinaryReader rdr)
		{
			tocStream = new MemoryStream();
			ulong length = rdr.ReadUInt64();
			BinaryWriter wtr = new BinaryWriter(tocStream);
			wtr.Write((ulong) length);
			wtr.Write(rdr.ReadBytes((int) length));
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			if (dataStream != null)
				Objects = new ObjectTable(tocStream, dataStream);
		}
		protected void ReadObjectData(NoxBinaryReader rdr)
		{
			dataStream = new MemoryStream();
			ulong length = rdr.ReadUInt64();
			BinaryWriter wtr = new BinaryWriter(dataStream);
			wtr.Write((ulong) length);
			wtr.Write(rdr.ReadBytes((int) length));
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			if (tocStream != null)
				Objects = new ObjectTable(tocStream, dataStream);
		}

		protected void ReadWallMap(NoxBinaryReader rdr)
		{
			byte x, y;
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;//order matters here!

			//rdr.ReadBytes(0x12);//unknown (section header?)
			SectHeader hed = new SectHeader("WallMap",12,rdr.ReadBytes(0x12));
			Headers.Add(hed.name,hed);

			while ((x = rdr.ReadByte()) != 0xFF && (y = rdr.ReadByte()) != 0xFF)//we'll get an 0xFF for x to signal end of section
			{
				rdr.BaseStream.Seek(-2, SeekOrigin.Current);
				Wall wall = new Wall(rdr.BaseStream);
				//NOTE: not checking for duplicates (there should never be any)
				WallMap.Add(wall.Location, wall);
			}
			
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (WallMap) ERROR: bad section length");
		}
		
		//TODO: make WallMap class that extends SortedList to clean this wall reading/writing up
		protected void ReadDestructableWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			
			SectHeader hed = new SectHeader("DestructableWalls",2,rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = (Wall) WallMap[new Point(x,y)];
				wall.Destructable = true;
				num--;
			}
			Debug.Assert(num == 0, "NoxMap (DestructableWalls) ERROR: bad header");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (DestructableWalls) ERROR: bad section length");
		}

		protected void ReadWindowWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

			SectHeader hed = new SectHeader("WindowWalls",2,rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();//the number of window walls
			
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = (Wall) WallMap[new Point(x,y)];
				wall.Window = true;
				num--;
			}
			Debug.Assert(num == 0, "NoxMap (WindowWalls) ERROR: bad header");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (WindowWalls) ERROR: bad section length");
		}

		protected void ReadSecretWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

			SectHeader hed = new SectHeader("SecretWalls",2,rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();//the number of window walls
			
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = (Wall) WallMap[new Point(x,y)];
				wall.Secret = true;
				wall.Secret_u1 = rdr.ReadInt32();
				wall.Secret_u2 = rdr.ReadInt32();
				rdr.ReadBytes(7);//7 nulls
				num--;
			}

			Debug.Assert(num == 0, "NoxMap (SecretWalls) ERROR: bad wall count");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (SecretWalls) ERROR: bad section length");
		}

		protected void ReadScriptObject(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			SectHeader hed = new SectHeader("ScriptObject",2,rdr.ReadBytes(2));//always 0x0001?
			Headers.Add(hed.name,hed);
			short unknown = BitConverter.ToInt16(hed.header, 0);
			Debug.WriteLineIf(unknown != 0x0001, "header for ScriptObject was not 0x0001, it was 0x" + unknown.ToString("x"));
			
			Scripts = new ScriptObject();
			
			int Sectlen = rdr.ReadInt32();
			while(rdr.BaseStream.Position < finish)
			{
				if(rdr.BaseStream.Position < finish-12 && new string(rdr.ReadChars(12)) == "SCRIPT03STRG")
				{
					int numStr = rdr.ReadInt32();
					Scripts.SctStr = new SortedList(numStr);
					for(int i = 0; i < numStr; i++)
						Scripts.SctStr.Add(i,new string(rdr.ReadChars(rdr.ReadInt32())));
				}
				else
					rdr.BaseStream.Seek(-12,SeekOrigin.Current);
				Scripts.rest = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));
			}		

		}
		//Just a utility method to skip sections, remove this when all sections are being read properly.
		private void SkipSection(NoxBinaryReader rdr, string name)
		{
			int len = (int) rdr.ReadInt64();
			SectHeader hed = new SectHeader(name,len,rdr.ReadBytes(len));
			Headers.Add(name,hed);
		}

		#endregion

		#region Writing Methods
		private void SkipSection(NoxBinaryWriter wtr, string str)
		{
			wtr.Write(str + "\0");//write the section name

			SectHeader hed = (SectHeader) Headers[str];
			long length = hed.length;
			wtr.SkipToNextBoundary();
			wtr.Write(length);
			wtr.Write(hed.header);
		}

		protected byte[] mapData;
		protected void Serialize()
		{
			NoxBinaryWriter wtr = new NoxBinaryWriter(new MemoryStream(), CryptApi.NoxCryptFormat.NONE);//encrypt later

			Header.Write(wtr.BaseStream);
			Info.Write(wtr.BaseStream);
			WriteWallMap(wtr);
			FloorMap.Write(wtr.BaseStream);
			WriteSecretWalls(wtr);
			WriteDestructableWalls(wtr);
			SkipSection(wtr,"WayPoints");
			SkipSection(wtr,"DebugData");
			WriteWindowWalls(wtr);
			SkipSection(wtr,"GroupData");
			WriteScriptObject(wtr);
			SkipSection(wtr,"AmbientData");
			Polygons.Write(wtr.BaseStream);
			SkipSection(wtr,"MapIntro");
			SkipSection(wtr,"ScriptData");
			Objects.Write(wtr.BaseStream);

			//write null bytes to next boundary -- this is needed only because
			// no more data is going to be written,
			// so the null bytes are not written implicitly by 'Seek()'ing
			wtr.Write(new byte[(8 - wtr.BaseStream.Position % 8) % 8]);
			
			//go back and write header again, with a proper checksum
			Header.GenerateChecksum(((MemoryStream) wtr.BaseStream).ToArray());
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			Header.Write(wtr.BaseStream);
			wtr.BaseStream.Seek(0, SeekOrigin.End);
			wtr.Close();

			mapData = ((MemoryStream) wtr.BaseStream).ToArray();
			if (Encrypted)
				mapData = CryptApi.NoxEncrypt(mapData, CryptApi.NoxCryptFormat.MAP);
		}

		public void WriteMap()
		{
			Serialize();
			BinaryWriter fileWtr = new BinaryWriter(File.Open(FileName, FileMode.Create));
			fileWtr.Write(mapData);
			fileWtr.Close();
		}

		public void WriteNxz()
		{
			Serialize();
			//do a stupid replace of ".map" -- better be named correctly!!!
			BinaryWriter fileWtr = new BinaryWriter(File.Open(FileName.Replace(".map", ".nxz"), FileMode.Create));
			fileWtr.Write((uint) mapData.Length);
			fileWtr.Write(CryptApi.NxzEncrypt(mapData));
			fileWtr.Close();
		}

		private void WriteWindowWalls(NoxBinaryWriter wtr)
		{
			string str = "WindowWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((short) 0);//placeholder for count

			//TODO: give these a consistent ordering before writing. the maps do have an ordering...
			//   seems to be based on x, y. figure it out and then enforce it here.
			short count = 0;
			foreach (Wall wall in WallMap.Values)
				if(wall.Window)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					count++;
				}
		
			//rewrite the length
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int) hed.length, SeekOrigin.Current);
			//rewrite the windowwall count
			wtr.Write((short) count);
			wtr.Seek(0, SeekOrigin.End);
		}

		private void WriteDestructableWalls(NoxBinaryWriter wtr)
		{
			string str = "DestructableWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((Int16)0);

			Int16 count = 0;
			foreach (Wall wall in WallMap.Values)
				if(wall.Destructable)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					count++;
				}

			//rewrite the length
			length = wtr.BaseStream.Position - (pos+8);
			wtr.Seek((int)pos,SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int)hed.length,SeekOrigin.Current);
			wtr.Write((Int16)count);
			wtr.Seek(0,SeekOrigin.End);
		}

		private void WriteSecretWalls(NoxBinaryWriter wtr)
		{
			string str = "SecretWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((Int16)0);

			Int16 count = 0;
			foreach (Wall wall in WallMap.Values)
				if(wall.Secret)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					wtr.Write((uint) wall.Secret_u1);
					wtr.Write((uint) wall.Secret_u2);
					wtr.Write(new byte[7]);//7 nulls
					count++;
				}

			//rewrite the length
			length = wtr.BaseStream.Position - (pos+8);
			wtr.Seek((int)pos,SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int)hed.length,SeekOrigin.Current);
			wtr.Write((Int16)count);
			wtr.Seek(0,SeekOrigin.End);
		}

		private void WriteWallMap(NoxBinaryWriter wtr)
		{
			string str = "WallMap";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str+"\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);

			foreach (Wall wall in WallMap.Values)
				wall.Write(wtr.BaseStream);
			wtr.Write((byte) 0xFF);//wallmap terminates with this byte

			//rewrite the length
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek(0, SeekOrigin.End);
		}
		private void WriteScriptObject(NoxBinaryWriter wtr)
		{
			string str = "ScriptObject";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str+"\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			// placeholder for whole section length
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			// placeholder for subsection length
			int sectlen = 0;
			long secpos;
			secpos = wtr.BaseStream.Position;
			wtr.Write(sectlen);
			// if there is a strings section
			if (Scripts.SctStr != null)//Eric fixed a bug here: "SCRIPTTO3STRG" should(? - see Bunker) be written even if count is 0
			{
				wtr.Write("SCRIPT03STRG".ToCharArray()); // tokens used to distiguish sections of the section
				wtr.Write(Scripts.SctStr.Count); // write number of strings
				foreach(String s in Scripts.SctStr.Values) // write each string
				{
					wtr.Write(s.Length);
					wtr.Write(s.ToCharArray());
				}
			}
			// if there was anything after the strings section
			if(Scripts.rest != null)
				wtr.Write(Scripts.rest); // write rest of the section
			// rewrite section length
			sectlen = (int)(wtr.BaseStream.Position - (secpos + 4));
			wtr.Seek((int)secpos,SeekOrigin.Begin);
			wtr.Write(sectlen);
			wtr.Seek(0, SeekOrigin.End);

			//rewrite the length
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek(0, SeekOrigin.End);
		}
		#endregion
	}
}
