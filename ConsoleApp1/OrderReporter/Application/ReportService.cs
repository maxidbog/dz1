using OrderReporter.Application.Interfaces;
using OrderReporter.Domain.Models;

namespace OrderReporter.Application;

public class ReportService : IReportService
{
    private readonly IReportDAO _reportDao;

    public ReportService(IReportDAO reportDao)
    {
        _reportDao = reportDao;
    }

    public Report MakeReport(long clientId)
    {
        return new Report(clientId, _reportDao.GetClientOrdersSum(clientId), _reportDao.GetClientFavoriteItemName(clientId));
    }
}