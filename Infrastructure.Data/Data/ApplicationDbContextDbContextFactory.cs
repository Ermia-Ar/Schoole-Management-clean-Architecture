using Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolManagementDatebase;Integrated Security=True;Trust Server Certificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

