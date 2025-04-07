using Core.Domain.Entities;

namespace Core.Application.DTOs.StudentCourse.StudentDtos
{
    public class StudentCourseResponse
    {
        public Domain.Entities.Student Student { get; set; }

        public Domain.Entities.Course Course { get; set; }
    }
}
