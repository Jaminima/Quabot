using System;
using System.Collections.Generic;

namespace DTBot_Template.Data._MySQL
{
        public class SQLObj
        {
        #region Fields

            public uint Id;
            private protected string Table;

            #endregion Fields

            #region Constructors

            public SQLObj()
            {
            }

            #endregion Constructors

            #region Methods

            public static T[] FromTable<T>(string Table, string Where = "1=1") where T : SQLObj, new()
            {
                List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM " + Table + " WHERE " + Where);
                T[] workplaces = new T[Data.Count];
                for (int i = 0; i < workplaces.Length; i++) { workplaces[i] = new T(); workplaces[i].SetValues(Data[i], true); }
                return workplaces;
            }

            public static SQLObj[] FromTable(Type T, string Table, string Where = "1=1")
            {
                List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM " + Table + " WHERE " + Where);
                SQLObj[] workplaces = new SQLObj[Data.Count];
                for (int i = 0; i < workplaces.Length; i++) { workplaces[i] = (SQLObj)Activator.CreateInstance(T); workplaces[i].SetValues(Data[i], true); }
                return workplaces;
            }

            public virtual void Delete()
            {
            }

            public virtual object[] GetValues(bool IncludeProtected = true)
            {
                return null;
            }

            public virtual void Insert()
            {
                int i = 0;
                string Vs = "";
                List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                foreach (object obj in GetValues(false))
                {
                    Vs += "@" + i + ",";
                    Params.Add(new Tuple<string, object>("@" + i, obj));
                    i++;
                }
                Vs = Vs.TrimEnd(',');
                SQL.pubInstance.Execute("INSERT INTO " + Table + " VALUES (" + Vs + ")", Params);
            }

            public virtual void SetValues(object[] Data, bool OverrideProtected = false)
            {
            }

            public virtual void Update()
            {
            }

            #endregion Methods
        }
}
