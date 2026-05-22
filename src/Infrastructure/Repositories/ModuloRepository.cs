using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ModuloRepository : IModuloRepository
{
    private readonly AppDbContext _context;

    public ModuloRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Modulo>> ObterTodosAtivosAsync(CancellationToken ct = default)
    {
        return await _context.Modulos
            .Where(m => m.Ativo)
            .OrderBy(m => m.Ordem)
            .ToListAsync(ct);
    }

    public async Task<Modulo?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Modulos
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }
}