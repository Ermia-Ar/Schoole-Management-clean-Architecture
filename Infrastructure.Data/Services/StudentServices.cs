
using AutoMapper;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Interfaces;
using Core.Domain.Shared;
using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.UnitOfWork;
using CoreStudent = Core.Domain.Entities.Student;

namespace Infrastructure.Data.Services
{
    public class StudentServices : IStudentServices
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }

        public StudentServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddStudentAsync(AddStudentRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //map to ApplicationUser
                var user = _mapper.Map<ApplicationUser>(request);

                //add to asp user table 
                var result = await _unitOfWork.Users.CreateUserAsync(user, request.Password, "Student");
                if (!result)
                {
                    return false;
                }
                // add to students table
                var student = _mapper.Map<Student>(user);
                student.Id = Guid.NewGuid();
                student.Grade = request.Grade;

                await _unitOfWork.Students.AddAsync(student);
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }

        }

        public async Task<List<CoreStudent>> GetStudentListAsync()
        {
            var Students = await _unitOfWork.Students.GetStudentListAsync();
            //map student to core student
            var coreStudents = _mapper.Map<List<CoreStudent>>(Students);

            return coreStudents;
        }

        public async Task<Result<CoreStudent>> GetStudentByIdAsync(string id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(Guid.Parse(id));
            if (student == null)
                return Result.Failure<CoreStudent>(Error.None);

            var coreStudent  = _mapper.Map<CoreStudent>(student);

            return Result.Success(coreStudent);

        }

        public async Task<CoreStudent?> DeleteStudentAsync(string id)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //find user by id in student table 
                var student = await _unitOfWork.Students.GetByIdAsync(Guid.Parse(id));
                if (student == null)
                {
                    return null;
                }
               
                //remove from student table 
                await _unitOfWork.Students.DeleteAsync(student);

                //remove from user table
                var user = _unitOfWork.Users.DeleteUserAsyncById(student.ApplicationUserId);

                //map to core student 
                var coreStudent = _mapper.Map<CoreStudent>(student);

                await transaction.CommitAsync();

                return coreStudent;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }

        }

        public async Task<bool> StudentIsInAnyCourse(string id)
        {
            var result = await _unitOfWork.StudentsCourse.StudentIsInAnyCourse(Guid.Parse(id));

            return result;  
        }
    }
}


