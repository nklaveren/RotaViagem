using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RotaViagem.AppConsole.Extensions;
using RotaViagem.AppConsole.Services;

var builder = Host.CreateApplicationBuilder(args);

// Configuração dos serviços
builder.Services.AddApplicationServices();

var host = builder.Build();

// Execução do menu
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