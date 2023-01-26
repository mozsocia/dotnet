using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi_net5.Models;

namespace TodoApi_net5.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll();
        Task<Todo> GetById(int id);
        Task<IEnumerable<Todo>> Create(Todo todo);
        Task Update(Todo todo);
        Task Delete(int id);
    }
}

