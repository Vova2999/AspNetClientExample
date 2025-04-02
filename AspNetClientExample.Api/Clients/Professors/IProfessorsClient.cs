using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Professors;

public interface IProfessorsClient
{
	Task<ProfessorDto> GetAsync(int id);
	Task<ProfessorDto[]> GetAsync(GetProfessorsRequest? request = null);
	Task<ProfessorDto> CreateAsync(ProfessorDto professor);
	Task<ProfessorDto> UpdateAsync(int id, ProfessorDto professor);
	Task DeleteAsync(int id);
}