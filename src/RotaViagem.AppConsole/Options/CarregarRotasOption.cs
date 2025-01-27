using RotaViagem.AppConsole.Interfaces;
using RotaViagem.Domain.Entities;

namespace RotaViagem.AppConsole.Options;

public class CarregarRotasOption : IMenuOption
{
    private readonly Viagem _viagem;
    private readonly IConsoleService _console;
    private const string SEPARADOR_ARQUIVO = ",";

    public string Description => "Carregar arquivo de rotas";

    public CarregarRotasOption(Viagem viagem, IConsoleService console)
    {
        _viagem = viagem;
        _console = console;
    }

    public void Execute()
    {
        _console.Clear();
        _console.WriteLine("Carregar Arquivo de Rotas");
        _console.Write("\nInforme o caminho do arquivo: ");
        var caminhoArquivo = _console.ReadLine();
        ValidarCaminhoArquivo(caminhoArquivo);

        var rotasCarregadas = ProcessarArquivoRotas(caminhoArquivo!);
        _console.WriteLine($"\n{rotasCarregadas} rota(s) carregada(s) com sucesso!");
    }

    private void ValidarCaminhoArquivo(string? caminhoArquivo)
    {
        if (string.IsNullOrWhiteSpace(caminhoArquivo))
            throw new ArgumentException("Caminho do arquivo não pode ser vazio");

        if (!File.Exists(caminhoArquivo))
            throw new FileNotFoundException("Arquivo não encontrado");
    }

    private int ProcessarArquivoRotas(string caminhoArquivo)
    {
        var linhas = File.ReadAllLines(caminhoArquivo);
        var rotasCarregadas = 0;

        foreach (var linha in linhas)
        {
            if (TentarProcessarLinha(linha))
                rotasCarregadas++;
        }

        return rotasCarregadas;
    }

    private bool TentarProcessarLinha(string linha)
    {
        if (string.IsNullOrWhiteSpace(linha))
            return false;

        var partes = linha.Split(SEPARADOR_ARQUIVO);
        if (partes.Length != 3)
            return false;

        if (!decimal.TryParse(partes[2], out var valor))
            return false;

        _viagem.AdicionarRota(partes[0], partes[1], valor);
        return true;
    }
}