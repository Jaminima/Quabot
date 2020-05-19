using DTBot_Template.Data;
using DTBot_Template.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DTBot_Template.Events
{
    public class Fisher
    {
        public BaseBot Bot;
        public Command command;
        public _userInfo User;
        public CurrencyConfig currency;

        public Fisher(BaseBot Bot, Command command, _userInfo User, CurrencyConfig currency)
        {
            this.Bot = Bot;
            this.command = command;
            this.User = User;
            this.currency = currency;
        }
    }

    public static class Rewards
    {
        public static Dictionary<_userInfo, DateTime> MessageRewards = new Dictionary<_userInfo, DateTime>();
        public static Dictionary<Fisher, DateTime> FisherMen = new Dictionary<Fisher, DateTime>();

        public static void RewardChecker()
        {
            while (true)
            {
                FisherMen.Where(x => (DateTime.Now - x.Value).TotalSeconds > CacheHandler.FindCurrency(x.Key.User.currency).FishWait).ToList().ForEach(x=>DoFish(x.Key));
                Thread.Sleep(5000);
            }
        }

        private static async void DoFish(Fisher fisher)
        {
            FishReward reward = fisher.currency.FishRewards[Controller.rnd.Next(0, fisher.currency.FishRewards.Length)];

            fisher.User.balance += reward.Reward;
            fisher.User.Update();

            await Controller.dBot.SendMessage(fisher.command, "{User} You Caught a {String} Worth {Value} {Currency}", fisher.currency, reward.Reward, reward.Name);
            FisherMen.Remove(fisher);
        }

        public static bool AddFisher(BaseBot Bot, Command command,_userInfo User, CurrencyConfig currency)
        {
            if (FisherMen.Count(x => x.Key.User.user.Equals(User.user) && x.Key.currency.Id == currency.Id) == 0) { FisherMen.Add(new Fisher(Bot,command,User,currency), DateTime.Now); return true; }
            return false;
        }

        private static bool CanReward(_userInfo User, CurrencyConfig currency)
        {
            KeyValuePair<_userInfo, DateTime>[] U = MessageRewards.Where(x => x.Key.user.Equals(User.user) && x.Key.currency == User.currency).ToArray();
            
            if (U.Length == 0) { MessageRewards.Add(User, DateTime.Now); return true; }
            else if ((DateTime.Now - U[0].Value).TotalSeconds > currency.MessageRewardDelay) { MessageRewards[U[0].Key] = DateTime.Now; return true; }

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
    }
}
