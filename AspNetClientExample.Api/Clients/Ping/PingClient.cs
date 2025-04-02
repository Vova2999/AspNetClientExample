using RestSharp;

namespace AspNetClientExample.Api.Clients.Ping;

public class PingClient : HttpClientBase, IPingClient
{
    public PingClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task Ping(CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Get,
            "api/ping",
            cancellationToken);
    }
}