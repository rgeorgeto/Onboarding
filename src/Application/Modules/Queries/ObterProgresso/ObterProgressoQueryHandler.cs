using AutoMapper;
using MediatR;
using Domain.Interfaces;
using Domain.Enums;
using Application.Common;
using Application.Modules.Queries.ObterConquistas;

namespace Application.Modules.Queries.ObterProgresso;

public class ObterProgressoQueryHandler : IRequestHandler<ObterProgressoQuery, Result<ProgressoDto>>
{
    private readonly IProgressoRepository _progressoRepository;
    private readonly IModuloRepository _moduloRepository;
    private readonly IConquistaRepository _conquistaRepository;
    private readonly IMapper _mapper;

    public ObterProgressoQueryHandler(
        IProgressoRepository progressoRepository,
        IModuloRepository moduloRepository,
        IConquistaRepository conquistaRepository,
        IMapper mapper)
    {
        _progressoRepository = progressoRepository;
        _moduloRepository = moduloRepository;
        _conquistaRepository = conquistaRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProgressoDto>> Handle(ObterProgressoQuery request, CancellationToken ct)
    {
        var progresso = await _progressoRepository.ObterPorProfissionalAsync(request.ProfissionalId, ct);
        if (progresso is null)
            return Result<ProgressoDto>.Failure("Profissional não encontrado ou onboarding não iniciado.");

        var todosModulos = await _moduloRepository.ObterTodosAtivosAsync(ct);
        var todasConquistas = await _conquistaRepository.ObterTodasAsync(ct);

        var modulosDto = progresso.Modulos.Select(pm =>
        {
            var modulo = todosModulos.First(m => m.Id == pm.ModuloId);
            return new ModuloProgressoDto(
                modulo.Id, modulo.Nome, modulo.Descricao, modulo.Tipo.ToString(), modulo.Ordem,
                modulo.DiasParaLiberar, modulo.PrazoEmDias, modulo.Icone, modulo.Cor,
                modulo.PossuiForm, modulo.FormUrl,
                pm.Status.ToString(), pm.DataConclusao);
        }).OrderBy(m => m.Ordem).ToList();

        var conquistasDto = todasConquistas.Select(c => new ConquistaDto(
            c.Id, c.Nome, c.Descricao, c.Icone, c.Ordem,
            progresso.ConquistasDesbloqueadas.Contains(c.Id)
        )).OrderBy(c => c.Ordem).ToList();

        int totalModulos = todosModulos.Count(m => m.Ativo);
        int concluidos = progresso.Modulos.Count(m => m.Status == StatusModulo.Concluido);
        double percentual = totalModulos > 0 ? Math.Round((double)concluidos / totalModulos * 100, 1) : 0;

        var result = new ProgressoDto(
            progresso.Id, progresso.ProfissionalId, progresso.DataAdmissao,
            modulosDto, conquistasDto, concluidos, totalModulos, percentual);

        return Result<ProgressoDto>.Success(result);
    }
}

