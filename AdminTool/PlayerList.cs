using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;

using NoxShared;

namespace NoxTrainer
{
	public class PlayerList : UserControl
	{
		private DataGrid dataGrid1;
		private System.Windows.Forms.TextBox boxPlayerId;
		private System.Windows.Forms.Button buttonKick;
		private System.Windows.Forms.Button buttonBan;
		private System.Windows.Forms.Panel panel1;

		protected DataTable dataTable = new DataTable();
		private System.Windows.Forms.Button buttonSaveBan;

		//use a sorted list since we are keying of one value, the serial
		public SortedList BannedPlayers = new SortedList();

		//TODO:
		// separate model from view

		public PlayerList()
		{
			InitializeComponent();

			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Login", typeof(string));
			dataTable.Columns.Add("#", typeof(int));
			//dataTable.Columns[2].Size =

			dataGrid1.DataSource = dataTable;
			dataGrid1.ReadOnly = true;

			//RefreshList();//initial refresh

			NoxMemoryHack.Instance.Players.PlayerJoined += new NoxMemoryHack.PlayerMemory.PlayerEvent(PlayerJoined);
			NoxMemoryHack.Instance.Players.PlayerLeft += new NoxMemoryHack.PlayerMemory.PlayerEvent(PlayerLeft);

			LoadBanList();
		}

		public void RefreshList()
		{
			if (!Created) return;
			dataTable.Rows.Clear();
			foreach (Player player in NoxMemoryHack.Instance.Players.PlayerList)
				if (player.Connected)
					PlayerJoined(this, new NoxMemoryHack.PlayerMemory.PlayerEventArgs((Player) player.Clone()));
		}

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.boxPlayerId = new System.Windows.Forms.TextBox();
			this.buttonKick = new System.Windows.Forms.Button();
			this.buttonBan = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonSaveBan = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.CaptionVisible = false;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(256, 376);
			this.dataGrid1.TabIndex = 1;
			// 
			// boxPlayerId
			// 
			this.boxPlayerId.Location = new System.Drawing.Point(224, 8);
			this.boxPlayerId.Name = "boxPlayerId";
			this.boxPlayerId.Size = new System.Drawing.Size(24, 20);
			this.boxPlayerId.TabIndex = 2;
			this.boxPlayerId.Text = "";
			// 
			// buttonKick
			// 
			this.buttonKick.Location = new System.Drawing.Point(136, 8);
			this.buttonKick.Name = "buttonKick";
			this.buttonKick.Size = new System.Drawing.Size(40, 23);
			this.buttonKick.TabIndex = 3;
			this.buttonKick.Text = "Kick";
			this.buttonKick.Click += new System.EventHandler(this.buttonKick_Click);
			// 
			// buttonBan
			// 
			this.buttonBan.Location = new System.Drawing.Point(176, 8);
			this.buttonBan.Name = "buttonBan";
			this.buttonBan.Size = new System.Drawing.Size(40, 23);
			this.buttonBan.TabIndex = 4;
			this.buttonBan.Text = "Ban";
			this.buttonBan.Click += new System.EventHandler(this.buttonBan_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonSaveBan);
			this.panel1.Controls.Add(this.buttonBan);
			this.panel1.Controls.Add(this.buttonKick);
			this.panel1.Controls.Add(this.boxPlayerId);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 336);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(256, 40);
			this.panel1.TabIndex = 5;
			// 
			// buttonSaveBan
			// 
			this.buttonSaveBan.Location = new System.Drawing.Point(8, 8);
			this.buttonSaveBan.Name = "buttonSaveBan";
			this.buttonSaveBan.Size = new System.Drawing.Size(80, 23);
			this.buttonSaveBan.TabIndex = 5;
			this.buttonSaveBan.Text = "Save BanList";
			this.buttonSaveBan.Click += new System.EventHandler(this.buttonSaveBan_Click);
			// 
			// PlayerList
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "PlayerList";
			this.Size = new System.Drawing.Size(256, 376);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonKick_Click(object sender, System.EventArgs e)
		{
			NoxMemoryHack.KickPlayer(Int32.Parse(boxPlayerId.Text));
		}

		private void buttonBan_Click(object sender, System.EventArgs e)
		{
			BanPlayer(Int32.Parse(boxPlayerId.Text));
		}

		//TODO: make these settings optional
		public bool AutoKickBanned = true;
		public bool AutoKickUnkickables = true;

