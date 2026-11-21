using NUnit.Framework;
using OrderReporter.Domain.Models;
using OrderReporter.Presentation;

namespace ProjectTests;

[TestFixture]
public class PresentationTests
{
    private TextReader _originalIn = null!;
    private TextWriter _originalOut = null!;
    private TextWriter _originalError = null!;

    [SetUp]
    public void SetUp()
    {
        _originalIn = Console.In;
        _originalOut = Console.Out;
        _originalError = Console.Error;
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetIn(_originalIn);
        Console.SetOut(_originalOut);
        Console.SetError(_originalError);
    }

    [TestCase("1010 1020", new long[] { 1010, 1020 })]
    [TestCase("1010\t1020   123", new long[] { 1010, 1020, 123 })]
    public void ConsoleReader_ReadClientIds_ShouldParseIds(string input, long[] expected)
    {
        Console.SetIn(new StringReader(input));

        var result = new ConsoleReader().ReadClientIds();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ConsoleReader_ReadClientIds_ShouldIgnoreIncorrectValue()
    {
        Console.SetIn(new StringReader("123.45 54"));
        Console.SetError(new StringWriter());

        var result = new ConsoleReader().ReadClientIds();

        Assert.That(result, Is.EqualTo(new long[] { 54 }));
    }

    [Test]
    public void ConsoleReader_ReadClientIds_ShouldReturnEmptyArray_WhenInputIsEmpty()
    {
        Console.SetIn(new StringReader("   "));

        var result = new ConsoleReader().ReadClientIds();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ConsoleWriter_WriteReports_ShouldWriteEachReport()
    {
        var writer = new StringWriter();
        Console.SetOut(writer);
        var reports = new[]
        {
            new Report(12345, 100f, "test"),
            new Report(67890, 200.14f, "test2")
        };

        new ConsoleWriter().WriteReports(reports);

        Assert.That(writer.ToString(),
            Is.EqualTo($"12345\t100.00\ttest{Environment.NewLine}" +
                       $"67890\t200.14\ttest2{Environment.NewLine}"));
    }
}
