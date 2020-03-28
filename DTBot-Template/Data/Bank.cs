using System;
using System.Collections.Generic;
using System.Text;

namespace DTBot_Template.Data
{
    public class Bank
    {
        public Generics.User user;
        public uint balance;

        public Bank(Generics.User _user,uint _balance=0)
        {
            this.user = _user;
            this.balance = _balance;
        }
    }
}
