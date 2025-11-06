namespace Stackworx.EfCoreGraphQL.Tests.Data;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int? Grade { get; set; } // payload
}