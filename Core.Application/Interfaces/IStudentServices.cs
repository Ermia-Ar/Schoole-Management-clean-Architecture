using Core.Application.DTOs.Student.StudentDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IStudentServices
    {
        Task<bool> AddStudentAsync(AddStudentRequest request);

        Task<List<Student>> GetStudentListAsync();

        Task<Student?> GetStudentByIdAsync(string id);

        Task<Student?> DeleteStudentAsync(string id);

        Task<bool> StudentIsInAnyCourse(string id);


    }
}
