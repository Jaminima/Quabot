using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace DTBot_Template.Data
{
    public class BotConfig
    {
        #region Fields

        private const string conf_Path = "./Data/Config.json";
        public OAuth Streamlabs;
        public string twitch_Username, twitch_token, twitch_Channel, discord_Token;

        #endregion Fields

        #region Methods

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

        public void Save()
        {
            File.WriteAllText(conf_Path, JToken.FromObject(this).ToString());
        }

        #endregion Methods
    }

    public class OAuth
    {
        #region Fields

        public string Token, client_Id, client_Secret, token_type;
        public bool TokenIsRefresh = false;

        #endregion Fields
    }
}