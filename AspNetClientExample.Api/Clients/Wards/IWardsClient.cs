using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Wards;

public interface IWardsClient
{
    Task<WardDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<WardDto[]> GetAsync(
        GetWardsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<WardDto> CreateAsync(
        WardDto ward,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<WardDto> UpdateAsync(
        int id,
        WardDto ward,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}