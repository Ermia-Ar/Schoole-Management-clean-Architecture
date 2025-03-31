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
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Admin = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var Teacher = "c309fa92-2123-47be-b397-a1c77adb502c";
            var Student = "a5510348-12d0-422c-9912-3faad602e3e4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = Admin,
                    ConcurrencyStamp = Admin,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = Teacher,
                    ConcurrencyStamp = Teacher,
                    Name = "Teacher",
                    NormalizedName = "Writer".ToUpper()
                },
                new IdentityRole
                {
                    Id = Student,
                    ConcurrencyStamp = Student,
                    Name = "Student",
                    NormalizedName = "Student".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}
