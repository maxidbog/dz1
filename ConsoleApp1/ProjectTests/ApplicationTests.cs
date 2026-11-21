using NUnit.Framework;
using OrderReporter.Application;
using OrderReporter.Application.Enums;
using OrderReporter.Application.Interfaces;
using OrderReporter.Domain.Models;
using OrderReporter.Presentation;
using OrderReporter.Presentation.Interfaces;

namespace ProjectTests;

[TestFixture]
public class ApplicationTests
{
    [Test]
    public void ReportApp_Run_ShouldBuildAndWriteReports()
    {
        var reader = new TestConsoleReader(1010, 1020);
        var dao = new TestReportDao();
        var writer = new TestReportWriter();
        var factory = new TestReportWriterFactory(writer);
        var app = new ReportApp(reader, factory, new ReportService(dao));

        app.Run(WriterType.Console);
        Assert.Multiple(() =>
        {
            Assert.That(writer.Reports.Select(r => r.ClientId),
                Is.EqualTo(new long[] { 1010, 1020 }));
            Assert.That(factory.RequestedType, Is.EqualTo(WriterType.Console));
        });
    }

    [Test]
    public void ReportService_MakeReport_ShouldUseDaoResults()
    {
        var dao = new TestReportDao();
        var report = new ReportService(dao).MakeReport(123);

        Assert.That(report.ClientId, Is.EqualTo(123));
        Assert.That(report.OrderSum, Is.EqualTo(150f).Within(0.001f));
        Assert.That(report.FavoriteItemName, Is.EqualTo("Чипсеки"));
    }

    private class TestConsoleReader : IConsoleReader
    {
        private readonly long[] _ids;

        public TestConsoleReader(params long[] ids)
        {
            _ids = ids;
        }

        public long[] ReadClientIds()
        {
            return _ids;
        }
    }

    private class TestReportDao : IReportDAO
    {
        public float GetClientOrdersSum(long clientId)
        {
            return 150f;
        }

        public string GetClientFavoriteItemName(long clientId)
        {
            return "Чипсеки";
        }
    }

    private class TestReportWriterFactory : IReportWriterFactory
    {
        private readonly IReportWriter _writer;

        public TestReportWriterFactory(IReportWriter writer)
        {
            _writer = writer;
        }

        public WriterType RequestedType { get; private set; }

        public IReportWriter CreateReportWriter(WriterType writerType)
        {
            RequestedType = writerType;
            return _writer;
        }
    }

    private class TestReportWriter : IReportWriter
    {
        public List<Report> Reports { get; } = new();

        public void WriteReport(Report report)
        {
            Reports.Add(report);
        }

        public void WriteReports(IEnumerable<Report> reports)
        {
            Reports.AddRange(reports);
        }
    }
}
