using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherCommands
{
    public class DeleteTeacherCommand : IRequest<Response<TeacherResponse>>
    {
        public string Id { get; set; }
    }
}
