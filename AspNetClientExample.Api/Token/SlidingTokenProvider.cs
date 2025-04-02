using System.IdentityModel.Tokens.Jwt;
using AspNetClientExample.Api.Clients.Account;
using AspNetClientExample.Domain.Dtos;
using Nito.AsyncEx;

namespace AspNetClientExample.Api.Token;

public class SlidingTokenProvider : ITokenProvider
{
    private readonly IAccountClient _accountClient;
    private readonly int _refreshTokenBeforeExpirationInPercent;

    private readonly AsyncLock _tokenAsyncLock = new();
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    private string? _token;

    public SlidingTokenProvider(
        IAccountClient accountClient,
        int refreshTokenBeforeExpirationInPercent)
    {
        _accountClient = accountClient;
        _refreshTokenBeforeExpirationInPercent = refreshTokenBeforeExpirationInPercent;
    }

    public async Task LoginAsync(LoginDto login)
    {
        using var _ = await _tokenAsyncLock.LockAsync();

        var token = await _accountClient.LoginAsync(login);
        _token = token.Token ?? throw new ArgumentNullException(nameof(token.Token), "Token is empty");
    }

    public async Task<string> GetTokenAsync()
    {
        if (_token == null)
            throw new InvalidOperationException("You must login before using the token");

        await RefreshTokenIfNeeded();
        return _token;
    }

    private async Task RefreshTokenIfNeeded()
    {
        if (!IsNeedRefreshToken())
            return;

        using var _ = await _tokenAsyncLock.LockAsync();

        if (!IsNeedRefreshToken())
            return;

        var token = await _accountClient.RefreshTokenAsync(_token!);
        _token = token.Token ?? throw new ArgumentNullException(nameof(token.Token), "Token is empty");
    }

    private bool IsNeedRefreshToken()
    {
        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(_token);

        var refreshTokenBeforeTicks =
            (jwtSecurityToken.ValidTo - jwtSecurityToken.ValidFrom).Ticks
            / 100 * _refreshTokenBeforeExpirationInPercent;

        var refreshTokenAfter = jwtSecurityToken.ValidTo.AddTicks(-refreshTokenBeforeTicks);

        var utcNow = DateTime.UtcNow;
        return refreshTokenAfter < utcNow && utcNow < jwtSecurityToken.ValidTo;
    }
}