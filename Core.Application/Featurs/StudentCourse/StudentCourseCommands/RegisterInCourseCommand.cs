using Core.Application.DTOs.StudentCourse;
using Core.Domain.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.StudentCourse.StudentCourseCommands
{
    public class RegisterInCourseCommand : IRequest<Response<string>>
    {
        public RegisterInCourseRequest StudentCourse { get; set; }
    }
}
