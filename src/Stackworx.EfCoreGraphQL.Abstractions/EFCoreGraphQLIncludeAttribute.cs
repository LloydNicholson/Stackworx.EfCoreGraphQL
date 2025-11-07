namespace Stackworx.EfCoreGraphQL.Abstractions;

/// <summary>
/// Mark Type for inclusion in processing for gradual adoption
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EFCoreGraphQLIncludeAttribute : Attribute;