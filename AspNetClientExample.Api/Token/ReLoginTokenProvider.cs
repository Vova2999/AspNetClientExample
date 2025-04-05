using System.Diagnostics.CodeAnalysis;
using System.Net;
using AspNetClientExample.Api.Clients.Account;
using AspNetClientExample.Api.Exceptions;
using AspNetClientExample.Domain.Dtos;
using Nito.AsyncEx;

namespace AspNetClientExample.Api.Token;

public class ReLoginTokenProvider : ITokenProvider
{
    private readonly IAccountClient _accountClient;

    private readonly AsyncLock _tokenAsyncLock = new();

    private string? _token;
    private LoginDto? _login;

    public ReLoginTokenProvider(
        IAccountClient accountClient)
    {
        _accountClient = accountClient;
    }

    public Task LoginAsync(LoginDto login)
    {
        _login = login;
        return GetTokenAsync();
    }

    public async Task<TResult> ExecuteWithToken<TResult>(Func<string, Task<TResult>> action)
    {
        try
        {
            return await action(await GetTokenAsync());
        }
        catch (SendRequestException exception) when (exception.StatusCode == HttpStatusCode.Unauthorized)
        {
            _token = null;
            return await action(await GetTokenAsync());
        }
    }

    private async Task<string> GetTokenAsync()
    {
        var token = _token;
        if (!IsNeedRefreshToken(token))
            return token;

        using var _ = await _tokenAsyncLock.LockAsync();

        if (!IsNeedRefreshToken(_token))
            return _token;

        var refreshResult = await _accountClient.LoginAsync(_login);
        _token = refreshResult.Token ?? throw new ArgumentNullException(nameof(refreshResult.Token), "Token is empty");

        return _token;
    }

    [MemberNotNullWhen(true, nameof(_login))]
    private bool IsNeedRefreshToken([NotNullWhen(false)] string? token)
    {
        if (token != null)
            return false;

        if (_login == null)
            throw new InvalidOperationException("You must login before using the token");

        return true;
    }
}