using NUnit.Framework;
using OrderReporter.Domain.Exceptions;
using OrderReporter.Infrastructure;

namespace ProjectTests;

[TestFixture]
public class InfrastructureTests
{
    [Test]
    public void ReportDao_GetClientFavoriteItemName_ShouldReturnExpectedFavorite()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new ReportDAO().GetClientFavoriteItemName(1010),
            Is.EqualTo("Чипсеки"));
            Assert.That(new ReportDAO().GetClientFavoriteItemName(1020),
                Is.EqualTo("Сыр"));
            Assert.That(new ReportDAO().GetClientFavoriteItemName(1022),
                Is.EqualTo("Дошик"));
        });
    }

    [Test]
    public void ReportDao_GetClientOrdersSum_ShouldReturnExpectedSum()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new ReportDAO().GetClientOrdersSum(1010),
            Is.EqualTo(5412.15f).Within(0.01f));
            Assert.That(new ReportDAO().GetClientOrdersSum(1020),
                Is.EqualTo(9809.56f).Within(0.01f));
            Assert.That(new ReportDAO().GetClientOrdersSum(1022),
                Is.EqualTo(21195.98f).Within(0.01f));
        });
    }

    [Test]
    public void ReportDao_ShouldThrow_WhenClientIsMissing()
    {
        Assert.Throws<ClientNotFoundException>(
            () => new ReportDAO().GetClientFavoriteItemName(228));
        Assert.Throws<ClientNotFoundException>(
            () => new ReportDAO().GetClientOrdersSum(228));
    }

    [Test]
    public void FileWriter_WriteReports_ShouldCreateTextFile()
    {
        var reportsDirectory = Path.Combine(AppContext.BaseDirectory, "reports");
        Directory.CreateDirectory(reportsDirectory);
        foreach (var file in Directory.GetFiles(reportsDirectory, "report_*.txt"))
        {
            File.Delete(file);
        }

        new FileWriter().WriteReports(new[]
        {
            new OrderReporter.Domain.Models.Report(12345, 100f, "test")
        });

        var files = Directory.GetFiles(reportsDirectory, "report_*.txt");
        Assert.Multiple(() =>
        {
            Assert.That(files, Has.Length.EqualTo(1));
            Assert.That(File.ReadAllText(files[0]),
                Is.EqualTo($"12345\t100.00\ttest"));
        });
    }
}
