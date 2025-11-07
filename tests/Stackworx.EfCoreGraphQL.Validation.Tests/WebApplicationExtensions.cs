namespace Stackworx.EfCoreGraphQL.Validation.Tests;

using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

internal static class WebApplicationExtensions
{
    internal static async Task<ISchema> GetSchema(this WebApplication builder)
    {
        var resolver = builder.Services.GetRequiredService<IRequestExecutorResolver>();
        var executor = await resolver.GetRequestExecutorAsync();
        return executor.Schema;
    }

    internal static IModel GetEfCoreModel<T>(this WebApplication builder)
    where T : DbContext
    {
        using var scope = builder.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        return dbContext.Model;
    }
}