using Moq;

using RotaViagem.AppConsole.Interfaces;
using RotaViagem.AppConsole.Options;

namespace RotaViagem.AppConsole.Tests.Options;

public class SairOptionTests : IntegrationTestBase
{
    private readonly Mock<IConsoleService> _consoleMock;
    private readonly SairOption _sairOption;

    public SairOptionTests()
    {
        _consoleMock = new Mock<IConsoleService>();
        _sairOption = new SairOption(_consoleMock.Object);
    }

    [Fact(DisplayName = "SairOption - Deve exibir mensagem de saída")]
    public void Execute_DeveExibirMensagemDeSair()
    {
        // Act
        _sairOption.Execute();

        // Assert
        _consoleMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains("Saindo do sistema..."))), Times.Once);
    }

}
