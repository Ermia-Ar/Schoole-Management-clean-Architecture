using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;

namespace Infrastructure.Data.Interfaces
{
    public interface IStudentRepository :IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetStudentListAsync();
    }
}
