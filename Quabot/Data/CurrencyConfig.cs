using DTBot_Template.Data._MySQL;
using System;
using System.Collections.Generic;

namespace DTBot_Template.Data
{
    public class CurrencyConfig : _MySQL.SQLObj
    {
        #region Fields

        public string name;

        #endregion Fields

        #region Constructors

        public CurrencyConfig()
        {
            Table = "currency_config";
        }

        #endregion Constructors

        #region Methods

        public static CurrencyConfig Find(uint curid)
        {
            List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
                new Tuple<string, object>("@0", curid)
            };
            List<object[]> Data = SQL.pubInstance.Read("SELECT * FROM currency_config WHERE currency_id = @0", Params);

            if (Data.Count == 0) return null;

            CurrencyConfig u = new CurrencyConfig();

            u.SetValues(Data[0]);

            return u;
        }

        public override object[] GetValues(bool IncludeProtected = true)
        {
            return new object[] { Id, null, name };
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            name = Data[2].ToString();
        }

        #endregion Methods

        //public override void Update()
        //{
        //    List<Tuple<string, object>> Params = new List<Tuple<string, object>> {
        //        new Tuple<string, object>("@0", name),
        //        new Tuple<string, object>("@1", Id)
        //    };
        //    SQL.pubInstance.Execute("UPDATE currency_config SET currency_name = @0 WHERE currency_id=@1", Params);
        //}
    }
}