using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Diagnostics;

using NoxShared.NoxType;

namespace NoxShared
{
	/// <summary>
	/// NoxPlayer represents the player data contained within a .plr file. This class handles the decryption/encryption when given a filename via the constructor.
	/// </summary>

	public class Map : Observable
	{
		// ----PUBLIC MEMBERS----
		public MapInfo Info;
		public SortedList WallMap;
		public bool Encrypted; //default true if map will be encrypted upon saving
		public SortedList FloorMap;
		public ObjectTable Objects;//contains objects, why would we reference them by location? -- TODO: enforce uniqueness of PointF key? <-- rethink this. Should this be a map at all? if so, we need to allow for multiple objects at the same point(?)

		// ----PROTECTED MEMBERS----
		protected MapHeader Header;
		public string FileName;
		protected Hashtable Headers;//contains the headers for each section or the complete section

		#region Inner Classes and Enumerations

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

			//lousy checksum algorithm, TODO: replace with crc32 or something similar
			public void GenerateChecksum(byte[] data)
			{
				BinaryReader rdr = new BinaryReader(new MemoryStream(data));

				CheckSum = 0;
				rdr.BaseStream.Seek(LENGTH, SeekOrigin.Current);//skip header
				while (rdr.BaseStream.Position < rdr.BaseStream.Length)
					CheckSum += 29 * rdr.ReadUInt32();
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

			public class Tile
			{
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

				public Tile(byte graphic, byte variation)
				{
					graphicId = graphic; Variation = variation;
				}

				public Tile(byte graphic, byte variation, ArrayList edgeTiles) : this(graphic, variation)
				{
					EdgeTiles = edgeTiles;
				}

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
					for (int numBlends = rdr.ReadByte(); numBlends > 0; numBlends--)
					{
						EdgeTile edge = new EdgeTile(rdr.BaseStream);
						EdgeTiles.Add(edge);
					}
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
					protected byte graphicId;
					public byte Variation;
					public byte unknown1; //Always 00(?)
					public byte unknown2; //Always 10(?)
					public EdgeTileDirection Direction;

					public string Graphic
					{
						get
						{
							return ((ThingDb.Tile) ThingDb.FloorTiles[graphicId]).Name;//FIXME
						}
					}

					public enum EdgeTileDirection : byte
					{
						SW = 0x00,
						WEST = 0x01,
						WEST_2 = 0x02,
						WEST_3 = 0x03,
						NW = 0x04,
						SOUTH = 0x05,
						SOUTH_7 = 0x07,
						SOUTH_9 = 0x09,
						NORTH = 0x06,
						NORTH_8 = 0x08,
						NORTH_A = 0x0A,
						SE = 0x0B,
						EAST = 0x0C,
						EAST_D = 0x0D,
						EAST_E = 0x0E,
						NE = 0x0F
						//TODO: figure out what's up with the differnt directions
						//  also: beyond 0x0F... do they occur in WW maps?
						// fix status bar so it doesnt truncate...
						// is "EdgeTile" a misnomer for blends? -- what are the EDGE thingdb entries for? where/when/how are they indexed? appended to end of floortiles maybe? prob. not
					}

					public EdgeTile(byte graphic,byte variation, byte unk1, byte unk2, byte dir)
					{
						graphicId = graphic; Variation = variation; unknown1 = unk1; unknown2 = unk2; Direction = (EdgeTileDirection) dir;
					}

					public EdgeTile(Stream stream)
					{
						Read(stream);
					}

					public void Read(Stream stream)
					{
						BinaryReader rdr = new BinaryReader(stream);

						graphicId = rdr.ReadByte();
						Variation = rdr.ReadByte();
						unknown1 = rdr.ReadByte();
						unknown2 = rdr.ReadByte();
						Direction = (EdgeTileDirection) rdr.ReadByte();
					}

					public void Write(Stream stream)
					{
						BinaryWriter wtr = new BinaryWriter(stream);

						wtr.Write((byte) graphicId);
						wtr.Write((byte) Variation);
						wtr.Write((byte) unknown1);
						wtr.Write((byte) unknown2);
						wtr.Write((byte) Direction);
					}
				}
			}
		}

		public class Wall : IComparable
		{
			public enum WallFacing : byte
			{
				NORTH = 0,
				//SOUTH = 0,//same as north
				WEST = 1,
				//EAST = 1,//same as west
				CROSS = 2,
				
