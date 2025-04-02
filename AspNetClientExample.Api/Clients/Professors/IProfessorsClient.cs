using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Professors;

public interface IProfessorsClient
{
    Task<ProfessorDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto[]> GetAsync(
        GetProfessorsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto> CreateAsync(
        ProfessorDto professor,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto> UpdateAsync(
        int id,
        ProfessorDto professor,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}