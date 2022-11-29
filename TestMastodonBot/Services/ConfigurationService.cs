using Microsoft.Extensions.Configuration;
using TestMastodonBot.Interfaces;
using TestMastodonBot.Models;

namespace TestMastodonBot.Services
{
    public class ConfigurationService: IConfigurationService
    {
        private readonly IConfiguration _config;
        
        private MastodonUser? _mastodonUser = null;

        public ConfigurationService(
            IConfiguration config)
        {
            _config = config;
        }

        public MastodonUser? GetUser()
        {
            if (_mastodonUser == null)
            {
                _mastodonUser = _config
                    .GetSection(MastodonUser.ConfigSectionName)
                    .Get<MastodonUser>();
            }

            return _mastodonUser;
        }
    }
}