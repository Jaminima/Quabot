using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using Discord;
using Discord.WebSocket;

namespace DTBot_Template.Generics
{
    public class Command : Message
    {
        public readonly string commandStr, commandArgString;
        public readonly string[] commandArgs;

        public Command(ChatCommand args) : base(args.ChatMessage)
        {
            commandStr = args.CommandText;
            commandArgs = args.ArgumentsAsList.ToArray();
            commandArgString = args.ArgumentsAsString;
        }

        public Command(SocketMessage args) : base(args)
        {
            commandStr = body.Substring(0,body.IndexOf(' '));
            commandArgString = body.Substring(commandStr.Length);
            commandArgs = commandArgString.Split(' ');
        }
    }
}
