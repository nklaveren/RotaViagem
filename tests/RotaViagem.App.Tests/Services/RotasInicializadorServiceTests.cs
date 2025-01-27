using Microsoft.Extensions.DependencyInjection;
using RotaViagem.AppConsole.Services;
using RotaViagem.Domain.Entities;
using Xunit;

namespace RotaViagem.App.Tests.Services;

public class RotasInicializadorServiceTests : IntegrationTestBase
{
    private readonly RotasInicializadorService _rotasInicializador;
    private readonly Viagem _viagem;

    public RotasInicializadorServiceTests()
    {
        _viagem = ServiceProvider.GetRequiredService<Viagem>();
        _rotasInicializador = ServiceProvider.GetRequiredService<RotasInicializadorService>();
    }

    [Fact]
    public void InicializarRotasPadrao_DeveAdicionarTodasAsRotasIniciais()
    {
        // Act
        _rotasInicializador.InicializarRotasPadrao();

        // Assert
        Assert.Equal(7, _viagem.Rotas.Count); // Verifica se todas as 7 rotas foram adicionadas

        // Verifica algumas rotas específicas
        var rotas = _viagem.Rotas.ToList();
        Assert.Contains(rotas, r => r.Origem == "GRU" && r.Destino == "BRC" && r.Valor == 10);
        Assert.Contains(rotas, r => r.Origem == "BRC" && r.Destino == "SCL" && r.Valor == 5);
        Assert.Contains(rotas, r => r.Origem == "GRU" && r.Destino == "CDG" && r.Valor == 75);
    }

    [Fact]
    public void InicializarRotasPadrao_DevePermitirConsultaDeMelhorRota()
    {
        // Arrange
        _rotasInicializador.InicializarRotasPadrao();

        // Act
        var (caminho, custoTotal) = _viagem.EncontrarMelhorRota("GRU", "CDG");

        // Assert
        Assert.Equal(40, custoTotal);
        Assert.Equal(new[] { "GRU", "BRC", "SCL", "ORL", "CDG" }, caminho);
    }
}