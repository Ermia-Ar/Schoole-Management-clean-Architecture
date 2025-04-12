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
            var sql = @"
                    SELECT C.* , 
                    T.Id as Teacher_Id
                    From Courses AS C 
                    INNER JOIN Teachers AS T ON C.TeacherId = T.Id";

            var courses = await _courses
                .FromSql($"{sql}")
                .AsNoTracking()
                .Select(x => new Course
                {
                    CourseFee = x.CourseFee,
                    EndDate = x.EndDate,
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    StudentCourses = x.StudentCourses,
                    Subject = x.Subject,
                    Teacher = x.Teacher,
                    TeacherId = x.TeacherId
                })
                .ToListAsync();

            return courses;
        }

        public async Task<List<Course>> GetTeacherCoursesById(string id)
        {
            var teacherId = Guid.Parse(id);
            var courses = await _courses
                .FromSqlInterpolated($@"
                    SELECT C.* , 
                    T.Id as Teacher_Id
                    From Courses AS C 
                    INNER JOIN Teachers AS T ON C.TeacherId = T.Id
                    WHERE C.TeacherId = {teacherId}")
                .Select(x => new Course
                {
                    CourseFee = x.CourseFee,
                    EndDate = x.EndDate,
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    StudentCourses = x.StudentCourses,
                    Subject = x.Subject,
                    Teacher = x.Teacher,
                    TeacherId = x.TeacherId
                })
                .AsNoTracking()
                .ToListAsync();

            return courses;
        }

        public async Task<bool> TeacherIsInAnyCourse(Guid id)
        {
            var teacherId = id;

            var isIn = await _courses
                .FromSqlInterpolated($@"
                    SELECT * FROM Courses
                    WHERE C.TeacherId = {teacherId}")
                .AnyAsync();
            return isIn;
        }
    }
}
