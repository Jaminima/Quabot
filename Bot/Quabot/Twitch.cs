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
            CurrencyConfig C = CacheHandler.FindCurrency(args.Command.ChatMessage.Channel, Generics.Source.Twitch);
            Generics.Command command = new Generics.Command(args.Command,C);
            await CommandHandler(command, this, CacheHandler.FindCurrency(command.channel.ChannelName, command.Source));
        }

        private async void MessageReceived(object e, OnMessageReceivedArgs args)
        {
            if (args.ChatMessage.Message[0] != Command)
            {
                CurrencyConfig C = CacheHandler.FindCurrency(args.ChatMessage.Channel, Generics.Source.Twitch);
                Generics.Message message = new Generics.Message(args.ChatMessage);
                await MessageHandler(message, this, C);
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

            _client.RemoveChatCommandIdentifier('!');
            _client.AddChatCommandIdentifier(Command);

            _client.OnMessageReceived += MessageReceived;
            _client.OnChatCommandReceived += CommandReceived;

            _client.Connect();

            while (!_client.IsConnected) { System.Threading.Thread.Sleep(200); }

            while (_client.JoinedChannels.Count != channels.Length)
            {
                foreach (string C in channels)
                {
                    _client.JoinChannel(C, true);
                    System.Threading.Thread.Sleep(500);
                }
            }
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