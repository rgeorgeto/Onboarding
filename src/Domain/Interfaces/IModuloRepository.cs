using Domain.Entities;

namespace Domain.Interfaces;

public interface IModuloRepository
{
    Task<Modulo?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Modulo>> ObterTodosAtivosAsync(CancellationToken ct = default);
}