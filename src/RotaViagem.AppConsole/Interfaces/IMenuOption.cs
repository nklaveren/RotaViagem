namespace RotaViagem.AppConsole.Interfaces;

public interface IMenuOption
{
    string Description { get; }
    void Execute();
}