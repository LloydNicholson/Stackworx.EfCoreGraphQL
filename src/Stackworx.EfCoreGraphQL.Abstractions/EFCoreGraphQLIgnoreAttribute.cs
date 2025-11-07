namespace Stackworx.EfCoreGraphQL.Abstractions;

/// <summary>
/// Mark type for exclusion from processing
/// </summary>
/// <param name="Reason"></param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EFCoreGraphQLIgnoreAttribute(string? Reason = null) : Attribute
{
    public string? Reason { get; } = Reason;
}