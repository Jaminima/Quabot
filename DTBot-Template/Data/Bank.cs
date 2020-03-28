namespace DTBot_Template.Data
{
    public class Bank
    {
        #region Fields

        public uint balance;
        public Generics.User user;

        #endregion Fields

        #region Constructors

        public Bank(Generics.User _user, uint _balance = 0)
        {
            this.user = _user;
            this.balance = _balance;
        }

        #endregion Constructors
    }
}