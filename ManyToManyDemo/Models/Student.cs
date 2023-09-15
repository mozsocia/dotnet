

namespace ManyToManyDemo.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}