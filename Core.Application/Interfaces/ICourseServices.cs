using Core.Application.DTOs.Course;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface ICourseServices
    {
        Task<bool> AddCourseAsync(AddCourseRequest request);

        Task<List<Course>> GetCourseListAsync();

        Task<bool> DeleteCourseAsync(string id);

        Task<Course?> GetCourseById(string id);

        Task<List<Course>> GetTeacherCoursesById(string id);
    }
}
