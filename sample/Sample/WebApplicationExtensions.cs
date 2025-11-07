namespace Sample;

using Microsoft.EntityFrameworkCore;
using Stackworx.EfCoreGraphQL.Tests.Data;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        var factory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var dbContext = await factory.CreateDbContextAsync();
        
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
        await dbContext.SeedAsync();
    }
}