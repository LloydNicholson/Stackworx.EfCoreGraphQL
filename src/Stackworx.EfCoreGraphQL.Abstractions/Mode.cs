namespace Stackworx.EfCoreGraphQL.Abstractions;

public enum Mode
{
    /// <summary>
    /// Types will need to be excluded by <see cref="EFCoreGraphQLIgnoreAttribute"/>
    /// </summary>
    OptOut,
    /// <summary>
    /// Types will need to be included by <see cref="EFCoreGraphQLIncludeAttribute"/>
    /// </summary>
    OptIn,
}