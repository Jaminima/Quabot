using DTBot_Template.Data;
using DTBot_Template.Generics;
using System.Linq;
using System.Threading.Tasks;

namespace DTBot_Template.Events
{
    public static class ChatEvents
    {
        #region Methods

        public static async Task HandleCommand(Command command, BaseBot Bot, CurrencyConfig currency)
        {
            _userInfo[] tBanks = CacheHandler.FindUsers(command.mentions, currency);
            _userInfo bank = CacheHandler.FindUser(command.sender, currency);

            switch (command.commandStr)
            {
                //case "echo":
                //    await Bot.SendMessage(command.channel, command.commandArgString, command.Source, currency);
                //    break;

                //case "echodm":
                //    await Bot.SendDM(command.sender, command.commandArgString, command.Source, currency);
                //    break;

                //case "wtf":
                //    new Thread(async() => await Bot.SendMessage(command, "{User} this was sent from another thread", currency)).Start();
                ////    //Streamlabs.CreateDonation("Jamm", 1, botConfig.Streamlabs);
                ////    await Bot.SendMessage(command.channel, Streamlabs.GetDonations(botConfig.Streamlabs).ToString());
                //    break;

                //case null when currency.BalanceCommands.Contains(command.commandStr):
                //    break;

                case string S when currency.BalanceCommands.Contains(command.commandStr):

                    if (command.mentions.Length == 0) await Bot.SendMessage(command, "{User} You Have {Value} {Currency}", currency, bank.balance);
                    else await Bot.SendMessage(command, "{User} {User0} Has {Value} {Currency}", currency, tBanks[0].balance);
                    break;

                case string S when currency.PayCommands.Contains(command.commandStr):
                    if (command.mentions.Length > 0 && command.values.Length > 0)
                    {
                        if (bank.balance >= command.values[0])
                        {
                            bank.balance -= command.values[0];
                            tBanks[0].balance += command.values[0];
                            bank.Update();
                            tBanks[0].Update();
                            await Bot.SendMessage(command, "{User} Paid {Value0} {Currency} To {User0}", currency);
                        }
                        else await Bot.SendMessage(command, "{User} You Only Have {Value} {Currency}", currency, bank.balance);
                    }
                    else await Bot.SendMessage(command, "{User} You Fucked Up {NWord}", currency);
                    break;

                case string S when currency.FishCommands.Contains(command.commandStr):
                    if (Rewards.AddFisher(Bot, command, bank, currency)) { await Bot.SendMessage(command, "{User} You Started Fishing", currency); bank.balance -= currency.FishCost; bank.Update(); }
                    else await Bot.SendMessage(command, "{User} You Are Already Fishing!", currency);
                    break;

                default:
                    if (currency.SimpleResponses.ContainsKey(command.commandStr)) await Bot.SendMessage(command, currency.SimpleResponses[command.commandStr], currency);
                    break;
            }
        }

        public static async Task HandleMessage(Message message, BaseBot Bot, CurrencyConfig currency)
        {
            _userInfo bank = CacheHandler.FindUser(message.sender, currency);
            Rewards.MessageRewardUser(bank);

            string[] iams = { "i am", "i'm", "im" };
            int index;

            string msg = message.body.ToLower();
            foreach (string iam in iams)
            {
                if (msg.Contains(iam))
                {
                    index = msg.IndexOf(iam) + iam.Length;
                    string newMsg = msg.Substring(index, msg.Length - index).Trim();

                    await Bot.SendMessage(message, $"Hi {newMsg}, Im Dad", currency);
                    break;
                }
            }
        }

        #endregion Methods
    }
}