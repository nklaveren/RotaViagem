using RotaViagem.Domain.ValueObjects;

namespace RotaViagem.Domain.Tests;

public class RotaTests
{
    [Fact(DisplayName = "Criar Rota com dados válidos deve criar Rota com sucesso")]
    public void CriarRota_ComDadosValidos_DeveCriarRotaComSucesso()
    {
        // Arrange & Act
        var rota = new Rota("GRU", "BRC", 10);

        // Assert
        Assert.Equal("GRU", rota.Origem);
        Assert.Equal("BRC", rota.Destino);
        Assert.Equal(10, rota.Valor);
    }

    [Theory(DisplayName = "Criar Rota com dados inválidos deve lançar ArgumentException")]
    [InlineData("", "BRC", 10, "origem")]
    [InlineData("GRU", "", 10, "destino")]
    [InlineData("GRU", "BRC", 0, "valor")]
    [InlineData("GRU", "BRC", -1, "valor")]
    public void CriarRota_ComDadosInvalidos_DeveLancarException(string origem, string destino, decimal valor, string parametroInvalido)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Rota(origem, destino, valor));
        Assert.Contains(parametroInvalido, exception.ParamName!, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Criar Rota com dados válidos deve converter para maiúsculo")]
    public void CriarRota_DeveConverterParaMaiusculo()
    {
        // Arrange & Act
        var rota = new Rota("gru", "brc", 10);

        // Assert
        Assert.Equal("GRU", rota.Origem);
        Assert.Equal("BRC", rota.Destino);
    }
}