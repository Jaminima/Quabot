using Site.Backend.Data.Generics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Site.Backend.Data._MySQL.Objects
{
    public class UserAccount:SQLObj
    {
        #region Fields

        public Generics.User user;

        #endregion Fields

        #region Constructors

        public UserAccount()
        {
            Table = "currency_users";
        }

        public UserAccount(Generics.User _user)
        {
            Table = "currency_users";
            this.user = _user;
        }

        #endregion Constructors

        #region Methods

        public static UserAccount Find(User user)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", user.Discord_Id),
                new Tuple<string, object>("@1", user.Twitch_Name)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_users WHERE discord_id = @0 OR twitch_name = @1", Params);

            if (Data.Count == 0) return null;

            UserAccount u = new UserAccount();

            u.SetValues(Data[0]);

            return u;
        }

        public static UserAccount Find(uint Accid)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", Accid)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_users WHERE user_id = @0", Params);

            if (Data.Count == 0) return null;

            UserAccount u = new UserAccount();

            u.SetValues(Data[0]);

            return u;
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            if (IncludeProtected) return new object[] { Id, user.Discord_Id, user.Twitch_Name };
            else return new object[] { null, user.Discord_Id, user.Twitch_Name };
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());

            user = new User(Data[2].ToString(), Data[1].ToString());
        }

        public override void Update()
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", user.Discord_Id),
                new Tuple<string, object>("@1", user.Twitch_Name),
                new Tuple<string, object>("@2", Id)
            };
            SQL.pubInstance.Execute("UPDATE currency_users SET discord_id = @0, twitch_name = @1 WHERE user_id=@2", Params);
        }
        public override void Delete()
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", Id)
            };
            SQL.pubInstance.Execute("DELETE FROM currency_users WHERE user_id=@0", Params);
        }

        #endregion Methods
    }
}
