using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Backend.Data.Generics
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

        #region Constructors

        public BaseGeneric()
        {
        }

        public BaseGeneric(Source source)
        {
            this.source = source;
        }

        #endregion Constructors

        #region Properties

        public Source Source
        {
            get { return source; }
        }

        #endregion Properties
    }
}
