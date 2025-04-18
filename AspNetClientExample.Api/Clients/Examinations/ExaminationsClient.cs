﻿using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Examinations;

public class ExaminationsClient : HttpClientBase, IExaminationsClient
{
    private readonly ITokenProvider _tokenProvider;

    public ExaminationsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
        : base(address, timeout)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<ExaminationDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ExaminationDto>(
                Method.Get,
                $"api/examinations/{id}",
                token,
                cancellationToken));
    }

    public Task<ExaminationDto[]> GetAsync(
        GetExaminationsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ExaminationDto[]>(
                Method.Get,
                "api/examinations",
                token,
                restRequest => restRequest
                    .AddQueryParametersIfNotNull("names", request?.Names),
                cancellationToken));
    }

    public Task<ExaminationDto> CreateAsync(
        ExaminationDto examination,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ExaminationDto>(
                Method.Post,
                "api/examinations",
                token,
                restRequest => restRequest
                    .AddBody(examination),
                cancellationToken));
    }

    public Task<ExaminationDto> UpdateAsync(
        int id,
        ExaminationDto examination,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync<ExaminationDto>(
                Method.Put,
                $"api/examinations/{id}",
                token,
                restRequest => restRequest
                    .AddBody(examination),
                cancellationToken));
    }

    public Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider.ExecuteWithToken(token =>
            SendRequestAsync(
                Method.Delete,
                $"api/examinations/{id}",
                token,
                cancellationToken));
    }
}