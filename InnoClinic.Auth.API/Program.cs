using BLL.AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using BLL.Settings;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using InnoClinic.Auth.API.Extensions;
using InnoClinic.Auth.API.Middleware;
using InnoClinic.Auth.API.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AuthConnection")
    )
);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseAppSwagger();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
