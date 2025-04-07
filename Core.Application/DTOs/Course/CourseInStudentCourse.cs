using Core.Domain;

namespace Core.Application.DTOs.Course
{
    public class CourseInStudentCourse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Subjects Subject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
