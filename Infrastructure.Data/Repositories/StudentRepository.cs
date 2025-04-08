using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _student;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _student = context.Set<Student>();
        }

        public async Task<List<Student>> GetStudentListAsync()
        {
            var Students = await _student.AsNoTracking().ToListAsync();

            return Students;
        }
    }
}
