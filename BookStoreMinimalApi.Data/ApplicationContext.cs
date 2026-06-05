using System.Reflection;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data;

public class ApplicationContext:DbContext
{
      public DbSet<Book> Books { get; set; }

      public DbSet<Author> Authors { get; set; }
      public ApplicationContext(DbContextOptions options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }
      
}
