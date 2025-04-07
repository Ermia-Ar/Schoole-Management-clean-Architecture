using Core.Domain.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Data
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

       

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "Admin",
                    ConcurrencyStamp = "Admin",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "Teacher",
                    ConcurrencyStamp = "Teacher",
                    Name = "Teacher",
                    NormalizedName = "Teacher".ToUpper()
                },
                new IdentityRole
                {
                    Id = "Student",
                    ConcurrencyStamp = "Student",
                    Name = "Student",
                    NormalizedName = "Student".ToUpper()
                }
            };
            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique(false);

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}
