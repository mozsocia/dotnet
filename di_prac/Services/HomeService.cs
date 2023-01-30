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
            // _homeRepository.InsertUser(new User { FirstName = "John", LastName = "Doe" });
            // _homeRepository.InsertUser(new User { FirstName = "Jane", LastName = "Doe" });
            // _homeRepository.InsertUser(new User { FirstName = "Bob", LastName = "Smith" });
        }
         public IEnumerable<User> GetUsers()
        {
            return _homeRepository.GetUsers();
        }
    }
}
