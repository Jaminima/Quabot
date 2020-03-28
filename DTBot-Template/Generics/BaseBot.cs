using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DTBot_Template.Generics
{
    public class BaseBot
    {
        public Func<Message,BaseBot,Task> MessageHandler;

        public async virtual Task SendMessage(Generics.Channel channel, string Message) { }
    }

    public class BaseGeneric
    {
        private protected Source source;
        public Source Source
        {
            get { return Source; }
        }
    }

    public enum Source
    {
        Twitch, Discord
    }
}
