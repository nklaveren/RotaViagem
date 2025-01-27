using Microsoft.Extensions.DependencyInjection;

using Moq;

using RotaViagem.AppConsole.Interfaces;
using RotaViagem.AppConsole.Options;
using RotaViagem.Domain.Entities;

using Xunit;

namespace RotaViagem.AppConsole.Tests.Options;

public class ConsultarRotaOptionTests : IntegrationTestBase
{
    private readonly ConsultarRotaOption _consultarRotaOption;
    private readonly Viagem _viagem;
    private readonly Mock<IConsoleService> _consoleMock;

    public ConsultarRotaOptionTests()
    {
        _viagem = ServiceProvider.GetRequiredService<Viagem>();
        _consoleMock = new Mock<IConsoleService>();
        _consultarRotaOption = new ConsultarRotaOption(_viagem, _consoleMock.Object);

        // Setup inicial de rotas
        _viagem.AdicionarRota("GRU", "BRC", 10);
        _viagem.AdicionarRota("BRC", "SCL", 5);
        _viagem.AdicionarRota("GRU", "CDG", 75);
    }

    [Fact(DisplayName = "ConsultarRotaOption - Deve exibir melhor rota")]
    public void Execute_ConsultaRotaValida_DeveExibirMelhorRota()
    {
        // Arrange
        _consoleMock.Setup(x => x.ReadLine()).Returns("GRU-CDG");

        // Act
        _consultarRotaOption.Execute();

        // Assert
        _consoleMock.Verify(x => x.WriteLine(It.Is<string>(s =>
            s.Contains("Melhor rota:") && s.Contains("GRU - CDG ao custo de $75"))), Times.Once);
    }

    [Theory(DisplayName = "ConsultarRotaOption - Deve exibir mensagem de erro ao carregar arquivo inválido")]
    [InlineData("")]
    [InlineData("GRU")]
    [InlineData("GRU-")]
    [InlineData("-CDG")]
    public void Execute_EntradaInvalida_DeveLancarException(string entradaInvalida)
    {
        // Arrange
        _consoleMock.Setup(x => x.ReadLine())
            .Returns(entradaInvalida);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var (_, _) = _consultarRotaOption.ProcessarEntradaRota(entradaInvalida);
        });

        Assert.Contains(exception.Message, new[] {
            "Rota não pode ser vazia",
            "Formato inválido. Use: ORIGEM-DESTINO"
        });
    }

    [Fact(DisplayName = "ConsultarRotaOption - Deve processar entrada válida")]
    public void ProcessarEntradaRota_EntradaValida_DeveRetornarOrigemEDestino()
    {
        // Arrange
        var entrada = "GRU-CDG";

        // Act
        var (origem, destino) = _consultarRotaOption.ProcessarEntradaRota(entrada);

        // Assert
        Assert.Equal("GRU", origem);
        Assert.Equal("CDG", destino);
    }
}