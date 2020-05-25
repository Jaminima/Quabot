using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Site.Backend.Integrations
{
    public static class Web
    {
        public static async Task<JToken> DoReq(string URL, string Method="GET", string[] Headers=null)
        {
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = Method;

            for (uint i = 0; i < Headers?.Length; i += 2) Req.Headers.Add(Headers[i], Headers[i + 1]);

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
