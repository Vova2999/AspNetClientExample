using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Interns;

public interface IInternsClient
{
	Task<InternDto> GetAsync(int id);
	Task<InternDto[]> GetAsync(GetInternsRequest? request = null);
	Task<InternDto> CreateAsync(InternDto intern);
	Task<InternDto> UpdateAsync(int id, InternDto intern);
	Task DeleteAsync(int id);
}