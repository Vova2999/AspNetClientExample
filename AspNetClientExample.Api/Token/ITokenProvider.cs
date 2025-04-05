using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Token;

public interface ITokenProvider
{
    Task LoginAsync(LoginDto login);

    Task<TResult> ExecuteWithToken<TResult>(Func<string, Task<TResult>> action);
}