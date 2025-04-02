using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Diseases;

public interface IDiseasesClient
{
    Task<DiseaseDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto[]> GetAsync(
        GetDiseasesRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto> CreateAsync(
        DiseaseDto disease,
        CancellationToken cancellationToken = default);

    Task<DiseaseDto> UpdateAsync(
        int id,
        DiseaseDto disease,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}