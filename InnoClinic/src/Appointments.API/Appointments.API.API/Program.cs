using Application.Extensions;
using Infrastructure.Extensions;
using InnoClinic.Shared.Middleware;
using InnoClinic.Shared.Extensions;
using InnoClinic.Appointments.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

app.UseCors();

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
