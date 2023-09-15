

namespace ManyToManyDemo.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? Name  { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }  = new List<StudentCourse>();

    }
}
