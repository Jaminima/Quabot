using DTBot_Template.Data;
using DTBot_Template.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTBot_Template.Events
{
    public static class Rewards
    {
        public static Dictionary<_userInfo, DateTime> MessageRewards = new Dictionary<_userInfo, DateTime>();

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
