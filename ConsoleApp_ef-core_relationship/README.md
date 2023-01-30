

teach me One To Many Relationships in Entity Framework Core

A one-to-many relationship in Entity Framework Core is a relationship where one entity is related to multiple entities. For example, a customer can have many orders. To implement this relationship, you need to follow these steps:

1.  Create two entities, e.g. Customer and Order
2.  In the Customer entity, add a collection property for Orders, e.g. `public virtual ICollection<Order> Orders { get; set; }`
3.  In the Order entity, add a reference property for the Customer, e.g. `public virtual Customer Customer { get; set; }`
4.  In the DbContext class, use the `HasOne` and `WithMany` methods to specify the relationship, e.g. `modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders);`
5.  Use migrations to create the tables in the database.

You can use the entities in your code to create, read, update, and delete related data.

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

give me full example in console app with file name and folder structure

Here is an example of a console application that demonstrates a one-to-many relationship in Entity Framework Core.

Folder Structure:

-   ConsoleApp
    -   ConsoleApp.csproj
    -   Program.cs
    -   Data
        -   ApplicationDbContext.cs
        -   Customer.cs
        -   Order.cs

File Contents:

Program.cs:

```
using System;
using System.Linq;
using ConsoleApp.Data;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ApplicationDbContext())
            {
                // Create a customer
                var customer = new Customer
                {
                    Name = "John Doe"
                };

                // Create orders for the customer
                var order1 = new Order
                {
                    Product = "Product 1",
                    Customer = customer
                };
                var order2 = new Order
                {
                    Product = "Product 2",
                    Customer = customer
                };

                // Add the orders to the customer
                customer.Orders.Add(order1);
                customer.Orders.Add(order2);

                // Add the customer to the database
                db.Customers.Add(customer);
                db.SaveChanges();

                // Get all customers and their orders
                var customers = db.Customers
                    .Include(c => c.Orders)
                    .ToList();

                // Print the customers and their orders
                foreach (var c in customers)
                {
                    Console.WriteLine($"Customer: {c.Name}");
                    Console.WriteLine("Orders:");
                    foreach (var o in c.Orders)
                    {
                        Console.WriteLine($"  - {o.Product}");
                    }
                }
            }
        }
    }
}

```

ApplicationDbContext.cs:

```
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConsoleApp;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders);
        }
    }
}

```

Customer.cs:

```
using System.Collections.Generic;

namespace ConsoleApp.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

```

Order.cs:

```
namespace ConsoleApp.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int

```

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

continue start with \`\`\` sign

2 / 4

```
using System.Collections.Generic;

namespace ConsoleApp.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
```
