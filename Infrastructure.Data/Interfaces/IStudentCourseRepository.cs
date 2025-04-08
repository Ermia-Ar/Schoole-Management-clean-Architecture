using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;

namespace Infrastructure.Data.Interfaces
{
    public interface IStudentCourseRepository : IGenericRepositoryAsync<StudentCourse>
    {
        Task<List<StudentCourse>> GetCourseStudentListAsync();
        Task<List<Course>> GetCoursesByStudentId(string id);
        Task<bool> StudentIsInAnyCourse(Guid id);
        Task<bool> DeleteRangeByCourseId(string courseId);

    }
}