				SOUTH_T = 3,
				EAST_T = 4,
				NORTH_T = 5,
				WEST_T = 6,

				SW_CORNER = 7,
				NW_CORNER = 8,
				NE_CORNER = 9,
				SE_CORNER = 10
			}

			public Point Location;
			//TODO: Make a list of all types and have a string
			//perhaps add these in the NoxTypes namespace?
			public WallFacing Facing;
			protected byte matId;
			public byte Unknown1;
			public byte Minimap;//name might not be appropriate
			public byte Unknown2;
			public bool Destructable;
			public bool Secret;
			public bool Window;

			//these unknowns follow the wall's entry in the SecretWalls section
			// and usually (always?) have these values
			//    (initialized here so they are the default for new walls)
			public int Secret_u1 = 0x00000003;
			public int Secret_u2 = 0x00000100;

			public string Material
			{
				get
				{
					return ((ThingDb.Wall) ThingDb.Walls[matId]).Name;
				}
			}

			/// <summary>
			/// Use this constructor when reading the entry and all values are supplied.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="facing"></param>
			/// <param name="mat"></param>
			/// <param name="mini"></param>
			/// <param name="u1"></param>
			/// <param name="u2"></param>
			public Wall(byte x, byte y, byte facing, byte mat, byte u1, byte mini, byte u2) : this(new Point(x, y), facing, mat)
			{
				Minimap = mini; Unknown1 = u1; Unknown2 = u2;
			}

			public Wall(Point loc, byte facing, byte mat) : this(loc, (WallFacing) facing, mat)
			{
			}

			public Wall(Point loc, WallFacing facing, byte mat)
			{
				Location = loc;	Facing = facing; matId = mat;
				Minimap = 0x64; Unknown1 = 0x00; Unknown2 = 0x01; Destructable = false; Secret = false; Window = false;//defaults
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
					if (toc[obj.Name] == null)
						toc.Add(obj.Name, id++);

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

		public class Object : IComparable
		{
			public string Name;
			public int Type;//unknown, really... seem to be only a few possible values with high frequency, usually 0x0040003C
			public int Extent;//TODO:enforce uniqueness?
			public PointF Location;
			public int Unknown;//always null?
			public byte Terminator;//usually 0x00, sometimes 0xFF (e.g., Flag objects)
			//TODO//public ArrayList Modifiers = new ArrayList();//modifiers this object has (elements are of type 'class Modifier')
			public byte[] modbuf;
			public ArrayList enchants;

			public Object()
			{
				//default values
				Name = "ExtentShortCylinderSmall";
				Extent = 0;
				Type = 0x0040003C;
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
				Type = rdr.ReadInt32();
				Extent = rdr.ReadInt32();
				Unknown = rdr.ReadInt32();//always null?
				Location = new PointF(rdr.ReadSingle(), rdr.ReadSingle());//x then y
				Terminator = rdr.ReadByte();
				//while (rdr.BaseStream.Position < endOfData)
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

			}
			/// <summary>
			/// 
			/// </summary>
			/// <param name="stream"></param>
			/// <param name="toc">A Mapping of string to short IDs</param>
			public void Write(Stream stream, IDictionary toc)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write((short) toc[Name]);
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				//if (Name == "WizardRobe" || Name == "LeatherBoots") modbuf = new byte[] {0,0,0,0,0x00,0x00};//HACK for testing
				//TODO:
				//   read from thing.bin? -> if type SIMPLE and not IMMOBILE, then add six nulls to the end of the entry?
				//   modifier support, read from modifier.bin?
				//   new object, move object, delete object
				long dataLength = 0x15 + (modbuf==null? 0 : modbuf.Length);
				wtr.Write((long) dataLength);
				wtr.Write((int) Type);
				wtr.Write((int) Extent);
				wtr.Write((int) Unknown);
				wtr.Write((float) Location.X);
				wtr.Write((float) Location.Y);
				wtr.Write((byte) Terminator);
				if(modbuf != null)
					wtr.Write(modbuf);
			}

			public int CompareTo(object obj)
			{
				return Name.CompareTo(((Object) obj).Name);
			}
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
			FloorMap = new SortedList(new LocationComparer());
			Headers = new Hashtable();
			Info = new MapInfo();
			Objects = new ObjectTable();//dummy table

			//encrypt file unless this is explicitly set to false
			//  OR we find upon loading that the file was originally unencrypted
			Encrypted = true;
		}

