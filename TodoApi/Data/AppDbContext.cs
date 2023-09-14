// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Todo> Todos { get; set; }
		public DbSet<Category> Categories { get; set; }
		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-many relationship between Todo and Category
            modelBuilder.Entity<Todo>()
                .HasOne<Category>(t => t.Category)
                .WithMany(c => c.Todos)
                .HasForeignKey(t => t.CategoryId);
        }
	}
}
