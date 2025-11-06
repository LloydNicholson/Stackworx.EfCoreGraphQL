namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Order
{
    public int Year { get; set; }
    public int Number { get; set; }
    public string Customer { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = new();
}