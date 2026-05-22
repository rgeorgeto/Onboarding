using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ConquistaRepository : IConquistaRepository
{
    private readonly AppDbContext _context;

    public ConquistaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Conquista>> ObterTodasAsync(CancellationToken ct = default)
    {
        return await _context.Conquistas
            .OrderBy(c => c.Ordem)
            .ToListAsync(ct);
    }
}