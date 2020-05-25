using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Site.Backend.Data;

namespace Site.Backend.Integrations
{
    public static class Twitch
    {
        public static async Task<JToken> GetUser(string AuthToken)
        {
            return await Web.DoReq("https://api.twitch.tv/helix/users",
                Headers:new string[] { "Authorization", "Bearer " + AuthToken, "Client-ID",Config.Conf.twitch_sign_client_id });
        }
    }
}
