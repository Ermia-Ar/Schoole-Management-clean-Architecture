using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Entities;
using Core.Domain.Shared;

namespace Core.Application.Interfaces
{
    public interface IStudentServices
    {
        Task<bool> AddStudentAsync(AddStudentRequest request);

        Task<List<Student>> GetStudentListAsync();

        Task<Result<Student>> GetStudentByIdAsync(string id);

        Task<Student?> DeleteStudentAsync(string id);

        Task<bool> StudentIsInAnyCourse(string id);


    }
}
