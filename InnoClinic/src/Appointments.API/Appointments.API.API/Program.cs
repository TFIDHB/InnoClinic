using Application.Extensions;
using Infrastructure.Extensions;
using InnoClinic.Appointments.Extensions;
using InnoClinic.Shared.Middleware;
using InnoClinic.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseAppSwagger("InnoClinic.Appointments.API");
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
