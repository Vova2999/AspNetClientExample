using AspNetClientExample.Common.Extensions;

namespace AspNetClientExample.Helpers;

public static class ConsoleMenuHelper
{
	public static void PrintCommands(
		IEnumerable<string> commands,
		string backCommand = "Назад",
		bool isSkipFirstEmptyLine = false)
	{
		if (!isSkipFirstEmptyLine)
			Console.WriteLine();

		commands
			.Select((command, i) => $"{i + 1}: {command}")
			.Append($"0: {backCommand}")
			.ForEach(Console.WriteLine);
	}
}