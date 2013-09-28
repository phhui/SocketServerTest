﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server;
    /// <summary>
    /// Socket 服务端例子
    /// 这个例子仅仅处理一个 客户端的访问， 处理完毕后退出.
    /// 服务端的输出为：
    /// 开始侦听 8088 端口……
    /// 接收到客户的连接
    /// 接收到来自客户端的数据为：1
    /// 客户端的输入为：
    /// telnet 127.0.0.1 8088
    /// 失去了跟主机的连接。
    /// </summary>
    public delegate void ProcessDelegate(string s);
    public delegate int callDelegate(byte[] b);
    class SocketServer
    {
        /// <summary>
        /// 字符编码处理.
        /// </summary>
        private static readonly Encoding ASCII;
        /// <summary>
        /// 用于 监听的端口.
        /// </summary>
        private const int PORT = 8888;
        //监听线程
        private Thread th;
        private ProcessDelegate nd;
        private callDelegate call;
        static SocketServer()
        {
            ASCII = Encoding.ASCII;
        }
        public void StartServer(ProcessDelegate trace)
        {
            th = new Thread(new ThreadStart(runServer));
            th.IsBackground = true;
            th.Start();
            nd = new ProcessDelegate(trace);
        }
        public void callback(string str)
        {
            nd(str);
        }
        public void send(string str)
        {
            call(Utils.GetBytes(str));
        }
        private void runServer()
        {
            TcpListener myListener = new TcpListener(IPAddress.Parse("127.0.0.1"), PORT);
            myListener.Start();
            callback("开始侦听  端口……" + PORT.ToString());
            while (true)
            {
                if (myListener.Pending())
                {
                    //Socket mySocket = myListener.AcceptSocket();
                    //call = new callDelegate(mySocket.Send);
                    //callback("接收到客户的连接");
                    //Byte[] recvBytes = new Byte[256];
                    //Int32 bytes = mySocket.Receive(recvBytes, recvBytes.Length, SocketFlags.None);
                    //String str = Utils.GetString(recvBytes);
                    //callback("接收：" + str);
                    TcpClient tc = myListener.AcceptTcpClient();
                    call = new callDelegate(tc.Client.Send);
                    Thread th = new Thread(new ParameterizedThreadStart(MyFun));
                    Client cl = new Client(tc, th);
                    Utils.clients.Add(cl);
                    th.Start(cl);
                    callback("客户端连接上来" + tc.Client.RemoteEndPoint);
                }
                else
                {
                    Thread.Sleep(500);
                    //callback("休眠5秒");
                }
            }
        }
        private void MyFun(object client)
        {
            Client ck = client as Client;
            NetworkStream sk = ck.Tc.GetStream();
            while (true)
            {
                try
                {
                    byte[] buff = new byte[1024];
                    sk.Read(buff, 0, buff.Length);
                    string data = Utils.GetString(buff);
                    if (data != "")
                    {
                        //callback("收到客户端" + ck.Tc.Client.RemoteEndPoint + "信息:" + data);
                        if (data.IndexOf("policy") > -1)
                        {
                            callback("<?xml version='1.0'?><cross-domain-policy><allow-access-from domain='*' to-ports='*' /></cross-domain-policy>");
                        }
                        else
                        {
                            callback(ck.NickName + ":" + data);
                        }
                    }
                    else
                    {
                        callback("未知数据");
                    }
                }
                catch (Exception e)
                {
                    callback("用户" + ck.NickName + "断开连接。"+e.Data);
                    sk.Close();
                    ck.th.Abort();
                }
            }
        }
    }