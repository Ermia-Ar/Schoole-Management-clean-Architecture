using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Core.Application.Interfaces.IdentitySevices;
using Infrastructure.Identity.Services;
using Core.Application.Commands;
using Core.Application.DTOs;
using FluentValidation;
using Core.Application.Validators;
using Core.Application.Interfaces.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


#region Identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    //user 
    option.User.RequireUniqueEmail = true;
    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
    //sign in 
    option.SignIn.RequireConfirmedEmail = true;
    // password
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = true;
    option.Password.RequiredLength = 8;
    // lock out
    option.Lockout.AllowedForNewUsers = false;
    option.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

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
builder.Services.AddScoped<IEmailSender, EmailSender>();

#endregion

#region Validation

builder.Services.AddScoped<IValidator<SignUpRequest> , SignUpRequestValidator>();
builder.Services.AddScoped<IValidator<SignInRequest>, SignInRequestValidator>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
