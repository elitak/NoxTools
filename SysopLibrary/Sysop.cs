using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SysopLibrary
{
    public class Sysop
    {
        static Sysop current;
        public TcpClient mTelnetClient;// = new TcpClient();
        System.Threading.Thread mReceiveThread;// = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(RecvPacket));
        string mPassword;
        string mKey;
        public string cmddata;
        bool closed = true;

        List<string> mUsers = new List<string>();

        public Sysop(IPEndPoint IP, string pass, string key)
        {
            current = this;
            mPassword = pass;
            mKey = key;
            Connect(IP,pass,key);
        }
        public Sysop()
        {
            current = this;
        }
        public void Connect(IPEndPoint IP,string pass, string key)
        {
            if (!closed)
            {
                Disconnect();
            }
            cmddata += "Trying to connect\n";
            mTelnetClient = new TcpClient();
            mReceiveThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(RecvPacket));
            mPassword = pass;
            mKey = key;
            mTelnetClient.SendTimeout = 400;
            mTelnetClient.ReceiveTimeout = 400;
            mTelnetClient.BeginConnect(IP.Address,IP.Port,null,null);
            //mTelnetClient.Connect(IP);
            mTelnetClient.Client.Blocking = false;
            mReceiveThread.Start();
            closed = false;
        }
        ~Sysop()
        {
            
        }
        public void Disconnect()
        {
            if (!closed)
            {
                cmddata += "Connection Closed\n";
                mReceiveThread.Abort();
                mTelnetClient.Close();
                closed = true;
            }
        }
        private static void RecvPacket(object d)
        {
            byte[] data = new byte[1024];
            int i;
            while(System.Threading.Thread.CurrentThread.ThreadState != System.Threading.ThreadState.Aborted)
            {
                try {
                   i = Sysop.current.mTelnetClient.Client.Receive(data);
                   Sysop.current.ParsePacket(System.Text.Encoding.ASCII.GetString(data, 0, i));
                }
                catch(SocketException se)
                {
                }
                System.Threading.Thread.Sleep(100);
            }
            return;
        }
        private void ParsePacket(string packet)
        {
            cmddata += packet;
            bool readingUsers = false;
            foreach (string line in packet.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] words = line.Split(' ');
                switch (words[0])
                {
                    case "TELNET:":
                        break;
                    case "Password:":
                        mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes(mPassword + "\n")); //send password
                        if( mKey.Length >0 )
                            mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes(mKey + "\n")); //send key
                        break;
                    case "Victom:":
                        break;
                    case "Users:":
                        mUsers.Clear();
                        readingUsers = true;
                        break;
                    case ">":
                        break;
                    default:
                        if (readingUsers)
                            mUsers.Add(line);
                        break;
                }
            }
        }
        public string[] GetUsers()
        {
            //if (mTelnetClient.Connected)
            //{
                mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes("list users\n"));
                System.Threading.Thread.Sleep(1000);
                return mUsers.ToArray();
            //}
            //return null;
        }
        public void KickUser(string user)
        {
            mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes("kick \"" + user + "\"\n"));
            return;
        }
        public void BanUser(string user)
        {
            mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes("ban \"" + user + "\"\n"));
            return;
        }
        public void SendData(string data)
        {
            mTelnetClient.Client.Send(System.Text.Encoding.ASCII.GetBytes(data + "\n"));
            return;
        }
    }
}
