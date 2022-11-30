using TestMastodonBot.Models;

namespace TestMastodonBot.Interfaces
{
    public interface IConfigurationService
    {
         MastodonUser? GetUser();
         string GetAppName();
         string GetInstance();
    }
}