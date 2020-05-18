using Discord.WebSocket;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class Message : BaseGeneric
    {
        #region Fields

        public readonly string body;
        public readonly Channel channel;
        public readonly SocketMessage discord_Source;
        public readonly User sender;
        public readonly ChatMessage twitch_Source;

        #endregion Fields

        #region Constructors

        public Message()
        {
        }

        public Message(ChatMessage args)
        {
            twitch_Source = args;
            source = Source.Twitch;
            body = args.Message;
            channel = new Channel(args.Channel);
            sender = new User(args);
        }

        public Message(SocketMessage args)
        {
            discord_Source = args;
            source = Source.Discord;
            body = args.Content;
            channel = new Channel(args.Channel);
            sender = new User(args.Author);
        }

        #endregion Constructors
    }
}