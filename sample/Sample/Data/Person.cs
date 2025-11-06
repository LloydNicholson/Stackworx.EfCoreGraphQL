namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // optional navigation
    public Passport? Passport { get; set; }
}