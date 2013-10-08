using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public static string GetString(byte[] b,int n)
        {
            string result = "";
            int l = Utils.getInt(b, n);
            byte[] by = new byte[l];
            n += 2;
            int i=0;
            while (l > 0)
            {
                by[i] = b[n];
                n++;
                i++;
                l--;
            }
            result = UnicodeEncoding.UTF8.GetString(by).Replace("\0", "");
            return result;
        }
        public static byte[] GetBytes(string data)
        {
            return UnicodeEncoding.UTF8.GetBytes(data);
        }
        public static int getInt(byte[] b,int i)
        {
            return Convert.ToInt32(b[i+3]);
        }
        public static long getLong(byte[] b, int i)
        {
            return System.BitConverter.ToInt64(b, i);
        }
        public static bool getBool(byte[] b, int i)
        {
            return System.BitConverter.ToBoolean(b, i);
        }
        public static Char getChar(byte[] b, int i)
        {
            return System.BitConverter.ToChar(b, i);
        }
        public static double getDouble(byte[] b, int i)
        {
            return System.BitConverter.ToDouble(b, i);
        }
        public static short getShort(byte[] b, int i)
        {
            return System.BitConverter.ToInt16(b, i);
        }
        public static float getFloat(byte[] b, int i)
        {
            return System.BitConverter.ToSingle(b, i);
        }
        public static uint getUint(byte[] b, int i)
        {
            return System.BitConverter.ToUInt32(b, i);
        }
    }
}
