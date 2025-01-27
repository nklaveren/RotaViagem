using System.Diagnostics.CodeAnalysis;

using RotaViagem.AppConsole.Interfaces;

namespace RotaViagem.AppConsole.Services;

[ExcludeFromCodeCoverage]
public class ConsoleService : IConsoleService
{
    public void Clear() => Console.Clear();
    public void WriteLine(string? message) => Console.WriteLine(message);
    public void Write(string? message) => Console.Write(message);
    public string? ReadLine() => Console.ReadLine();
    public void SetIn(TextReader reader) => Console.SetIn(reader);
    public void SetOut(TextWriter writer) => Console.SetOut(writer);
}