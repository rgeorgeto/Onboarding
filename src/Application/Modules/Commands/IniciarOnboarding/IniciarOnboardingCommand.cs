using MediatR;
using Application.Common;
using Application.Modules.Queries.ObterProgresso;

namespace Application.Modules.Commands.IniciarOnboarding;

public record IniciarOnboardingCommand(
    Guid ProfissionalId,
    DateTime DataAdmissao
) : IRequest<Result<ProgressoDto>>;