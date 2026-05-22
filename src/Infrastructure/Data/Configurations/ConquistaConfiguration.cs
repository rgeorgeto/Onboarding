using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class ConquistaConfiguration : IEntityTypeConfiguration<Conquista>
{
    public void Configure(EntityTypeBuilder<Conquista> builder)
    {
        builder.ToTable("Conquistas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Icone)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.HasIndex(x => x.Ordem);
    }
}