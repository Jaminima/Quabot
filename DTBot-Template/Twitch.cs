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

        public Twitch(string username, string token, string channel,char Command='!'):base(Command)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(username,token);

            _client = new TwitchClient();

            _client.Initialize(credentials,channel);
            _client.AddChatCommandIdentifier(Command);

            _client.OnMessageReceived += MessageReceived;
            _client.OnChatCommandReceived += CommandReceived;

            _client.Connect();
        }

        private async void CommandReceived(object e, OnChatCommandReceivedArgs args)
        {
            Generics.Command command = new Generics.Command(args.Command);
            await CommandHandler(command, this);
        }

        private async void MessageReceived(object e,OnMessageReceivedArgs args)
        {
            if (args.ChatMessage.Message[0] != Command)
            {
                Generics.Message message = new Generics.Message(args.ChatMessage);
                await MessageHandler(message, this);
            }
        }

        public async override Task SendMessage(Generics.Channel channel, string Message)
        {
            await channel.SendMessage(Message, _client);
        }
    }
}
