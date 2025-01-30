using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Abstractions.Data;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISqlConnectionFactory>(sp =>
        {
            string? connectionString = configuration.GetConnectionString("sqlDb");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("SQL connection string 'sqlDb' is not configured.");
            }

            return new SqlConnectionFactory(connectionString);
        });

        return services;
    }
}
