using DTBot_Template.Data._MySQL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTBot_Template.Data
{
    public class CurrencyConfig : _MySQL.SQLObj
    {
        #region Fields

        public Dictionary<string, Emote> CustomEmotes;
        public uint DefaultBalance;
        public uint FishCost;
        public FishReward[] FishRewards;
        public uint FishWait;
        public uint MessageReward;
        public uint MessageRewardDelay;
        public string name;
        public Dictionary<string, string> SimpleResponses;

        public string[] BalanceCommands;
        public string[] PayCommands;
        public string[] FishCommands;
        public string[] GambleCommands;

        public uint GambleOdds;

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

            SimpleResponses = FormatSet(Data[3]).ToDictionary(x => x.Split('¬')[0], x => x.Split('¬')[1]);

            CustomEmotes = FormatSet(Data[4]).ToDictionary(x => '{' + x.Split('¬')[0] + '}', x => new Emote(x.Split('¬')[2], x.Split('¬')[1]));

            FishRewards = FormatSet(Data[5]).Select(x => new FishReward(x.Split('¬')[0], uint.Parse(x.Split('¬')[1]), uint.Parse(x.Split('¬')[2]))).ToArray();

            FishWait = uint.Parse(Data[6].ToString());
            FishCost = uint.Parse(Data[7].ToString());

            MessageRewardDelay = uint.Parse(Data[8].ToString());
            MessageReward = uint.Parse(Data[9].ToString());

            DefaultBalance = uint.Parse(Data[10].ToString());

            BalanceCommands = FormatSet(Data[11]);
            PayCommands = FormatSet(Data[12]);
            FishCommands = FormatSet(Data[13]);
            GambleCommands = FormatSet(Data[14]);

            GambleOdds = uint.Parse(Data[15].ToString());
        }

        private string[] FormatSet(object O)
        {
            return O.ToString().TrimEnd(';').Split(';');
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
        #region Fields

        public string Twitch, Discord;

        #endregion Fields

        #region Constructors

        public Emote(string Twitch, string Discord)
        {
            this.Twitch = Twitch;
            this.Discord = Discord;
        }

        #endregion Constructors
    }

    public class FishReward
    {
        #region Fields

        public string Name;
        public uint Reward, Odds;

        #endregion Fields

        #region Constructors

        public FishReward(string Name, uint Odds, uint Reward)
        {
            this.Name = Name;
            this.Odds = Odds;
            this.Reward = Reward;
        }

        #endregion Constructors
    }
}