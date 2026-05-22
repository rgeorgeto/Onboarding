using MediatR;
using Application.Common;
using Application.Modules.Queries.ObterProgresso;

namespace Application.Modules.Commands.ConcluirModulo;

public record ConcluirModuloCommand(
    Guid ProfissionalId,
    Guid ModuloId
) : IRequest<Result<ProgressoDto>>;