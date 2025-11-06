namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}