using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public class DoctorExaminationsClient : HttpClientBase, IDoctorExaminationsClient
{
    private readonly ITokenProvider _tokenProvider;

    public DoctorExaminationsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<DoctorExaminationDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorExaminationDto>(
            Method.Get,
            $"api/doctorExaminations/{id}",
            token,
            cancellationToken);
    }

    public async Task<DoctorExaminationDto[]> GetAsync(
        GetDoctorExaminationsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorExaminationDto[]>(
            Method.Get,
            "api/doctorExaminations",
            token,
            restRequest => restRequest
                .AddQueryParameterIfNotNull("dateFrom", request?.DateFrom)
                .AddQueryParameterIfNotNull("dateTo", request?.DateTo)
                .AddQueryParametersIfNotNull("diseaseNames", request?.DiseaseNames)
                .AddQueryParametersIfNotNull("doctorNames", request?.DoctorNames)
                .AddQueryParametersIfNotNull("examinationNames", request?.ExaminationNames)
                .AddQueryParametersIfNotNull("wardNames", request?.WardNames),
            cancellationToken);
    }

    public async Task<DoctorExaminationDto> CreateAsync(
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorExaminationDto>(
            Method.Post,
            "api/doctorExaminations",
            token,
            restRequest => restRequest
                .AddBody(doctorExamination),
            cancellationToken);
    }

    public async Task<DoctorExaminationDto> UpdateAsync(
        int id,
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorExaminationDto>(
            Method.Put,
            $"api/doctorExaminations/{id}",
            token,
            restRequest => restRequest
                .AddBody(doctorExamination),
            cancellationToken);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        await SendRequestAsync(
            Method.Delete,
            $"api/doctorExaminations/{id}",
            token,
            cancellationToken);
    }
}