using Microsoft.EntityFrameworkCore;
using Domain.Aggregates;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProgressoRepository : IProgressoRepository
{
    private readonly AppDbContext _context;

    public ProgressoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProgressoProfissional?> ObterPorProfissionalAsync(Guid profissionalId, CancellationToken ct = default)
    {
        return await _context.ProgressosProfissionais
            .Include(p => p.Modulos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.ProfissionalId == profissionalId, ct);
    }

    public async Task<List<ProgressoProfissional>> ObterPorDiasDeCasaAsync(int dias, CancellationToken ct = default)
    {
        var dataLimite = DateTime.UtcNow.AddDays(-dias);
        return await _context.ProgressosProfissionais
            .Include(p => p.Modulos)
            .AsSplitQuery()
            .Where(p => p.DataAdmissao <= dataLimite)
            .ToListAsync(ct);
    }

    public async Task SalvarAsync(ProgressoProfissional progresso, CancellationToken ct = default)
    {
        var existente = await _context.ProgressosProfissionais
            .Include(p => p.Modulos)
            .FirstOrDefaultAsync(p => p.Id == progresso.Id, ct);

        if (existente is not null)
        {
            await _context.Entry(existente)
                .Collection(p => p.Modulos)
                .LoadAsync(ct);

            _context.Entry(existente).CurrentValues.SetValues(progresso);

            foreach (var modulo in progresso.Modulos)
            {
                var existingModulo = existente.Modulos
                    .FirstOrDefault(m => m.ModuloId == modulo.ModuloId);

                if (existingModulo is not null)
                {
                    _context.Entry(existingModulo).CurrentValues.SetValues(modulo);
                }
                else
                {
                    _context.Entry(modulo).State = EntityState.Added;
                }
            }
        }
        else
        {
            _context.ProgressosProfissionais.Add(progresso);
        }

        await _context.SaveChangesAsync(ct);
    }
}
