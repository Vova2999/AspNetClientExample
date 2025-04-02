using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Wards;

public interface IWardsClient
{
	Task<WardDto> GetAsync(int id);
	Task<WardDto[]> GetAsync(GetWardsRequest? request = null);
	Task<WardDto> CreateAsync(WardDto ward);
	Task<WardDto> UpdateAsync(int id, WardDto ward);
	Task DeleteAsync(int id);
}