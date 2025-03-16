using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity.Data;

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
{
    public AppIdentityDbContext CreateDbContext(string[] args)
    {
        // ساخت Configuration برای دسترسی به Connection String
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // ساخت DbContextOptions با استفاده از Connection String
        var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("IdentityConnection");
        builder.UseSqlServer(connectionString);

        return new AppIdentityDbContext(builder.Options);
    }
}

