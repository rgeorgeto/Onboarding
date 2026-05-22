namespace Application.Modules.Queries.ObterModulos;

public record ModuloDto(
    Guid Id,
    string Nome,
    string Descricao,
    string Tipo,
    int Ordem,
    int? DiasParaLiberar,
    int? PrazoEmDias,
    string? Icone,
    string? Cor,
    bool PossuiForm,
    string? FormUrl
);