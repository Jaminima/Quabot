using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Site.Backend.Data;

namespace Site.Backend.Integrations
{
    public static class Discord
    {
        public static async Task<JToken> GetUser(string AuthToken)
        {
            return await Web.DoReq("https://discord.com/api/users/@me",
                Headers: new string[] { "Authorization", "Bearer " + AuthToken, "Client-ID", Config.Conf.discord_bot_client_id });
        }
    }
}
