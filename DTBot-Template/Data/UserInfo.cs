using DTBot_Template.Data._MySQL;
using System;
using System.Collections.Generic;

namespace DTBot_Template.Data
{
    public class _userInfo : _MySQL.SQLObj
    {
        #region Fields

        public Generics.User user;
        public uint balance = 1000;
        public uint currency;

        #endregion Fields

        #region Constructors

        public _userInfo()
        {
            Table = "currency_account";
        }

        public _userInfo(Generics.User _user)
        {
            this.user = _user;
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            currency = uint.Parse(Data[1].ToString());
            balance = uint.Parse(Data[2].ToString());

            user = new Generics.User(Data[4].ToString(),Data[3].ToString(),(Generics.Source)Convert.ToInt32(Data[5]));
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            if (IncludeProtected) return new object[] { Id, currency, balance, user.Id, user.Name, user.Source };
            else return new object[] { null, currency, balance, user.Id, user.Name, user.Source };
        }

        public override void Update()
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", balance),
                new Tuple<string, object>("@1", Id)
            };
            SQL.pubInstance.Execute("UPDATE currency_account SET currency_balance = @0 WHERE account_id=@1", Params);
        }

        #endregion Constructors
    }
}