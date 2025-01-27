using RotaViagem.Domain.ValueObjects;

namespace RotaViagem.Domain.Entities;

public class Viagem
{
    private readonly List<Rota> _rotas = [];

    public IReadOnlyCollection<Rota> Rotas => _rotas.AsReadOnly();

    public void AdicionarRota(string origem, string destino, decimal valor)
    {
        var rota = new Rota(origem, destino, valor);

        _rotas.Add(rota);
    }

    public (List<string> caminho, decimal custoTotal) EncontrarMelhorRota(string origem, string destino)
    {
        var visitados = new HashSet<string>();
        var melhorCaminho = new List<string>();
        var melhorCusto = decimal.MaxValue;
        EncontrarCaminho(origem.ToUpper(), destino.ToUpper(), [origem.ToUpper()], 0, visitados, ref melhorCaminho, ref melhorCusto);

        return (melhorCaminho, melhorCusto);
    }

    private void EncontrarCaminho(
        string atual,
        string destino,
        List<string> caminhoAtual,
        decimal custoAtual,
        HashSet<string> visitados,
        ref List<string> melhorCaminho,
        ref decimal melhorCusto)
    {
        if (atual == destino)
        {
            if (custoAtual < melhorCusto)
            {
                melhorCusto = custoAtual;
                melhorCaminho = [.. caminhoAtual];
            }
            return;
        }

        visitados.Add(atual);

        var rotasDisponiveis = _rotas.Where(r => r.Origem == atual && !visitados.Contains(r.Destino));

        foreach (var rota in rotasDisponiveis)
        {
            caminhoAtual.Add(rota.Destino);

            EncontrarCaminho(
                rota.Destino,
                destino,
                caminhoAtual,
                custoAtual + rota.Valor,  // Acumula o custo para melhor caminho
                visitados,
                ref melhorCaminho,
                ref melhorCusto
            );

            caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
        }

        visitados.Remove(atual);
    }
}