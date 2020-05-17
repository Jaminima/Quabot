using System;
using System.Collections.Generic;
using System.Text;

namespace DTBot_Template.Data
{
    public class CurrencyConfig : _MySQL.SQLObj
    {
        public string name;

        public CurrencyConfig()
        {
            Table = "currency_config";
        }

        public override void SetValues(object[] Data, bool OverrideProtected = false)
        {
            Id = uint.Parse(Data[0].ToString());
            name = Data[2].ToString();
        }
    }
}
