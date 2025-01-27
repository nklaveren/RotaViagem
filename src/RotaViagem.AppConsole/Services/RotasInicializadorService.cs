using RotaViagem.Domain.Entities;

namespace RotaViagem.AppConsole.Services;

public class RotasInicializadorService
{
    private readonly Viagem _viagem;

    public RotasInicializadorService(Viagem viagem)
    {
        _viagem = viagem;
    }

    public void InicializarRotasPadrao()
    {
        var rotas = new (string origem, string destino, decimal valor)[]
        {
            ("GRU", "BRC", 10),
            ("BRC", "SCL", 5),
            ("GRU", "CDG", 75),
            ("GRU", "SCL", 20),
            ("GRU", "ORL", 56),
            ("ORL", "CDG", 5),
            ("SCL", "ORL", 20)
        };

        foreach (var (origem, destino, valor) in rotas)
        {
            _viagem.AdicionarRota(origem, destino, valor);
        }
    }
}