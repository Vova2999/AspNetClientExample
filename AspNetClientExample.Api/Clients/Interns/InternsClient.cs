using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Interns;

public class InternsClient : HttpClientBase, IInternsClient
{
    public InternsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<InternDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<InternDto>(
            Method.Get,
            $"/interns/{id}",
            token,
            cancellationToken);
    }

    public Task<InternDto[]> GetAsync(
        GetInternsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<InternDto[]>(
            Method.Get,
            "/interns",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("doctorName", request?.DoctorNames),
            cancellationToken);
    }

    public Task<InternDto> CreateAsync(
        InternDto intern,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<InternDto>(
            Method.Post,
            "/interns",
            token,
            restRequest => restRequest
                .AddBody(intern),
            cancellationToken);
    }

    public Task<InternDto> UpdateAsync(
        int id,
        InternDto intern,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<InternDto>(
            Method.Put,
            $"/interns/{id}",
            token,
            restRequest => restRequest
                .AddBody(intern),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/interns/{id}",
            token,
            cancellationToken);
    }
}