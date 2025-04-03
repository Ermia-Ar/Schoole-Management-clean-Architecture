using AutoMapper;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Teachers.TeacherProfile
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher , TeacherResponse>().ReverseMap();
        }
    }
}
