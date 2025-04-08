using Core.Application.Interfaces;
using Core.Application.Interfaces.Email;
using Infrastructure.Data.InfrustructureBases;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Services;
using Infrastructure.Identity.CurrentUserServices;
using Infrastructure.Identity.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.UnitOfWork;

namespace Infrastructure.Data
{
    public static class InfrastuctureDependencies
    {
        public static IServiceCollection AddInfrustructureDependendcies(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITeacherServices, TeacherServices>();
            services.AddScoped<IStudentCourseServices, StudentCourseServices>();
            services.AddScoped<ICourseServices, CourseServices>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurrentUserServices, CurrentUserServices>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            return services;
        }

    }
}
