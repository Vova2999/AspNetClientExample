using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public class DoctorExaminationsClient : HttpClientBase, IDoctorExaminationsClient
{
    public DoctorExaminationsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<DoctorExaminationDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorExaminationDto>(
            Method.Get,
            $"/doctorExaminations/{id}",
            token,
            cancellationToken);
    }

    public Task<DoctorExaminationDto[]> GetAsync(
        GetDoctorExaminationsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorExaminationDto[]>(
            Method.Get,
            "/doctorExaminations",
            token,
            restRequest => restRequest
                .AddQueryParameterIfNotNull("dateFrom", request?.DateFrom)
                .AddQueryParameterIfNotNull("dateTo", request?.DateTo)
                .AddQueryParametersIfNotNull("diseaseName", request?.DiseaseNames)
                .AddQueryParametersIfNotNull("doctorName", request?.DoctorNames)
                .AddQueryParametersIfNotNull("examinationName", request?.ExaminationNames)
                .AddQueryParametersIfNotNull("wardName", request?.WardNames),
            cancellationToken);
    }

    public Task<DoctorExaminationDto> CreateAsync(
        DoctorExaminationDto doctorExamination,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorExaminationDto>(
            Method.Post,
            "/doctorExaminations",
            token,
            restRequest => restRequest
                .AddBody(doctorExamination),
            cancellationToken);
    }

    public Task<DoctorExaminationDto> UpdateAsync(
        int id,
        DoctorExaminationDto doctorExamination,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorExaminationDto>(
            Method.Put,
            $"/doctorExaminations/{id}",
            token,
            restRequest => restRequest
                .AddBody(doctorExamination),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/doctorExaminations/{id}",
            token,
            cancellationToken);
    }
}