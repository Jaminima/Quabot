using Newtonsoft.Json.Linq;

namespace DTBot_Template.Intergrations
{
    public static class Streamlabs
    {
        #region Methods

        public static JToken CreateDonation(string Donor_Name, uint amount, Data.OAuth _Config, string CurCode = "GBP", string Message = "")
        {
            return WebRequests.PerformReq("https://streamlabs.com/api/v1.0/donations", "POST",
                Content: string.Format("name={0}&identifier={1}&amount={2:0.00}&currency={3}&message={4}&access_token={5}", Donor_Name, Donor_Name, amount, CurCode, Message, _Config.Token));
        }

        public static JToken GetDonations(Data.OAuth _Config)
        {
            return WebRequests.PerformReq("https://streamlabs.com/api/v1.0/donations?access_token=" + _Config.Token);
        }

        public static JToken PlayAlert(Data.OAuth _Config, string Title = "", string SubText = "", string imageUrl = "", string soundUrl = "", uint Duration = 5)
        {
            return WebRequests.PerformReq("https://streamlabs.com/api/v1.0/alerts", "POST",
                Content: string.Format("access_token={0}&type=donation&message={1}&user_message={2}&image_href={3}&sound_href={4}&duration={5}", _Config.Token, Title, SubText, imageUrl, soundUrl, Duration));
        }

        #endregion Methods
    }
}