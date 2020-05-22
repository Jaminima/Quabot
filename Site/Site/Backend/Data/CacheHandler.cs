using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Site.Backend.Data._MySQL.Objects;
using Site.Backend.Data.Generics;

namespace Site.Backend.Data
{
    public static class DataHandler
    {
        #region Fields

        private static Dictionary<_userInfo, DateTime> _userCache = new Dictionary<_userInfo, DateTime>();

        #endregion Fields

        #region Methods

        private static T Find<T>(Dictionary<T, DateTime> Cache, Func<T, bool> Pred)
        {
            foreach (T Key in Cache.Keys)
            {
                if ((DateTime.Now - Cache[Key]).TotalSeconds > 60) Cache.Remove(Key);
                else if (Pred(Key)) return Key;
            }
            return default(T);
        }

        private static T[] FindAll<T>(Dictionary<T, DateTime> Cache, Func<T, bool> Pred)
        {
            List<T> temp = new List<T>();
            foreach (T Key in Cache.Keys)
            {
                if ((DateTime.Now - Cache[Key]).TotalSeconds > 60) Cache.Remove(Key);
                else if (Pred(Key)) temp.Add(Key);
            }
            return temp.ToArray();
        }

        private static _userInfo AddUser(_userInfo user)
        {
            user.Insert();
            user = _userInfo.Find(user.user.user, user.currency);
            _userCache.Add(user, DateTime.Now);
            return user;
        }

        public static Streamer FindStreamer(uint streamerid)
        {
            
               return Streamer.Find(streamerid);
        }

        public static Streamer FindStreamer(string twitch_name, string twitch_email)
        {
            Streamer _streamer = Streamer.Find(twitch_name);

            if (_streamer == null)
            {
                _streamer = new Streamer(twitch_name, twitch_email);
                _streamer.Insert();
            }

            return _streamer;
        }

        public static UserAccount FindAccount(User User)
        {
            UserAccount _userAcc = UserAccount.Find(User);

            if (_userAcc == null)
            {
                _userAcc = new UserAccount(User);
                _userAcc.Insert();
            }

            return _userAcc;
        }

        public static UserAccount FindAccount(uint AccId)
        {
            return UserAccount.Find(AccId);
        }

        public static CurrencyConfig FindCurrency(uint currencyId)
        {

            return CurrencyConfig.Find(currencyId);
        }

        public static CurrencyConfig[] FindCurrencyByStreamer(uint streamerid)
        {
            return CurrencyConfig.FindByStreamer(streamerid);
        }

        public static CurrencyConfig FindCurrency(string sIdentifier, Source source)
        {
            CurrencyParticipant _participant;

            if (source == Generics.Source.Discord) _participant = CurrencyParticipant.FindDiscord(sIdentifier);
            else _participant = CurrencyParticipant.FindTwitch(sIdentifier);

            return FindCurrency(_participant.currencyid);
        }

        public static _userInfo FindUser(User user, CurrencyConfig currency)
        {
            _userInfo _uInfo = Find(_userCache, x => x.user.Equals(user) && x.currency == currency.Id);

            if (_uInfo == null)
            {
                _uInfo = _userInfo.Find(user, currency.Id);
                if (_uInfo != null) _userCache.Add(_uInfo, DateTime.Now);
            }

            if (_uInfo == null)
            {
                _uInfo = new _userInfo(user, currency);
                return AddUser(_uInfo);
            }

            return _uInfo;
        }

        public static _userInfo[] FindUsers(User[] users, CurrencyConfig currency)
        {
            _userInfo[] _uInfos = new _userInfo[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                _uInfos[i] = FindUser(users[i], currency);
            }

            return _uInfos;
        }

        #endregion Methods
    }
}
