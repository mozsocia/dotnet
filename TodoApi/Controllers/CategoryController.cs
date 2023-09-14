// Controllers/CategoryController.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CategoryController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Category
		[HttpGet]
		public ActionResult<IEnumerable<Category>> GetCategories()
		{
			return _context.Categories.Include(c => c.Todos).ToList();
		}
		
		// POST: api/Category
		[HttpPost]
		public async Task<ActionResult<Todo>> CreateCategoryItem(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
		}

		// ... other CRUD actions ...
	}
}
