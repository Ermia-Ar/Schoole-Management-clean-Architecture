using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;

namespace Infrastructure.Data.Interfaces
{
    public interface ICourseRepository : IGenericRepositoryAsync<Course>
    {
        Task<List<Course>> GetCourseListAsync();
        Task<List<Course>> GetTeacherCoursesById(string id);
        Task<bool> TeacherIsInAnyCourse(Guid id);

    }
}
