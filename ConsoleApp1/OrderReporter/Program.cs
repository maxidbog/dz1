using Microsoft.Extensions.DependencyInjection;
using OrderReporter.Application;
using OrderReporter.Application.Enums;
using OrderReporter.Application.Interfaces;
using OrderReporter.Infrastructure;
using OrderReporter.Presentation;
using OrderReporter.Presentation.Interfaces;

internal class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 0 || !Enum.TryParse<WriterType>(args[0], true, out var writerType))
        {
            Console.Error.WriteLine("аргумент не найден: console|file \nDefault: console");
            writerType = WriterType.Console;
        }

        var services = new ServiceCollection();
        
        services.AddKeyedSingleton<IReportWriter, ConsoleWriter>(WriterType.Console);
        services.AddKeyedSingleton<IReportWriter, FileWriter>(WriterType.File);

        services.AddSingleton<IReportWriterFactory, ReportWriterFactory>();

        services.AddSingleton<IConsoleReader, ConsoleReader>();

        services.AddSingleton<IReportService, ReportService>();

        services.AddSingleton<IReportDAO, ReportDAO>();

        services.AddSingleton<ReportApp>();

        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<ReportApp>();
        app.Run(writerType);
        return 0;
    }
}
