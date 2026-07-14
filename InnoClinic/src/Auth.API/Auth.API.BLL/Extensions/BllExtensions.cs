using BLL.AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions
{
    public static class BllExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(UserMapper));
            return services;
        }
    }
}
