using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Site.Backend.Data._MySQL.Objects;
using Site.Backend.Data.Generics;

namespace Site.Backend.Data
{
    public static class CacheHandler
    {
        #region Fields

        private static Dictionary<CurrencyConfig, DateTime> _currencyCache = new Dictionary<CurrencyConfig, DateTime>();
        private static Dictionary<CurrencyParticipant, DateTime> _currencyParticipantCache = new Dictionary<CurrencyParticipant, DateTime>();
        private static Dictionary<UserAccount, DateTime> _userAccCache = new Dictionary<UserAccount, DateTime>();
        private static Dictionary<_userInfo, DateTime> _userCache = new Dictionary<_userInfo, DateTime>();
        private static Dictionary<Streamer, DateTime> _streamerCache = new Dictionary<Streamer, DateTime>();

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

        private static UserAccount AddAccount(UserAccount user)
        {
            user.Insert();
            user = UserAccount.Find(user.user);
            _userAccCache.Add(user, DateTime.Now);
            return user;
        }

        private static _userInfo AddUser(_userInfo user)
        {
            user.Insert();
            user = _userInfo.Find(user.user.user, user.currency);
            _userCache.Add(user, DateTime.Now);
            return user;
        }

        private static Streamer AddStreamer(Streamer strmr)
        {
            strmr.Insert();
            strmr = Streamer.Find(strmr.twitch_name);
            _streamerCache.Add(strmr, DateTime.Now);
            return strmr;
        }

        public static Streamer FindStreamer(uint streamerid)
        {
            Streamer _streamer = Find(_streamerCache, x => x.Id == streamerid);

            if (_streamer == null)
            {
                _streamer = Streamer.Find(streamerid);
                if (_streamer != null) _streamerCache.Add(_streamer, DateTime.Now);
            }

            return _streamer;
        }

        public static Streamer FindStreamer(string twitch_name, string twitch_email)
        {
            Streamer _streamer = Find(_streamerCache, x => x.twitch_name == twitch_name);

            if (_streamer == null)
            {
                _streamer = Streamer.Find(twitch_name);
                if (_streamer != null) _streamerCache.Add(_streamer, DateTime.Now);
            }

            if (_streamer == null)
            {
                _streamer = new Streamer(twitch_name, twitch_email);
                return AddStreamer(_streamer);
            }


            return _streamer;
        }

        public static UserAccount FindAccount(User User)
        {
            UserAccount _userAcc = Find(_userAccCache, x => x.user.Equals(User));

            if (_userAcc == null)
            {
                _userAcc = UserAccount.Find(User);
                if (_userAcc != null) _userAccCache.Add(_userAcc, DateTime.Now);
            }

            if (_userAcc == null)
            {
                _userAcc = new UserAccount(User);
                return AddAccount(_userAcc);
            }

            return _userAcc;
        }

        public static UserAccount FindAccount(uint AccId)
        {
            UserAccount _userAcc = Find(_userAccCache, x => x.Id == AccId);

            if (_userAcc == null)
            {
                _userAcc = UserAccount.Find(AccId);
                if (_userAcc != null) _userAccCache.Add(_userAcc, DateTime.Now);
            }

            return _userAcc;
        }

        public static CurrencyConfig FindCurrency(uint currencyId)
        {
            CurrencyConfig _currency = Find(_currencyCache, x => x.Id == currencyId);

            if (_currency == null)
            {
                _currency = CurrencyConfig.Find(currencyId);
                if (_currency != null) _currencyCache.Add(_currency, DateTime.Now);
            }

            return _currency;
        }

        public static CurrencyConfig FindCurrency(string Source, Source source)
        {
            CurrencyParticipant _participant = Find(_currencyParticipantCache, x => (x.discord_guild == Source && source == Generics.Source.Discord) || (x.twitch_name == Source && source == Generics.Source.Twitch));

            if (_participant == null)
            {
                if (source == Generics.Source.Discord) _participant = CurrencyParticipant.FindDiscord(Source);
                else _participant = CurrencyParticipant.FindTwitch(Source);

                if (_participant != null) _currencyParticipantCache.Add(_participant, DateTime.Now);
            }

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
