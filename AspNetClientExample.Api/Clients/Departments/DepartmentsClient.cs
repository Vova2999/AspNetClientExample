﻿using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Departments;

public class DepartmentsClient : HttpClientBase, IDepartmentsClient
{
    private readonly ITokenProvider _tokenProvider;

    public DepartmentsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<DepartmentDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DepartmentDto>(
                Method.Get,
                $"api/departments/{id}",
                token,
                cancellationToken));
    }

    public Task<DepartmentDto[]> GetAsync(
        GetDepartmentsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DepartmentDto[]>(
                Method.Get,
                "api/departments",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("buildings", request?.Buildings)
                    .AddQueryParameterIfNotNull("financingFrom", request?.FinancingFrom)
                    .AddQueryParameterIfNotNull("financingTo", request?.FinancingTo)
                    .AddQueryParametersIfNotNull("names", request?.Names),
                cancellationToken));
    }

    public Task<DepartmentDto> CreateAsync(
        DepartmentDto department,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DepartmentDto>(
                Method.Post,
                "api/departments",
                token,
                restRequest => restRequest
                    .AddBody(department),
                cancellationToken));
    }

    public Task<DepartmentDto> UpdateAsync(
        int id,
        DepartmentDto department,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DepartmentDto>(
                Method.Put,
                $"api/departments/{id}",
                token,
                restRequest => restRequest
                    .AddBody(department),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token => SendRequestAsync(
            Method.Delete,
            $"api/departments/{id}",
            token,
            cancellationToken));
    }
}