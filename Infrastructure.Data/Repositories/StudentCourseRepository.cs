using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class StudentCourseRepository : GenericRepositoryAsync<StudentCourse>, IStudentCourseRepository
    {
        private DbSet<StudentCourse> _studentCourses {  get; set; }

        public StudentCourseRepository(ApplicationDbContext context) : base(context)
        {
            _studentCourses = context.Set<StudentCourse>();
        }

        public async Task<List<StudentCourse>> GetCourseStudentListAsync()
        {
            var studentCourses = await _studentCourses
                .Include(x => x.Course).Include(x => x.Course.Teacher).Include(x => x.Student)
                .ToListAsync();

            return studentCourses;
        }

        public async Task<List<Course>> GetCoursesByStudentId(string id)
        {
            var courses = await _studentCourses.Where(x => x.StudentId == Guid.Parse(id))
                .Include(x => x.Course)
                .Include(x => x.Course.Teacher)
                .Select(x => x.Course)
                .ToListAsync();

            return courses; 
        }

        public async Task<bool> StudentIsInAnyCourse(Guid id)
        {
            var isIn = await _studentCourses.AnyAsync(x => x.StudentId == id);
            return isIn;
        }
    }
}
