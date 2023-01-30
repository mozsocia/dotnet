namespace di_prac.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        public string GetWelcomeMessage()
        {
            // this is where it get data from the database and return the data
            return "Welcome to my .NET Core app using a four-layered architecture!";
        }
    }
}
