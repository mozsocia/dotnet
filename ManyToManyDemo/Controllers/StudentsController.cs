using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyToManyDemo.Models;
using ManyToManyDemo.Data;

namespace ManyToManyDemo.Controllers
{
	[Route("api/students")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public StudentsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/students
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
		{
			var students = await _context.Students.ToListAsync();
			return Ok(students);
		}

		// GET: api/students/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Student>> GetStudent(int id)
		{
			var student = await _context.Students
				.Include(s => s.StudentCourses)
				.ThenInclude(sc => sc.Course)
				.FirstOrDefaultAsync(s => s.StudentId == id);

			if (student == null)
			{
				return NotFound();
			}
			
			// return Ok(student);

			// Create a projection of the student object to achieve the desired JSON format
			var studentWithCourses = new
			{
				student.StudentId,
				student.Name,
				Courses = student.StudentCourses.Select(sc => new
				{
					
						sc.Course.CourseId,
						sc.Course.Name,
						// Optionally, you can include related data for courses here if needed
				
				}).ToList()
			};

			return Ok(studentWithCourses);
		}

		// POST: api/students
		[HttpPost]
		public async Task<ActionResult<Student>> CreateStudent(Student student)
		{
			_context.Students.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
		}

		// Add other CRUD endpoints for students as needed


		// POST: api/students/{studentId}/courses/{courseId}
		[HttpPost("{studentId}/courses/{courseId}")]
		public async Task<IActionResult> EnrollStudentInCourse(int studentId, int courseId)
		{
			var student = await _context.Students.FindAsync(studentId);
			if (student == null)
			{
				return NotFound("Student not found");
			}

			var course = await _context.Courses.FindAsync(courseId);
			if (course == null)
			{
				return NotFound("Course not found");
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

		// DELETE: api/students/{studentId}/courses/{courseId}
		[HttpDelete("{studentId}/courses/{courseId}")]
		public async Task<IActionResult> RemoveStudentFromCourse(int studentId, int courseId)
		{
			var student = await _context.Students.FindAsync(studentId);
			if (student == null)
			{
				return NotFound("Student not found");
			}

			var course = await _context.Courses.FindAsync(courseId);
			if (course == null)
			{
				return NotFound("Course not found");
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
