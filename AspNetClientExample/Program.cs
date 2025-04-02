using AspNetClientExample.Api.Clients.Ping;
using AspNetClientExample.Menus;
using AspNetClientExample.Menus.Logic;

namespace AspNetClientExample;

public static class Program
{
    public static async Task Main()
    {
        if (!await TryPing(Locator.Current.Locate<IPingClient>()))
            return;

        await StaticConsoleMenu.RunAsync(typeof(MainMenu));
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