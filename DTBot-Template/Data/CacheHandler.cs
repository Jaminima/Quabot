using DTBot_Template.Data._MySQL;
using DTBot_Template.Generics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace DTBot_Template.Data
{
    public class CacheHandler
    {
        #region Fields

        private List<_userInfo> _userCache = new List<_userInfo>();
        private List<CurrencyConfig> _currencyCache = new List<CurrencyConfig>();

        #endregion Fields

        #region Methods

        public CurrencyConfig FindCurrency(uint curid)
        {
            CurrencyConfig _currency = _currencyCache.Find(x => x.Id == curid);

            if (_currency == null)
            {
                _currency = CurrencyConfig.Find(curid);
                if (_currency != null) _currencyCache.Add(_currency);
            }

            return _currency;
        }

        public virtual void AddUser(_userInfo user)
        {
            user.Insert();
            _userCache.Add(user);
        }

        public _userInfo FindUser(User user, uint curid)
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

        public _userInfo[] FindUsers(User[] users, uint curid)
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