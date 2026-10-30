using OrderReporter.Domain.Models;

namespace OrderReporter.Application.Interfaces;

public interface IReportWriter
{
    public void WriteReport(Report report);

    public void WriteReports(IEnumerable<Report> reports);
}