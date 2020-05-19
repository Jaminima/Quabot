using DTBot_Template.Data._MySQL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DTBot_Template.Data
{
    public class CurrencyConfig : _MySQL.SQLObj
    {
        #region Fields

        public string name;
        public Dictionary<string, string> SimpleResponses;
        public Dictionary<string, Emote> CustomEmotes;
        public FishReward[] FishRewards;

        public uint FishWait;
        public uint FishCost;

        public uint MessageRewardDelay;
        public uint MessageReward;

        public uint DefaultBalance;

        #endregion Fields

        #region Constructors

        public CurrencyConfig()
        {
            Table = "currency_config";
        }

        #endregion Constructors

        #region Methods

        public static CurrencyConfig Find(uint curid)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", curid)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_config WHERE currency_id = @0", Params);

            if (Data.Count == 0) return null;

            CurrencyConfig u = new CurrencyConfig();

            u.SetValues(Data[0]);

            return u;
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            return new object[] { Id, null, name };
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            name = Data[2].ToString();

            SimpleResponses = Data[3].ToString().TrimEnd(';').Split(';').ToDictionary(x=>x.Split('¬')[0], x=>x.Split('¬')[1]);

            CustomEmotes = Data[4].ToString().TrimEnd(';').Split(';').ToDictionary(x => '{'+x.Split('¬')[0]+'}',x => new Emote(x.Split('¬')[2], x.Split('¬')[1]));

            FishRewards = Data[5].ToString().TrimEnd(';').Split(';').Select(x => new FishReward(x.Split('¬')[0], uint.Parse(x.Split('¬')[1]), uint.Parse(x.Split('¬')[2]))).ToArray();

            FishWait = uint.Parse(Data[6].ToString());
            FishCost = uint.Parse(Data[7].ToString());

            MessageRewardDelay = uint.Parse(Data[8].ToString());
            MessageReward = uint.Parse(Data[9].ToString());

            DefaultBalance = uint.Parse(Data[10].ToString());
        }

        #endregion Methods

        //public override void Update()
        //{
        //    List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
        //        new Tuple<string, object>("@0", name),
        //        new Tuple<string, object>("@1", Id)
        //    };
        //    SQL.pubInstance.Execute("UPDATE currency_config SET currency_name = @0 WHERE currency_id=@1", Params);
        //}
    }

    public class Emote
    {
        public string Twitch, Discord;

        public Emote(string Twitch, string Discord)
        {
            this.Twitch = Twitch;
            this.Discord = Discord;
        }
    }

    public class FishReward
    {
        public string Name;
        public uint Reward, Odds;

        public FishReward(string Name, uint Odds, uint Reward)
        {
            this.Name = Name;
            this.Odds = Odds;
            this.Reward = Reward;
        }
    }
}