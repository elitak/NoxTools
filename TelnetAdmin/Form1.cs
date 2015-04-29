using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using SysopLibrary;

namespace TelnetAdmin
{

    public partial class Form1 : Form
    {
        public AsyncCallback myrecvCall;
        public Sysop telnet = new Sysop();
        //private NetworkStream networkStream;
        //private StreamReader streamReader;
        //private StreamWriter streamWriter;

        public Form1()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[255];
            System.Net.IPEndPoint IP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(txtIP.Text),Convert.ToInt32(txtPort.Text));
            telnet.Connect(IP,txtsysop.Text,txtlock.Text);
            timRecv.Enabled = true;
            
        }
        private void timRecv_Tick(object sender, EventArgs e)
        {
            if (telnet.cmddata == "")
                return;

            cmdline.Text += telnet.cmddata;
            telnet.cmddata = "";
            cmdline.SelectionStart = cmdline.Text.Length;
            cmdline.ScrollToCaret();
            }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            telnet.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listUsers.Items.Clear();
            string[] users = telnet.GetUsers();
            foreach (string i in users)
            {
                listUsers.Items.Add(i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            telnet.KickUser(listUsers.SelectedItem.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            telnet.BanUser(listUsers.SelectedItem.ToString());
        }

        private void txtcmd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                telnet.SendData(txtcmd.Text);
                txtcmd.Text = "";
            }
        }

        private void cboPlLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            telnet.SendData("set players " + cboPlLimit.SelectedItem.ToString() + "\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            telnet.Disconnect();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            telnet.SendData("set spell " + listSpells.SelectedItem.ToString() + " off\n");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            telnet.SendData("set spell " + listSpells.SelectedItem.ToString() + " on\n");
        }
}
}