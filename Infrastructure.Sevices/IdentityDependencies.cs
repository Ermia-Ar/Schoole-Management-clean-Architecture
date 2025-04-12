using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
    public static class IdentityDependencies
    {
        public static IServiceCollection AddIdentityDependendcies(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login"; // آدرس صفحه ورود
                    options.LogoutPath = "/account/logout";
                    options.ExpireTimeSpan = TimeSpan.FromDays(14);
                    options.SlidingExpiration = true;
                });
            services.AddAuthorization();



            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(op =>
            //op.TokenValidationParameters = new TokenValidationParameters()
            //{
            //    ValidateIssuer = true,
            //    ValidateAudience = true,
            //    ValidateLifetime = true,
            //    ValidateIssuerSigningKey = true,
            //    ValidIssuer = JwtSettings.Issuer,
            //    ValidAudience = JwtSettings.Audience,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
            //});


            return services;
        }
    }
}
