using Core.Application.DTOs.StudentCourse;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseCommands
{
    public class RegisterInCourseCommand : IRequest<Response<string>>
    {
        public RegisterInCourseRequest StudentCourse { get; set; }
    }
}
