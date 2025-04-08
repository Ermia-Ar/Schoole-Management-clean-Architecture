using Core.Application.DTOs.StudentCourse;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseQueries
{
    public class GetCourseStudentListQuery : IRequest<Response<PaginatedResult<StudentCourseResponse>>>
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
