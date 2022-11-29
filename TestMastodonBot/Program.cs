using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestMastodonBot.Interfaces;
using TestMastodonBot.Models;
using TestMastodonBot.Services;
using TestMastodonBot.Workers;

public class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = CreateDefaultBuilder();
        var host = hostBuilder.Build();

        await host.RunAsync();
    }

    private static IHostBuilder CreateDefaultBuilder()
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(app => 
            {
                app
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile("local.appsettings.json", true, true);
            })
            .ConfigureServices((hostContext, services) => 
            {
                services.AddSingleton<IConfigurationService, ConfigurationService>();
                services.AddHostedService<Bot>();
            });

        return builder;
    }
}
