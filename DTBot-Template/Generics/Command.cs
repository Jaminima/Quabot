using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models;

namespace DTBot_Template.Generics
{
    public class Command : Message
    {
        #region Methods

        private string Nums = "0123456789";

        private bool IsValidMention(string Mention)
        {
            Mention = Mention.Replace("!", "");
            Mention = Mention.Replace("<@", "@");

            if (source == Source.Discord && Mention.Count(x => !Nums.Contains(x)) == 2 && Mention.EndsWith(">")) return true;

            if (source == Source.Twitch && Mention.StartsWith("@")) return true;

            return false;
        }

        private User[] GetMentions()
        {
            string[] _mentions = commandArgs.Where(x => IsValidMention(x)).ToArray();
            User[] mentions = new User[_mentions.Length];
            for (int i = 0; i < mentions.Length; i++) { mentions[i] = new User(_mentions[i], source,"THIS FORCES MENTIONS"); }
            return mentions;
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

        #endregion Methods

        #region Fields

        public readonly string[] commandArgs;
        public readonly string commandStr, commandArgString;
        public readonly User[] mentions;
        public readonly uint[] values;

        #endregion Fields

        #region Constructors

        public Command()
        {
        }

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

        #endregion Constructors
    }
}