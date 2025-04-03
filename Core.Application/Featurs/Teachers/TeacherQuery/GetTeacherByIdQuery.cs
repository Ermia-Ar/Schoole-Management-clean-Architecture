using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherQuery
{
    public class GetTeacherByIdQuery : IRequest<Response<TeacherResponse>>
    {
        public string Id { get; set; }  
    }
}
