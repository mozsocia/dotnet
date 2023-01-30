using di_prac.Data;
using di_prac.Models;
using System.Linq;
using System.Collections.Generic;

namespace di_prac.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly MyAppContext _context;

        public HomeRepository(MyAppContext context)
        {
            _context = context;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public string GetWelcomeMessage()
        {
            // // Insert three users
            // _context.Users.Add(new User { FirstName = "John", LastName = "Doe" });
            // _context.Users.Add(new User { FirstName = "Jane", LastName = "Doe" });
            // _context.Users.Add(new User { FirstName = "Bob", LastName = "Smith" });
            // _context.SaveChanges();

            // // Retrieve users and display in console
            // var users = _context.Users.ToList();
            // foreach (var user in users)
            // {
            //     Console.WriteLine(user.FirstName + " " + user.LastName);
            // }

            return "Welcome to my .NET Core app using a four-layered architecture!";
        }


    }
}
