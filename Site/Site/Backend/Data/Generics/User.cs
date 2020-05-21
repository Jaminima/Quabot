using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Backend.Data.Generics
{
    public class User : BaseGeneric
    {
        #region Fields

        public readonly string Twitch_Name, Discord_Id;

        #endregion Fields

        #region Constructors

        public User(string Name, string Id)
        {
            this.Twitch_Name = Name;
            this.Discord_Id = Id;
        }

        public User(string Identifier, Source source) : base(source)
        {
            if (Source.Discord == source) this.Discord_Id = Identifier;
            else this.Twitch_Name = Identifier;
        }

        public User(string Mention, Source source, string M) : base(source)
        {
            if (source == Source.Discord) Discord_Id = Mention.Substring(2, Mention.Length - 3).Replace("!", "");
            else Twitch_Name = Mention.Substring(1);
        }

        #endregion Constructors

        #region Methods

        public bool Equals(User other)
        {
            return other.Discord_Id == this.Discord_Id || other.Twitch_Name == this.Twitch_Name;
        }

        #endregion Methods
    }
}
