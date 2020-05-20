using DTBot_Template.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTBot_Template.Generics
{
    public class BaseBot
    {
        #region Fields

        protected readonly char Command;
        public Func<Command, BaseBot, Data.CurrencyConfig, Task> CommandHandler;
        public Func<Message, BaseBot, Data.CurrencyConfig, Task> MessageHandler;

        #endregion Fields

        #region Constructors

        public BaseBot(char Command = '!')
        {
            this.Command = Command;
        }

        #endregion Constructors

        #region Methods

        public string replacePeramaters(string original, Source source, CurrencyConfig Currency, User Source = null, User[] Targets = null, uint[] Values = null, uint Value = 0, string Str = "")
        {
            if (source == Generics.Source.Discord) original = original.Replace("{User}", "<@" + Source?.Discord_Id + ">");
            else original = original.Replace("{User}", "@" + Source?.Twitch_Name);

            original = original.Replace("{String}", Str);
            original = original.Replace("{Currency}", Currency.name);
            original = original.Replace("{Value}", Value.ToString());

            for (int i = 0; i < Values?.Length; i++)
            {
                original = original.Replace("{Value" + i + "}", Values[i].ToString());
            }

            for (int i = 0; i < Targets?.Length; i++)
            {
                if (source == Generics.Source.Discord) { original = original.Replace("{User" + i + "}", "<@" + Targets[i].Discord_Id + ">"); }
                else { original = original.Replace("{User" + i + "}", "@" + Targets[i].Twitch_Name); }
            }

            KeyValuePair<string, Emote> Eitem;
            for (int i = 0; i < Currency.CustomEmotes.Count; i++)
            {
                Eitem = Currency.CustomEmotes.ElementAt(i);
                if (source == Generics.Source.Discord) original = original.Replace(Eitem.Key, Eitem.Value.Discord);
                else original = original.Replace(Eitem.Key, Eitem.Value.Twitch);
            }

            return original;
        }

        public async virtual Task SendDM(User user, string Message)
        {
        }

        public async Task SendDM(User user, string Message, Source source, CurrencyConfig Currency, User[] Targets = null, uint[] Values = null, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, source, Currency, user, Targets, Values, Value, Str);
            await SendDM(user, Message);
        }

        public async Task SendDM(Command _command, string Message, CurrencyConfig Currency, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, _command.Source, Currency, _command.sender, _command.mentions, _command.values, Value, Str);
            await SendDM(_command.sender, Message);
        }

        public async Task SendDM(Message _message, string Message, CurrencyConfig Currency, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, _message.Source, Currency, _message.sender, Value: Value, Str: Str);
            await SendDM(_message.sender, Message);
        }

        public async virtual Task SendMessage(Channel channel, string Message)
        {
        }

        public async Task SendMessage(Channel channel, string Message, Source source, CurrencyConfig Currency, User user = null, User[] Targets = null, uint[] Values = null, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, source, Currency, user, Targets, Values, Value, Str);
            await SendMessage(channel, Message);
        }

        public async Task SendMessage(Command _command, string Message, CurrencyConfig Currency, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, _command.Source, Currency, _command.sender, _command.mentions, _command.values, Value, Str);
            await SendMessage(_command.channel, Message);
        }

        public async Task SendMessage(Message _message, string Message, CurrencyConfig Currency, uint Value = 0, string Str = "")
        {
            Message = replacePeramaters(Message, _message.Source, Currency, _message.sender, Value: Value, Str: Str);
            await SendMessage(_message.channel, Message);
        }

        #endregion Methods
    }
}