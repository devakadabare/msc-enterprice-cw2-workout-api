#pragma warning disable 0168, 0219


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkoutApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<Data.StoreContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                //context.Database.Migrate();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during migration");
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
