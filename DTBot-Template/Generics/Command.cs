using Discord.WebSocket;
using System.Linq;
using TwitchLib.Client.Models;
using System.Collections.Generic;

namespace DTBot_Template.Generics
{
    public class Command : Message
    {
        #region Fields

        public readonly string[] commandArgs;
        public readonly string commandStr, commandArgString;
        public readonly User[] mentions;
        public readonly uint[] values;

        #endregion Fields

        #region Constructors

        public Command(ChatCommand args) : base(args.ChatMessage)
        {
            source = Source.Twitch;
            commandStr = args.CommandText;
            commandArgs = args.ArgumentsAsList.ToArray();
            commandArgString = args.ArgumentsAsString;

            this.mentions = GetMentions();
            this.values = GetValues();
        }

        public Command(SocketMessage args) : base(args)
        {
            source = Source.Discord;
            int CommEnd = body.IndexOf(' ');
            if (CommEnd == -1) { CommEnd = body.Length; }

            commandStr = body.Substring(0, CommEnd).Substring(1);
            commandArgString = body.Substring(CommEnd);
            commandArgs = commandArgString.Split(' ');

            this.mentions = GetMentions();
            this.values = GetValues();
        }

        private uint[] GetValues()
        {
            uint tInt;
            List<uint> uints = new List<uint>();
            foreach (string s in commandArgs)
            {
                if (uint.TryParse(s, out tInt)) uints.Add(tInt);
            }
            return uints.ToArray();
        }

        private User[] GetMentions()
        {
            string[] _mentions = commandArgs.Where(x => x.StartsWith("<@") && x.EndsWith(">")||x.StartsWith("@")).ToArray();
            User[] mentions = new User[_mentions.Length];
            for (int i = 0; i < mentions.Length; i++) { mentions[i] = new User(_mentions[i], source); }
            return mentions;
        }

        #endregion Constructors
    }
}