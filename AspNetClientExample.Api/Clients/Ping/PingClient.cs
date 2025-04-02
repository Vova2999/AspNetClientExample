namespace AspNetClientExample.Api.Clients.Ping;

public class PingClient : HttpClientBase, IPingClient
{
	public PingClient(string address) : base(address)
	{
	}

	public Task Ping()
	{
		return SendAsync(
			HttpMethod.Get,
			"/ping",
			null,
			null);
    }
}