using MediatR;
using Application.Common;

namespace Application.Modules.Queries.ObterConquistas;

public record ObterConquistasQuery(Guid? ProfissionalId = null) : IRequest<Result<List<ConquistaDto>>>;