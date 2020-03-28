using Discord.WebSocket;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class Command : Message
    {
        #region Fields

        public readonly string[] commandArgs;
        public readonly string commandStr, commandArgString;

        #endregion Fields

        #region Constructors

        public Command(ChatCommand args) : base(args.ChatMessage)
        {
            commandStr = args.CommandText;
            commandArgs = args.ArgumentsAsList.ToArray();
            commandArgString = args.ArgumentsAsString;
        }

        public Command(SocketMessage args) : base(args)
        {
            int CommEnd = body.IndexOf(' ');
            commandStr = body.Substring(0, CommEnd).Substring(1);
            commandArgString = body.Substring(CommEnd);
            commandArgs = commandArgString.Split(' ');
        }

        #endregion Constructors
    }
}