using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class User : BaseGeneric
    {
        #region Fields

        private SocketUser discord_Source;

        public readonly string Name, Id;

        #endregion Fields

        #region Constructors

        [JsonConstructor]
        public User(string Name, string Id, Source source) : base(source)
        {
            this.Name = Name;
            this.Id = Id;
        }

        public User(ChatMessage args) : base(Source.Twitch)
        {
            Name = args.Username;
            Id = args.UserId;
        }

        public User(SocketUser args) : base(Source.Discord)
        {
            discord_Source = args;
            Name = args.Username;
            Id = args.Id.ToString();
        }

        public User(string Mention, Source source) : base(source)
        {
            if (source == Source.Discord) Id = Mention.Substring(2, Mention.Length - 3).Replace("!", "");
            else Name = Mention.Substring(1);
        }

        #endregion Constructors

        #region Methods

        public bool Equals(User other)
        {
            return other.Id == this.Id || other.Name == this.Name;
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