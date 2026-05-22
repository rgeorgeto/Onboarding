using AutoMapper;
using MediatR;
using Domain.Interfaces;
using Application.Common;

namespace Application.Modules.Queries.ObterConquistas;

public class ObterConquistasQueryHandler : IRequestHandler<ObterConquistasQuery, Result<List<ConquistaDto>>>
{
    private readonly IConquistaRepository _conquistaRepository;
    private readonly IProgressoRepository _progressoRepository;
    private readonly IMapper _mapper;

    public ObterConquistasQueryHandler(
        IConquistaRepository conquistaRepository,
        IProgressoRepository progressoRepository,
        IMapper mapper)
    {
        _conquistaRepository = conquistaRepository;
        _progressoRepository = progressoRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ConquistaDto>>> Handle(ObterConquistasQuery request, CancellationToken ct)
    {
        var conquistas = await _conquistaRepository.ObterTodasAsync(ct);
        var desbloqueadas = new HashSet<Guid>();

        if (request.ProfissionalId.HasValue)
        {
            var progresso = await _progressoRepository.ObterPorProfissionalAsync(request.ProfissionalId.Value, ct);
            if (progresso is not null)
                desbloqueadas = progresso.ConquistasDesbloqueadas.ToHashSet();
        }

        var dto = conquistas.Select(c => new ConquistaDto(
            c.Id, c.Nome, c.Descricao, c.Icone, c.Ordem,
            desbloqueadas.Contains(c.Id)
        )).ToList();

        return Result<List<ConquistaDto>>.Success(dto);
    }
}