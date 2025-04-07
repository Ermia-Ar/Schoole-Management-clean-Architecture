namespace Core.Application.DTOs.StudentCourse
{
    public class StudentCourseResponse
    {
        public Domain.Entities.Student Student { get; set; }

        public Domain.Entities.Course Course { get; set; }
    }
}
