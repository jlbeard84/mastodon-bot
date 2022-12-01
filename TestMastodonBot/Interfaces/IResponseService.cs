namespace TestMastodonBot.Interfaces
{
    public interface IResponseService
    {
         Task<string> RespondWithRandomMessage(
            string replyToAccountName,
            string replyToDisplayName,
            string originalStatusId);
    }
}