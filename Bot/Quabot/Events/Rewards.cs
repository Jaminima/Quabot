using DTBot_Template.Data;
using DTBot_Template.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DTBot_Template.Events
{
    public static class Rewards
    {
        #region Methods

        private static bool CanReward(_userInfo User, CurrencyConfig currency)
        {
            KeyValuePair<_userInfo, DateTime>[] U = MessageRewards.Where(x => x.Key.user.Equals(User.user) && x.Key.currency == User.currency).ToArray();

            if (U.Length == 0) { MessageRewards.Add(User, DateTime.Now); return true; }
            else if ((DateTime.Now - U[0].Value).TotalSeconds > currency.MessageRewardDelay) { MessageRewards[U[0].Key] = DateTime.Now; return true; }

            return false;
        }

        private static async void DoFish(Fisher fisher)
        {
            FishReward reward = fisher.currency.FishRewards[Controller.rnd.Next(0, fisher.currency.FishRewards.Length)];

            fisher.User = _userInfo.Find(fisher.User.user.user, fisher.currency.Id);

            fisher.User.balance += reward.Reward;
            fisher.User.Update();

            await fisher.Bot.SendMessage(fisher.command, "{User} You Caught a {String} Worth {Value} {Currency}", fisher.currency, reward.Reward, reward.Name);
            FisherMen.Remove(fisher);
        }

        #endregion Methods

        #region Fields

        public static Dictionary<Fisher, DateTime> FisherMen = new Dictionary<Fisher, DateTime>();
        public static Dictionary<_userInfo, DateTime> MessageRewards = new Dictionary<_userInfo, DateTime>();

        #endregion Fields

        public static bool AddFisher(BaseBot Bot, Command command, _userInfo User, CurrencyConfig currency)
        {
            if (FisherMen.Count(x => x.Key.User.user.user.Equals(User.user.user) && x.Key.currency.Id == currency.Id) == 0) { FisherMen.Add(new Fisher(Bot, command, User, currency), DateTime.Now); return true; }
            return false;
        }

        public static void MessageRewardUser(_userInfo User)
        {
            CurrencyConfig C = CacheHandler.FindCurrency(User.currency);
            if (CanReward(User, C))
            {
                User.balance += C.MessageReward;
                User.Update();
            }
        }

        public static void RewardChecker()
        {
            while (true)
            {
                FisherMen.Where(x => (DateTime.Now - x.Value).TotalSeconds > CacheHandler.FindCurrency(x.Key.User.currency).FishWait).ToList().ForEach(x => DoFish(x.Key));
                Thread.Sleep(5000);
            }
        }
    }

    public class Fisher
    {
        #region Fields

        public BaseBot Bot;
        public Command command;
        public CurrencyConfig currency;
        public _userInfo User;

        #endregion Fields

        #region Constructors

        public Fisher(BaseBot Bot, Command command, _userInfo User, CurrencyConfig currency)
        {
            this.Bot = Bot;
            this.command = command;
            this.User = User;
            this.currency = currency;
        }

        #endregion Constructors
    }
}