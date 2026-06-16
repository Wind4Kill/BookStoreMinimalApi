using System.Reflection;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data;

public class ApplicationContext : DbContext
{
      public DbSet<Book> Books { get; set; }

      public DbSet<Author> Authors { get; set; }

      public DbSet<Category> Categories { get; set; }
      public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }

      public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
      {
            var bookEntities = ChangeTracker.Entries<Book>();
            foreach (var entity in bookEntities)
            {
                  if (entity.State == EntityState.Added)
                  {
                        Entry(entity.Entity).Property<DateTime>("CreatedAt").CurrentValue = DateTime.Now;
                        Entry(entity.Entity).Property<DateTime>("UpdatedAt").CurrentValue = DateTime.Now;
                  }
                  else if (entity.State == EntityState.Modified)
                  {
                        Entry(entity.Entity).Property<DateTime>("UpdatedAt").CurrentValue = DateTime.Now;
                  }
            }
            return base.SaveChangesAsync(cancellationToken);
      }

}
