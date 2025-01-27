using Microsoft.Extensions.DependencyInjection;

using RotaViagem.AppConsole.Interfaces;
using RotaViagem.AppConsole.Options;
using RotaViagem.AppConsole.Services;
using RotaViagem.Domain.Entities;

namespace RotaViagem.AppConsole.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<Viagem>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<RotasInicializadorService>();

        // Registra as opções tanto como interface quanto como implementação concreta
        services.AddSingleton<CarregarRotasOption>();
        services.AddSingleton<ConsultarRotaOption>();
        services.AddSingleton<SairOption>();

        services.AddSingleton<IMenuOption>(sp => sp.GetRequiredService<CarregarRotasOption>());
        services.AddSingleton<IMenuOption>(sp => sp.GetRequiredService<ConsultarRotaOption>());
        services.AddSingleton<IMenuOption>(sp => sp.GetRequiredService<SairOption>());

        services.AddSingleton(serviceProvider =>
        {
            var options = new Dictionary<string, IMenuOption>
            {
                { "1", serviceProvider.GetRequiredService<CarregarRotasOption>() },
                { "2", serviceProvider.GetRequiredService<ConsultarRotaOption>() },
                { "3", serviceProvider.GetRequiredService<SairOption>() }
            };
            return new MenuService(options, serviceProvider.GetRequiredService<IConsoleService>());
        });

        return services;
    }
}