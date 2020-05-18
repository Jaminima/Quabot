using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class User : BaseGeneric
    {
        #region Fields

        private SocketUser discord_Source;

        public readonly string Twitch_Name, Discord_Id;

        #endregion Fields

        #region Constructors

        public User(string Name, string Id, Source source) : base(source)
        {
            this.Twitch_Name = Name;
            this.Discord_Id = Id;
        }

        public User(string Identifier, Source source) : base(source)
        {
            if (Source.Discord == source) this.Discord_Id = Identifier;
            else this.Twitch_Name = Identifier;
        }

        public User(ChatMessage args) : base(Source.Twitch)
        {
            Twitch_Name = args.Username;
        }

        public User(SocketUser args) : base(Source.Discord)
        {
            discord_Source = args;
            Discord_Id = args.Id.ToString();
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

        public async Task SendDM(string Message)
        {
            await discord_Source.SendMessageAsync(Message);
        }

        public async Task SendDM(string Message, Twitch _client)
        {
            //_client._client.SendWhisper(Name, Message);
        }

        #endregion Methods
    }
}