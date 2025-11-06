namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public UserProfile Profile { get; set; } = null!; // required
}