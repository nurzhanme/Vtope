using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vtope.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
           
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("VtopeDbConnection")));

        return services;
    }
}