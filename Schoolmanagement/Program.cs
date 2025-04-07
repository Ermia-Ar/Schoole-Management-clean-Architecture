using Core.Application.DTOs.Authontication;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.Student.Validator;
using Core.Application.Featurs.Authontication.Commands;
using Core.Application.Mapper;
using Core.Application.Middleware;
using Core.Application.Validators;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Data;
using Infrastructure.Identity;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.InfrastructureProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo() { Title = "Student-Management", Version = "v1" });
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id  = JwtBearerDefaults.AuthenticationScheme,
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
             },
             new List<string>()
        }
    });
});

// add Contexts
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection")));


//Identity
builder.Services.AddIdentityDependendcies();

//mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInAsyncCommand).Assembly));
builder.Services.AddInfrustructureDependendcies();

builder.Services.AddAutoMapper(typeof(InfraProfile));
builder.Services.AddAutoMapper(typeof(AppMapper));


#region Validation
builder.Services.AddScoped<IValidator<SignInRequest>, SignInRequestValidator>();
builder.Services.AddScoped<IValidator<AddStudentRequest>, AddStudentRequestValidator>();

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
