using BLL.AutoMapper;
using BLL.Clients;
using BLL.Interfaces;
using BLL.Options;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BLL.Extensions
{
    public static class BllExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(UserMapper));
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();

            services.Configure<ProfilesApiOptions>(configuration.GetSection("ProfilesApi"));
            services.AddHttpClient<IProfilesClient, ProfilesClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ProfilesApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });
            return services;
        }
    }
}
