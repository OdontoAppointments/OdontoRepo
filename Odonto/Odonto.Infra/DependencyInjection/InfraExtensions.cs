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
        var host     = Environment.GetEnvironmentVariable("DB_HOST")     
                       ?? throw new InvalidOperationException("Variável de ambiente 'DB_HOST' não definida.");
        var port     = Environment.GetEnvironmentVariable("DB_PORT")     
                       ?? throw new InvalidOperationException("Variável de ambiente 'DB_PORT' não definida.");
        var db       = Environment.GetEnvironmentVariable("DB_NAME")     
                       ?? throw new InvalidOperationException("Variável de ambiente 'DB_NAME' não definida.");
        var user     = Environment.GetEnvironmentVariable("DB_USER")     
                       ?? throw new InvalidOperationException("Variável de ambiente 'DB_USER' não definida.");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") 
                       ?? throw new InvalidOperationException("Variável de ambiente 'DB_PASSWORD' não definida.");

        if (!int.TryParse(port, out _))
            throw new InvalidOperationException($"Variável de ambiente 'DB_PORT' inválida: '{port}' não é um número.");

        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}