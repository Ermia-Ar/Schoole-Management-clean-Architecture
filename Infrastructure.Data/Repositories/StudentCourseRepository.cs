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
                .FromSql(@$"SELECT SC.*,
                         S.Id AS Student_Id,
                         C.Id AS Course_Id,
                         T.Id AS Teacher_Id
                         FROM StudentCourses AS SC 
                         INNER JOIN Courses AS C ON SC.CourseId = C.Id
                         INNER JOIN Teachers AS T ON C.TeacherId = T.Id
                         INNER JOIN Students AS S ON SC.StudentId = S.Id")
                .Select(x => new StudentCourse
                {
                    Course = x.Course,
                    CourseId = x.CourseId,
                    Score = x.Score,
                    Student = x.Student,
                    StudentId = x.StudentId
                })
                .AsNoTracking()
                .ToListAsync();

            return studentCourses;
        }

        public async Task<List<Course>> GetCoursesByStudentId(string id)
        {
            var studentId = Guid.Parse(id);
            var courses = await _studentCourses
                .FromSqlInterpolated(@$"
                    SELECT 
                    SC.CourseId,
                    SC.StudentId,
                    C.Id AS Course_Id,
                    T.Id AS Teacher_Id
                    FROM StudentCourses AS SC
                    INNER JOIN Courses AS C ON SC.CourseId = C.Id
                    INNER JOIN Teachers AS T ON C.TeacherId = T.Id
                    WHERE SC.StudentId = {studentId} ")
                .Select(x => new Course
                {
                    CourseFee = x.Course.CourseFee,
                    EndDate = x.Course.EndDate,
                    Id = x.Course.Id,
                    Name = x.Course.Name,
                    StartDate = x.Course.StartDate,
                    StudentCourses = x.Course.StudentCourses,
                    Subject = x.Course.Subject,
                    Teacher = x.Course.Teacher,
                    TeacherId = x.Course.TeacherId  
                } )
                .AsNoTracking()
                .ToListAsync();

            return courses; 
        }

        public async Task<bool> StudentIsInAnyCourse(Guid id)
        {
            var studentId = id;
            var isIn = await _studentCourses
                .FromSqlInterpolated(@$"
                    SELECT StudentId FROM StudentCourses
                    WHERE StudentId = {studentId}")
                .AnyAsync();

            return isIn;
        }

        public async Task<bool> DeleteRangeByCourseId(string courseId)
        {
            try
            {
                var CourseId = Guid.Parse(courseId);
                var studentCourses = _studentCourses
                    .FromSqlInterpolated(@$"
                    DELETE FROM StudentCourses
                    WHERE Id = {CourseId}");

                await Task.CompletedTask;  
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
