using Mastonet;
using Microsoft.Extensions.Logging;
using TestMastodonBot.Interfaces;

namespace TestMastodonBot.Services
{
    public class TootService: ITootService
    {
        private readonly ILogger<TootService> _logger;
        private readonly IConfigurationService _configService;
        private readonly IRegistrationService _registrationService;

        public TootService(
            ILogger<TootService> logger,
            IConfigurationService configService,
            IRegistrationService registrationService)
        {
            _logger = logger;
            _configService = configService;
            _registrationService = registrationService;
        }

        public async Task Execute(
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            TimelineStreaming? stream = null;

            try
            {
                await _registrationService.Register();

                var client = await _registrationService.GetMastodonClient();
                
                stream = client.GetUserStreaming();
                RegisterDelegates(stream);

                await stream.Start();

                while (!cancellationToken.IsCancellationRequested)
                {
                    // loop forever for now
                    await Task.Delay(500);
                }             
            }
            catch(OperationCanceledException cancelledException)
            {
                _logger.LogInformation($"Cancellation requested in {nameof(TootService)}.{nameof(Execute)}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(TootService)}.{nameof(Execute)}");
            }
            finally
            {
                if (stream != null)
                {
                    stream.Stop();
                    UnregisterDelegates(stream);   
                }
            }
        }

        private void RegisterDelegates(TimelineStreaming stream)
        {
            stream.OnUpdate += OnUpdate;
            stream.OnNotification += OnNotificataion;
        }

        private void UnregisterDelegates(TimelineStreaming stream)
        {
            stream.OnUpdate -= OnUpdate;
            stream.OnNotification -= OnNotificataion;
        }

        private void OnUpdate(object? sender, StreamUpdateEventArgs e)
        {
            _logger.LogInformation(e.Status.Content);
        }

        private void OnNotificataion(object? sender, StreamNotificationEventArgs e)
        {
            var message = $"Got message from {e.Notification.Account.AccountName}: {e.Notification.Status.Content}";
            _logger.LogInformation(message);
        }

    }
}