using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherQuery
{
    public class GetTeacherListQuery : IRequest<Response<PaginatedResult<TeacherResponse>>>
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
