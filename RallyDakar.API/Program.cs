using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using RallyDakar.Dominio.DbContexto;

namespace RallyDakar.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Iniciando o aplicativo");

            try
            {
                var host = CreateHostBuilder(args).Build();

                using(var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    BaseDados.CargaInicial(services);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Aplica��o parou de rodar.");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseNLog();
                });
    }
}
