using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Departments;

public interface IDepartmentsClient
{
	Task<DepartmentDto> GetAsync(int id);
	Task<DepartmentDto[]> GetAsync(GetDepartmentsRequest? request = null);
	Task<DepartmentDto> CreateAsync(DepartmentDto department);
	Task<DepartmentDto> UpdateAsync(int id, DepartmentDto department);
	Task DeleteAsync(int id);
}