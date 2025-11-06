namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class UserProfile
{
    public int Id { get; set; }
    public string? DisplayName { get; set; }

    // explicit FK + unique index
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}