using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Site.Backend.Data
{
    public class Config
    {
        #region Fields

        public static Config Conf; 

        private const string conf_Path = "./Data/Config.json";
        public string sql_Server, sql_Username, sql_Password,
            oAuthRedirect,
            discord_bot_client_id, discord_bot_client_secret,
            twitch_sign_client_id, twitch_sign_client_secret;

        public uint CacheTimeout;

        #endregion Fields

        #region Methods

        public static Config Load()
        {
            try { return JToken.Parse(File.ReadAllText(conf_Path)).ToObject<Config>(); }
            catch
            {
                Console.WriteLine("Failed To Load Config File");
                new Config().Save();
            }
            return null;
        }

        public void Save()
        {
            File.WriteAllText(conf_Path, JToken.FromObject(this).ToString());
        }

        #endregion Methods
    }
}
