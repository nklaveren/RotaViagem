using Microsoft.Extensions.DependencyInjection;

using RotaViagem.AppConsole.Services;
using RotaViagem.Domain.Entities;

namespace RotaViagem.AppConsole.Tests;

public class MenuServiceIntegrationTests : IntegrationTestBase
{
    private readonly MenuService _menuService;
    private readonly Viagem _viagem;

    public MenuServiceIntegrationTests()
    {
        _menuService = ServiceProvider.GetRequiredService<MenuService>();
        _viagem = ServiceProvider.GetRequiredService<Viagem>();
    }

    [Fact]
    public void ProcessarOpcao_OpcaoInvalida_DeveLancarException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _menuService.ProcessarOpcao("4"));
    }

    [Fact]
    public void ProcessarOpcao_OpcaoSair_DeveRetornarTrue()
    {
        // Arrange & Act
        var resultado = _menuService.ProcessarOpcao("3");

        // Assert
        Assert.True(resultado);
    }
}