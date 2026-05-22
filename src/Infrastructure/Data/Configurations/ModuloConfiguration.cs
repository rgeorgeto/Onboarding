using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class ModuloConfiguration : IEntityTypeConfiguration<Modulo>
{
    public void Configure(EntityTypeBuilder<Modulo> builder)
    {
        builder.ToTable("Modulos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.Property(x => x.DiasParaLiberar);

        builder.Property(x => x.PrazoEmDias);

        builder.Property(x => x.Icone)
            .HasMaxLength(100);

        builder.Property(x => x.Cor)
            .HasMaxLength(50);

        builder.Property(x => x.FormUrl)
            .HasMaxLength(500);

        builder.HasIndex(x => x.Ordem);
    }
}