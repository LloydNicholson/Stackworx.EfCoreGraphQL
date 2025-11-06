namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class OrderItem
{
    // Composite PK includes the order key + LineNo
    public int OrderYear { get; set; }
    public int OrderNumber { get; set; }
    public int LineNo { get; set; }

    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }

    public Order Order { get; set; } = null!;
}