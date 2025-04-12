using AutoMapper;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Interfaces;
using Infrastructure.Data.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Infrustructure.UnitOfWork;
using CoreTeacher = Core.Domain.Entities.Teacher;


namespace Infrastructure.Data.Services
{
    public class TeacherServices : ITeacherServices
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public TeacherServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context
            , IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddTeacherAsync(AddTeacherRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // map to ApplicationUser
                var user = _mapper.Map<ApplicationUser>(request);

                //add to user table and role table
                var result = await _unitOfWork.Users.CreateUserAsync(user, request.Password, "Teacher");
                if (!result)
                {
                    return false;
                }

                // add to teachers table
                var teacher = _mapper.Map<Entities.Teacher>(user);
                teacher.Id = Guid.NewGuid();
                teacher.Salary = request.Salary;
                teacher.Subject = request.Subject;

                await _unitOfWork.Teachers.AddAsync(teacher);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<CoreTeacher?> DeleteTeacherAsync(string id)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //find user by id in Teachers table 
                var teacher = await _unitOfWork.Teachers.GetByIdAsync(Guid.Parse(id));
                if (teacher == null)
                {
                    return null;
                }
                //remove from teachers table 
                await _unitOfWork.Teachers.DeleteAsync(teacher);

                // remove from user table
                var result = await _unitOfWork.Users.DeleteUserAsyncById(teacher.ApplicationUserId);
                if (!result)
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
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<CoreTeacher?> GetTeacherByIdAsync(string id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(Guid.Parse(id));
            if (teacher == null)
                return null;
            //map ro core teacher
            var coreTeacher = _mapper.Map<CoreTeacher>(teacher);

            return coreTeacher;
        }

        public async Task<List<CoreTeacher>> GetTeacherListAsync()
        {
            var Teachers = await _unitOfWork.Teachers.GetTeacherListAsync();

            //map teachers to core teachers
            var coreTeachers = _mapper.Map<List<CoreTeacher>>(Teachers);

            return coreTeachers;
        }

        public async Task<bool> TeacherIsInAnyCourse(string id)
        {
            var IsIn = await _unitOfWork.Courses.TeacherIsInAnyCourse(Guid.Parse(id));

            return IsIn;
        }
    }
}
