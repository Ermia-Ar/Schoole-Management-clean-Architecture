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
        private readonly DbSet<Course> _course;

        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
            _teacher = context.Set<Teacher>();
            _course = context.Set<Course>();
        }

        public async Task<List<Teacher>> GetTeacherListAsync()
        {
            var Teachers = await _teacher.ToListAsync();

            return Teachers;
        }

    }
}
