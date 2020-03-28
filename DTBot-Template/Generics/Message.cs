using Discord.WebSocket;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class Message : BaseGeneric
    {
        #region Fields

        private SocketMessage discord_Source;
        private ChatMessage twitch_Source;
        public readonly string body;
        public readonly Channel channel;
        public readonly User sender;

        #endregion Fields

        #region Constructors

        public Message(ChatMessage args)
        {
            source = Source.Twitch;
            body = args.Message;
            channel = new Channel(args.Channel);
            sender = new User(args);
        }

        public Message(SocketMessage args)
        {
            source = Source.Discord;
            body = args.Content;
            channel = new Channel(args.Channel);
            sender = new User(args.Author);
        }

        #endregion Constructors
    }
}