using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Site.Backend.Data._MySQL;
using Site.Backend.Data;

namespace Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Config.Conf = Config.Load();

            SQL.pubInstance = new SQL(Config.Conf.sql_Username, "sys", Config.Conf.sql_Password, Config.Conf.sql_Server);
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
