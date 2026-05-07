using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Odonto.Infra.Context.Data.Context;
using Odonto.Infra.Repositories.ClienteRepo;
using Odonto.Infra.RepositoryInterfaces.IClienteRepo;

namespace Odonto.Infra.DependencyInjection;

public static class InfraExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("PostgreConnection")
                               ?? throw new ArgumentException("Invalid Connection String!!!");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}