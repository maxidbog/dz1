namespace OrderReporter.Application.Interfaces;

public interface IReportDAO
{
    float GetClientOrdersSum(long clientId);

    string GetClientFavoriteItemName(long clientId);
}