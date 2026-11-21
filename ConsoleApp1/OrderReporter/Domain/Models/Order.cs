namespace OrderReporter.Domain.Models;

public record Order(
    long ClientId,
    long OrderId,
    string ItemName,
    int ItemQuantity,
    float ItemPrice);