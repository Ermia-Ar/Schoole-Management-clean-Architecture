using AutoMapper;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Students.StudentProfile
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentResponse, Student>().ReverseMap();
        }
    }
}
