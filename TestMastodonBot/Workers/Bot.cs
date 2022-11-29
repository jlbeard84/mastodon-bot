using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestMastodonBot.Interfaces;

namespace TestMastodonBot.Workers
{
    public class Bot: BackgroundService
    {
        private readonly ILogger<Bot> _logger;
        private readonly IHost _host;
        private readonly IConfigurationService _configService;

        public Bot(
            ILogger<Bot> logger,
            IHost host,
            IConfigurationService configService)
        {
            _logger = logger;
            _host = host;
            _configService = configService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var user = _configService.GetUser();
                _logger.LogInformation($"Logging at {DateTime.Now} for user {user?.Email ?? "N/A"}");
                await Task.Delay(1000);
            }
        }
    }
}