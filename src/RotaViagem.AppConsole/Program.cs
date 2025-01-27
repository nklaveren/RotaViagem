using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RotaViagem.AppConsole.Extensions;
using RotaViagem.AppConsole.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

var host = builder.Build();

var rotasInicializador = host.Services.GetRequiredService<RotasInicializadorService>();
rotasInicializador.InicializarRotasPadrao();

var menuService = host.Services.GetRequiredService<MenuService>();

while (true)
{
    try
    {
        menuService.ExibirMenu();
        var opcao = Console.ReadLine();

        if (menuService.ProcessarOpcao(opcao))
            break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro: {ex.Message}");
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}