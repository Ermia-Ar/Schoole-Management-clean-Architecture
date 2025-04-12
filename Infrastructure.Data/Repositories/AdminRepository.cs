using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class AdminRepository : GenericRepositoryAsync<Admin>, IAdminRepository
    {
        private DbSet<Admin> _admins;

        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            _admins = context.Set<Admin>();
        }
    }
}
