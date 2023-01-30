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
