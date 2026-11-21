using OrderReporter.Domain.Models;

namespace OrderReporter.Application.Interfaces;

public interface IReportWriter
{
    void WriteReport(Report report);

    void WriteReports(IEnumerable<Report> reports);
}