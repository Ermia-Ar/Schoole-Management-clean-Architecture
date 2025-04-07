using Core.Application.DTOs.Course.CourseDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseQueries
{
    public class GetCourseByIdQuery : IRequest<Response<CourseResponse>>
    {
        public string Id { get; set; }
    }
}
