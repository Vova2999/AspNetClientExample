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

    public Task<DoctorExaminationDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DoctorExaminationDto>(
                Method.Get,
                $"api/doctorExaminations/{id}",
                token,
                cancellationToken));
    }

    public Task<DoctorExaminationDto[]> GetAsync(
        GetDoctorExaminationsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DoctorExaminationDto[]>(
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
                cancellationToken));
    }

    public Task<DoctorExaminationDto> CreateAsync(
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DoctorExaminationDto>(
                Method.Post,
                "api/doctorExaminations",
                token,
                restRequest => restRequest
                    .AddBody(doctorExamination),
                cancellationToken));
    }

    public Task<DoctorExaminationDto> UpdateAsync(
        int id,
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<DoctorExaminationDto>(
                Method.Put,
                $"api/doctorExaminations/{id}",
                token,
                restRequest => restRequest
                    .AddBody(doctorExamination),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token => SendRequestAsync(
            Method.Delete,
            $"api/doctorExaminations/{id}",
            token,
            cancellationToken));
    }
}