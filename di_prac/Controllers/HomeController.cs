using di_prac.Models;
using di_prac.Services;
using System;
using System.Collections.Generic;

namespace di_prac.Controllers
{
    public class HomeController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public void Run()
        {
            // insert data
            List<User> users = new List<User>
                {
                    new User { FirstName = "John", LastName = "Doe" },
                    new User { FirstName = "Jane", LastName = "Doe" },
                    new User { FirstName = "Bob", LastName = "Smith" }
                };
            _homeService.InsertSampleData(users);



            // show data
            var FromDBusers = _homeService.GetUsers();

            foreach (var user in FromDBusers)
            {
                Console.WriteLine(user.FirstName + " " + user.LastName);
            }
            
            var message = _homeService.GetWelcomeMessage();
            Console.WriteLine(message);
        }
    }
}
