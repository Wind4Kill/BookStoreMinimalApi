using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data;

public class ApplicationContext:DbContext
{
      public ApplicationContext(DbContextOptions options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            
      }
      
}
