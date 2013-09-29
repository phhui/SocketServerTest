using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocketServerTest
{
    public partial class Form1 : Form
    {
        public delegate void MyInvoke(string str);
        private SocketServer s;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            s = new SocketServer();
            s.StartServer(showText);
            btn_start.Enabled = false;
        }
        public void showText(string str)
        {
            if (txt_output.InvokeRequired)
            {
                MyInvoke invoke = new MyInvoke(showText);
                this.Invoke(invoke, str);
            }
            else
            {
                txt_output.Text += str + " [" + DateTime.Now + "]\r\n";
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (btn_start.Enabled) return;
            if (txt_send.Text.Length < 1) return ;
            s.send(txt_send.Text);
            showText("广播：" + txt_send.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string str="";
            for(int i=0;i<50000;i++){
                str += "public var p" + i.ToString() + ":String;\r\n";
            }
            File.WriteAllText("a.txt",str);
        }
    }
}
