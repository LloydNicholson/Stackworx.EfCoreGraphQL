namespace Sample.Types;

using Microsoft.EntityFrameworkCore;
using Stackworx.EfCoreGraphQL.Tests.Data;

public class Query
{
    public IQueryable<Book> GetBooks(AppDbContext dbContext)
    {
        return dbContext.Books.AsQueryable();
    }
    
    public IQueryable<Author> GetAuthors(AppDbContext dbContext)
    {
        return dbContext.Authors.AsQueryable();
    }
}