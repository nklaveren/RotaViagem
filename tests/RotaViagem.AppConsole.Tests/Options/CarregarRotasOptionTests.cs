using Microsoft.Extensions.DependencyInjection;

using Moq;

using RotaViagem.AppConsole.Interfaces;
using RotaViagem.AppConsole.Options;
using RotaViagem.Domain.Entities;

using Xunit;

namespace RotaViagem.AppConsole.Tests.Options;

public class CarregarRotasOptionTests : IntegrationTestBase
{
    private readonly CarregarRotasOption _carregarRotasOption;
    private readonly Viagem _viagem;
    private readonly Mock<IConsoleService> _consoleMock;

    public CarregarRotasOptionTests()
    {
        _viagem = ServiceProvider.GetRequiredService<Viagem>();
        _consoleMock = new Mock<IConsoleService>();
        _carregarRotasOption = new CarregarRotasOption(_viagem, _consoleMock.Object);
    }

    [Fact(DisplayName = "CarregarRotasOption - Deve carregar rotas com sucesso")]
    public void Execute_ArquivoValido_DeveCarregarRotas()
    {
        // Arrange
        var caminhoArquivo = Path.GetTempFileName();
        File.WriteAllLines(caminhoArquivo, new[]
        {
            "GRU,BRC,10",
            "BRC,SCL,5",
            "GRU,CDG,75"
        });

        _consoleMock.Setup(x => x.ReadLine()).Returns(caminhoArquivo);

        // Act
        _carregarRotasOption.Execute();

        // Assert

        _consoleMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains("3 rota(s) carregada(s) com sucesso!"))), Times.Once);
        _consoleMock.Verify(x => x.Clear(), Times.Once);
        Assert.Equal(3, _viagem.Rotas.Count);

        // Cleanup
        File.Delete(caminhoArquivo);
    }

    [Fact(DisplayName = "CarregarRotasOption - Não importa rotas inválidas")]
    public void Execute_ArquivoInvalido_NaoImportaRotasInvalidas()
    {
        // Arrange
        var caminhoArquivo = Path.GetTempFileName();
        File.WriteAllLines(caminhoArquivo, ["GRU;BRC;10", "BRC;SCL;5", "GRU;CDG;75"]);

        _consoleMock.Setup(x => x.ReadLine()).Returns(caminhoArquivo);

        // Act
        _carregarRotasOption.Execute();

        // Assert
        _consoleMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains("0 rota(s) carregada(s) com sucesso!"))), Times.Once);
    }

    [Fact(DisplayName = "CarregarRotasOption - Deve exibir mensagem de erro ao carregar arquivo inválido")]
    public void Execute_ArquivoInvalido_DeveExibirMensagemDeErro()
    {
        // Arrange
        _consoleMock.Setup(x => x.ReadLine()).Returns(string.Empty);

        // Assert
        Assert.Throws<ArgumentException>(() => _carregarRotasOption.Execute());
    }
}