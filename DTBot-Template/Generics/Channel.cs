using Discord.WebSocket;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace DTBot_Template.Generics
{
    public class Channel : BaseGeneric
    {
        #region Fields

        private ISocketMessageChannel discord_Source;
        public string ChannelName;

        #endregion Fields

        #region Constructors

        public Channel()
        {
        }

        public Channel(string Channel)
        {
            source = Source.Twitch;
            ChannelName = Channel;
        }

        public Channel(ISocketMessageChannel channel)
        {
            source = Source.Discord;
            discord_Source = channel;
            ChannelName = discord_Source.Name;
        }

        #endregion Constructors

        #region Methods

        public async Task SendMessage(string Message)
        {
            await discord_Source.SendMessageAsync(Message);
        }

        public async Task SendMessage(string Message, TwitchClient _client)
        {
            _client.SendMessage(ChannelName, Message);
        }

        #endregion Methods
    }
}