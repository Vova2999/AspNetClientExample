using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Examinations;

public interface IExaminationsClient
{
    Task<ExaminationDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto[]> GetAsync(
        GetExaminationsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto> CreateAsync(
        ExaminationDto examination,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto> UpdateAsync(
        int id,
        ExaminationDto examination,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}