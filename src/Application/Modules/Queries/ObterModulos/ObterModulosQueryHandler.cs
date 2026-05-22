using AutoMapper;
using MediatR;
using Domain.Interfaces;
using Application.Common;

namespace Application.Modules.Queries.ObterModulos;

public class ObterModulosQueryHandler : IRequestHandler<ObterModulosQuery, Result<List<ModuloDto>>>
{
    private readonly IModuloRepository _moduloRepository;
    private readonly IMapper _mapper;

    public ObterModulosQueryHandler(IModuloRepository moduloRepository, IMapper mapper)
    {
        _moduloRepository = moduloRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ModuloDto>>> Handle(ObterModulosQuery request, CancellationToken ct)
    {
        var modulos = await _moduloRepository.ObterTodosAtivosAsync(ct);
        var dto = _mapper.Map<List<ModuloDto>>(modulos);
        return Result<List<ModuloDto>>.Success(dto);
    }
}