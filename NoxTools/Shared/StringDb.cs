using System;
using System.IO;
using System.Text;
using System.Collections;


namespace NoxShared
{
	public class StringDb
	{
		public class NoxStringEncoding : Encoding
		{
			//decode methods
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				for (int offset = 0; offset < byteCount; offset++)
					bytes[byteIndex + offset] = (byte) ~bytes[byteIndex + offset];

				return Encoding.Unicode.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			public override int GetMaxCharCount(int byteCount)
			{
				return byteCount / 2;
			}

			//encode methods
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				for (int offset = 0; offset < charCount; offset++)
					chars[charIndex + offset] = (char) ~chars[charIndex + offset];//FIXME?

				return Encoding.Unicode.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			public override int GetByteCount(char[] chars, int index, int count)
			{
				return count * 2;
			}

			public override int GetMaxByteCount(int charCount)
			{
				return charCount * 2;
			}
		}

		protected Hashtable Strings;

		public StringDb()
		{
			Strings = new Hashtable();
		}

		public void Load(string filename)
		{
			FileStream stream = File.Open(filename, FileMode.Open);
			BinaryReader rdr = new BinaryReader(stream);
			NoxStringEncoding enc = new NoxStringEncoding();

			//the header
			rdr.ReadChars(4);//file identifier (" FSC")
			rdr.ReadInt32();//always 2?
			uint numEntries = rdr.ReadUInt32();//number of entries in the file
			rdr.ReadInt32();//some length
			rdr.ReadBytes(8);//null padding

			//the string entries
			while (Strings.Count < numEntries)
			{
				rdr.ReadChars(4);//always " LBL"
				int numStrings = rdr.ReadInt32();//number of strings associated with this key
				string key = new String(rdr.ReadChars(rdr.ReadInt32()));
				ArrayList val = new ArrayList();
				while (numStrings-- > 0)
				{
					string valueCode = new String(rdr.ReadChars(4));//" rtS" or "WrtS" ???
					val.Add(enc.GetString(rdr.ReadBytes(rdr.ReadInt32() * 2)));	
					if (valueCode == "WrtS")
						val.Add(new String(rdr.ReadChars(rdr.ReadInt32())));
				}
				Strings.Add(key, val);
			}

			//*
			BinaryWriter wtr = new BinaryWriter(File.Open("output.txt", FileMode.OpenOrCreate));
			foreach (DictionaryEntry de in Strings)
			{
				wtr.Write(((string)de.Key + "\n").ToCharArray());
				
				foreach (string str in (ArrayList)de.Value)
					wtr.Write(String.Format("\t{0}\n", str).ToCharArray());
			}
			//*/
		}
	}
}