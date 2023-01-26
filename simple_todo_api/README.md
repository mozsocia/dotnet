### super basic and simple example

```cs

namespace TodoApi_net5.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
```
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi_net5.Repositories;
using TodoApi_net5.Models;

namespace TodoApi_net5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        // 
         private static List<Todo> _todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Task 1", IsComplete = false },
        new Todo { Id = 2, Title = "Task 2", IsComplete = true },
        new Todo { Id = 3, Title = "Task 3", IsComplete = false },
    };

    [HttpGet]
    public ActionResult<List<Todo>> GetAll()
    {
        return _todos;
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Create(Todo todo)
    {
        _todos.Add(todo);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Todo todo)
    {
        var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
        {
            return NotFound();
        }
        existingTodo.Title = todo.Title;
        existingTodo.IsComplete = todo.IsComplete;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
        {
            return NotFound();
        }
        _todos.Remove(existingTodo);
        return NoContent();
    }

    }
    
}
```
----------------
------------------
-----------------
<br>
<br>

### DI with repository

```
|-- MyTodoApi
    |-- Controllers
        |-- TodotwoController.cs
    |-- Models
        |-- Todo.cs
    |-- Repositories
        |-- ITodoRepository.cs
        |-- TodoRepository.cs
    |-- Startup.cs
    |-- Program.cs

```
Todo.cs
```cs
//Todo.cs
namespace TodoApi_net5.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
```
ITodoRepository.cs
```cs

//
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

```

TodoRepository.cs
```cs
//TodoRepository.cs
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

```

Startup.cs
```cs

public void ConfigureServices(IServiceCollection services)
{

    ...
    services.AddSingleton<ITodoRepository, TodoRepository>();
    ...
}



```
TodotwoController.cs
```cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi_net5.Repositories;
using TodoApi_net5.Models;
namespace TodoApi_net5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodotwoController : ControllerBase
    {
        // 

        
        private readonly ITodoRepository _todoRepository;

        public TodotwoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Get()
        {
            var todos = await _todoRepository.GetAll();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post(Todo todo)
        {
            var todos = await _todoRepository.Create(todo);
            return Ok(todos);
            
            // return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            await _todoRepository.Update(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoRepository.Delete(id);
            return NoContent();
        }
    }
    
}



