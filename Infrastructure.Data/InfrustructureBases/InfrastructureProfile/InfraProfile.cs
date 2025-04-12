using AutoMapper;
using Core.Application.DTOs.Course;
//using CoreTCourse = Core.Domain.Entities.Course;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Infrastructure.Data.Entities;
using Infrastructure.Identity.Models;
using CoreAdmin = Core.Domain.Entities.Admin;
using CoreCourse = Core.Domain.Entities.Course;
using CoreStudent = Core.Domain.Entities.Student;
using CoreTeacher = Core.Domain.Entities.Teacher;

namespace Infrastructure.Data.InfrustructureBases.InfrastructureProfile
{
    public class InfraProfile : Profile
    {

        public InfraProfile()
        {
            //Teacher Maps
            CreateMap<ApplicationUser, Teacher>()
               .ForMember(op => op.ApplicationUserId, des => des.MapFrom(op => op.Id))
               .ReverseMap();

            CreateMap<CoreTeacher, Teacher>()
              .ForMember(op => op.Id, des => des.MapFrom(op => op.Id))
             .ReverseMap();

            CreateMap<ApplicationUser, AddTeacherRequest>()
                .ReverseMap();

            //Student Maps
            CreateMap<ApplicationUser, Student>()
              .ForMember(op => op.ApplicationUserId, des => des.MapFrom(op => op.Id))
              .ReverseMap();

            CreateMap<CoreStudent, Student>()
                 .ForMember(op => op.Id, des => des.MapFrom(op => Guid.Parse(op.Id)))
                .ReverseMap();

            CreateMap<ApplicationUser, AddStudentRequest>()
                 .ReverseMap();

            //Amin Maps

            CreateMap<ApplicationUser, Admin>()
                .ForMember(op => op.ApplicationUserId, des => des.MapFrom(op => op.Id))
                .ReverseMap();

            CreateMap<CoreAdmin, Admin>()
                 .ForMember(op => op.Id, des => des.MapFrom(op => op.Id))
                .ReverseMap();

            //Course Maps
            CreateMap<AddCourseRequest, Course>()
                .ForMember(op => op.TeacherId, dex => dex.MapFrom(op => Guid.Parse(op.TeacherId)))
                .ReverseMap();

            CreateMap<Course, CoreCourse>()
                .ForMember(x => x.TeacherCourse, dex => dex.MapFrom(x => Convertor.ConvertToTeacher(x.Teacher)))
                .ReverseMap();

            CreateMap<Course, Course>()
                .ForMember(op => op.Teacher, dex => dex.MapFrom(x => x.Teacher))
                .ReverseMap();




        }
    }
}
