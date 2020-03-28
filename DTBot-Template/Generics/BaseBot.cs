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

        #endregion Methods
    }
}