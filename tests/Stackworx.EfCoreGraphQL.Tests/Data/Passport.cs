namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Passport
{
    public int Id { get; set; }
    public required string Number { get; set; }

    // optional FK (nullable)
    public int? PersonId { get; set; }
    public Person? Person { get; set; }
}