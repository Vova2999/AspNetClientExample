using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Diseases;

public class DiseasesClient : HttpClientBase, IDiseasesClient
{
    private readonly ITokenProvider _tokenProvider;

    public DiseasesClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<DiseaseDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DiseaseDto>(
                Method.Get,
                $"api/diseases/{id}",
                token,
                cancellationToken));
    }

    public Task<DiseaseDto[]> GetAsync(
        GetDiseasesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DiseaseDto[]>(
                Method.Get,
                "api/diseases",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("names", request?.Names),
                cancellationToken));
    }

    public Task<DiseaseDto> CreateAsync(
        DiseaseDto disease,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DiseaseDto>(
                Method.Post,
                "api/diseases",
                token,
                restRequest => restRequest
                    .AddBody(disease),
                cancellationToken));
    }

    public Task<DiseaseDto> UpdateAsync(
        int id,
        DiseaseDto disease,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DiseaseDto>(
                Method.Put,
                $"api/diseases/{id}",
                token,
                restRequest => restRequest
                    .AddBody(disease),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync(
                Method.Delete,
                $"/diseases/{id}",
                token,
                cancellationToken));
    }
}