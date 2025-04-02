using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Professors;

public class ProfessorsClient : HttpClientBase, IProfessorsClient
{
    public ProfessorsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<ProfessorDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ProfessorDto>(
            Method.Get,
            $"/professors/{id}",
            token,
            cancellationToken);
    }

    public Task<ProfessorDto[]> GetAsync(
        GetProfessorsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ProfessorDto[]>(
            Method.Get,
            "/professors",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("doctorName", request?.DoctorNames),
            cancellationToken);
    }

    public Task<ProfessorDto> CreateAsync(
        ProfessorDto professor,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ProfessorDto>(
            Method.Post,
            "/professors",
            token,
            restRequest => restRequest
                .AddBody(professor),
            cancellationToken);
    }

    public Task<ProfessorDto> UpdateAsync(
        int id,
        ProfessorDto professor,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ProfessorDto>(
            Method.Put,
            $"/professors/{id}",
            token,
            restRequest => restRequest
                .AddBody(professor),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/professors/{id}",
            token,
            cancellationToken);
    }
}