using DTBot_Template;
using DTBot_Template.Data;
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

        private static UserInfoHandler<ExtendedUser> infoHandler = new UserInfoHandler<ExtendedUser>();

        #endregion Fields

        public static async Task HandleCommand(Command command, BaseBot Bot)
        {
            ExtendedUser[] tBanks = infoHandler.FindUsers(command.mentions);
            ExtendedUser bank = infoHandler.FindUser(command.sender);

            switch (command.commandStr)
            {
                case "echo":
                    await Bot.SendMessage(command.channel, command.commandArgString);
                    break;

                case "echodm":
                    await Bot.SendDM(command.sender, command.commandArgString);
                    break;

                case "bal":
                    if (command.mentions.Length == 0) await Bot.SendMessage(command.channel, "{User} You Have {Value} {Currency}", command.Source, command.sender, Value: bank.balance, CurrencyName: "ShitCoin");
                    else await Bot.SendMessage(command.channel, "{User} {User0} Has {Value} {Currency}", command.Source, command.sender, command.mentions, Value: tBanks[0].balance, CurrencyName: "ShitCoin");
                    break;

                case "pay":
                    if (command.mentions.Length > 0 && command.values.Length > 0)
                    {
                        if (bank.balance >= command.values[0])
                        {
                            bank.balance -= command.values[0];
                            tBanks[0].balance += command.values[0];
                            await Bot.SendMessage(command.channel, "{User} Paid {Value} {Currency} To {User0}", command.Source, command.sender, command.mentions, command.values[0], "Shit Coin");
                        }
                        else await Bot.SendMessage(command.channel, "{User} You Only Have {Value} {Currency}", command.Source, command.sender, Value: bank.balance, CurrencyName: "Shit Coin");
                    }
                    else await Bot.SendMessage(command.channel, "{User} You Fucked Up {NWord}", command.Source, command.sender);
                    break;
            }
        }

        public static async Task HandleMessage(Message message, BaseBot Bot)
        {
            string[] iams = { "i am", "i'm", "im" };
            int index;

            string msg = message.body.ToLower();
            foreach (string iam in iams)
            {
                if (msg.Contains(iam))
                {
                    index = msg.IndexOf(iam) + iam.Length;
                    string newMsg = msg.Substring(index, msg.Length - index).Trim();

                    await Bot.SendMessage(message.channel, $"Hi {newMsg}, Im Dad", message.Source, message.sender);
                    break;
                }
            }
        }
    }
}