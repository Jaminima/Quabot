using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTBot_Template;

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

            dBot = new Discord("NjU4MjU0NzAyNzczMDEwNDUy.Xn8LvQ.gZPKusUqJv-dy9kk47lYPL7wadU");
            dBot.MessageHandler += HandleMessage;

            while (true) { Console.ReadLine(); }
        }

        public static async Task HandleMessage(DTBot_Template.Generics.Message message, DTBot_Template.Generics.BaseBot Bot)
        {
            await Bot.SendMessage(message.Channel,message.Body);
        }
    }
}