		private void PlayerJoined(object sender, NoxShared.NoxMemoryHack.PlayerMemory.PlayerEventArgs e)
		{
			//this is necessary for thread safety, since background thread will call this
			if (InvokeRequired)
			{
				Invoke(new NoxMemoryHack.PlayerMemory.PlayerEvent(PlayerJoined),new object[] {sender, e});
				return;
			}

			AppConsole.WriteLine("{0} has joined the game.", e.Player.Name);
			if (e.Player.Unkickable)
			{
				AppConsole.WriteLine("{0} has an 'unkickable' name.", e.Player.Name);
				AppConsole.WriteLine("Fixing {0}'s 'unkickable' name.", e.Player.Name);
				NoxMemoryHack.Fixes.FixUnkickable(e.Player.Number);
			}
            if (e.Player.Login.Length > 9)
            {
                AppConsole.WriteLine("{0} has an fake login.", e.Player.Name);
                AppConsole.WriteLine("Kicking {0}", e.Player.Name);
                NoxMemoryHack.KickPlayer(e.Player.Number);
            }
            if (e.Player.Name.Contains("HurtsMore"))
            {
                AppConsole.WriteLine("{0} has an crash char.", e.Player.Name);
                AppConsole.WriteLine("Kicking {0}", e.Player.Name);
                NoxMemoryHack.KickPlayer(e.Player.Number);
            }
            // ADD A CLOTHING CHECK HERE

			Player orig = (Player) BannedPlayers[e.Player.Serial];
			if (AutoKickBanned && orig != null)
			{
				AppConsole.WriteLine("{0} was banned under the alias {1}, kicking...", e.Player.Name, orig.Name);
				NoxMemoryHack.KickPlayer(e.Player.Number);
			}
			else if (e.Player.Unkickable && AutoKickUnkickables)
			{
					AppConsole.WriteLine("Autokicking {0} for 'unkickable' name.", e.Player.Name);
					NoxMemoryHack.KickPlayer(e.Player.Number);
			}

			if (Created)
				dataTable.Rows.Add(new object[] {e.Player.Name, e.Player.Login, e.Player.Number});
		}

		private void PlayerLeft(object sender, NoxShared.NoxMemoryHack.PlayerMemory.PlayerEventArgs e)
		{
			//this is necessary for thread safety, since background thread will call this
			if (InvokeRequired)
			{
				Invoke(new NoxMemoryHack.PlayerMemory.PlayerEvent(PlayerLeft),new object[] {sender, e});
				return;
			}

			AppConsole.WriteLine("{0} has left the game.", e.Player.Name);
			if (Created)
				for (int ndx = 0; ndx < dataTable.Rows.Count; ndx++)
					if ((int) ((DataRow) dataTable.Rows[ndx]).ItemArray[2] == e.Player.Number)//FIXME: hardcoded index
					{
						dataTable.Rows.RemoveAt(ndx);
						break;//there should only be one, so we can quit looking now
					}
		}

		protected void SaveBanList()
		{
			RegistryKey noxReg = Registry.LocalMachine.OpenSubKey("Software\\Westwood\\Nox");
			string path = "ban.xml";
			if (noxReg != null)
				path = ((string) noxReg.GetValue("InstallPath")).Replace("Nox.EXE", "") + path;

			if (File.Exists(path))
				File.Copy(path, path + ".backup", true);

			try
			{
				XmlTextWriter wtr = new XmlTextWriter(path, null);
				wtr.Formatting = Formatting.Indented;
				wtr.WriteStartDocument();

				wtr.WriteStartElement("BanList");
				XmlSerializer ser = new XmlSerializer(typeof(Player));
				foreach (Player player in BannedPlayers.Values)
					ser.Serialize(wtr, player);
				wtr.WriteEndElement();

				wtr.WriteEndDocument();
				wtr.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("Couldn't write banlist to '" + path + "'", "Error");
			}
		}

		protected void LoadBanList()
		{
			RegistryKey noxReg = Registry.LocalMachine.OpenSubKey("Software\\Westwood\\Nox");
			string path = "ban.xml";
			if (noxReg != null)
				path = ((string) noxReg.GetValue("InstallPath")).Replace("Nox.EXE", "") + path;

			if (!File.Exists(path))
				return;

			try
			{
				XmlTextReader rdr = new XmlTextReader(path);
				rdr.ReadStartElement("BanList");
				XmlSerializer ser = new XmlSerializer(typeof(Player));
				while (rdr.Read())
				{
					if (rdr.Name == "Player")
					{
						Player player = (Player) ser.Deserialize(rdr);
						BannedPlayers.Add(player.Serial, player);
					}
				}
				rdr.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("There was a problem loading '" + path + "', ban list not restored!", "Warning");
			}
		}
		
		public void BanPlayer(int playerId)
		{
			Player player = (Player) NoxMemoryHack.Instance.Players.PlayerList[playerId];
			BannedPlayers.Add(player.Serial, player);
			NoxMemoryHack.BanPlayer(player.Number);
		}

		private void buttonSaveBan_Click(object sender, System.EventArgs e)
		{
			SaveBanList();
		}
	}
}
