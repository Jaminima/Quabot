using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTBot_Template;
using DTBot_Template.Generics;

namespace Test_App
{
    class Program
    {
        public static DTBot_Template.Twitch tBot;
        public static DTBot_Template.Discord dBot;

        static void Main(string[] args)
        {
            tBot = new Twitch("jccjaminima", "z321fi3bq2q9p6bgs5yn5pep7bz3sq", "jccjaminima");
            tBot.MessageHandler += HandleMessage;
            tBot.CommandHandler += HandleCommand;

            dBot = new DTBot_Template.Discord("NjU4MjU0NzAyNzczMDEwNDUy.Xn8LvQ.gZPKusUqJv-dy9kk47lYPL7wadU");
            dBot.MessageHandler += HandleMessage;
            dBot.CommandHandler += HandleCommand;

            while (true) { Console.ReadLine(); }
        }

        public static async Task HandleMessage(Message message, BaseBot Bot)
        {
            await Bot.SendMessage(message.channel,message.body);
        }

        public static async Task HandleCommand(Command command, BaseBot Bot)
        {
            switch (command.commandStr)
            {
                case "echo":
                    await Bot.SendMessage(command.channel, command.commandArgString);
                    break;
            }
        }
    }
}
