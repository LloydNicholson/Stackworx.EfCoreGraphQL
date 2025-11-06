namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Post> Posts { get; set; } = new();
}