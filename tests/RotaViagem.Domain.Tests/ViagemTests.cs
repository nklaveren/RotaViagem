namespace RotaViagem.Domain.Tests;

public class ViagemTests
{
    private readonly Entities.Viagem _viagem;

    public ViagemTests()
    {
        _viagem = new Entities.Viagem();
        // Configurando as rotas do exemplo
        _viagem.AdicionarRota("GRU", "BRC", 10);
        _viagem.AdicionarRota("BRC", "SCL", 5);
        _viagem.AdicionarRota("GRU", "CDG", 75);
        _viagem.AdicionarRota("GRU", "SCL", 20);
        _viagem.AdicionarRota("GRU", "ORL", 56);
        _viagem.AdicionarRota("ORL", "CDG", 5);
        _viagem.AdicionarRota("SCL", "ORL", 20);
    }

    [Fact(DisplayName = "Adicionar Rota com dados válidos deve adicionar Rota com sucesso e retornar a rota adicionada como valor")]
    public void AdicionarRota_DeveAdicionarRotaComSucesso()
    {
        // Arrange
        var viagem = new Entities.Viagem();

        // Act
        viagem.AdicionarRota("GRU", "BRC", 10);

        // Assert
        Assert.Single(viagem.Rotas);
        var rota = viagem.Rotas.First();
        Assert.Equal("GRU", rota.Origem);
        Assert.Equal("BRC", rota.Destino);
        Assert.Equal(10, rota.Valor);
    }

    [Fact(DisplayName = "Encontrar Melhor Rota deve retornar melhor rota e custo")]
    public void EncontrarMelhorRota_GRUparaCDG_DeveRetornarMelhorRotaECusto()
    {
        // Arrange & Act
        (List<string> caminho, decimal custoTotal) resultado = _viagem.EncontrarMelhorRota("GRU", "CDG");

        // Assert
        Assert.Equal(40, resultado.custoTotal);

        var expected = new[] { "GRU", "BRC", "SCL", "ORL", "CDG" };
        Assert.Equal(expected, resultado.caminho);
    }

    [Fact(DisplayName = "Encontrar Melhor Rota e exibir o caminho completo BRC -> SCL")]
    public void EncontrarMelhorRota_BRCparaSCL_DeveRetornarRotaDireta()
    {
        // Arrange & Act
        (var caminho, decimal custoTotal) = _viagem.EncontrarMelhorRota("BRC", "SCL");

        // Assert
        Assert.Equal(5, custoTotal);

        var expected = new[] { "BRC", "SCL" };
        Assert.Equal(expected, caminho);
    }

    [Fact(DisplayName = "Encontrar Melhor Rota deve retornar caminho vazio e custo máximo quando a rota não existe")]
    public void EncontrarMelhorRota_RotaInexistente_DeveRetornarCaminhoVazioECustoMaximo()
    {
        // Arrange & Act
        (List<string> caminho, decimal custoTotal) resultado = _viagem.EncontrarMelhorRota("GRU", "XXX");

        // Assert
        Assert.Empty(resultado.caminho);
        Assert.Equal(decimal.MaxValue, resultado.custoTotal);
    }

    [Theory(DisplayName = "Encontrar Melhor Rota deve retornar o custo total da melhor rota")]
    [InlineData("GRU", "CDG", 40)]  // GRU -> BRC -> SCL -> ORL -> CDG
    [InlineData("GRU", "SCL", 15)]  // GRU -> BRC -> SCL
    [InlineData("SCL", "CDG", 25)]  // SCL -> ORL -> CDG
    [InlineData("GRU", "ORL", 35)]  // GRU -> BRC -> SCL -> ORL
    public void EncontrarMelhorRota_DeveEncontrarMelhorCusto(string origem, string destino, decimal custoEsperado)
    {
        // Arrange & Act
        (List<string> _, decimal custoTotal) resultado = _viagem.EncontrarMelhorRota(origem, destino);

        // Assert
        Assert.Equal(custoEsperado, resultado.custoTotal);
    }

    [Fact(DisplayName = "Encontrar Melhor Rota deve retornar mehor rota com conexões GRU -> BRC -> SCL -> ORL")]
    public void EncontrarMelhorRota_GRUparaORL_DeveRetornarMelhorRotaViaConexoes()
    {
        // Arrange & Act
        (List<string> caminho, decimal custoTotal) resultado = _viagem.EncontrarMelhorRota("GRU", "ORL");

        // Assert
        Assert.Equal(35, resultado.custoTotal);
        var expected = new[] { "GRU", "BRC", "SCL", "ORL" };
        Assert.Equal(expected, resultado.caminho);
    }

    [Fact(DisplayName = "Encontrar Melhor Rota Com Cases Diferentes deve retornar melhor rota com conexões")]
    public void EncontrarMelhorRota_RotaComCasesDiferentes_DeveEncontrarRota()
    {
        // Arrange & Act
        (List<string> caminho, decimal custoTotal) resultado = _viagem.EncontrarMelhorRota("gru", "cdg");

        // Assert
        Assert.Equal(40, resultado.custoTotal);
        var expected = new[] { "GRU", "BRC", "SCL", "ORL", "CDG" };
        Assert.Equal(expected, resultado.caminho);
    }
}