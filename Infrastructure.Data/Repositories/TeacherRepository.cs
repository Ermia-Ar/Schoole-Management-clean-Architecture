using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TeacherRepository : GenericRepositoryAsync<Teacher>, ITeacherRepository
    {
        private readonly DbSet<Teacher> _teacher;

        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
            _teacher = context.Set<Teacher>();
        }

        public async Task<List<Teacher>> GetTeacherListAsync()
        {
            var Teachers = await _teacher
                .FromSqlInterpolated($@"
                    SELECT * FROM Teachers
                    ")
                .AsNoTracking()
                .ToListAsync();

            return Teachers;
        }

    }
}
