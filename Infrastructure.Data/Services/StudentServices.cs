
using AutoMapper;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Interfaces;
using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreStudent = Core.Domain.Entities.Student;

namespace Infrastructure.Data.Services
{
    public class StudentServices : IStudentServices
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public StudentServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            //_signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddStudentAsync(AddStudentRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //map to ApplicationUser
                var user = _mapper.Map<ApplicationUser>(request);

                //add to asp user table 
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return false;
                }

                //add to role table
                result = await _userManager.AddToRoleAsync(user, "Student");
                if (!result.Succeeded)
                {
                    return false;
                }

                // add to students table
                var student = _mapper.Map<Student>(user);
                student.Id = Guid.NewGuid();
                student.Grade = request.Grade;

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<CoreStudent>> GetStudentListAsync()
        {
            var Students = await _context.Students.ToListAsync();

            //map student to core student
            var coreStudents = _mapper.Map<List<CoreStudent>>(Students);

            return coreStudents;
        }

        public async Task<CoreStudent> GetStudentByIdAsync(string id)
        {
            var student = await _context.Students.FindAsync(Guid.Parse(id));

            var coreStudent  = _mapper.Map<CoreStudent>(student);

            return coreStudent;

        }

        public async Task<CoreStudent?> DeleteStudentAsync(string id)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //find user by id in student table 
                var student = await _context.Students.FindAsync(Guid.Parse(id));
                if (student == null)
                {
                    return null;
                }

                //remove from student table 
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                //find user by ApplicationsUserId
                var user = await _userManager.FindByIdAsync(student.ApplicationUserId);
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
                var coreStudent = _mapper.Map<CoreStudent>(student);

                await transaction.CommitAsync();

                return coreStudent;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}


