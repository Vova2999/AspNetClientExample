using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Interns;

public class InternsClient : HttpClientBase, IInternsClient
{
    private readonly ITokenProvider _tokenProvider;

    public InternsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<InternDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<InternDto>(
                Method.Get,
                $"api/interns/{id}",
                token,
                cancellationToken));
    }

    public Task<InternDto[]> GetAsync(
        GetInternsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<InternDto[]>(
                Method.Get,
                "api/interns",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("doctorNames", request?.DoctorNames),
                cancellationToken));
    }

    public Task<InternDto> CreateAsync(
        InternDto intern,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<InternDto>(
                Method.Post,
                "api/interns",
                token,
                restRequest => restRequest
                    .AddBody(intern),
                cancellationToken));
    }

    public Task<InternDto> UpdateAsync(
        int id,
        InternDto intern,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<InternDto>(
                Method.Put,
                $"api/interns/{id}",
                token,
                restRequest => restRequest
                    .AddBody(intern),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync(
                Method.Delete,
                $"api/interns/{id}",
                token,
                cancellationToken));
    }
}