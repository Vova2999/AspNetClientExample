using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Account;

public interface IAccountClient
{
    Task<TokenDto> LoginAsync(
        LoginDto login,
        CancellationToken cancellationToken = default);

    Task RegisterAsync(
        RegisterDto register,
        CancellationToken cancellationToken = default);

    Task<TokenDto> RefreshTokenAsync(
        CancellationToken cancellationToken = default);

    Task ChangePasswordAsync(
        ChangePasswordDto changePassword,
        CancellationToken cancellationToken = default);
}