// Models/Category.cs
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
