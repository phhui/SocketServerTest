using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class SockDataDecode
    {
        private static int index;
        private static ArrayList al;
        public static ArrayList decode(byte[] b)
        {
            index = 0;
            al = new ArrayList();
            int key = Utils.getInt(b, 0);
            index += 4;
            ArrayList type = KeyType.getType(key);
            int n = type.Count;
            decodeType(type, b);
            return al;
        }
        private static void decodeType(string t,byte[] b){
            switch (t)
            {
                case "int":
                    al.Add(Utils.getInt(b, index));
                    index += 4;
                    break;
                case "double":
                    al.Add(Utils.getDouble(b, index));
                    index += 8;
                    break;
                case "long":
                    al.Add(Utils.getLong(b, index));
                    index += 8;
                    break;
                case "string":
                    int l = Utils.getInt(b, index);
                    al.Add(Utils.GetString(b, index));
                    index += l + 2;
                    break;
            }
        }
        private static void decodeType(ArrayList t, byte[] b)
        {
            int n = t.Count;
            for (int i = 0; i < n; i++)
            {
                if (t[i] is String)
                {
                    decodeType(t[i].ToString(), b);
                }
                else if (t[i] is ArrayList)
                {
                    int m = Utils.getInt(b, index);
                    index += 4;
                    for (int j = 0; j < m; j++)
                    {
                        decodeType(t[i] as ArrayList, b);
                    }
                }
            }
        }
    }
}
