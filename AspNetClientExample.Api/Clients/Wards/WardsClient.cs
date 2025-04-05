using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Wards;

public class WardsClient : HttpClientBase, IWardsClient
{
    private readonly ITokenProvider _tokenProvider;

    public WardsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<WardDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<WardDto>(
                Method.Get,
                $"api/wards/{id}",
                token,
                cancellationToken));
    }

    public Task<WardDto[]> GetAsync(
        GetWardsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<WardDto[]>(
                Method.Get,
                "api/wards",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("names", request?.Names)
                    .AddQueryParameterIfNotNull("placesFrom", request?.PlacesFrom)
                    .AddQueryParameterIfNotNull("placesTo", request?.PlacesTo)
                    .AddQueryParametersIfNotNull("departmentNames", request?.DepartmentNames),
                cancellationToken));
    }

    public Task<WardDto> CreateAsync(
        WardDto ward,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<WardDto>(
                Method.Post,
                "api/wards",
                token,
                restRequest => restRequest
                    .AddBody(ward),
                cancellationToken));
    }

    public Task<WardDto> UpdateAsync(
        int id,
        WardDto ward,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<WardDto>(
                Method.Put,
                $"api/wards/{id}",
                token,
                restRequest => restRequest
                    .AddBody(ward),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync(
                Method.Delete,
                $"api/wards/{id}",
                token,
                cancellationToken));
    }
}