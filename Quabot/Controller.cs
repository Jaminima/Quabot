using DTBot_Template.Data;
using DTBot_Template.Data._MySQL;
using DTBot_Template.Events;
using System;
using System.Linq;
using System.Threading;

namespace DTBot_Template
{
    public class Controller
    {
        public static Random rnd = new Random();

        private static BotConfig botConfig;
        public static DTBot_Template.Discord dBot;
        public static DTBot_Template.Twitch tBot;

        private static void Main(string[] args)
        {
            Start();

            while (true) { Console.ReadLine(); }
        }

        public static void Start()
        {
            botConfig = BotConfig.Load();

            if (botConfig == null) Console.WriteLine("Please fill the config file with valid details, and run again");
            else
            {
                SQL.pubInstance = new SQL(botConfig.sql_Username, "sys", botConfig.sql_Password, botConfig.sql_Server);

                string[] Channels = CurrencyParticipant.FromTable<CurrencyParticipant>("currency_participants").Select(x => x.twitch_name).ToArray();

                tBot = new Twitch(botConfig.twitch_Username, botConfig.twitch_token, Channels);
                tBot.MessageHandler += ChatEvents.HandleMessage;
                tBot.CommandHandler += ChatEvents.HandleCommand;

                dBot = new DTBot_Template.Discord(botConfig.discord_Token);
                dBot.MessageHandler += ChatEvents.HandleMessage;
                dBot.CommandHandler += ChatEvents.HandleCommand;

                Console.WriteLine("Bots Started");

                new Thread(() => Rewards.RewardChecker()).Start();
            }
        }
    }
}