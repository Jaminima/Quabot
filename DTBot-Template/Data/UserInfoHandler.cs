using System;
using System.Collections.Generic;
using System.Text;
using DTBot_Template.Generics;

namespace DTBot_Template.Data
{
    public class UserInfoHandler<T> where T : _userInfo, new()
    {
        private List<T> _infoCollection = new List<T>();

        public virtual void AddUser(T user)
        {
            _infoCollection.Add(user);
        }

        public T FindUser(User user)
        {
            T _uInfo = _infoCollection.Find(x => x.user.Equals(user));
            if (_uInfo == null) { _uInfo = new T(); _uInfo.user = user; AddUser(_uInfo); }
            return _uInfo;
        }

        public T[] FindUsers(User[] users)
        {
            T[] _uInfos = new T[users.Length];
            
            for (int i = 0; i < users.Length; i++)
            {
                _uInfos[i] = FindUser(users[i]);
            }

            return _uInfos;
        }
    }
}
