using Core.Application.DTOs.Course.CourseDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherQuery
{
    public class GetTeacherCoursesByIdQuery : IRequest<Response<List<CourseResponse>>>
    {
        public string Id { get; set; }
    }
}
