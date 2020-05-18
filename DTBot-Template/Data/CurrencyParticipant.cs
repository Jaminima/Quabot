using DTBot_Template.Data._MySQL;
using System;
using System.Collections.Generic;

namespace DTBot_Template.Data
{
    public class CurrencyParticipant : SQLObj
    {
        #region Fields

        public uint currencyid;
        public string discord_guild, twitch_name;
        public uint streamerid;

        #endregion Fields

        #region Constructors

        public CurrencyParticipant()
        {
            Table = "currency_participants";
        }

        #endregion Constructors

        #region Methods

        public static CurrencyParticipant FindDiscord(string GuildID)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", GuildID)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_participants WHERE currency_participants.discord_guild = @0", Params);

            if (Data.Count == 0) return null;

            CurrencyParticipant u = new CurrencyParticipant();

            u.SetValues(Data[0]);

            return u;
        }

        public static CurrencyParticipant FindTwitch(string TwitchName)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", TwitchName)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_participants WHERE currency_participants.twitch_name = @0", Params);

            if (Data.Count == 0) return null;

            CurrencyParticipant u = new CurrencyParticipant();

            u.SetValues(Data[0]);

            return u;
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            currencyid = uint.Parse(Data[0].ToString());
            streamerid = uint.Parse(Data[1].ToString());
            discord_guild = Data[2].ToString();
            twitch_name = Data[3].ToString();
        }

        #endregion Methods
    }
}