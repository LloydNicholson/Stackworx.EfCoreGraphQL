namespace Stackworx.EfCoreGraphQL.Tests;

using FluentAssertions;
using FluentAssertions.Primitives;

public static class StringAssertionsExtensions
{
    public static void MatchSource(this StringAssertions assertions, string expected)
    {
        var actual = assertions.Subject.TrimEnd().ReplaceLineEndings();
        actual.Should().Be(expected.ReplaceLineEndings());
    }
}