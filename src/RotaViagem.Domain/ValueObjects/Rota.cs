namespace RotaViagem.Domain.ValueObjects;

public class Rota
{
    public string Origem { get; private set; }
    public string Destino { get; private set; }
    public decimal Valor { get; private set; }

    public Rota(string origem, string destino, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(origem))
            throw new ArgumentException("Origem não pode ser vazia", nameof(origem));

        if (string.IsNullOrWhiteSpace(destino))
            throw new ArgumentException("Destino não pode ser vazia", nameof(destino));

        if (valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero", nameof(valor));

        Origem = origem.ToUpper();
        Destino = destino.ToUpper();
        Valor = valor;
    }
}