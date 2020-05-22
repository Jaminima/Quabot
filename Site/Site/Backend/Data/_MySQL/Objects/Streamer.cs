using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Backend.Data._MySQL.Objects
{
    public class Streamer:SQLObj
    {
        public string twitch_email, twitch_name;

        public Streamer()
        {
            Table = "streamer_account";
        }

        public Streamer(string name, string email)
        {
            Table = "streamer_account";
            twitch_name = name;
            twitch_email = email;
        }

        public static Streamer Find(string twitchname)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", twitchname)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM streamer_account WHERE twitch_name = @0", Params);

            if (Data.Count == 0) return null;

            Streamer u = new Streamer();

            u.SetValues(Data[0]);

            return u;
        }

        public static Streamer Find(uint strid)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", strid)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM streamer_account WHERE streamer_id = @0", Params);

            if (Data.Count == 0) return null;

            Streamer u = new Streamer();

            u.SetValues(Data[0]);

            return u;
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            twitch_email = Data[1].ToString();
            twitch_name = Data[2].ToString();
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            if (IncludeProtected) return new object[] { Id, twitch_email, twitch_name };
            else return new object[] { null, twitch_email, twitch_name };
        }
    }
}
