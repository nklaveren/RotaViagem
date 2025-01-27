namespace RotaViagem.AppConsole.Interfaces;

public interface IConsoleService
{
    void Clear();
    void WriteLine(string? message);
    void Write(string? message);
    string? ReadLine();
    void SetIn(TextReader reader);
    void SetOut(TextWriter writer);
}