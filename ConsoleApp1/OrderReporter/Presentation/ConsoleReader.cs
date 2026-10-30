using OrderReporter.Presentation.Interfaces;

namespace OrderReporter.Presentation;

public class ConsoleReader : IConsoleReader
{
    public long[] ReadClientIds()
    {
        Console.WriteLine("Введите id клиентов через пробел:");
        var ids = Console.ReadLine();
        return string.IsNullOrWhiteSpace(ids)
            ? Array.Empty<long>()
            : ids.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();
    }
}