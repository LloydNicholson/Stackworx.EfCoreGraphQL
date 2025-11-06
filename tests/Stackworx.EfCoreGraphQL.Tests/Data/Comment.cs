namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Comment
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public Post? Post { get; set; } // FK is shadow: int? PostId
}