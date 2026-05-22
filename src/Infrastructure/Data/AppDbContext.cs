using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Aggregates;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Modulo> Modulos => Set<Modulo>();
    public DbSet<Conquista> Conquistas => Set<Conquista>();
    public DbSet<ProgressoProfissional> ProgressosProfissionais => Set<ProgressoProfissional>();
    public DbSet<ProgressoModulo> ProgressoModulos => Set<ProgressoModulo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
