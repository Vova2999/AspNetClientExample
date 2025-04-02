using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Doctors;

public class DoctorsClient : HttpClientBase, IDoctorsClient
{
    public DoctorsClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<DoctorDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorDto>(
            Method.Get,
            $"/doctors/{id}",
            token,
            cancellationToken);
    }

    public Task<DoctorDto[]> GetAsync(
        GetDoctorsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorDto[]>(
            Method.Get,
            "/doctors",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("name", request?.Names)
                .AddQueryParameterIfNotNull("salaryFrom", request?.SalaryFrom)
                .AddQueryParameterIfNotNull("salaryTo", request?.SalaryTo)
                .AddQueryParametersIfNotNull("surname", request?.Surnames),
            cancellationToken);
    }

    public Task<DoctorDto> CreateAsync(
        DoctorDto doctor,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorDto>(
            Method.Post,
            "/doctors",
            token,
            restRequest => restRequest
                .AddBody(doctor),
            cancellationToken);
    }

    public Task<DoctorDto> UpdateAsync(
        int id,
        DoctorDto doctor,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<DoctorDto>(
            Method.Put,
            $"/doctors/{id}",
            token,
            restRequest => restRequest
                .AddBody(doctor),
            cancellationToken);
    }

    public Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Delete,
            $"/doctors/{id}",
            token,
            cancellationToken);
    }
}