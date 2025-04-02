using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Departments;

public class DepartmentsClient : HttpClientBase, IDepartmentsClient
{
    public DepartmentsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<DepartmentDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DepartmentDto>(
            Method.Get,
            $"/departments/{id}",
            token,
            cancellationToken);
    }

    public Task<DepartmentDto[]> GetAsync(
        GetDepartmentsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DepartmentDto[]>(
            Method.Get,
            "/departments",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("building", request?.Buildings)
                .AddQueryParameterIfNotNull("financingFrom", request?.FinancingFrom)
                .AddQueryParameterIfNotNull("financingTo", request?.FinancingTo)
                .AddQueryParametersIfNotNull("name", request?.Names),
            cancellationToken);
    }

    public Task<DepartmentDto> CreateAsync(
        DepartmentDto department,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DepartmentDto>(
            Method.Post,
            "/departments",
            token,
            restRequest => restRequest
                .AddBody(department),
            cancellationToken);
    }

    public Task<DepartmentDto> UpdateAsync(
        int id,
        DepartmentDto department,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DepartmentDto>(
            Method.Put,
            $"/departments/{id}",
            token,
            restRequest => restRequest
                .AddBody(department),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/departments/{id}",
            token,
            cancellationToken);
    }
}