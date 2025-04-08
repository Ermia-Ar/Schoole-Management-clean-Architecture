using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Students.StudentQuery
{
    public class GetStudentListQuery : IRequest<Response<PaginatedResult<StudentResponse>>>
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
