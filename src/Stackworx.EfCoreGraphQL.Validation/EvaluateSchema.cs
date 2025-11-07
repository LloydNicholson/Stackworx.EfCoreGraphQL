namespace Stackworx.EfCoreGraphQL.Validation;

using System.Reflection;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Stackworx.EfCoreGraphQL.Abstractions;
using Stackworx.EfCoreGraphQL.Shared;

public class EvaluateSchema
{
    // TODO: create variation that relies on Xunit.assert to report multiple failures
    // Assert.Multiple();
    public static List<Error> Evaluate(
        ISchema schema,
        IModel model,
        Mode mode = Mode.OptOut)
    {
        var entities = model
            .GetEntityTypes()
            .Where(e => !e.IsOwned());

        var typesByRuntimeType = schema.Types
            .Where(x => x is IHasRuntimeType)
            .GroupBy(t => t.ToRuntimeType())
            .ToDictionary(x => x.Key, x => x.ToList());
        
        var errors = new List<Error>();

        // then
        foreach (var entity in entities)
        {
            if (entity.ShouldIgnore())
            {
                continue;
            }

            if (mode == Mode.OptIn && !entity.ShouldInclude())
            {
                continue;
            }
            
            if (typesByRuntimeType.TryGetValue(entity.ClrType, out var types))
            {
                switch (types.Count)
                {
                    case 0:
                        break;
                    case 1:
                    {
                        var t = types[0];

                        if (t is ObjectType objectType)
                        {
                            errors.AddRange(Validate(objectType, entity));
                        }

                        break;
                    }

                    default:
                        throw new ApplicationException(
                            $"{entity.ClrType} maps to multiple GraphQL Types: {string.Join(",", types.Select(t => t.Name))}");
                }
            }
        }

        return errors;
    }

    private static IList<Error> Validate(ObjectType objectType, IEntityType entity)
    {
        var errors = new List<Error>();
        
        var navigations = entity
            .GetNavigations()
            .Where(n => !n.IsEagerLoaded && !n.TargetEntityType.IsOwned())
            .ToList();

        foreach (var nav in navigations)
        {
            var field = FindFieldForNavigation(objectType, nav);

            // TODO: handle gitignore case
            if (field is null)
            {
                // Field could be ignored
                continue;
            }

            var hasExplicitResolver = HasExplicitResolver(field);

            if (!hasExplicitResolver)
            {
                errors.Add(new Error
                {
                    EntityType = entity,
                    ObjectType = objectType,
                    Field = field,
                    Message = "Missing explicit resolver for navigation " + nav.Name + " on type " + objectType.Name + ".",
                });
            }
        }

        return errors;
    }

    private static IObjectField? FindFieldForNavigation(ObjectType objectType, INavigation nav)
    {
        // 1) Try by bound member
        if (nav.PropertyInfo is MemberInfo member)
        {
            var byMember = objectType.Fields.FirstOrDefault(f =>
                f is IObjectField { Member: not null } of && SymbolEquals(of.Member, member));
            if (byMember is not null)
            {
                return byMember;
            }
        }

        // 2) Try exact name
        var byName = objectType.Fields.FirstOrDefault(f => string.Equals(f.Name, nav.Name, StringComparison.Ordinal));
        if (byName is not null)
        {
            return byName;
        }

        // 3) Try camelCase name
        var camel = ToCamel(nav.Name);
        return objectType.Fields.FirstOrDefault(f => string.Equals(f.Name, camel, StringComparison.Ordinal));
    }

    private static bool SymbolEquals(MemberInfo a, MemberInfo b)
        => a.MetadataToken == b.MetadataToken && a.Module == b.Module;

    private static string ToCamel(string name)
        => string.IsNullOrEmpty(name) || char.IsLower(name[0])
            ? name
            : char.ToLowerInvariant(name[0]) + name.Substring(1);

    private static bool HasExplicitResolver(IObjectField field)
    {
        // https://chillicream.com/docs/hotchocolate/v15/defining-a-schema/dynamic-schemas#resolver-types
        if (field.PureResolver is not null)
        {
            return false;
        }

        // Code first special case
        if (field.Member is null && field.ResolverMember is null)
        {
            return true;
        }
        
        if (field.ResolverMember is MethodInfo)
        {
            return true;
        }
        
        if (field.Member is MethodInfo)
        {
            return true;
        }

        return false;
    }

    public record Error
    {
        public required IEntityType EntityType { get; init; }
        
        public required IObjectType ObjectType { get; init; }
        
        public required IObjectField Field { get; set; }
        
        public required string Message { get; init; }
    }
}