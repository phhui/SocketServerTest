using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class KeyType
    {
        private static Dictionary<int, ArrayList> dict = new Dictionary<int, ArrayList>();
        public static ArrayList getType(int key)
        {
            return dict[key];
        }
        public static void addType(int key, ArrayList al)
        {
            if (dict.ContainsKey(key)) return;
            dict.Add(key, al);
        }
    }
}
