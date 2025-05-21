
namespace BeautySalon.AuthandClient.Application.Interfaces
{
    public interface IEventBus
    {
        Task SendMessageAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}