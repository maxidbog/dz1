using OrderReporter.Application.Interfaces;
using OrderReporter.Application.Enums;
using OrderReporter.Presentation.Interfaces;

namespace OrderReporter.Presentation;

public class ReportApp
{
    private readonly IConsoleReader _consoleReader;
    private readonly IReportService _reportService;
    private readonly IReportWriterFactory _reportWriterFactory;

    public ReportApp(IConsoleReader consoleReader, IReportWriterFactory writerFactory, IReportService service)
    {
        _consoleReader = consoleReader;
        _reportWriterFactory = writerFactory;
        _reportService = service;
    }

    public void Run(WriterType writerType)
    {
        var reportWriter = _reportWriterFactory.CreateReportWriter(writerType);
        var clientIds = _consoleReader.ReadClientIds();
        var reports = clientIds.Select(clientId => _reportService.MakeReport(clientId)).ToArray();
        reportWriter.WriteReports(reports);
    }
}
