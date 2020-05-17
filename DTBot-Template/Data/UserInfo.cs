using TwitchLib.Api.Helix.Models.Users;
using System;

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

        #endregion Constructors
    }
}