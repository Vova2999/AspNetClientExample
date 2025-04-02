using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Examinations;

public interface IExaminationsClient
{
	Task<ExaminationDto> GetAsync(int id);
	Task<ExaminationDto[]> GetAsync(GetExaminationsRequest? request = null);
	Task<ExaminationDto> CreateAsync(ExaminationDto examination);
	Task<ExaminationDto> UpdateAsync(int id, ExaminationDto examination);
	Task DeleteAsync(int id);
}