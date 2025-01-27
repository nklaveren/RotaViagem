using System.Collections.ObjectModel;

using RotaViagem.AppConsole.Interfaces;

namespace RotaViagem.AppConsole.Services;

public class MenuService
{
    private readonly Dictionary<string, IMenuOption> _options;

    public ReadOnlyDictionary<string, IMenuOption> Options => new(_options);
    private readonly IConsoleService _console;

    public MenuService(Dictionary<string, IMenuOption> options, IConsoleService console)
    {
        _options = options;
        _console = console;
    }

    public void ExibirMenu()
    {
        _console.Clear();
        _console.WriteLine("Sistema de Rotas de Viagem");

        foreach (var (key, option) in _options)
        {
            _console.WriteLine($"{key} - {option.Description}");
        }

        _console.Write("\nEscolha uma opção: ");
    }

    public bool ProcessarOpcao(string? opcao)
    {
        if (opcao == null || !_options.ContainsKey(opcao))
            throw new ArgumentException("Opção inválida!");
        _options[opcao].Execute();
        return false;
    }
}