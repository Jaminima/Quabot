using Discord.WebSocket;
using System.Linq;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class Command : Message
    {
        #region Fields

        public readonly string[] commandArgs;
        public readonly string commandStr, commandArgString;
        public readonly User[] mentions;

        #endregion Fields

        #region Constructors

        public Command(ChatCommand args) : base(args.ChatMessage)
        {
            source = Source.Twitch;
            commandStr = args.CommandText;
            commandArgs = args.ArgumentsAsList.ToArray();
            commandArgString = args.ArgumentsAsString;

            string[] _mentions = commandArgs.Where(x => x.StartsWith("@")).ToArray();
            mentions = new User[_mentions.Length];
            for (int i = 0; i < mentions.Length; i++) { mentions[i] = new User(_mentions[i], source); }
        }

        public Command(SocketMessage args) : base(args)
        {
            source = Source.Discord;
            int CommEnd = body.IndexOf(' ');
            if (CommEnd == -1) { CommEnd = body.Length; }

            commandStr = body.Substring(0, CommEnd).Substring(1);
            commandArgString = body.Substring(CommEnd);
            commandArgs = commandArgString.Split(' ');

            string[] _mentions = commandArgs.Where(x => x.StartsWith("<@") && x.EndsWith(">")).ToArray();
            mentions = new User[_mentions.Length];
            for (int i = 0; i < mentions.Length; i++) { mentions[i] = new User(_mentions[i], source); }
        }

        #endregion Constructors
    }
}