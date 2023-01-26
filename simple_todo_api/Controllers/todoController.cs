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
