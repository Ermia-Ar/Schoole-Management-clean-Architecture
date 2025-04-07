using AutoMapper;
using Core.Application.DTOs.Course.CourseDtos;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.StudentCourse.StudentDtos;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Entities;

namespace Core.Application.Mapper
{
    public class AppMapper :Profile
    {
        public AppMapper()
        {
            //student 
            CreateMap<StudentResponse, Student>()
                .ReverseMap();

            //teacher
            CreateMap<TeacherResponse, Teacher>()
                .ReverseMap();

            //course
            CreateMap<CourseResponse, Course>()
                .ReverseMap();

            //Student Course
            CreateMap<StudentCourseResponse , StudentCourse>()
                .ReverseMap();   
        }
    }
}
