using Microsoft.Extensions.DependencyInjection;

using Moq;

using RotaViagem.AppConsole.Interfaces;
using RotaViagem.AppConsole.Services;

namespace RotaViagem.AppConsole.Tests.Services;

public class MenuServiceTests : IntegrationTestBase
{
    private readonly MenuService _menuService;
    private readonly Mock<IConsoleService> _consoleService;


    public MenuServiceTests()
    {
        _menuService = ServiceProvider.GetRequiredService<MenuService>();
        var console = ServiceProvider.GetRequiredService<IConsoleService>();
        _consoleService = Mock.Get(console);
    }

    [Fact]
    public void ProcessarOpcao_OpcaoInvalida_DeveLancarException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _menuService.ProcessarOpcao("4"));
        Assert.Throws<ArgumentException>(() => _menuService.ProcessarOpcao(null));
    }

    [Fact]
    public void ExibirMenu_DeveExibirMenu()
    {
        // Arrange & Act 
        _menuService.ExibirMenu();

        // Assert
        foreach (var option in _menuService.Options)
        {
            _consoleService.Verify(x => x.WriteLine($"{option.Key} - {option.Value.Description}"), Times.Once);
        }

        _consoleService.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Exactly(_menuService.Options.Count + 1));
    }

    [Fact]
    public void ProcessarOpcao_OpcaoValida_DeveRetornarFalse()
    {
        // Arrange & Act & Assert
        Assert.False(_menuService.ProcessarOpcao("3"));
    }

}