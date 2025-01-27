using System.Diagnostics.CodeAnalysis;

using RotaViagem.AppConsole.Interfaces;

namespace RotaViagem.AppConsole.Options;

[ExcludeFromCodeCoverage]
public class SairOption : IMenuOption
{
    private readonly IConsoleService _console;

    public string Description => "Sair";

    public SairOption(IConsoleService console)
    {
        _console = console;
    }

    public void Execute()
    {
        _console.Clear();
        _console.WriteLine("Saindo do sistema...");
    }
}