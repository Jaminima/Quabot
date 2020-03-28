using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DTBot_Template.Data
{
    public class BotConfig
    {
        public string twitch_Username, twitch_token, twitch_Channel, discord_Token;

        public OAuth Streamlabs;

        private const string conf_Path = "./Data/Config.json";

        public void Save()
        {
            File.WriteAllText(conf_Path, JToken.FromObject(this).ToString());
        }

        public static BotConfig Load()
        {
            try { return JToken.Parse(File.ReadAllText(conf_Path)).ToObject<BotConfig>(); }
            catch
            {
                Console.WriteLine("Failed To Load Config File");
                new BotConfig().Save();
            }
            return null;
        }
    }

    public class OAuth
    {
        public string Token, client_Id, client_Secret, token_type;
        public bool TokenIsRefresh = false;
    }
}
