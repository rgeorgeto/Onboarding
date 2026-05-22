namespace Domain.Entities;

public class Modulo
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Enums.TipoModulo Tipo { get; private set; }
    public int Ordem { get; private set; }
    public int? DiasParaLiberar { get; private set; }
    public int? PrazoEmDias { get; private set; }
    public string? Icone { get; private set; }
    public string? Cor { get; private set; }
    public bool PossuiForm { get; private set; }
    public string? FormUrl { get; private set; }
    public bool Ativo { get; private set; }

    private Modulo() { }

    public Modulo(
        string nome,
        string descricao,
        Enums.TipoModulo tipo,
        int ordem,
        int? diasParaLiberar,
        int? prazoEmDias,
        string? icone,
        string? cor,
        bool possuiForm,
        string? formUrl)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Tipo = tipo;
        Ordem = ordem;
        DiasParaLiberar = diasParaLiberar;
        PrazoEmDias = prazoEmDias;
        Icone = icone;
        Cor = cor;
        PossuiForm = possuiForm;
        FormUrl = formUrl;
        Ativo = true;
    }

    public bool EhInformativo => Tipo == Enums.TipoModulo.Informativo;
    public bool PossuiPrazo => PrazoEmDias.HasValue;
}