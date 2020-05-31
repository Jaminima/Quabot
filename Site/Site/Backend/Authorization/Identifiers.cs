using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Site.Backend.Data._MySQL.Objects;

namespace Site.Backend.Authorization
{
    public static class Identifiers
    {
        public static List<Ident> idents = new List<Ident>();
        private static Random rnd = new Random();

        public static Ident FindIdent(string ident)
        {
            Ident i = idents.Find(x => x.ident_string == ident);
            idents.Remove(i);
            return i;
        }

        public static string AddIdent(Streamer S, uint cuid)
        {
            string _str = RandString();
            idents.Add(new Ident(S, cuid, _str));
            return _str;
        }

        private static string RandString()
        {
            string s = "";
            for (uint i = 0;i<16;i++)
            {
                switch (rnd.Next(0, 3))
                {
                    case 0: s += (char)rnd.Next(65, 90); break;
                    case 1: s += (char)rnd.Next(97, 122); break;
                    case 2: s += (char)rnd.Next(48, 57); break;
                }
            }
            return s;
        }
    }

    public class Ident
    {
        public Streamer streamer;
        public uint CurrencyConfig;
        public string ident_string;

        public Ident(Streamer S, uint cuid, string ident)
        {
            CurrencyConfig = cuid;
            streamer = S;
            ident_string = ident;
        }
    }
}
