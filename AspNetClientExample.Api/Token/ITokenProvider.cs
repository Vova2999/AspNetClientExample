using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Token;

public interface ITokenProvider
{
    Task LoginAsync(LoginDto login);

    Task<string> GetTokenAsync();
}