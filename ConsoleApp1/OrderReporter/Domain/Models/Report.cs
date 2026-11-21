using System.Globalization;

namespace OrderReporter.Domain.Models;

public record Report(
    long ClientId,
    float OrderSum,
    string FavoriteItemName)
{
    public override string ToString()
    {
        return $"{ClientId}\t{OrderSum.ToString("F2", CultureInfo.InvariantCulture)}\t{FavoriteItemName}";
    }
}