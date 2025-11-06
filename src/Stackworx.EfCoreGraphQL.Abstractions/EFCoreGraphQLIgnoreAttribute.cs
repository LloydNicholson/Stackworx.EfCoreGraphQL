namespace Stackworx.EfCoreGraphQL.Abstractions;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EFCoreGraphQLIgnoreAttribute(string? Reason = null) : Attribute;