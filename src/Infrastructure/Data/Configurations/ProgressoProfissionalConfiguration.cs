using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Aggregates;

namespace Infrastructure.Data.Configurations;

public class ProgressoProfissionalConfiguration : IEntityTypeConfiguration<ProgressoProfissional>
{
    public void Configure(EntityTypeBuilder<ProgressoProfissional> builder)
    {
        builder.ToTable("ProgressosProfissionais");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProfissionalId)
            .IsRequired();

        builder.Property(x => x.DataAdmissao)
            .IsRequired();

        builder.Property(x => x.ConquistasDesbloqueadas)
            .HasColumnType("nvarchar(max)")
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(Guid.Parse)
                      .ToList())
            .IsRequired();

        builder.HasMany(x => x.Modulos)
            .WithOne()
            .HasForeignKey("ProgressoProfissionalId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProfissionalId)
            .IsUnique();
    }
}