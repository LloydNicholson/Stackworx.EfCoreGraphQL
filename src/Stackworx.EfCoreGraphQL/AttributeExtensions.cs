namespace Stackworx.EfCoreGraphQL;

using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Stackworx.EfCoreGraphQL.Abstractions;

public static class AttributeExtensions
{
    public static bool HasGraphQLIgnore(this INavigationBase nav)
    {
        var member = nav.PropertyInfo ?? (MemberInfo?)nav.FieldInfo;
        if (member is null)
        {
            return false;
        }

        return member
            .GetCustomAttributes(inherit: true)
            .Any(a =>
            {
                var type = a.GetType();
                return type.Name == "GraphQLIgnoreAttribute"
                       || type.FullName == "HotChocolate.GraphQLIgnoreAttribute";
            });
    }
    
    public static bool ShouldIgnore(this IEntityType entityType)
    {
        var clrType = entityType.ClrType;
        // Check for any attribute whose name matches GraphQLIgnoreAttribute

        var ignored = clrType
            .GetCustomAttribute<EFCoreGraphQLIgnoreAttribute>() is not null;

        if (ignored)
        {
            return true;
        }
        
        return clrType
            .GetCustomAttributes(inherit: true)
            .Any(a =>
            {
                var type = a.GetType();
                return type.Name == "GraphQLIgnoreAttribute"
                       || type.FullName == "HotChocolate.GraphQLIgnoreAttribute"
                       || type.FullName == "HotChocolate.Types.GraphQLIgnoreAttribute";
            });
    }
}