using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistencia;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateHostBuilder(args).Build();
            using(var ambiente = hostserver.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CursosOnlineContext>();
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(ex, "Ocurrió un error en la migración");
                }
                hostserver.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
