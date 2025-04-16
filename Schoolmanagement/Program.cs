using Application.Behaviors;
using Core.Application;
using Core.Application.Mapper;
using Core.Application.Middleware;
using FluentValidation.AspNetCore;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Data.Data;
using Infrastructure.Data.InfrustructureBases.InfrastructureProfile;
using Infrastructure.Identity;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Mapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
});

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
//Hang fire setting
builder.Services.AddHangfire(option =>
{
    option.UseSqlServerStorage(builder.Configuration.GetConnectionString("IdentityConnection"));

    GlobalConfiguration.Configuration.UseFilter(new AutomaticRetryAttribute
    {
        Attempts = 3,
        DelaysInSeconds = new[] { 10, 30, 60 }
    });
});
builder.Services.AddHangfireServer();


builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddInfrustructureDependendcies()
    .AddIdentityDependendcies()
    .AddCoreDependencies();

builder.Services.AddScoped(
    typeof(IPipelineBehavior<,>),
    typeof(LoggingPipelineBehavior<,>));

builder.Services.AddAutoMapper(typeof(InfraProfile));
builder.Services.AddAutoMapper(typeof(AppMapper));
builder.Services.AddAutoMapper(typeof(IdentityProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHangfireDashboard();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
