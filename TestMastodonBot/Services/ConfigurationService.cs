using Microsoft.Extensions.Configuration;
using TestMastodonBot.Interfaces;
using TestMastodonBot.Models;

namespace TestMastodonBot.Services
{
    public class ConfigurationService: IConfigurationService
    {
        private const string InstanceVariableName = "instance";

        private readonly IConfiguration _config;
        
        private MastodonUser? _mastodonUser = null;
        private string? _instance = null;

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

        public string? GetInstance()
        {
            if (string.IsNullOrWhiteSpace(_instance))
            {
                _instance = _config
                    .GetSection(InstanceVariableName)
                    .Get<string>();
            }

            return _instance;
        }
    }
}