		public Map(string filename) : this()
		{
			Load(filename);
		}

		public void Load(string filename)
		{
			this.FileName = filename;
      		ReadFile();
		}

		#region Reading Methods
		public void ReadFile()
		{
			NoxBinaryReader rdr	= new NoxBinaryReader(File.Open(FileName, FileMode.Open), NoxCryptFormat.MAP);

			//check to see if the file is not encrypted
			if (rdr.ReadUInt32() != 0xFADEFACE)//all unencrypted maps start with this
			{
				rdr = new NoxBinaryReader(File.Open(FileName, FileMode.Open), NoxCryptFormat.NONE);
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
						ReadFloorMap(rdr);
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
						SkipSection(rdr,"ScriptObject");
						break;
					case "AmbientData":
						//TODO
						SkipSection(rdr,"AmbientData");
						break;
					case "Polygons":
						//TODO
						SkipSection(rdr,"Polygons");
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
				}
			}

			rdr.Close();
			NotifyObservers();
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
				if (WallMap[new Point(x,y)] == null)//do not add duplicates (there should not be duplicate entries in a file anyway)
				{
					Wall wall = new Wall(x, y, rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte());
					WallMap.Add(wall.Location, wall);
				}
			}
			
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (WallMap) ERROR: bad section length");
		}

		protected void ReadFloorMap(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;//order matters here!
			
			SectHeader hed = new SectHeader("FloorMap",12,rdr.ReadBytes(0x12));
			Headers.Add(hed.name,hed);	
			while (true)//we'll get an 0xFF for both x and y to signal end of section
			{
				byte y = rdr.ReadByte();
				byte x = rdr.ReadByte();

				if (y == 0xFF && x == 0xFF)
					break;
				else
					rdr.BaseStream.Seek(-2, SeekOrigin.Current);//rewind back to beginning of current entry

				TilePair tilePair = new TilePair(rdr.BaseStream);

				if (FloorMap[tilePair.Location] == null)//do not add duplicates (there should not be duplicate entries in a file anyway)
					FloorMap.Add(tilePair.Location, tilePair);
			}
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (FloorMap) ERROR: bad section length");
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

		public void WriteFile()
		{
			MemoryStream mapData = new MemoryStream();
			NoxBinaryWriter wtr = new NoxBinaryWriter(mapData, NoxType.NoxCryptFormat.NONE);//encrypt later

			Header.Write(wtr.BaseStream);
			Info.Write(wtr.BaseStream);
			WriteWallMap(wtr);
			WriteFloorMap(wtr);
			WriteSecretWalls(wtr);
			WriteDestructableWalls(wtr);
			SkipSection(wtr,"WayPoints");
			SkipSection(wtr,"DebugData");
			WriteWindowWalls(wtr);
			SkipSection(wtr,"GroupData");
			SkipSection(wtr,"ScriptObject");
			SkipSection(wtr,"AmbientData");
			SkipSection(wtr,"Polygons");
			SkipSection(wtr,"MapIntro");
			SkipSection(wtr,"ScriptData");
			Objects.Write(wtr.BaseStream);

			//write null bytes to next boundary -- this is needed only because
			// no more data is going to be written,
			// so the null bytes are not written implicitly by 'Seek()'ing
			wtr.Write(new byte[(8 - wtr.BaseStream.Position % 8) % 8]);
			
			//go back and write header again, with a proper checksum
			Header.GenerateChecksum(mapData.ToArray());
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			Header.Write(wtr.BaseStream);
			wtr.BaseStream.Seek(0, SeekOrigin.End);

			wtr.Close();

			FileStream fStr = File.Open(FileName, FileMode.Create);
			NoxBinaryWriter fileWtr;
			if (Encrypted)
				fileWtr = new NoxBinaryWriter(fStr, NoxType.NoxCryptFormat.MAP);
			else
				fileWtr = new NoxBinaryWriter(fStr, NoxType.NoxCryptFormat.NONE);
			fileWtr.Write(mapData.ToArray());
			fileWtr.Close();
		}

		protected void WriteFloorMap(NoxBinaryWriter wtr)
		{
			string str = "FloorMap";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);

			foreach (TilePair tilePair in FloorMap.Values)
				tilePair.Write(wtr.BaseStream);

			wtr.Write((ushort) 0xFFFF);//terminating x and y
						
			//rewrite the length now that we can find it
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek(0, SeekOrigin.End);
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
		#endregion
	}
}
