﻿using AutoMapper;
using Core.Application.DTOs.StudentCourse;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Identity.CurrentUserServices;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.UnitOfWork;
using CoreCourses = Core.Domain.Entities.Course;
using StudentCore = Core.Domain.Entities.Student;
using StudentCourse = Infrastructure.Data.Entities.StudentCourse;
using StudentCourseCore = Core.Domain.Entities.StudentCourse;

namespace Infrastructure.Data.Services
{
    public class StudentCourseServices : IStudentCourseServices
    {
        private ApplicationDbContext _context { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ICurrentUserServices _currentUserServices { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }

        public StudentCourseServices(IMapper mapper, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, ICurrentUserServices currentUserServices)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _currentUserServices = currentUserServices;
        }

        public async Task<bool> RegisterInCourse(RegisterInCourseRequest request)
        {
            try
            {
                var ApplicationUserId = _currentUserServices.GetUserId();

                var Student = await _unitOfWork.Students.GetTableNoTracking()
                    .FirstOrDefaultAsync(x => x.ApplicationUserId == ApplicationUserId);

                var studentCourse = new StudentCourse
                {
                    CourseId = Guid.Parse(request.CourseId),
                    StudentId = Student.Id,
                };

                await _unitOfWork.StudentsCourse.AddAsync(studentCourse);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> CheckRegisterInfo(string codeMelly, string password, string courseId)
        {
            var course = await _context.Courses.FindAsync(Guid.Parse(courseId));
            if (course == null)
                return false;

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.CodeMelly == codeMelly);
            if (user == null)
                return false;

            var passwordValidate = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValidate)
                return false;

            return true;
        }

        public async Task<List<StudentCourseCore>> GetCourseStudentList()
        {
            var studentCourses = await _unitOfWork.StudentsCourse.GetCourseStudentListAsync();

            // map to StudentCourseCore
            var coreStudentCourses = new List<StudentCourseCore>();
            foreach (var studentCourse in studentCourses)
            {
                var coreStudentCourse = new StudentCourseCore
                {
                    Course = _mapper.Map<CoreCourses>(studentCourse.Course),
                    Student = _mapper.Map<StudentCore>(studentCourse.Student),
                };
                coreStudentCourses.Add(coreStudentCourse);
            }
            return coreStudentCourses;
        }

        public async Task<List<CoreCourses>> GetCoursesByStudentId(string id)
        {
            var courses = await _unitOfWork.StudentsCourse.GetCoursesByStudentId(id);

            var coreCourses = _mapper.Map<List<CoreCourses>>(courses);
            return coreCourses;
        }



    }
}
