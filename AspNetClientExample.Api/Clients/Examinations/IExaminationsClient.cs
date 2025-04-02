using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Examinations;

public interface IExaminationsClient
{
    Task<ExaminationDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto[]> GetAsync(
        GetExaminationsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto> CreateAsync(
        ExaminationDto examination,
        CancellationToken cancellationToken = default);

    Task<ExaminationDto> UpdateAsync(
        int id,
        ExaminationDto examination,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}