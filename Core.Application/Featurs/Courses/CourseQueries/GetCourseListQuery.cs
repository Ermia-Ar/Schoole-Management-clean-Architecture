using Core.Application.DTOs.Course;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseQueries
{
    public class GetCourseListQuery : IRequest<Response<PaginatedResult<CourseResponse>>>
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
