using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;

namespace NoxBagTool
{
	/// <summary>
	/// Summary description for VideoBag.
	/// </summary>
	public class VideoBag : Bag
	{
		protected Header header;
		protected ArrayList sections;

		public class Header
		{
			public int Type;
			public int FileLength;//this includes the header
			public int SectionCount;
			public int u2;//0x00008000 for video{,8}.bag
			public int u3;//0x00008000 for video8, 0x00007AD5 for video
			public int u4;//25 above total number of entries??

			public Header(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Type = rdr.ReadInt32();
				FileLength = rdr.ReadInt32();
				SectionCount = rdr.ReadInt32();
				u2 = rdr.ReadInt32();
				u3 = rdr.ReadInt32();
				u4 = rdr.ReadInt32();
			}
		}

		public class Section
		{
			public uint SectionLength;//total number of bytes in this section
			public uint SizeUncompressed;
			public uint SizeCompressed;//aka the length of the entry's data
			public int EntryCount;//sometimes -1?

			public ArrayList Entries;

			//this is assigned by whatever constructs this object
			public uint Offset;//the offset of this section in the .bag

			public class SectionEntry
			{
				public string Name;
				public byte u1;
				public uint Length;//size of this entry in the uncompressed section
				public int u3;

				public SectionEntry(Stream stream)
				{
					BinaryReader rdr = new BinaryReader(stream);
					Name = new String(rdr.ReadChars(rdr.ReadByte()));
					Name = Name.TrimEnd('\0');//dont include the required terminator
					u1 = rdr.ReadByte();
					Length = rdr.ReadUInt32();
					u3 = rdr.ReadInt32();
				}
			}

			public Section(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				SectionLength = rdr.ReadUInt32();
				SizeUncompressed = rdr.ReadUInt32();
				SizeCompressed = rdr.ReadUInt32();
				EntryCount = rdr.ReadInt32();
				
				Entries = new ArrayList();

				long startPos = stream.Position;
				uint length = 0;
				while (stream.Position < startPos + SectionLength)
				{
					SectionEntry entry = new SectionEntry(stream);
					length += entry.Length;
					Entries.Add(entry);
				}
				Debug.Assert(stream.Position == startPos + SectionLength, "bad section length encountered");
				Debug.Assert(length == SizeUncompressed, "section length mismatch");
			}
		}

		public static uint BagOffset;
		public VideoBag(string path) : base(path)
		{
			VideoBag.BagOffset = 0;
			sections = new ArrayList();
			if (File.Exists(bagPath))
				Read();
		}

		protected override void Read()
		{
			base.Read();
			header = new Header(idx);
			uint currentOffset = 0;

			for (int count = 0; count < header.SectionCount; count++)
			{
				Section sct = new Section(idx);
				sct.Offset = currentOffset;
				currentOffset += sct.SizeCompressed;
				sections.Add(sct);
			}

			Debug.Assert(idx.Position == header.FileLength, "wrong number of bytes read");
		}

		public override void ExtractAll(string path)
		{
			//TODO
		}

	}
}
