using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi_net5.Models;
namespace TodoApi_net5.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<Todo> _todos = new List<Todo>()
        {
        new Todo { Id = 1, Title = "Task 103", IsComplete = false },
        new Todo { Id = 2, Title = "Task 2", IsComplete = true },
        new Todo { Id = 3, Title = "Task 3", IsComplete = false },
    };

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await Task.FromResult(_todos);
        }

        public async Task<Todo> GetById(int id)
        {
            return await Task.FromResult(_todos.FirstOrDefault(t => t.Id == id));
        }

        public async Task<IEnumerable<Todo>> Create(Todo todo)
        {
            _todos.Add(todo);
            return await Task.FromResult(_todos);
        }

        public async Task Update(Todo todo)
        {
            var index = _todos.FindIndex(t => t.Id == todo.Id);
            _todos[index] = todo;
            await Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            _todos.RemoveAll(t => t.Id == id);
            await Task.CompletedTask;
        }
    }
}
