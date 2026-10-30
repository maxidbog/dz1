using System.Globalization;

namespace OrderReporter.Domain.Models;

public class Report
{
    public long ClientId { get;}
    public float OrderSum { get;}
    public string FavoriteItemName { get;}

    public Report(long clientId, float orderSum, string favoriteItemName)
    {
        ClientId = clientId;
        OrderSum = orderSum;
        FavoriteItemName = favoriteItemName;
    }
    
    public override string ToString()
    {
        return $"{ClientId}\t{OrderSum.ToString("F2", CultureInfo.InvariantCulture)}\t{FavoriteItemName}";
    }
}