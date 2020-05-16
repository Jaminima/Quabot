using DTBot_Template.Generics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace DTBot_Template.Data
{
    public class UserInfoHandler<T> where T : _userInfo, new()
    {
        #region Fields

        private List<T> _infoCollection = new List<T>();

        #endregion Fields

        #region Methods

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

        public void Load()
        {
            if (File.Exists("./Data/Memory.json")) _infoCollection = JArray.Parse(File.ReadAllText("./Data/Memory.json")).ToObject<List<T>>();
        }

        public void Save()
        {
            File.WriteAllText("./Data/Memory.json", JArray.FromObject(_infoCollection).ToString());
        }

        #endregion Methods
    }
}