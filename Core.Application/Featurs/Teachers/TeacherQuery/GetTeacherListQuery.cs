using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Teachers.TeacherQuery
{
    public class GetTeacherListQuery : IRequest<Response<List<TeacherResponse>>>
    {
    }
}
