using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Data;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TodoController(AppDbContext context)
		{
			_context = context;
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<ActionResult<Todo>> CreateTodoItem(Todo todo)
		{
			_context.Todos.Add(todo);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTodoItems), new { id = todo.Id }, todo);
		}

		// GET: api/Todo
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Todo>>> GetTodoItems()
		{
			return await _context.Todos.Include(t => t.Category).ToListAsync();
		}
		
		
	}
}
