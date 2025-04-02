namespace AspNetClientExample.Api.Clients.Ping;

public interface IPingClient
{
    Task Ping(CancellationToken cancellationToken = default);
}