using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Interns;

public interface IInternsClient
{
    Task<InternDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<InternDto[]> GetAsync(
        GetInternsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<InternDto> CreateAsync(
        InternDto intern,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<InternDto> UpdateAsync(
        int id,
        InternDto intern,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}