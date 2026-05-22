using AutoMapper;
using MediatR;
using Domain.Aggregates;
using Domain.Interfaces;
using Application.Common;
using Application.Modules.Queries.ObterProgresso;

namespace Application.Modules.Commands.IniciarOnboarding;

public class IniciarOnboardingCommandHandler : IRequestHandler<IniciarOnboardingCommand, Result<ProgressoDto>>
{
    private readonly IProgressoRepository _progressoRepository;
    private readonly IModuloRepository _moduloRepository;
    private readonly IMapper _mapper;

    public IniciarOnboardingCommandHandler(
        IProgressoRepository progressoRepository,
        IModuloRepository moduloRepository,
        IMapper mapper)
    {
        _progressoRepository = progressoRepository;
        _moduloRepository = moduloRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProgressoDto>> Handle(IniciarOnboardingCommand request, CancellationToken ct)
    {
        var existente = await _progressoRepository.ObterPorProfissionalAsync(request.ProfissionalId, ct);
        if (existente is not null)
            return Result<ProgressoDto>.Failure("Profissional já possui onboarding em andamento.");

        var modulos = await _moduloRepository.ObterTodosAtivosAsync(ct);
        if (modulos.Count == 0)
            return Result<ProgressoDto>.Failure("Nenhum módulo de onboarding cadastrado.");

        var progresso = new ProgressoProfissional(request.ProfissionalId, request.DataAdmissao, modulos);
        await _progressoRepository.SalvarAsync(progresso, ct);

        var dto = _mapper.Map<ProgressoDto>(progresso, opt =>
            opt.Items["ModulosOriginais"] = modulos);

        return Result<ProgressoDto>.Success(dto);
    }
}