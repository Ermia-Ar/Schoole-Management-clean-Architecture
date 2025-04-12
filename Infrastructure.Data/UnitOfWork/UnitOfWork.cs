using AutoMapper;
using Infrastructure.Data.Data;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;

namespace SchoolProject.Infrustructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IStudentRepository Students { get; private set; }

        public IStudentCourseRepository StudentsCourse { get; private set; }

        public ITeacherRepository Teachers { get; private set; }

        public IAdminRepository Admins { get; private set; }

        public ICourseRepository Courses { get; private set; }

        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IUserRepository users)
        {
            _context = context;
            Students = new StudentRepository(_context);
            StudentsCourse = new StudentCourseRepository(_context);
            Teachers = new TeacherRepository(_context);
            Admins = new AdminRepository(_context);
            Courses = new CourseRepository(_context);
            Users = users;
        }

        // did not use cause there is async methods 
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
