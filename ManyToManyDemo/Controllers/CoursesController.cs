using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyToManyDemo.Models;
using ManyToManyDemo.Data;


namespace ManyToManyDemo.Controllers
{
	[Route("api/courses")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CoursesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/courses
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
		{
			var courses = await _context.Courses.ToListAsync();
			return Ok(courses);
		}

		// GET: api/courses/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Course>> GetCourse(int id)
		{
			var course = await _context.Courses
				.Include(c => c.StudentCourses)
				.ThenInclude(sc => sc.Student)
				.FirstOrDefaultAsync(c => c.CourseId == id);

			if (course == null)
			{
				return NotFound();
			}

			// return Ok(course);
			
			var studentWithCourses = new
			{
				course.CourseId,
				course.Name,
				Students = course.StudentCourses.Select(sc => new
				{

					sc.Student.StudentId,
					sc.Student.Name,
					// Optionally, you can include related data for courses here if needed

				}).ToList()
			};

			return Ok(studentWithCourses);



	
		}

		// POST: api/courses
		[HttpPost]
		public async Task<ActionResult<Course>> CreateCourse(Course course)
		{
			_context.Courses.Add(course);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
		}

		// Add other CRUD endpoints for courses as needed

		// POST: api/courses/{courseId}/students/{studentId}
		[HttpPost("{courseId}/students/{studentId}")]
		public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
		{
			var course = await _context.Courses.FindAsync(courseId);
			if (course == null)
			{
				return NotFound("Course not found");
			}

			var student = await _context.Students.FindAsync(studentId);
			if (student == null)
			{
				return NotFound("Student not found");
			}

			var existingEnrollment = await _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
				.FirstOrDefaultAsync();

			if (existingEnrollment != null)
			{
				return BadRequest("Student is already enrolled in this course");
			}

			var studentCourse = new StudentCourse
			{
				StudentId = studentId,
				CourseId = courseId
			};

			_context.StudentCourses.Add(studentCourse);
			await _context.SaveChangesAsync();

			return Ok($"Student {student.Name} enrolled in course {course.Name}");
		}

		// DELETE: api/courses/{courseId}/students/{studentId}
		[HttpDelete("{courseId}/students/{studentId}")]
		public async Task<IActionResult> RemoveStudentFromCourse(int courseId, int studentId)
		{
			var course = await _context.Courses.FindAsync(courseId);
			if (course == null)
			{
				return NotFound("Course not found");
			}

			var student = await _context.Students.FindAsync(studentId);
			if (student == null)
			{
				return NotFound("Student not found");
			}

			var enrollment = await _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
				.FirstOrDefaultAsync();

			if (enrollment == null)
			{
				return NotFound("Student is not enrolled in this course");
			}

			_context.StudentCourses.Remove(enrollment);
			await _context.SaveChangesAsync();

			return Ok($"Student {student.Name} removed from course {course.Name}");
		}
	}
}
