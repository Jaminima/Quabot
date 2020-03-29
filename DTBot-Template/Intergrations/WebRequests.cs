using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace DTBot_Template.Intergrations
{
    public static class WebRequests
    {
        #region Methods

        public static JToken PerformReq(string URL, string Method = "GET", Tuple<string, string>[] Headers = null, string Content = null)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = Method;
            req.ContentType = "application/x-www-form-urlencoded";

            if (Headers != null)
            {
                foreach (Tuple<string, string> Head in Headers) req.Headers.Add(Head.Item1, Head.Item2);
            }

            if (Content != null)
            {
                Stream _stream;
                try { _stream = req.GetRequestStream(); }
                catch (Exception e) { Console.WriteLine(e); return null; }

                StreamWriter streamReq = new StreamWriter(_stream);
                streamReq.Write(Content);
                streamReq.Flush();
                streamReq.Close();
            }

            WebResponse resp;
            try { resp = req.GetResponse(); }
            catch (Exception e) { Console.WriteLine(e); return null; }

            StreamReader streamRes = new StreamReader(resp.GetResponseStream());
            string Data = streamRes.ReadToEnd();
            JToken token = JToken.Parse(Data);

            return token;
        }

        #endregion Methods
    }
}