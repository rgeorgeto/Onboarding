using Domain.Entities;

namespace Domain.Interfaces;

public interface IConquistaRepository
{
    Task<List<Conquista>> ObterTodasAsync(CancellationToken ct = default);
}