using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Collections;
//using System.Threading;
using System.Text;
using System.Drawing;

namespace NoxShared
{
	/// <summary>
	/// Summary description for NoxMemoryHack.
	/// </summary>
	public class NoxMemoryHack
	{
		protected static ProcessMemory process = ProcessMemory.OpenProcess("GAME");

		public static ProcessMemory Process
		{
			get
			{
				return process;
			}
		}

		//TODO: use values from xml file for addresses

		public static void Refresh()
		{
			Teams.Refresh();
		}

		public class Teams
		{
			private static ArrayList teams = new ArrayList();
			public static ArrayList TeamList
			{
				get
				{
					return teams;
				}
				set
				{
					teams = value;
				}
			}

			public static int TeamCount
			{
				get
				{
					int count = 0;
					foreach (Team team in TeamList)
						if (team.Enabled)
							count += 1;
					return count;
				}
			}

			public enum TeamFlags
			{
				TEAM_DAMAGE = 0x01,
				AUTO_ASSIGN = 0x02,
				USE_TEAMS = 0x04
			}

			private static int flags;
			public static bool UseTeams
			{
				get
				{
					return (flags & (int) TeamFlags.USE_TEAMS) != 0;
				}
				set
				{
					SetTeamFlagsBit(TeamFlags.USE_TEAMS, value);
				}
			}

			public static bool AutoAssign
			{
				get
				{
					return (flags & (int) TeamFlags.AUTO_ASSIGN) != 0;
				}
				set
				{
					SetTeamFlagsBit(TeamFlags.AUTO_ASSIGN, value);
				}
			}

			public static bool TeamDamage
			{
				get
				{
					return (flags & (int) TeamFlags.TEAM_DAMAGE) != 0;
				}
				set
				{
					SetTeamFlagsBit(TeamFlags.TEAM_DAMAGE, value);
				}
			}

			private static void SetTeamFlagsBit(TeamFlags bit, bool enable)
			{
				if (enable)
					flags |= (int) bit;
				else
					flags &= ~((int) bit);
			}

			private const int MAX_TEAMS = 0x10;
			private const int TEAM_TABLE_ADDRESS = 0x654D5C;
			private const int TEAM_HEADER_LENGTH = 0x5C;
			private const int TEAM_ENTRY_LENGTH = 0x50;
			private const int TEAM_NAME_LENGTH = 0x2C;
			private const int TEAM_TABLE_LENGTH = TEAM_HEADER_LENGTH + MAX_TEAMS * TEAM_ENTRY_LENGTH;

			public static void Refresh()
			{
				//TODO: aquire lock somehow?
				BinaryReader rdr = new BinaryReader(new MemoryStream(NoxMemoryHack.process.Read(TEAM_TABLE_ADDRESS, TEAM_TABLE_LENGTH)));

				int  numTeams = rdr.ReadInt32();
				flags = rdr.ReadInt32();

				rdr.BaseStream.Seek(TEAM_HEADER_LENGTH, SeekOrigin.Begin);//skip to end of header

				//read the teams
				teams.Clear();
				for (int ndx = 0; ndx < numTeams; ndx++)
				{
					teams.Add(Team.FromBytes(rdr.ReadBytes(TEAM_ENTRY_LENGTH)));
					((Team) teams[ndx]).TeamNumber = ndx + 1;//TODO? find a better way of keeping track of the team number/index
				}
			}

			public static void Write()
			{
				BinaryWriter wtr = new BinaryWriter(new MemoryStream(NoxMemoryHack.process.Read(TEAM_TABLE_ADDRESS, TEAM_TABLE_LENGTH)));

				wtr.Write((int) TeamCount);
				wtr.Write((int) flags);

				wtr.BaseStream.Seek(TEAM_HEADER_LENGTH, SeekOrigin.Begin);//skip to end of header

				//write the teams
				foreach (Team team in teams)
				{
					team.Write(wtr.BaseStream);
				}

				NoxMemoryHack.process.Write(TEAM_TABLE_ADDRESS, ((MemoryStream) wtr.BaseStream).ToArray());
			}

			public static void AddTeam()
			{
				teams.Add(new Team(teams.Count + 1));
			}

			public class Team
			{
				public string Name;
				public int TeamNumber;
				public int MemberCount;
				public Color Color;
				public bool Enabled;

				private int unknownAddress;

				public static Color[] TeamColor = {
													  Color.LightGray,//FIXME: no team? may be white also
													  Color.Red,
													  Color.Blue,
													  Color.Green,
													  Color.DarkBlue,
													  Color.Yellow,
													  Color.DarkRed,
													  Color.Black,
													  Color.White,
													  Color.Orange
												  };

