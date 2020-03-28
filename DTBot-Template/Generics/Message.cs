using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using Discord;
using Discord.WebSocket;

namespace DTBot_Template.Generics
{
    public class Message:BaseGeneric
    {
        private ChatMessage twitch_Source;
        private SocketMessage discord_Source;

        public readonly string body;

        public readonly Channel channel;

        public Message(ChatMessage args)
        {
            source = Source.Twitch;
            body = args.Message;
            channel = new Channel(args.Channel);
        }

        public Message(SocketMessage args)
        {
            source = Source.Discord;
            body = args.Content;
            channel = new Channel(args.Channel);
        }
    }

    
}
