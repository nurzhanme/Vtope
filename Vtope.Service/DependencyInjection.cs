using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vtope.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IInstaService, InstaService>();

        return services;
    }
}