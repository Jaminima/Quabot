using System;
using System.Threading.Tasks;

namespace DTBot_Template.Generics
{
    public class BaseBot
    {
        #region Fields

        protected readonly char Command;
        public Func<Command, BaseBot, Task> CommandHandler;
        public Func<Message, BaseBot, Task> MessageHandler;

        #endregion Fields

        #region Constructors

        public BaseBot(char Command = '!')
        {
            this.Command = Command;
        }

        #endregion Constructors

        #region Methods

        public async virtual Task SendDM(User user, string Message)
        {
        }

        public async virtual Task SendMessage(Channel channel, string Message)
        {
        }

        public async Task SendDM(User user, string Message, Source source, User[] Targets = null, int Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, source, user, Targets, Value, CurrencyName);
            await SendDM(user,Message);
        }

        public async Task SendMessage(Channel channel, string Message, Source source, User user = null, User[] Targets = null, int Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, source, user, Targets, Value, CurrencyName);
            await SendMessage(channel, Message);
        }

        public string replacePeramaters(string original,Source source,User Source=null,User[] Targets = null,int Value=0,string CurrencyName="")
        {
            if (source == Generics.Source.Discord) original = original.Replace("{User}", "<@" + Source?.Id + ">"); 
            else original = original.Replace("{User}", "@" + Source?.Id);

            original = original.Replace("{Value}", Value.ToString());
            original = original.Replace("{Currency}", CurrencyName);

            if (Targets != null)
            {
                for (int i = 0; i < Targets.Length; i++)
                {
                    if (source == Generics.Source.Discord) { original = original.Replace("{User"+i+"}", "<@" + Targets[i].Id + ">"); }
                    else { original = original.Replace("{User"+i+"}", "@" + Targets[i].Name); }
                }
            }

            return original;
        }

        #endregion Methods
    }
}