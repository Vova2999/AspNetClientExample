﻿using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Doctors;

public class DoctorsClient : HttpClientBase, IDoctorsClient
{
    private readonly ITokenProvider _tokenProvider;

    public DoctorsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<DoctorDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorDto>(
            Method.Get,
            $"api/doctors/{id}",
            token,
            cancellationToken);
    }

    public async Task<DoctorDto[]> GetAsync(
        GetDoctorsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorDto[]>(
            Method.Get,
            "api/doctors",
            token,
            restRequest => restRequest
                .AddQueryParametersIfNotNull("names", request?.Names)
                .AddQueryParameterIfNotNull("salaryFrom", request?.SalaryFrom)
                .AddQueryParameterIfNotNull("salaryTo", request?.SalaryTo)
                .AddQueryParametersIfNotNull("surnames", request?.Surnames),
            cancellationToken);
    }

    public async Task<DoctorDto> CreateAsync(
        DoctorDto doctor,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorDto>(
            Method.Post,
            "api/doctors",
            token,
            restRequest => restRequest
                .AddBody(doctor),
            cancellationToken);
    }

    public async Task<DoctorDto> UpdateAsync(
        int id,
        DoctorDto doctor,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        return await SendRequestAsync<DoctorDto>(
            Method.Put,
            $"api/doctors/{id}",
            token,
            restRequest => restRequest
                .AddBody(doctor),
            cancellationToken);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync();
        await SendRequestAsync(
            Method.Delete,
            $"api/doctors/{id}",
            token,
            cancellationToken);
    }
}