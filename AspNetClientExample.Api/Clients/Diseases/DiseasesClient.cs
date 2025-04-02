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

    public async Task<DiseaseDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DiseaseDto>(
            Method.Get,
            $"api/diseases/{id}",
            token,
            cancellationToken);
    }

    public async Task<DiseaseDto[]> GetAsync(
        GetDiseasesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DiseaseDto[]>(
            Method.Get,
            "api/diseases",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("names", request?.Names),
            cancellationToken);
    }

    public async Task<DiseaseDto> CreateAsync(
        DiseaseDto disease,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DiseaseDto>(
            Method.Post,
            "api/diseases",
            token,
            restRequest => restRequest
                .AddBody(disease),
            cancellationToken);
    }

    public async Task<DiseaseDto> UpdateAsync(
        int id,
        DiseaseDto disease,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DiseaseDto>(
            Method.Put,
            $"api/diseases/{id}",
            token,
            restRequest => restRequest
                .AddBody(disease),
            cancellationToken);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        await SendRequestAsync(
            Method.Delete,
            $"/diseases/{id}",
            token,
            cancellationToken);
    }
}