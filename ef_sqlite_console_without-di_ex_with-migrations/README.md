---
created: 2023-01-30T23:50:20 (UTC +06:00)
tags: []
source: https://chat.openai.com/chat/5e09ec7e-33db-477d-a7ff-e6a2898e4c8f
author: 
---

# EF Core CRUD and Migration

> ## Excerpt
> A conversational AI system that listens, learns, and challenges

---
user dotnet core cli

Yes, you can use the .NET Core CLI to create a new console application and add the EF Core and SQLite packages. Here's an example of the steps you would take:

1.  Open a command prompt or terminal window and navigate to the directory where you want to create your project.
    
2.  Create a new console application by running the following command:
    

```
dotnet new console -n MyApp

```

This will create a new folder named "MyApp" with the basic structure of a console application.

3.  Change the directory to your project folder:

```
cd MyApp

```

4.  Add the Microsoft.EntityFrameworkCore.Sqlite package to your project:

```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

```

5.  Create a new folder named "Models" and in that folder add a new class "MyDbContext" that inherits from the DbContext class. This class should include a DbSet property for each table in your database and the connection string.

```
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydb.db");
        }
    }
}

```

6.  Create model classes for your tables in the same folder.

```
using System;

namespace MyApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}

```

7.  In the Main method of your Program.cs file, you can use the DbContext class to perform CRUD operations on the data.

```
using (var db = new MyDbContext())
{
    // Add new person
    var person = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
    db.People.Add(person);
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

```

8.  Run migrations using `dotnet ef migrations add InitialCreate` command to create migrations and `dotnet ef database update` command to update the database

Note that this is

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

give me full code with with file name and folder structure

Here is an example of the file and folder structure for a console application using EF Core and SQLite:

```
MyApp/
|
+-- Models/
|   |
|   +-- MyDbContext.cs
|   |
|   +-- Person.cs
|   |
|   +-- Address.cs
|
+-- Program.cs

```

`MyDbContext.cs`:

```
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydb.db");
        }
    }
}

```

`Person.cs`:

```
namespace MyApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}

```

`Address.cs`:

```
namespace MyApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}

```

`Program.cs`:

```
using System;
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

```

You should also run migration command `dotnet ef migrations add InitialCreate` to create migrations and `dotnet ef database update` command to update the database
