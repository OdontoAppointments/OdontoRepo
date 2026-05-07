using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.ServicesInterfaces.IntClientesServ;
using Odonto.BL.Validators.ClienteValidators;

namespace Odonto.BL.DependencyInjection;

public static class BusinessLogicExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<IClienteService, IClienteService>();
        
        services.AddScoped<IValidator<CriarClienteRequest>, CriarClienteRequestValidator>();
        services.AddScoped<IValidator<AtualizarClienteRequest>, AtualizarClienteRequestValidator>();

        return services;
    }
}