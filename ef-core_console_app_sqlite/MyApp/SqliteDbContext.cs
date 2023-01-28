using Microsoft.EntityFrameworkCore;

namespace MyApp {
    public class SqliteDbContext :  DbContext {
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(@"Data Source=./Reminders.sqlite");
        }
    }
}
