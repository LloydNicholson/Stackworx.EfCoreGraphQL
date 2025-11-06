namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}