using Core.Application.DTOs.Course;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseQueries
{
    public class GetCoursesByStudentIdQuery : IRequest<Response<List<CourseResponse>>>
    {
        public string Id { get; set; }
    }
}
