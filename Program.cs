using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Migrations;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinalWork_BD_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
                DataSeed.Seed(context);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
