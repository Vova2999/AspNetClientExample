using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;

namespace AspNetClientExample.Api;

public abstract class HttpClientBase : IDisposable
{
	private readonly string _address;
	private readonly HttpClient _httpClient = new();

	protected HttpClientBase(string address)
	{
		_address = address.TrimEnd('/');
	}

	protected async Task SendAsync(
		HttpMethod method,
		string path,
		NameValueCollection? parameters,
		NameValueCollection? headers)
	{
		await SendInternalAsync(method, path, parameters, headers, (string?) null);
	}

	protected async Task SendAsync<TRequest>(
		HttpMethod method,
		string path,
		NameValueCollection? parameters,
		NameValueCollection? headers,
		TRequest? body)
	{
		await SendInternalAsync(method, path, parameters, headers, body);
	}

	protected async Task<TResponse> SendAsync<TResponse>(
		HttpMethod method,
		string path,
		NameValueCollection? parameters,
		NameValueCollection? headers)
	{
		var response = await SendInternalAsync(method, path, parameters, headers, (string?) null);
		return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())!;
	}

	protected async Task<TResponse> SendAsync<TRequest, TResponse>(
		HttpMethod method,
		string path,
		NameValueCollection? parameters,
		NameValueCollection? headers,
		TRequest? body)
	{
		var response = await SendInternalAsync(method, path, parameters, headers, body);
		return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())!;
	}

	private async Task<HttpResponseMessage> SendInternalAsync<TRequest>(
		HttpMethod method,
		string path,
		NameValueCollection? parameters,
		NameValueCollection? headers,
		TRequest? body)
	{
		var pathWithParameters = new UriBuilder(_address + path)
		{
			Query = parameters == null
				? null
				: string.Join("&",
					parameters.AllKeys
						.Where(key => key != null)
						.SelectMany(key => parameters.GetValues(key)!
							.Select(value => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}")))
		}.Uri;

		using var request = new HttpRequestMessage(method, pathWithParameters);

		if (headers != null)
			foreach (var key in headers.AllKeys.Where(key => key != null))
			foreach (var value in headers.GetValues(key)!)
				request.Headers.Add(key!, value);

		if (body is not null)
			request.Content = new StringContent(JsonConvert.SerializeObject(body));

		var response = await _httpClient.SendAsync(request);
		response.EnsureSuccessStatusCode();

		return response;
	}

	public void Dispose()
	{
		_httpClient.Dispose();
	}
}