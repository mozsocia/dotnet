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
