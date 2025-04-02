using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Examinations;

public class ExaminationsClient : HttpClientBase, IExaminationsClient
{
    public ExaminationsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<ExaminationDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ExaminationDto>(
            Method.Get,
            $"/examinations/{id}",
            token,
            cancellationToken);
    }

    public Task<ExaminationDto[]> GetAsync(
        GetExaminationsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ExaminationDto[]>(
            Method.Get,
            "/examinations",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("name", request?.Names),
            cancellationToken);
    }

    public Task<ExaminationDto> CreateAsync(
        ExaminationDto examination,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ExaminationDto>(
            Method.Post,
            "/examinations",
            token,
            restRequest => restRequest
                .AddBody(examination),
            cancellationToken);
    }

    public Task<ExaminationDto> UpdateAsync(
        int id,
        ExaminationDto examination,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<ExaminationDto>(
            Method.Put,
            $"/examinations/{id}",
            token,
            restRequest => restRequest
                .AddBody(examination),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/examinations/{id}",
            token,
            cancellationToken);
    }
}