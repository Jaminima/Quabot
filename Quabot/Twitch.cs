using DTBot_Template.Data;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace DTBot_Template
{
    public class Twitch : Generics.BaseBot
    {
        #region Methods

        private async void CommandReceived(object e, OnChatCommandReceivedArgs args)
        {
            Generics.Command command = new Generics.Command(args.Command);
            await CommandHandler(command, this, CacheHandler.FindCurrency(command.channel.ChannelName, command.Source));
        }

        private async void MessageReceived(object e, OnMessageReceivedArgs args)
        {
            if (args.ChatMessage.Message[0] != Command)
            {
                Generics.Message message = new Generics.Message(args.ChatMessage);
                await MessageHandler(message, this, CacheHandler.FindCurrency(message.channel.ChannelName, message.Source));
            }
        }

        #endregion Methods

        #region Fields

        public readonly TwitchClient _client;

        #endregion Fields

        #region Constructors

        public Twitch(string username, string token, string[] channels, char Command = '!') : base(Command)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(username, token);

            _client = new TwitchClient();

            _client.Initialize(credentials);

            _client.AddChatCommandIdentifier(Command);

            _client.OnMessageReceived += MessageReceived;
            _client.OnChatCommandReceived += CommandReceived;

            _client.Connect();

            while (!_client.IsConnected) { System.Threading.Thread.Sleep(200); }

            foreach (string C in channels) _client.JoinChannel(C);
        }

        #endregion Constructors

        public override async Task SendDM(Generics.User user, string Message)
        {
            await user.SendDM(Message, this);
        }

        public async override Task SendMessage(Generics.Channel channel, string Message)
        {
            await channel.SendMessage(Message, _client);
        }
    }
}