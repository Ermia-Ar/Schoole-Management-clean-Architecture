using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;

namespace Infrastructure.Data.Interfaces
{
    public interface ITeacherRepository : IGenericRepositoryAsync<Teacher>
    {
        public Task<List<Teacher>> GetTeacherListAsync();

    }
}
