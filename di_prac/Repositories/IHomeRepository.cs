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
