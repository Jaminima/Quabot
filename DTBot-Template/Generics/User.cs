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

        public readonly ulong Id;
        public readonly string Name;

        #endregion Fields

        #region Constructors

        public User(ChatMessage args)
        {
            Name = args.Username;
            Id = ulong.Parse(args.UserId);
        }

        public User(SocketUser args)
        {
            discord_Source = args;
            Name = args.Username;
            Id = args.Id;
        }

        #endregion Constructors

        #region Methods

        public bool Equals(User other)
        {
            return other.Id == this.Id && other.Name == this.Name;
        }

        public async Task SendDM(string Message)
        {
            await discord_Source.SendMessageAsync(Message);
        }

        public async Task SendDM(string Message, Twitch _client)
        {
            _client._client.SendWhisper(Name, Message);
        }

        #endregion Methods
    }
}