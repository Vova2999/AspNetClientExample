using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Diseases;

public class DiseasesClient : HttpClientBase, IDiseasesClient
{
    public DiseasesClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<DiseaseDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DiseaseDto>(
            Method.Get,
            $"/diseases/{id}",
            token,
            cancellationToken);
    }

    public Task<DiseaseDto[]> GetAsync(
        GetDiseasesRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DiseaseDto[]>(
            Method.Get,
            "/diseases",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("name", request?.Names),
            cancellationToken);
    }

    public Task<DiseaseDto> CreateAsync(
        DiseaseDto disease,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DiseaseDto>(
            Method.Post,
            "/diseases",
            token,
            restRequest => restRequest
                .AddBody(disease),
            cancellationToken);
    }

    public Task<DiseaseDto> UpdateAsync(
        int id,
        DiseaseDto disease,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DiseaseDto>(
            Method.Put,
            $"/diseases/{id}",
            token,
            restRequest => restRequest
                .AddBody(disease),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/diseases/{id}",
            token,
            cancellationToken);
    }
}