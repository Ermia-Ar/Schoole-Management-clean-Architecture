using Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
{
    public AppIdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IdentityDatabase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        return new AppIdentityDbContext(optionsBuilder.Options);
    }
}

