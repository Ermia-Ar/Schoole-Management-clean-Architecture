using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Application.Commands;
using FluentValidation;
using Core.Application.Validators;
using Core.Application.Interfaces.Email;
using Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Core.Application.Interfaces.IdentitySevices;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Identity.Models;
using Infrastructure.Data.Data;
using Infrastructure.Identity.InfrastructureProfile;
using Core.Application.Interfaces;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Application.Featurs.Students.StudentProfile;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.Student.Validator;
using Core.Application.DTOs.NewFolder;
using Core.Application.Featurs.Teachers.TeacherProfile;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo() { Title = "Walks Api", Version = "v1" });
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


builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection")));



#region Identity

builder.Services.AddIdentityCore<ApplicationUser>()
        .AddRoles<IdentityRole>()
        .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("Auth")
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
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
#endregion

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Account/SignIn";
    option.LogoutPath = "/";
    option.LoginPath = "/Account/SignIn";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromSeconds(3);
});

#region ImediatR

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInAsyncCommand).Assembly));
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddAutoMapper(typeof(InfraProfile));
builder.Services.AddAutoMapper(typeof(StudentMapper));
builder.Services.AddAutoMapper(typeof(TeacherProfile));



#endregion

#region Validation
builder.Services.AddScoped<IValidator<SignUpRequest>, SignUpRequestValidator>();
builder.Services.AddScoped<IValidator<SignInRequest>, SignInRequestValidator>();
builder.Services.AddScoped<IValidator<AddStudentRequest>, AddStudentRequestValidator>();

#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
