using AspNetClientExample.Common.Extensions;

namespace AspNetClientExample.Helpers;

public static class ConsoleReadHelper
{
    public static int ReadInt(string message, int from = int.MinValue, int to = int.MaxValue)
    {
        while (true)
        {
            Console.Write(message);

            if (!int.TryParse(Console.ReadLine(), out var value))
            {
                Console.WriteLine("Введено некорректное число, повторите ввод");
                continue;
            }

            if (from > value || value > to)
            {
                Console.WriteLine($"Введенное число должно быть в диапазоне от {from} до {to}, повторите ввод");
                continue;
            }

            return value;
        }
    }

    public static string ReadString(string message, bool isCanBeNull = false)
    {
        while (true)
        {
            Console.Write(message);

            var value = Console.ReadLine();

            if (isCanBeNull && value.IsNullOrEmpty())
            {
                Console.WriteLine("Введена некорректная строка, повторите ввод");
                continue;
            }

            return value;
        }
    }
}