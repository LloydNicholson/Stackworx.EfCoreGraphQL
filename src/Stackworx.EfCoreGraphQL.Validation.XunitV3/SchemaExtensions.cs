// ReSharper disable once CheckNamespace
namespace HotChocolate;

using Microsoft.EntityFrameworkCore.Metadata;
using Stackworx.EfCoreGraphQL.Abstractions;
using Stackworx.EfCoreGraphQL.Validation;
using Xunit;

public static class SchemaExtensions
{
    public static void ValidateDbContext(
        this ISchema schema,
        IModel model,
        Mode mode = Mode.OptOut,
        params IEntityType[] entityTypesToIgnore)
    {
        var groupedErrors = EvaluateSchema
            .Evaluate(schema, model, mode)
            .GroupBy(e => e.EntityType)
            .ToDictionary(g => g.Key, g => g.ToList());

        var actions = new List<Action>();

        foreach (var (entityType, errors) in groupedErrors)
        {
            if (entityTypesToIgnore.Contains(entityType))
            {
                continue;
            }

            actions.Add(() => Assert.Empty(
                errors.Select(e => $"GraphQL: {e.EntityType.Name}, DB: {e.ObjectType.Name}, Field: {e.Field.Name}, Message: {e.Message}")));
        }
        
        Assert.Multiple(actions.ToArray());
    }
}