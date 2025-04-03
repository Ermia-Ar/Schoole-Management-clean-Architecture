using AutoMapper;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Interfaces;
using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreTeacher = Core.Domain.Entities.Teacher;


namespace Infrastructure.Data.Services
{
    public class TeacherServices : ITeacherServices
    {

        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public TeacherServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddTeacherAsync(AddTeacherRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // map to ApplicationUser
                var user = _mapper.Map<ApplicationUser>(request);

                //add to user table 
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return false;
                }
                //add to role table
                result = await _userManager.AddToRoleAsync(user, "Teacher");
                if (!result.Succeeded)
                {
                    return false;
                }

                // add to teachers table
                var teacher = _mapper.Map<Teacher>(user);
                teacher.Id = Guid.NewGuid();
                teacher.Salary = request.Salary;
                teacher.Subject = request.Subject;

                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CoreTeacher?> DeleteTeacherAsync(string id)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //find user by id in student table 
                var teacher = await _context.Teachers.FindAsync(Guid.Parse(id));
                if (teacher == null)
                {
                    return null;
                }

                //remove from student table 
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();

                //find user by ApplicationsUserId
                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId);
                if (user == null)
                {
                    return null;
                }

                // remove from user table
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return null;
                }

                //map to core student 
                var coreTeacher = _mapper.Map<CoreTeacher>(teacher);

                await transaction.CommitAsync();

                return coreTeacher;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CoreTeacher> GetTeacherByIdAsync(string id)
        {
            var teacher = await _context.Teachers.FindAsync(Guid.Parse(id));
            //map ro core teacher
            var coreTeacher = _mapper.Map<CoreTeacher>(teacher);

            return coreTeacher;
        }

        public async Task<List<CoreTeacher>> GetTeacherListAsync()
        {
            var Teachers = await _context.Teachers.ToListAsync();

            //map teachers to core teachers
            var coreTeachers = _mapper.Map<List<CoreTeacher>>(Teachers);

            return coreTeachers;
        }
    }
}
