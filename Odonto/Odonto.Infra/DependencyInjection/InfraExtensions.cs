using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Odonto.Infra.Context.Data.Context;

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

        return services;
    }
}