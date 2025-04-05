using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Professors;

public class ProfessorsClient : HttpClientBase, IProfessorsClient
{
    private readonly ITokenProvider _tokenProvider;

    public ProfessorsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<ProfessorDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ProfessorDto>(
                Method.Get,
                $"api/professors/{id}",
                token,
                cancellationToken));
    }

    public Task<ProfessorDto[]> GetAsync(
        GetProfessorsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ProfessorDto[]>(
                Method.Get,
                "api/professors",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("doctorNames", request?.DoctorNames),
                cancellationToken));
    }

    public Task<ProfessorDto> CreateAsync(
        ProfessorDto professor,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ProfessorDto>(
                Method.Post,
                "api/professors",
                token,
                restRequest => restRequest
                    .AddBody(professor),
                cancellationToken));
    }

    public Task<ProfessorDto> UpdateAsync(
        int id,
        ProfessorDto professor,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ProfessorDto>(
                Method.Put,
                $"api/professors/{id}",
                token,
                restRequest => restRequest
                    .AddBody(professor),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync(
                Method.Delete,
                $"api/professors/{id}",
                token,
                cancellationToken));
    }
}