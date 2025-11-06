namespace Stackworx.EfCoreGraphQL;

using Microsoft.EntityFrameworkCore.Metadata;

internal static class LoaderNames
{
    internal static string BatchLoaderName(IEntityType type, IProperty pkProp)
    {
        return $"{type.ClrType.Name}By{pkProp.Name}";
    }
    
    internal static string GroupLoaderName(IEntityType type, IProperty prop)
    {
        // TODO: is plural a good idea?
        return $"{type.ClrType.Name}sBy{prop.Name}";
    }
    
    internal static string GroupLoaderName(ISkipNavigation nav)
    {
        return $"{nav.Name}By{nav.Inverse.Name}";
    }
}