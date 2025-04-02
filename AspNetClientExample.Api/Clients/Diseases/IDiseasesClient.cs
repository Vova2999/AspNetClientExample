using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Diseases;

public interface IDiseasesClient
{
	Task<DiseaseDto> GetAsync(int id);
	Task<DiseaseDto[]> GetAsync(GetDiseasesRequest? request = null);
	Task<DiseaseDto> CreateAsync(DiseaseDto disease);
	Task<DiseaseDto> UpdateAsync(int id, DiseaseDto disease);
	Task DeleteAsync(int id);
}