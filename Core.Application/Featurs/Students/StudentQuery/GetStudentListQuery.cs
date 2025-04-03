using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Students.StudentQuery
{
    public class GetStudentListQuery : IRequest<Response<List<StudentResponse>>>
    {
    }
}
