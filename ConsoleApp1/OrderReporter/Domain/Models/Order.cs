namespace OrderReporter.Domain.Models;

public class Order
{
    public long ClientId;
    public long OrderId;
    public string ItemName;
    public int ItemQuantity;
    public float ItemPrice;

    public Order(long clientId, long orderId, string itemName, int itemQuantity, float itemPrice)
    {
        ClientId = clientId;
        OrderId = orderId;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
        ItemPrice = itemPrice;
    }
}