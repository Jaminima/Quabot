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

namespace Site.Backend.Authorization
{
    public static class AuthAction
    {
        public static void GoDiscordBot(NavigationManager nav)
        {
            GoOauthTo("https://discordapp.com/oauth2/authorize?client_id=" + Config.Conf.discord_bot_client_id + "&scope=bot&permissions=0&response_type=code&redirect_uri=" + new UriBuilder(Config.Conf.oAuthRedirect).ToString() + "&state=identifier%3D{0}%26auth_mode=discord-bot", nav);
        }

        public static void GoDiscordSignin(NavigationManager nav)
        {
            GoOauthTo("https://discord.com/api/oauth2/authorize?client_id=" + Config.Conf.discord_bot_client_id + "&redirect_uri=" + new UriBuilder(Config.Conf.oAuthRedirect).ToString() + "&response_type=code&scope=email+identify&state=identifier%3D{0}%26auth_mode=discord-login", nav);
        }

        public static void GoTwitchSignin(NavigationManager nav)
        {
            GoOauthTo("https://id.twitch.tv/oauth2/authorize?response_type=code&client_id=" + Config.Conf.twitch_sign_client_id + "&redirect_uri=" + Config.Conf.oAuthRedirect + "&scope=user_read+user:read:email&state=identifier%3D{0}%26auth_mode=twitch-login",nav);
        }

        private static void GoOauthTo(string URL, NavigationManager nav)
        {
            URL = String.Format(URL, Identifiers.AddIdent(DataHandler.FindStreamer(1), 1));
            nav.NavigateTo(URL);
        }
    }
}
