using Microsoft.EntityFrameworkCore;
using Sample;
using Sample.Types;
using Stackworx.EfCoreGraphQL.Tests.Data;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContextFactory<AppDbContext>(opts =>
{
    // opts.UseSqlite("DataSource=:memory:");
    opts.UseSqlite("DataSource=db/app.db");
});

// EntityFrameworkRequestExecutorBuilderExtensions
builder
    .AddGraphQL()
    .AddQueryType<Query>()
    .RegisterDbContextFactory<AppDbContext>()
    .AddSampleTypes()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => Results.Redirect("/graphql"));

await app.SeedAsync();

app.Run();