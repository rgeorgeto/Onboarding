using AutoMapper;
using MediatR;
using Domain.Interfaces;
using Application.Common;
using Application.Modules.Queries.ObterProgresso;

namespace Application.Modules.Commands.ConcluirModulo;

public class ConcluirModuloCommandHandler : IRequestHandler<ConcluirModuloCommand, Result<ProgressoDto>>
{
    private readonly IProgressoRepository _progressoRepository;
    private readonly IModuloRepository _moduloRepository;
    private readonly IConquistaRepository _conquistaRepository;
    private readonly IMapper _mapper;

    public ConcluirModuloCommandHandler(
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

    public async Task<Result<ProgressoDto>> Handle(ConcluirModuloCommand request, CancellationToken ct)
    {
        var progresso = await _progressoRepository.ObterPorProfissionalAsync(request.ProfissionalId, ct);
        if (progresso is null)
            return Result<ProgressoDto>.Failure("Profissional não encontrado ou onboarding não iniciado.");

        var modulo = await _moduloRepository.ObterPorIdAsync(request.ModuloId, ct);
        if (modulo is null)
            return Result<ProgressoDto>.Failure("Módulo não encontrado.");

        var todosModulos = await _moduloRepository.ObterTodosAtivosAsync(ct);
        var conquistas = await _conquistaRepository.ObterTodasAsync(ct);

        var (sucesso, erro, _) = progresso.ConcluirModulo(request.ModuloId, modulo, todosModulos, conquistas);
        if (!sucesso)
            return Result<ProgressoDto>.Failure(erro!);

        await _progressoRepository.SalvarAsync(progresso, ct);

        var dto = _mapper.Map<ProgressoDto>(progresso, opt =>
            opt.Items["ModulosOriginais"] = todosModulos);

        return Result<ProgressoDto>.Success(dto);
    }
}