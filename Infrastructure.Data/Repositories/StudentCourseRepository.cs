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
            var studentCourses = await GetTableNoTracking()
                .Include(x => x.Course).Include(x => x.Course.Teacher).Include(x => x.Student)
                .ToListAsync();

            return studentCourses;
        }

        public async Task<List<Course>> GetCoursesByStudentId(string id)
        {
            var courses = await GetTableNoTracking().Where(x => x.StudentId == Guid.Parse(id))
                .Include(x => x.Course)
                .Include(x => x.Course.Teacher)
                .Select(x => x.Course)
                .ToListAsync();

            return courses; 
        }

        public async Task<bool> StudentIsInAnyCourse(Guid id)
        {
            var isIn = await GetTableNoTracking().AnyAsync(x => x.StudentId == id);
            return isIn;
        }

        public async Task<bool> DeleteRangeByCourseId(string courseId)
        {
            var studentCourses = await _studentCourses.AsNoTracking()
                .Where(x => x.CourseId.ToString() == courseId).ToListAsync();

            try
            {
                await DeleteRangeAsync(studentCourses);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
