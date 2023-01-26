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
