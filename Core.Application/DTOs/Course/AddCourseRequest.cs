using Core.Domain;

namespace Core.Application.DTOs.Course
{
    public class AddCourseRequest
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subjects Subject { get; set; }

        public decimal CourseFee { get; set; }

        public string TeacherId { get; set; }
    }
}
