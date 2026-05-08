using BLL.Extensions;
using DAL.Extensions;
using InnoClinic.Auth.API.Extensions;
using InnoClinic.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddBll(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddAppSwagger(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseAppSwagger();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();