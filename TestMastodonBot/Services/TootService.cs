using Microsoft.Extensions.Logging;
using TestMastodonBot.Interfaces;

namespace TestMastodonBot.Services
{
    public class TootService: ITootService
    {
        private readonly ILogger<TootService> _logger;
        private readonly IConfigurationService _configService;

        public TootService(
            ILogger<TootService> logger,
            IConfigurationService configService)
        {
            _logger = logger;
            _configService = configService;
        }

        public async Task Execute(
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                _logger.LogInformation("Mastadon stuff goes here");
                await Task.Delay(1000);
            }
            catch(OperationCanceledException cancelledException)
            {
                _logger.LogInformation($"Cancellation requested in {nameof(TootService)}.{nameof(Execute)}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(TootService)}.{nameof(Execute)}");
            }
        }
    }
}