using RotaViagem.AppConsole.Interfaces;
using RotaViagem.Domain.Entities;

namespace RotaViagem.AppConsole.Options;

public class ConsultarRotaOption : IMenuOption
{
    private readonly Viagem _viagem;
    private readonly IConsoleService _console;
    private const string SEPARADOR_ROTA = "-";

    public string Description => "Consultar melhor rota";

    public ConsultarRotaOption(Viagem viagem, IConsoleService console)
    {
        _viagem = viagem;
        _console = console;
    }

    public void Execute()
    {
        _console.Clear();
        _console.WriteLine("Consultar Melhor Rota");
        _console.Write("\nInforme a rota (Origem-Destino): ");

        var entrada = _console.ReadLine();
        var (origem, destino) = ProcessarEntradaRota(entrada);
        ExibirResultadoConsulta(origem, destino);
    }

    public (string origem, string destino) ProcessarEntradaRota(string? entrada)
    {
        if (string.IsNullOrWhiteSpace(entrada))
            throw new ArgumentException("Rota não pode ser vazia");

        var partes = entrada.Split(SEPARADOR_ROTA);
        if (partes.Length != 2)
            throw new ArgumentException("Formato inválido. Use: ORIGEM-DESTINO");

        var origem = partes[0].Trim().ToUpper();
        var destino = partes[1].Trim().ToUpper();
        if (string.IsNullOrWhiteSpace(origem) || string.IsNullOrWhiteSpace(destino))
            throw new ArgumentException("Formato inválido. Use: ORIGEM-DESTINO");

        return (origem, destino);
    }

    private void ExibirResultadoConsulta(string origem, string destino)
    {
        var (caminho, custoTotal) = _viagem.EncontrarMelhorRota(origem, destino);

        if (RotaNaoEncontrada(caminho, custoTotal))
        {
            _console.WriteLine("\nRota não encontrada!");
            return;
        }

        _console.WriteLine($"\nMelhor rota: {string.Join(" - ", caminho)} ao custo de ${custoTotal}");
    }

    private static bool RotaNaoEncontrada(List<string> caminho, decimal custoTotal)
        => caminho.Count == 0 || custoTotal == decimal.MaxValue;
}