﻿namespace DTBot_Template.Data
{
    public class _userInfo
    {
        #region Fields

        public Generics.User user;
        public uint balance = 1000;

        #endregion Fields

        #region Constructors

        public _userInfo()
        {
        }

        public _userInfo(Generics.User _user)
        {
            this.user = _user;
        }

        #endregion Constructors
    }
}