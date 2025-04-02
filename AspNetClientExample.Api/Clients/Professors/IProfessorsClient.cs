using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Professors;

public interface IProfessorsClient
{
    Task<ProfessorDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto[]> GetAsync(
        GetProfessorsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto> CreateAsync(
        ProfessorDto professor,
        CancellationToken cancellationToken = default);

    Task<ProfessorDto> UpdateAsync(
        int id,
        ProfessorDto professor,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}