				/*
				public enum ColorCode
				{
					RED = 0x01,
					BLUE = 0x02,
					GREEN = 0x03,
					DARK_BLUE = 0x04,
					DARK_RED = 0x05,
					YELLOW = 0x06,
					BLACK = 0x07,
					WHITE = 0x08,
					ORANGE = 0x09,
					//RED = 0x0A,
					//RED = 0x0B
				}*/
				
				public Team(int teamNumber) : base()
				{
					TeamNumber = teamNumber;
					Enabled = false;
					Color = TeamColor[teamNumber - 1];
				}

				protected Team()
				{
					//default values in case not all details are filled in
					Name = "Unnamed Team";
					MemberCount = 0;
					Enabled = true;
					Color = TeamColor[0];
				}

				public static Team FromBytes(byte[] data)
				{
					Team team = new Team();
					BinaryReader rdr = new BinaryReader(new MemoryStream(data));
					team.Name = Encoding.Unicode.GetString(rdr.ReadBytes(TEAM_NAME_LENGTH), 0, TEAM_NAME_LENGTH);
					team.Name = team.Name.Substring(0, team.Name.IndexOf('\0'));//handle nulls appropriately
					
					team.unknownAddress = rdr.ReadInt32();//some kind of address
					team.MemberCount = rdr.ReadInt32();
					rdr.ReadBytes(4);//always null?

					team.Color = TeamColor[Math.Max(0, rdr.ReadByte() - 1)];
					rdr.ReadByte();//UNKOWN -- doesnt seem to do anything but tends to match teamnumber byte and is zeroed out when team is disabled
					team.TeamNumber = rdr.ReadByte();//team number is the index in our array, so this will be ignored and overwritten

					rdr.ReadBytes(5);//null bytes
					team.Enabled = rdr.ReadBoolean();
					//rdr.ReadBytes(7);//last null bytes are meaningless?

					return team;
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);

					wtr.Write(Encoding.Unicode.GetBytes(Name));
					wtr.Write(new byte[TEAM_NAME_LENGTH - Encoding.Unicode.GetByteCount(Name)]);//write the rest of the space used for the name with nulls
					wtr.BaseStream.Seek(4, SeekOrigin.Current);
					wtr.Write((int) MemberCount);
					wtr.BaseStream.Seek(4, SeekOrigin.Current);
					wtr.Write((byte) (new ArrayList(TeamColor).IndexOf(Color) + 1));//assumes that color is valid and will be found in list
					//wtr.BaseStream.Seek(1, SeekOrigin.Current);//cant be zero!
					wtr.Write((byte) TeamNumber);//dont know what this is, but it should not be 0, otherwise, teams after will not show
					wtr.Write((byte) TeamNumber);
					wtr.BaseStream.Seek(5, SeekOrigin.Current);
					wtr.Write((bool) Enabled);
					wtr.BaseStream.Seek(15, SeekOrigin.Current);//advance to end of this entry
				}

				public override bool Equals(object obj)
				{
					Team rhs = obj as Team;
					return rhs != null && TeamNumber == rhs.TeamNumber && Name == rhs.Name;
				}

				//just to suppress that warning, HACK
				public override int GetHashCode()
				{
					return base.GetHashCode ();
				}

			}
		}

		public enum ConsoleColor
		{
			BLACK = 0x00,
			DARK_GRAY = 0x01,
			GRAY = 0x02,
			LIGHT_GRAY = 0x03,
			WHITE = 0x04,
			DARK_RED = 0x05,
			RED = 0x06,
			PINK = 0x07,
			DARK_GREEN = 0x08,
			GREEN = 0x09,
			LIGHT_GREEN = 0x0A,
			DARK_BLUE = 0x0B,
			BLUE = 0x0C,
			LIGHT_BLUE = 0x0D,
			DARK_YELLOW = 0x0E,
			YELLOW = 0x0F,
			LIGHT_YELLOW = 0x10
			//anything higher goes back to GRAY
		}

		public Color[] ConsoleColors = {
										   Color.Black,
										   Color.LightGray,
										   Color.Gray,
										   Color.DarkGray,
			Color.White,
		};

		public static void PrintToConsole(string text, ConsoleColor color)
		{
			process.CallFunction((IntPtr) 0x450b90, (byte) color, text);
		}

		public static void PrintToScreen(string text1, string text2)
		{
			process.CallFunction((IntPtr) 0x565ca3, text1, text2);
		}
		
		/*public static void KickPlayer(string name)
		{
			process.CallFunction((IntPtr) 0x4432B0, 1, */
		public static void ConsoleCommand(string commandLine)
		{
			process.CallFunction((IntPtr) 0x443c80, commandLine );
		}
	}
}