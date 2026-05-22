using Application.Modules.Queries.ObterConquistas;

namespace Application.Modules.Queries.ObterProgresso;

public record ProgressoDto(
    Guid Id,
    Guid ProfissionalId,
    DateTime DataAdmissao,
    List<ModuloProgressoDto> Modulos,
    List<ConquistaDto> Conquistas,
    int ModulosConcluidos,
    int TotalModulos,
    double PercentualConclusao
);

public record ModuloProgressoDto(
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
    string? FormUrl,
    string Status,
    DateTime? DataConclusao
);
