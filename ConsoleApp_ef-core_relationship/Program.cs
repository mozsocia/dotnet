using System;
using System.Linq;
using ConsoleApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ApplicationDbContext())
            {
                // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
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
