using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Wards;

public class WardsClient : HttpClientBase, IWardsClient
{
    public WardsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<WardDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<WardDto>(
            Method.Get,
            $"/wards/{id}",
            token,
            cancellationToken);
    }

    public Task<WardDto[]> GetAsync(
        GetWardsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<WardDto[]>(
            Method.Get,
            "/wards",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("name", request?.Names)
                .AddQueryParameterIfNotNull("placesFrom", request?.PlacesFrom)
                .AddQueryParameterIfNotNull("placesTo", request?.PlacesTo)
                .AddQueryParametersIfNotNull("departmentName", request?.DepartmentNames),
            cancellationToken);
    }

    public Task<WardDto> CreateAsync(
        WardDto ward,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<WardDto>(
            Method.Post,
            "/wards",
            token,
            restRequest => restRequest
                .AddBody(ward),
            cancellationToken);
    }

    public Task<WardDto> UpdateAsync(
        int id,
        WardDto ward,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<WardDto>(
            Method.Put,
            $"/wards/{id}",
            token,
            restRequest => restRequest
                .AddBody(ward),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/wards/{id}",
            token,
            cancellationToken);
    }
}