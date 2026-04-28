using Infrastructure.Persistence;
using InnoClinic.Appointments.Extensions;
using InnoClinic.Appointments.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAppSwagger();

builder.Services.AddDbContext<AppointmentDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppointmentsConnection"));
});

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidators();

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
