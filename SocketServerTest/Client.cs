using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Client
    {
        public TcpClient Tc = null;
        public Thread th = null;
        public string NickName = "";
        public int No = 0;
        public int _x = 0;
        public int _y = 0;

        public Client(TcpClient tc,Thread t)
        {
            Tc = tc;
            th = t;
            No = Utils.clients.Count;
            NickName = "сн©м_" + Utils.clients.Count;
        }
    }
}
