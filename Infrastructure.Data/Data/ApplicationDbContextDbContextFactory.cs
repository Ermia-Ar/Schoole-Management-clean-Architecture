using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Data;

public class ApplicationDbContextDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolManagementDatebase;Integrated Security=True;Trust Server Certificate=True");
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

