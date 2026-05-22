using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;
using Domain.Enums;

namespace Infrastructure.Data.Configurations;

public class ProgressoModuloConfiguration : IEntityTypeConfiguration<ProgressoModulo>
{
    public void Configure(EntityTypeBuilder<ProgressoModulo> builder)
    {
        builder.ToTable("ProgressoModulos");

        builder.HasKey(x => x.ModuloId);

        builder.Property(x => x.ModuloId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.DataConclusao);
    }
}
