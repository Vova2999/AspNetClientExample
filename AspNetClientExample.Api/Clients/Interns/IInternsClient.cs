using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Interns;

public interface IInternsClient
{
    Task<InternDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<InternDto[]> GetAsync(
        GetInternsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<InternDto> CreateAsync(
        InternDto intern,
        CancellationToken cancellationToken = default);

    Task<InternDto> UpdateAsync(
        int id,
        InternDto intern,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}