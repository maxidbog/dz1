using Microsoft.Extensions.DependencyInjection;
using OrderReporter.Application.Interfaces;
using OrderReporter.Application.Enums;

namespace OrderReporter.Application;

public class ReportWriterFactory : IReportWriterFactory
{
    private readonly IServiceProvider _provider;

    public ReportWriterFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IReportWriter CreateReportWriter(WriterType writerType)
    {
        return _provider.GetRequiredKeyedService<IReportWriter>(writerType);
    }
}