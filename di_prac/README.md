```cs

namespace di_prac.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

```
```cs

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


```
```cs

using System;
using Microsoft.Extensions.DependencyInjection;
using di_prac.Controllers;
using di_prac.Services;
using di_prac.Repositories;
using di_prac.Data;


namespace di_prac
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Create service provider
            var serviceProvider = services.BuildServiceProvider();
            CreateDb(serviceProvider);

            // Run app
            var controller = serviceProvider.GetService<HomeController>();

            controller.Run();
  
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add services
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            // Add controllers
            services.AddTransient<HomeController>();

            // Add db context
            services.AddDbContext<MyAppContext>();
        }

        private static void CreateDb(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyAppContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}


```
```cs


using System.Collections.Generic;
using di_prac.Models;

namespace di_prac.Repositories
{
    public interface IHomeRepository
    {
         IEnumerable<User> GetUsers();
        string GetWelcomeMessage();
        void InsertUser(User user);
    }
}

```
```cs

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
            return "Welcome to my .NET Core app using a four-layered architecture!";
        }


    }
}

```
```cs


using System.Collections.Generic;
using di_prac.Models;

namespace di_prac.Services
{
    public interface IHomeService
    {
        string GetWelcomeMessage();
        public void InsertSampleData(List<User> users);
        public IEnumerable<User> GetUsers();
    }
}

```
```cs

using System;
using System.Collections.Generic;
using di_prac.Models;
using di_prac.Repositories;


namespace di_prac.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public string GetWelcomeMessage()
        {
            return _homeRepository.GetWelcomeMessage();
        }
         public void InsertSampleData(List<User> users)
        {

            foreach (var user in users)
            {
                _homeRepository.InsertUser(user);
            }
        }
         public IEnumerable<User> GetUsers()
        {
            return _homeRepository.GetUsers();
        }
    }
}

```
```cs


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
