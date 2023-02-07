using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyApp {
    public class Program {
        public static void Main(string[] args) {
            using (var context = new SqliteDbContext()) {

                // Start with a clean database
                // context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Add reminders.
                context.Reminders.Add(new Reminder {
                    Title = "Meditate"
                });
                context.Reminders.Add(new Reminder {
                    Title = "Eat a nutritious breakfast"
                });
                context.SaveChanges();

                // Fetch Reminders
                var reminders = context.Reminders.ToArray();
                foreach(var reminder in reminders) {
                    Console.WriteLine($"{reminder.Title}");
                }

          
              
            }
        }
    }
}