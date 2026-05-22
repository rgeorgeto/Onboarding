using MediatR;
using Application.Common;

namespace Application.Modules.Queries.ObterModulos;

public record ObterModulosQuery : IRequest<Result<List<ModuloDto>>>;