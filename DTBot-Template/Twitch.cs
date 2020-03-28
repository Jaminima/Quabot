using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace DTBot_Template
{
    public class Twitch : Generics.BaseBot
    {
        TwitchClient _client;

        public Twitch(string username, string token, string channel)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(username,token);

            _client = new TwitchClient();
            _client.Initialize(credentials,channel);

            _client.OnMessageReceived += MessageReceived;

            _client.Connect();
        }

        private async void MessageReceived(object e,OnMessageReceivedArgs args)
        {
            Generics.Message message = new Generics.Message(args.ChatMessage);
            await MessageHandler(message,this);
        }

        public async override Task SendMessage(Generics.Channel channel, string Message)
        {
            await channel.SendMessage(Message, _client);
        }
    }
}
