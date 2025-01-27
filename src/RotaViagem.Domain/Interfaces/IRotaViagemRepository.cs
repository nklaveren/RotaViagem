using RotaViagem.Domain.ValueObjects;

namespace RotaViagem.Domain.Interfaces;

public interface IViagemRepository
{
    void AdicionarRota(Rota rota);
    IEnumerable<Rota> ObterTodasRotas();
}