using DTBot_Template.Data;
using DTBot_Template.Generics;
using System.Linq;
using System;

namespace Test_App
{
    public class ExtendedUser:_userInfo
    {
        public uint balance=1000;

        public ExtendedUser():base(null) { }
    }
}
