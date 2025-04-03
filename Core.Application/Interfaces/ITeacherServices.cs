using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITeacherServices
    {
        Task<bool> AddTeacherAsync(AddTeacherRequest request);

        Task<List<Teacher>> GetTeacherListAsync();

        Task<Teacher> GetTeacherByIdAsync(string Id);

        Task<Teacher?> DeleteTeacherAsync(string id);
    }
}
