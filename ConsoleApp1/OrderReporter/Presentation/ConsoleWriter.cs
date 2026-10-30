using OrderReporter.Application.Interfaces;
using OrderReporter.Domain.Models;

namespace OrderReporter.Presentation;

public class ConsoleWriter : IReportWriter
{
    public void WriteReport(Report report)
    {
        Console.WriteLine(report.ToString());
    }

    public void WriteReports(IEnumerable<Report> reports)
    {
        foreach (var report in reports)
        {
            Console.WriteLine(report.ToString());
        }
    }
}