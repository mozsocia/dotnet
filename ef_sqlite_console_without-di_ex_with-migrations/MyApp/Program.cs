using System;
using System.Linq;
using MyApp.Models;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                // Add new person
                var person = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
                db.People.Add(person);
                db.SaveChanges();

                var person1 = new Person { FirstName = "John1", LastName = "Doe1", Age = 31 };
                db.People.Add(person1);
                db.SaveChanges();

                // Update person
                person.Age = 35;
                db.SaveChanges();

                // Get all people
                var people = db.People.ToList();
                foreach (var p in people)
                {
                    Console.WriteLine($"{p.FirstName} {p.LastName} is {p.Age} years old.");
                }

                // Delete person
                db.People.Remove(person);
                db.SaveChanges();
            }
        }
    }
}
