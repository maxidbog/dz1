using OrderReporter.Domain.Models;

namespace OrderReporter.Application.Interfaces;

public interface IReportService
{
    Report MakeReport(long clientId);
}