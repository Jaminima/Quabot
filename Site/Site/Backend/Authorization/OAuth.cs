using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Builder;
using System.IO;

namespace Site.Backend.Authorization
{
    public static class OAuth
    {
        public static async Task<JToken> PerformOAuth(string URL)
        {
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = "POST";

            try
            {
                WebResponse Res = await Req.GetResponseAsync();
                StreamReader stream = new StreamReader(Res.GetResponseStream());
                string Cont = stream.ReadToEnd();
                return JToken.Parse(Cont);
            }
            catch { return null; }
        }
    }
}
