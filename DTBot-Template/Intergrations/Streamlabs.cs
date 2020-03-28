using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DTBot_Template.Intergrations
{
    public static class Streamlabs
    {
        public static JToken GetDonations(Data.OAuth _Config)
        {
            return WebRequests.PerformReq("https://streamlabs.com/api/v1.0/donations?access_token=" + _Config.Token);
        }
    }
}
