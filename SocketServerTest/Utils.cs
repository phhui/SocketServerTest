using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Utils
    {
        public static List<Client> clients = new List<Client>();

        public static string GetString(byte[] data)
        {
            string result = "";
            result = UnicodeEncoding.UTF8.GetString(data).Replace("\0", "");
            return result;
        }

        public static byte[] GetBytes(string data)
        {
            return UnicodeEncoding.UTF8.GetBytes(data);
        }

        public static void SendMsg(string data)
        {
            if (clients.Count > 0)
            {
                byte[] result = GetBytes(data + "\0");
                foreach (Client c in clients)
                {
                    try
                    {
                        c.Tc.GetStream().Write(result, 0, result.Length);
                    }
                    catch (Exception) { }
                }
            }
        }

        public static void SendMsg(string data, Client c)
        {
            if (clients.Count > 0)
            {
                byte[] result = GetBytes(data + "\0");
                foreach (Client cx in clients)
                {
                    try
                    {
                        if (cx != c)
                            cx.Tc.GetStream().Write(result, 0, result.Length);
                    }
                    catch (Exception) { }
                }
            }
        }

        public static void SendMsg(Client c, string data)
        {
            byte[] result = GetBytes(data + "\0");
            c.Tc.GetStream().Write(result, 0, result.Length);
        }
    }
}
