using Core.Application.DTOs.Course;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseQueries
{
    public class GetTeacherCoursesByIdQuery : IRequest<Response<PaginatedResult<CourseResponse>>>
    {
        public string Id { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
