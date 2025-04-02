using System.IdentityModel.Tokens.Jwt;
using AspNetClientExample.Api.Clients.Account;
using AspNetClientExample.Api.Clients.Ping;
using AspNetClientExample.Domain.Dtos;
using AspNetClientExample.Menus;
using AspNetClientExample.Menus.Logic;

namespace AspNetClientExample;

public static class Program
{
	public static async Task Main()
    {
        //if (!await TryPing(Locator.Current.Locate<IPingClient>()))
		//	return;

		//await StaticConsoleMenu.RunAsync(typeof(MainMenu));

        //var accountClient = Locator.Current.Locate<IAccountClient>();
        //var login = await accountClient.LoginAsync(new LoginDto { Login = "Vova2999", Password = "111111" });

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVm92YTI5OTkiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Ijc2YjhiYWQxLWJhYTAtNDg1YS05ZGYwLTJiMDA1Njk4YzAyYyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJBZG1pbiIsIlN3YWdnZXIiXSwibmJmIjoxNzQzNjIwOTkxLCJleHAiOjE3NDM2MjQ1OTEsImlzcyI6IkFzcE5ldEV4YW1wbGUiLCJhdWQiOiJBc3BOZXRFeGFtcGxlQ2xpZW50In0.bLfNF0AEHBZV8d93vEl1vh0WZKA648L2GxEGeD8_RJY");
        var validFrom = jwtSecurityToken.ValidFrom;
        var validTo = jwtSecurityToken.ValidTo;
    }

    private static async Task<bool> TryPing(IPingClient pingClient)
	{
		try
		{
			await pingClient.Ping();
			return true;
		}
		catch
		{
			Console.WriteLine("Ping failed!");
			Console.ReadKey();
			return false;
		}
	}
}