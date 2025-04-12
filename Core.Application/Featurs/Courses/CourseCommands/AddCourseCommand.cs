using Core.Application.DTOs.Course;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseCommands
{
    public class AddCourseCommand : IRequest<Response<string>>
    {
        public AddCourseRequest Course { get; set; }
    }
}
