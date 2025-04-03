using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherCommands
{
    public class AddTeacherCommand : IRequest<Response<string>>
    {
        public AddTeacherRequest Teacher { get; set; }
    }
}
