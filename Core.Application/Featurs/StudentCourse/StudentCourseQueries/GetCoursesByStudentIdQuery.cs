using Core.Application.DTOs.Course;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseQueries
{
    public class GetCoursesByStudentIdQuery : IRequest<Response<PaginatedResult<CourseResponse>>>
    {
        public string Id { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
