using DTBot_Template.Data._MySQL;
using DTBot_Template.Generics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace DTBot_Template.Data
{
    public static class CacheHandler
    {
        #region Fields

        private static List<_userInfo> _userCache = new List<_userInfo>();
        private static List<CurrencyConfig> _currencyCache = new List<CurrencyConfig>();
        private static List<CurrencyParticipant> _currencyParticipantCache = new List<CurrencyParticipant>();

        #endregion Fields

        #region Methods

        public static CurrencyConfig FindCurrency(uint curid)
        {
            CurrencyConfig _currency = _currencyCache.Find(x => x.Id == curid);

            if (_currency == null)
            {
                _currency = CurrencyConfig.Find(curid);
                if (_currency != null) _currencyCache.Add(_currency);
            }

            return _currency;
        }

        public static CurrencyConfig FindCurrency(string Source, Source source)
        {
            CurrencyParticipant _participant = _currencyParticipantCache.Find(x => (x.discord_guild == Source && source==Generics.Source.Discord) || (x.twitch_name == Source && source == Generics.Source.Twitch));

            if (_participant == null)
            {
                if (source == Generics.Source.Discord) _participant = CurrencyParticipant.FindDiscord(Source);
                else _participant = CurrencyParticipant.FindTwitch(Source);

                if (_participant != null) _currencyParticipantCache.Add(_participant);
            }

            return FindCurrency(_participant.currencyid);
        }

        public static void AddUser(_userInfo user)
        {
            user.Insert();
            _userCache.Add(user);
        }

        public static _userInfo FindUser(User user, uint curid)
        {
            _userInfo _uInfo = _userCache.Find(x => x.user.Equals(user));

            if (_uInfo == null) { 
                _uInfo = _userInfo.Find(user, curid);
                if (_uInfo != null) _userCache.Add(_uInfo);
            }

            if (_uInfo == null) {
                _uInfo = new _userInfo(user);
                AddUser(_uInfo);
                _uInfo.Insert();
            }

            return _uInfo;
        }

        public static _userInfo[] FindUsers(User[] users, uint curid)
        {
            _userInfo[] _uInfos = new _userInfo[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                _uInfos[i] = FindUser(users[i],curid);
            }

            return _uInfos;
        }

        #endregion Methods
    }
}