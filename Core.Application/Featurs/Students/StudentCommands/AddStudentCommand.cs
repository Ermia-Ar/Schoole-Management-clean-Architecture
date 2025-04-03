using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Students.StudentCommands
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        public AddStudentRequest student { get; set; }
    }
}
