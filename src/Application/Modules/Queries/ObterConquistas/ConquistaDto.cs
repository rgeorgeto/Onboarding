namespace Application.Modules.Queries.ObterConquistas;

public record ConquistaDto(
    Guid Id,
    string Nome,
    string Descricao,
    string Icone,
    int Ordem,
    bool Desbloqueada
);
