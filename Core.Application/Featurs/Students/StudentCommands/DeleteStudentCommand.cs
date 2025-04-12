using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Students.StudentCommands
{
    public class DeleteStudentCommand : IRequest<Response<StudentResponse>>
    {
        public string Id { get; set; }
    }
}
