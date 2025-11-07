// See https://aka.ms/new-console-template for more information

using Stackworx.EfCoreGraphQL;
using Stackworx.EfCoreGraphQL.Tests.Data;

var (dbContext, _) = await AppDbContext.CreateSqliteInMemoryAsync();


var outPath = Path.Combine(Environment.CurrentDirectory, "DataLoaders.g.cs");
await DataLoaderGenerator.Generate(dbContext, null, outPath);