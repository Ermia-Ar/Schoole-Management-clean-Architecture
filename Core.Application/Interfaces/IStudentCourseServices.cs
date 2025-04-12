using Core.Application.DTOs.StudentCourse;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface IStudentCourseServices
    {
        Task<bool> RegisterInCourse(RegisterInCourseRequest request);

        Task<List<StudentCourse>> GetCourseStudentList();

        Task<List<Course>> GetCoursesByStudentId(string id);

    }
}
