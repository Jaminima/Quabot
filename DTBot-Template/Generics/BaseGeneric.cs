namespace DTBot_Template.Generics
{
    public enum Source
    {
        Twitch, Discord
    }

    public class BaseGeneric
    {
        #region Fields

        private protected Source source;

        #endregion Fields

        #region Properties

        public Source Source
        {
            get { return Source; }
        }

        #endregion Properties
    }
}