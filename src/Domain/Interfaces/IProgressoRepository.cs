using Domain.Aggregates;

namespace Domain.Interfaces;

public interface IProgressoRepository
{
    Task<ProgressoProfissional?> ObterPorProfissionalAsync(Guid profissionalId, CancellationToken ct = default);
    Task<List<ProgressoProfissional>> ObterPorDiasDeCasaAsync(int dias, CancellationToken ct = default);
    Task SalvarAsync(ProgressoProfissional progresso, CancellationToken ct = default);
}