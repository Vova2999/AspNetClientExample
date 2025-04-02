using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Departments;

public interface IDepartmentsClient
{
    Task<DepartmentDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto[]> GetAsync(
        GetDepartmentsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto> CreateAsync(
        DepartmentDto department,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DepartmentDto> UpdateAsync(
        int id,
        DepartmentDto department,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}