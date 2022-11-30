using Mastonet;

namespace TestMastodonBot.Interfaces
{
    public interface IRegistrationService
    {
        Task Register();
        AuthenticationClient GetAuthClient();
        Task<MastodonClient> GetMastodonClient();
    }
}