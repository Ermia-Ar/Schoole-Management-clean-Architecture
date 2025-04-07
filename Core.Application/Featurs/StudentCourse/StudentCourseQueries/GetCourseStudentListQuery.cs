using Core.Application.DTOs.StudentCourse;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseQueries
{
    public class GetCourseStudentListQuery : IRequest<Response<List<StudentCourseResponse>>>
    {
    }
}
