using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Students.StudentCommands
{
    public class DeleteStudentCommand : IRequest<Response<StudentResponse>>
    {
        public string Id { get; set; }
    }
}
