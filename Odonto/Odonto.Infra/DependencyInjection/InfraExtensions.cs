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
        var connectionString = Environment.GetEnvironmentVariable("PostgreeConnection")
                               ?? throw new ArgumentException("Invalid Connection String!!!");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        using var scope = services.BuildServiceProvider().CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        
        // Repositórios aqui

        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}