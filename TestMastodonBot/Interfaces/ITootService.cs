namespace TestMastodonBot.Interfaces
{
    public interface ITootService
    {
         Task Execute(CancellationToken cancellationToken);
    }
}