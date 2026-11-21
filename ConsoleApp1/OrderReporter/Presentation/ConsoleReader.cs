using OrderReporter.Presentation.Interfaces;

namespace OrderReporter.Presentation;

public class ConsoleReader : IConsoleReader
{
    public long[] ReadClientIds()
    {
        Console.WriteLine("Введите id клиентов через пробел:");
        var ids = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ids))
        {
            return Array.Empty<long>();
        }

        var clientIds = new List<long>();

        foreach (var value in ids.Split(
                     (char[]?)null,
                     StringSplitOptions.RemoveEmptyEntries))
        {
            if (long.TryParse(value, out var clientId))
            {
                clientIds.Add(clientId);
            }
            else
            {
                Console.Error.WriteLine($"Некорректный id клиента: {value}");
            }
        }

        return clientIds.ToArray();
    }
}