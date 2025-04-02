using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Wards;

public interface IWardsClient
{
    Task<WardDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<WardDto[]> GetAsync(
        GetWardsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<WardDto> CreateAsync(
        WardDto ward,
        CancellationToken cancellationToken = default);

    Task<WardDto> UpdateAsync(
        int id,
        WardDto ward,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}