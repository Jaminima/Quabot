using DTBot_Template;
using DTBot_Template.Generics;
using System;
using System.Threading.Tasks;

namespace Test_App
{
    internal class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            tBot = new Twitch("tdrewardbot", "ipq91r9cehomlgftlrty4fha7ca8my", "jccjaminima");
            tBot.MessageHandler += HandleMessage;
            tBot.CommandHandler += HandleCommand;

            dBot = new DTBot_Template.Discord("NjU4MjU0NzAyNzczMDEwNDUy.Xn8LvQ.gZPKusUqJv-dy9kk47lYPL7wadU");
            dBot.MessageHandler += HandleMessage;
            dBot.CommandHandler += HandleCommand;

            while (true) { Console.ReadLine(); }
        }

        #endregion Methods

        #region Fields

        public static DTBot_Template.Discord dBot;
        public static DTBot_Template.Twitch tBot;

        #endregion Fields

        public static async Task HandleCommand(Command command, BaseBot Bot)
        {
            switch (command.commandStr)
            {
                case "echo":
                    await Bot.SendMessage(command.channel, command.commandArgString);
                    break;

                case "echodm":
                    await Bot.SendDM(command.sender, command.commandArgString);
                    break;
            }
        }

        public static async Task HandleMessage(Message message, BaseBot Bot)
        {
            if (message.body.ToLower().StartsWith("i am") || message.body.ToLower().StartsWith("i'm"))
            {
                await Bot.SendMessage(message.channel, "Hi {User} Im Dad");
            }
        }
    }
}