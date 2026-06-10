using Application.Extensions;
using Infrastructure.Extensions;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.Middleware;
using Offices.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseAppSwagger("InnoClinic.Offices.API");
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
