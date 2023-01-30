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
