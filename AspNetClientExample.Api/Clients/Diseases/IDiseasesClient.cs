using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Diseases;

public interface IDiseasesClient
{
    Task<DiseaseDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto[]> GetAsync(
        GetDiseasesRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto> CreateAsync(
        DiseaseDto disease,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto> UpdateAsync(
        int id,
        DiseaseDto disease,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}