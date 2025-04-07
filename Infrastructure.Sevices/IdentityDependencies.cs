using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Identity
{
    public static class IdentityDependencies
    {
        public static IServiceCollection AddIdentityDependendcies(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("Auth")
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(option =>
            {
                //user 
                option.User.RequireUniqueEmail = false;
                // password
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredLength = 8;
                // lock out
                option.Lockout.AllowedForNewUsers = false;
                option.Lockout.MaxFailedAccessAttempts = 3;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.HttpOnly = true;
                option.ExpireTimeSpan = TimeSpan.FromSeconds(3);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
                });
                
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Teacher", policy =>
            //        policy.RequireRole("Teacher"));
            //    options.AddPolicy("Student", policy =>
            //        policy.RequireRole("Student"));
            //    options.AddPolicy("Admin", policy =>
            //        policy.RequireRole("Admin"));
            //});
            return services;
        }
    }
}
