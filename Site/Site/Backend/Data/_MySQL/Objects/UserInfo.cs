﻿using Site.Backend.Data.Generics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Site.Backend.Data._MySQL.Objects
{
    public class _userInfo : _MySQL.SQLObj
    {
        #region Fields

        public uint balance = 1000;
        public uint currency;
        public UserAccount user;

        #endregion Fields

        #region Constructors

        public _userInfo()
        {
            Table = "currency_account";
        }

        public _userInfo(Generics.User _user)
        {
            Table = "currency_account";
            this.user = new UserAccount(_user);
        }

        public _userInfo(Generics.User _user, CurrencyConfig currency)
        {
            this.currency = currency.Id;
            this.balance = currency.DefaultBalance;
            Table = "currency_account";
            //this.user = CacheHandler.FindAccount(_user);
        }

        #endregion Constructors

        #region Methods

        public static _userInfo[] FindAll(User user)
        {
            //UserAccount A = CacheHandler.FindAccount(user);
            UserAccount A = UserAccount.Find(user);

            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", A.Id)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_account WHERE currency_user = @0", Params);

            if (Data.Count == 0) return null;

            List<_userInfo> T = new List<_userInfo>();

            Data.ForEach(x => { T.Add(new _userInfo()); T.Last().SetValues(x); });

            return T.ToArray();
        }

        public static _userInfo Find(User user, uint curid)
        {
            //UserAccount A = CacheHandler.FindAccount(user);
            UserAccount A = UserAccount.Find(user);

            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", A.Id),
                new Tuple<string, object>("@1", curid)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_account WHERE currency_user = @0 AND currency_id = @1", Params);

            if (Data.Count == 0) return null;

            _userInfo u = new _userInfo();

            u.SetValues(Data[0]);

            return u;
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            if (IncludeProtected) return new object[] { Id, currency, balance, user.Id };
            else return new object[] { null, currency, balance, user.Id };
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            currency = uint.Parse(Data[1].ToString());
            balance = uint.Parse(Data[2].ToString());

            user = UserAccount.Find(uint.Parse(Data[3].ToString()));
        }

        public override void Delete()
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", Id)
            };
            SQL.pubInstance.Execute("DELETE FROM currency_account WHERE account_id=@0", Params);
        }

        public override void Update()
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", balance),
                new Tuple<string, object>("@1", Id)
            };
            SQL.pubInstance.Execute("UPDATE currency_account SET currency_balance = @0 WHERE account_id=@1", Params);
        }

        #endregion Methods
    }
}
