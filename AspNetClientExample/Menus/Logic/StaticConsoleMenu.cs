using System.Reflection;
using AspNetClientExample.Api.Exceptions;
using AspNetClientExample.Helpers;

namespace AspNetClientExample.Menus.Logic;

public static class StaticConsoleMenu
{
    public static async Task RunAsync(Type typeWithStaticCommands)
    {
        var commands = GetCommands(typeWithStaticCommands);

        while (true)
        {
            Console.Clear();
            PrintCommands(commands);

            var selector = ReadSelector(commands);
            if (selector == 0)
                break;

            await ExecuteCommandAsync(commands, selector - 1);
            PauseAfterExecute();
        }
    }

    private static IList<StaticConsoleCommand> GetCommands(IReflect typeWithStaticCommands)
    {
        return typeWithStaticCommands
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(IsMatchMethod)
            .Select(CreateStaticConsoleCommand)
            .ToArray();
    }

    private static bool IsMatchMethod(MethodInfo method)
    {
        return method.GetCustomAttribute<StaticConsoleMenuCommandAttribute>() != null;
    }

    private static StaticConsoleCommand CreateStaticConsoleCommand(MethodInfo method)
    {
        var isAsyncCommand = method.ReturnType.IsAssignableTo(typeof(Task));
        return new StaticConsoleCommand(
            method.GetCustomAttribute<StaticConsoleMenuCommandAttribute>()!.DisplayText,
            isAsyncCommand ? null : () => method.Invoke(null, null),
            isAsyncCommand ? () => (Task) method.Invoke(null, null)! : null
        );
    }

    private static void PrintCommands(IEnumerable<StaticConsoleCommand> commands)
    {
        ConsoleMenuHelper.PrintCommands(
            commands.Select(command => command.DisplayText),
            "Выход",
            true);
    }

    private static int ReadSelector(ICollection<StaticConsoleCommand> commands)
    {
        return ConsoleReadHelper.ReadInt(" => ", 0, commands.Count);
    }

    private static async Task ExecuteCommandAsync(IList<StaticConsoleCommand> commands, int index)
    {
        var command = commands[index];

        try
        {
            if (command.InvokeMethod != null)
                command.InvokeMethod.Invoke();
            else if (command.InvokeMethodAsync != null)
                await command.InvokeMethodAsync.Invoke();
            else
                throw new InvalidOperationException("Static command methods is missing");
        }
        catch (SendRequestException exception)
        {
            Console.WriteLine("Произошла ошибка запроса при выполнении операции");
            Console.WriteLine($"Uri = {exception.Uri}");
            Console.WriteLine($"StatusCode = {exception.StatusCode}");
            Console.WriteLine("Content:");
            Console.WriteLine(exception.Content);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Произошла неизвестная ошибка при выполнении операции");
            Console.WriteLine(exception);
        }
    }

    private static void PauseAfterExecute()
    {
        Console.WriteLine();
        Console.WriteLine("Выполнение команды завершено");
        Console.WriteLine("Нажмите любую клавишу для продолжения");
        Console.ReadKey();
    }
}