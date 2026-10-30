using OrderReporter.Application.Enums;

namespace OrderReporter.Application.Interfaces;

public interface IReportWriterFactory
{
    IReportWriter CreateReportWriter(WriterType writerType);
}