using DTBot_Template.Data;
using DTBot_Template.Generics;
using System.Collections.Generic;

namespace Test_App
{
    public static class BankHandler
    {
        #region Fields

        public static List<Bank> bankAccounts = new List<Bank>();

        #endregion Fields

        #region Methods

        public static Bank[] FromUsers(User[] users)
        {
            Bank[] banks = new Bank[users.Length];
            for (int i = 0; i < users.Length; i++)
            {
                banks[i] = bankAccounts.Find(x => x.user.Equals(users[i]));
                if (banks[i] == null) { banks[i] = new Bank(users[i], 1000); }
            }
            return banks;
        }

        #endregion Methods
    }
}