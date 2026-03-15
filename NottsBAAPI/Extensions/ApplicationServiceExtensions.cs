using Microsoft.Identity.Client;
using NottsBAAPI.Interface;
using NottsBAAPI.Service;
using System.Runtime.CompilerServices;

namespace NottsBAAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthNottsBA, AuthService>();

        return services;
    }
}
