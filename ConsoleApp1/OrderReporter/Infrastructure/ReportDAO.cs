using System.Globalization;
using OrderReporter.Application.Interfaces;
using OrderReporter.Domain.Exceptions;
using OrderReporter.Domain.Models;

namespace OrderReporter.Infrastructure;

public class ReportDAO : IReportDAO
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "orders_log.csv");

    private Order[] ReadCSV()
    {
        return File.ReadAllLines(_filePath)
            .Skip(1)
            .Select(line => line.Split(','))
            .Select(fields => new Order(long.Parse(fields[0]), long.Parse(fields[1]), fields[2], int.Parse(fields[3]), float.Parse(fields[4], CultureInfo.InvariantCulture))).ToArray();
    }

    public string GetClientFavoriteItemName(long clientId)
    {
        var clientOrders = ReadCSV()
            .Where(x => x.ClientId == clientId)
            .ToArray();
        
        if (clientOrders.Length == 0)
            throw new ClientNotFoundException(clientId);
        
        return clientOrders
            .GroupBy(x => x.ItemName)
            .OrderByDescending(g => g.Sum(x => x.ItemQuantity))
            .FirstOrDefault()?.Key!;
    }

    public float GetClientOrdersSum(long clientId)
    {
        var clientOrders = ReadCSV()
            .Where(x => x.ClientId == clientId)
            .ToArray();

        if (clientOrders.Length == 0)
            throw new ClientNotFoundException(clientId);

        return clientOrders
            .Sum(x => x.ItemQuantity * x.ItemPrice);
    }
}