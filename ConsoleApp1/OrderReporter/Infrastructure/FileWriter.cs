using OrderReporter.Application.Interfaces;
using OrderReporter.Domain.Models;

namespace OrderReporter.Infrastructure;

public class FileWriter : IReportWriter
{
    private readonly string _reportsDirectory;

    public FileWriter()
    {
        _reportsDirectory = Path.Combine(AppContext.BaseDirectory, "reports");
        if (!Directory.Exists(_reportsDirectory))
            Directory.CreateDirectory(_reportsDirectory);
    }
    public void WriteReport(Report report)
    {
        var filePath = GetUniqueFilePath();
        File.WriteAllText(filePath, report.ToString());
    }

    public void WriteReports(IEnumerable<Report> reports)
    {
        var filePath = GetUniqueFilePath();
        var content = string.Join(Environment.NewLine, reports.Select(r => r.ToString()));
        File.WriteAllText(filePath, content);
    }
    
    private string GetUniqueFilePath()
    {
        return Path.Combine(_reportsDirectory, $"report_{Guid.NewGuid():N}.txt");
    }
}