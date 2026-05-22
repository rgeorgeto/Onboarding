// Query
using MediatR;
using Application.Common;

namespace Application.Modules.Queries.ObterProgresso;

public record ObterProgressoQuery(Guid ProfissionalId) : IRequest<Result<ProgressoDto>>;