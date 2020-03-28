using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DTBot_Template.Generics
{
    public class Channel:BaseGeneric
    {
        private string ChannelName;
        private ISocketMessageChannel discord_Source;

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

        public async Task SendMessage(string Message)
        {
            await discord_Source.SendMessageAsync(Message);
        }

        public async Task SendMessage(string Message, TwitchClient _client)
        {
            _client.SendMessage(ChannelName, Message);
        }
    }
}
