﻿using DTBot_Template;
using DTBot_Template.Data;
using DTBot_Template.Data._MySQL;
using DTBot_Template.Generics;
using DTBot_Template.Intergrations;
using System;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        #region Fields

        private static BotConfig botConfig;

        private static UserInfoHandler<_userInfo> infoHandler = new UserInfoHandler<_userInfo>();

        #endregion Fields

        #region Methods

        private static void Main(string[] args)
        {
            botConfig = BotConfig.Load();

            if (botConfig == null) Console.WriteLine("Please fill the config file with valid details, and run again");
            else
            {
                tBot = new Twitch(botConfig.twitch_Username, botConfig.twitch_token, botConfig.twitch_Channel);
                tBot.MessageHandler += HandleMessage;
                tBot.CommandHandler += HandleCommand;

                dBot = new DTBot_Template.Discord(botConfig.discord_Token);
                dBot.MessageHandler += HandleMessage;
                dBot.CommandHandler += HandleCommand;

                SQL.pubInstance = new SQL(botConfig.sql_Username, "sys", botConfig.sql_Password, botConfig.sql_Server);

                infoHandler.Load();

                Console.WriteLine("Bots Started");
            }

            while (true) { Console.ReadLine(); }
        }

        #endregion Methods

        public static DTBot_Template.Discord dBot;
        public static DTBot_Template.Twitch tBot;

        public static async Task HandleCommand(Command command, BaseBot Bot)
        {
            _userInfo[] tBanks = infoHandler.FindUsers(command.mentions);
            _userInfo bank = infoHandler.FindUser(command.sender);

            switch (command.commandStr)
            {
                case "echo":
                    await Bot.SendMessage(command.channel, command.commandArgString);
                    break;

                case "echodm":
                    await Bot.SendDM(command.sender, command.commandArgString);
                    break;

                case "WTF":
                    Streamlabs.CreateDonation("Jamm", 1, botConfig.Streamlabs);
                    await Bot.SendMessage(command.channel, Streamlabs.GetDonations(botConfig.Streamlabs).ToString());
                    break;

                case "bal":

                    if (command.mentions.Length == 0) await Bot.SendMessage(command, "{User} You Have {Value} {Currency}", bank.balance, "Shit Coin");
                    else await Bot.SendMessage(command, "{User} {User0} Has {Value} {Currency}", tBanks[0].balance, "Shit Coin");
                    break;

                case "pay":
                    if (command.mentions.Length > 0 && command.values.Length > 0)
                    {
                        if (bank.balance >= command.values[0])
                        {
                            bank.balance -= command.values[0];
                            tBanks[0].balance += command.values[0];
                            await Bot.SendMessage(command, "{User} Paid {Value0} {Currency} To {User0}", CurrencyName: "Shit Coin");
                        }
                        else await Bot.SendMessage(command, "{User} You Only Have {Value} {Currency}", bank.balance, CurrencyName: "Shit Coin");
                    }
                    else await Bot.SendMessage(command, "{User} You Fucked Up {NWord}");
                    break;
            }

            infoHandler.Save();
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

                    await Bot.SendMessage(message, $"Hi {newMsg}, Im Dad");
                    break;
                }
            }
        }
    }
}