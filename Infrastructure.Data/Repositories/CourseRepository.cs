using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class CourseRepository : GenericRepositoryAsync<Course>, ICourseRepository
    {
        private DbSet<Course> _courses;

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _courses = context.Set<Course>();
        }

        public async Task<List<Course>> GetCourseListAsync()
        {
            var courses = await _courses.Include(x => x.Teacher).ToListAsync();

            return courses;
        }

        public async Task<List<Course>> GetTeacherCoursesById(string id)
        {
            var courses = await _courses
                .Where(x => x.TeacherId == Guid.Parse(id)).ToListAsync();

            return courses;
        }

        public async Task<bool> TeacherIsInAnyCourse(Guid id)
        {
            var isIn = await _courses.AnyAsync(x => x.TeacherId == id);
            return isIn;
        }
    }
}
