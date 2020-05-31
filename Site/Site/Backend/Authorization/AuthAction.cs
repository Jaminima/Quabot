using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Site.Backend.Data._MySQL.Objects;
using Site.Backend.Data;
using Site.Backend.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Site.Backend.Integrations;

namespace Site.Backend.Authorization
{
    public static class AuthAction
    {
        public static void GoDiscordBot(NavigationManager nav, Streamer streamer, string redirectURL="")
        {
            GoOauthTo("https://discord.com/api/oauth2/authorize", "bot", "discord-bot", redirectURL, 0, streamer,  nav);
        }

        public static void GoDiscordSignin(NavigationManager nav,  string redirectURL = "")
        {
            GoOauthTo("https://discord.com/api/oauth2/authorize", "email+identify", "discord-login", redirectURL, 0, null,  nav);
        }

        public static void GoTwitchSignin(NavigationManager nav, string redirectURL = "")
        {
            GoOauthTo("https://id.twitch.tv/oauth2/authorize", "user_read+user:read:email", "twitch-login", redirectURL, 1, null, nav);
        }

        private static void GoOauthTo(string URL, string scope, string authMode, string redirectURL, int Src, Streamer streamer, NavigationManager nav)
        {
            URL += "?";
            URL += "response_type=code";
            if (Src == 0)//Discord Bot
            {
                URL += "&redirect_uri=" + new UriBuilder(Config.Conf.oAuthRedirect).ToString();
                URL += "&client_id=" + Config.Conf.discord_bot_client_id;
            }
            else if (Src == 1)//Twitch
            {
                URL += "&redirect_uri=" + Config.Conf.oAuthRedirect;
                URL += "&client_id=" + Config.Conf.twitch_sign_client_id;
            }

            URL += "&scope=" + scope;
            URL += "&state=identifier%3D{0}%26auth_mode="+authMode+ "%26redirect="+redirectURL;

            GoOauthTo(URL, streamer, nav);
        }

        private static void GoOauthTo(string URL, Streamer S, NavigationManager nav)
        {
            URL = String.Format(URL, Identifiers.AddIdent(S, 1));
            nav.NavigateTo(URL);
        }

        public static async Task<JToken> GetTwitchLogin(NavigationManager nav, ISessionStorageService sessionStorage, string Redirect = "")
        {
            string token = await sessionStorage.GetItemAsync<string>("twitch-token");
            string temp;
            JToken Data=null;
            if (token != null)
            {
                temp = await sessionStorage.GetItemAsync<string>("t_Data");

                if (temp == null)
                {
                    Data = (await Twitch.GetUser(token))["data"][0];
                    await sessionStorage.SetItemAsync("t_Data", Data.ToString());
                }
                else
                {
                    Data = JToken.Parse(temp);
                }
            }
            else { AuthAction.GoTwitchSignin(nav,Redirect); }
            return Data;
        }

        public static async Task<JToken> GetDiscordLogin(NavigationManager nav, ISessionStorageService sessionStorage, string Redirect = "")
        {
            string token = await sessionStorage.GetItemAsync<string>("discord-token");
            string temp;
            JToken Data = null;
            if (token != null)
            {
                temp = await sessionStorage.GetItemAsync<string>("d_Data");

                if (temp == null)
                {
                    Data = (await Discord.GetUser(token));
                    await sessionStorage.SetItemAsync("d_Data", Data.ToString());
                }
                else
                {
                    Data = JToken.Parse(temp);
                }
            }
            else { AuthAction.GoDiscordSignin(nav, Redirect); }
            return Data;
        }
    }
}
