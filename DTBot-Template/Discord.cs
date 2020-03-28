using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DTBot_Template
{
    public class Discord : Generics.BaseBot
    {
        DiscordSocketClient _client;

        public Discord(string token)
        {
            _client = new DiscordSocketClient();

            _client.MessageReceived += MessageReceived;

            new Thread(async () => await Start(token)).Start();
        }

        private async Task Start(string token)
        {
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        private async Task MessageReceived(SocketMessage args)
        {
            if (!args.Author.IsBot)
            {
                Generics.Message message = new Generics.Message(args);
                await MessageHandler(message, this);
            }
        }

        public async override Task SendMessage(Generics.Channel channel, string Message)
        {
            await channel.SendMessage(Message);
        }
    }
}
