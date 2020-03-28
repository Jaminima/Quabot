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

        public string replacePeramaters(string original, Source source, User Source = null, User[] Targets = null, uint[] Values = null, uint Value = 0, string CurrencyName = "")
        {
            if (source == Generics.Source.Discord) original = original.Replace("{User}", "<@" + Source?.Id + ">");
            else original = original.Replace("{User}", "@" + Source?.Id);

            original = original.Replace("{Currency}", CurrencyName);
            original = original.Replace("{Value}", Value.ToString());

            for (int i = 0; i < Values?.Length; i++)
            {
                original = original.Replace("{Value" + i + "}", Values[i].ToString());
            }

            for (int i = 0; i < Targets?.Length; i++)
            {
                if (source == Generics.Source.Discord) { original = original.Replace("{User" + i + "}", "<@" + Targets[i].Id + ">"); }
                else { original = original.Replace("{User" + i + "}", "@" + Targets[i].Name); }
            }

            return original;
        }

        public async virtual Task SendDM(User user, string Message)
        {
        }

        public async virtual Task SendMessage(Channel channel, string Message)
        {
        }

        #region Send Overloads

        public async Task SendDM(User user, string Message, Source source, User[] Targets = null, uint[] Values = null, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, source, user, Targets, Values, Value, CurrencyName);
            await SendDM(user, Message);
        }

        public async Task SendDM(Command _command, string Message, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, _command.Source, _command.sender, _command.mentions, _command.values, Value, CurrencyName);
            await SendDM(_command.sender, Message);
        }

        public async Task SendDM(Message _message, string Message, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, _message.Source, _message.sender, Value: Value, CurrencyName: CurrencyName);
            await SendDM(_message.sender, Message);
        }

        public async Task SendMessage(Channel channel, string Message, Source source, User user = null, User[] Targets = null, uint[] Values = null, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, source, user, Targets, Values, Value, CurrencyName);
            await SendMessage(channel, Message);
        }

        public async Task SendMessage(Command _command, string Message, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, _command.Source, _command.sender, _command.mentions, _command.values, Value, CurrencyName);
            await SendMessage(_command.channel, Message);
        }

        public async Task SendMessage(Message _message, string Message, uint Value = 0, string CurrencyName = "")
        {
            Message = replacePeramaters(Message, _message.Source, _message.sender, Value: Value, CurrencyName: CurrencyName);
            await SendMessage(_message.channel, Message);
        }

        #endregion Send Overloads

        #endregion Methods
    }
}