using AspNetClientExample.Domain.Dtos;
using RestSharp;

namespace AspNetClientExample.Api.Clients.Account;

public class AccountClient : HttpClientBase, IAccountClient
{
    public AccountClient(string address, TimeSpan timeout)
        : base(address, timeout)
    {
    }

    public Task<TokenDto> LoginAsync(
        LoginDto login,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<TokenDto>(
            Method.Post,
            "api/account/login",
            request => request
                .AddBody(login),
            cancellationToken);
    }

    public Task RegisterAsync(
        RegisterDto register,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Post,
            "api/account/register",
            request => request
                .AddBody(register),
            cancellationToken);
    }

    public Task<TokenDto> RefreshTokenAsync(
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync<TokenDto>(
            Method.Post,
            "api/account/refresh",
            cancellationToken);
    }

    public Task ChangePasswordAsync(
        ChangePasswordDto changePassword,
        CancellationToken cancellationToken = default)
    {
        return SendRequestAsync(
            Method.Post,
            "api/account/change-password",
            request => request
                .AddBody(changePassword),
            cancellationToken);
    }
}