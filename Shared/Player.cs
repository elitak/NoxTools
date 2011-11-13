using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Text;

namespace NoxShared
{
	public class Player : ICloneable// : IXmlSerializable
	{
		private const int LOGIN_LENGTH = 0x10;
		private const int SERIAL_LENGTH = 0x20;
		private const int MAX_NAME_CHARS = 24;
		private const int NAME_LENGTH = 2 * MAX_NAME_CHARS;

		[XmlIgnore] public int Team;
		[XmlIgnore] public bool Connected;
		public string Login = "";
		public string Serial = "";
		public string Name = "";
		[XmlIgnore] public int Number;
		[XmlIgnore] public bool Unkickable;

		public static Player ReadFromMemory(Stream stream)
		{
			Player player = new Player();
			BinaryReader rdr = new BinaryReader(stream);
			long startPos = rdr.BaseStream.Position;
			rdr.BaseStream.Seek(0x44, SeekOrigin.Current);
			player.Number = rdr.ReadInt32();
			rdr.BaseStream.Seek(0x18, SeekOrigin.Current);
			player.Connected = rdr.ReadInt32() != 0;
			player.Login = new string(Encoding.ASCII.GetChars(rdr.ReadBytes(LOGIN_LENGTH))).Split('\0')[0];
			player.Serial = new string(Encoding.ASCII.GetChars(rdr.ReadBytes(SERIAL_LENGTH))).Split('\0')[0];
			rdr.BaseStream.Seek(0x29, SeekOrigin.Current);
			player.Name = new string(Encoding.Unicode.GetChars(rdr.ReadBytes(NAME_LENGTH))).Split('\0')[0];
			player.Unkickable = rdr.ReadInt16() != 0 && player.Name.Length == MAX_NAME_CHARS;

			rdr.BaseStream.Seek(0x12dc - (rdr.BaseStream.Position - startPos), SeekOrigin.Current);

			return player;
		}

		public override bool Equals(object obj)
		{
			Player rhs = (Player) obj;
			return
				Connected == rhs.Connected
				&& Number == rhs.Number
				&& Login == rhs.Login
				&& Serial == rhs.Serial
				&& Name == rhs.Name;
		}

		public override int GetHashCode() {return base.GetHashCode();}

		/*
		public XmlSchema GetSchema()
		{
			// TODO:  Add Player.GetSchema implementation
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			// TODO:  Add Player.ReadXml implementation
		}

		public void WriteXml(XmlWriter writer)
		{
			// TODO:  Add Player.WriteXml implementation
		}
		*/

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
