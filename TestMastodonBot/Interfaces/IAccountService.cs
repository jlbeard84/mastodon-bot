namespace TestMastodonBot.Interfaces
{
    public interface IAccountService
    {
         Task FollowBack(
            string accountIdToFollow);
    }
}