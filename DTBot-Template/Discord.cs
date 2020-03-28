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

        public Discord(string token,char Command = '!'):base(Command)
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
                if (args.Content[0]==Command)
                {
                    Generics.Command command = new Generics.Command(args);
                    await CommandHandler(command, this);
                }
                else
                {
                    Generics.Message message = new Generics.Message(args);
                    await MessageHandler(message, this);
                }
            }
        }

        public async override Task SendMessage(Generics.Channel channel, string Message)
        {
            await channel.SendMessage(Message);
        }
    }
}
