using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface ITeacherServices
    {
        Task<bool> AddTeacherAsync(AddTeacherRequest request);

        Task<List<Teacher>> GetTeacherListAsync();

        Task<Teacher?> GetTeacherByIdAsync(string Id);

        Task<Teacher?> DeleteTeacherAsync(string id);

        Task<bool> TeacherIsInAnyCourse(string id);

    }
}
