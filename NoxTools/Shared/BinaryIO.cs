using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing;

using NoxShared.NoxType;

namespace NoxShared
{
	/// <summary>
	/// NoxBinaryReader functions as a regular System.IO.BinaryReader except that it decrypts the stream automatically
	/// </summary>
	public class NoxBinaryReader : BinaryReader
	{
		[System.Runtime.InteropServices.DllImportAttribute("noxcrypt.dll")]
		protected static extern int NoxCrypt_crypt(byte[] data, int length, int format, int mode);

		public NoxBinaryReader(Stream stream, NoxCryptFormat format) : base(DecryptStream(stream, format))
		{
		}

		protected static Stream DecryptStream(Stream stream, NoxCryptFormat format)
		{
			//return original stream if no encryption
			if (format == NoxCryptFormat.NONE)
				return stream;

			int length = (int) stream.Length;
			byte[] buffer = new byte[length];

			stream.Read(buffer, 0, length);
			stream.Close();

			NoxCrypt_crypt(buffer, length, (int) format, (int) NoxCryptMode.DECRYPT);

			return new MemoryStream(buffer);
		}

		//Nox usually stores string lengths as bytes, not ints, so override this method
		public override string ReadString()
		{
			return ReadString(Type.GetType("System.Byte"));
		}

		public string ReadString(Type lengthType)
		{
			string str;

			if (lengthType.Equals(Type.GetType("System.Byte")))
				str = ReadString(ReadByte());
			else if (lengthType.Equals(Type.GetType("System.Int16")))
				str = ReadString(ReadInt16());
			else if (lengthType.Equals(Type.GetType("System.Int32")))
				str = ReadString(ReadInt32());
			else
				str = null;//throw exception instead?

			return str;
		}

		//read the specified number of bytes as a string
		//and throw away anything after the first null encountered
		public string ReadString(int bytes)
		{
			string str = new string(ReadChars(bytes));

			if (str.IndexOf('\0') >= 0)
				str = str.Substring(0, str.IndexOf('\0'));

			return str;
		}


		public string ReadUnicodeString()
		{
			//read the first byte as the string's length
			return Encoding.Unicode.GetString(ReadBytes(ReadByte() * 2));
		}

		public System.Drawing.Color ReadColor()
		{
			return System.Drawing.Color.FromArgb(ReadByte(), ReadByte(), ReadByte());
		}

		public NoxType.UserColor ReadUserColor()
		{
			return new NoxType.UserColor(ReadByte() + 1);
		}

		/// <summary>
		/// Skips to the next qword (8 byte) boundary and returns the number of bytes skipped
		/// does not skip any bytes if already on a qword boundary.
		/// </summary>

		
		public int SkipToNextBoundary()
		{
			int skip = (int) (8 - BaseStream.Position % 8) % 8;//0 iff BaseStream%8 == 0
			BaseStream.Seek(skip, SeekOrigin.Current);
			return skip;
		}

	}

	public class NoxBinaryWriter : BinaryWriter
	{
		protected NoxType.NoxCryptFormat format;

		[System.Runtime.InteropServices.DllImportAttribute("noxcrypt.dll")]
		protected static extern int NoxCrypt_crypt(byte[] data, int length, int format, int mode);

		public NoxBinaryWriter(Stream stream, NoxCryptFormat format) : base(stream)
		{
			this.format = format;
		}

		public override void Close()
		{
			//encrypt before closing
			SkipToNextBoundary();//pad so total length is divisible by 8
			int length = (int) BaseStream.Position;
			byte[] buffer = new byte[length];

			BaseStream.Seek(0, SeekOrigin.Begin);
			BaseStream.Read(buffer, 0, length);

			NoxCrypt_crypt(buffer, length, (int) format, (int) NoxCryptMode.ENCRYPT);

			BaseStream.Seek(0, SeekOrigin.Begin);
			Write(buffer);
			base.Close();
		}

		public int SkipToNextBoundary()
		{
			int skip = (int) (8 - BaseStream.Position % 8) % 8;//0 iff BaseStream%8 == 0
			BaseStream.Seek(skip, SeekOrigin.Current);
			return skip;
		}

		public override void Write(string str)
		{
			Write((byte)str.Length);
			Write(str.ToCharArray());
		}

		public void WriteColor(Color color)
		{
			Write((short) color.R);
			Write((short) color.G);
			Write((short) color.B);
		}

		public void WriteUserColor(UserColor color)
		{
			Write(color.Code - 1);
		}
	}
}
