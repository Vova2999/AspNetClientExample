using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Departments;

public interface IDepartmentsClient
{
    Task<DepartmentDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto[]> GetAsync(
        GetDepartmentsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto> CreateAsync(
        DepartmentDto department,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto> UpdateAsync(
        int id,
        DepartmentDto department,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}