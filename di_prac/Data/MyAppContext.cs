using Microsoft.EntityFrameworkCore;
using di_prac.Models;

namespace di_prac.Data
{
    public class MyAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=myapp.db");
        }
    }
}