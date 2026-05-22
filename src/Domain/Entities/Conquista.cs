namespace Domain.Entities;

public class Conquista
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Icone { get; private set; }
    public int Ordem { get; private set; }
    public string Regra { get; private set; }

    private Conquista() { }

    public Conquista(
        string nome,
        string descricao,
        string icone,
        int ordem,
        string regra)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Icone = icone;
        Ordem = ordem;
        Regra = regra;
    }
}