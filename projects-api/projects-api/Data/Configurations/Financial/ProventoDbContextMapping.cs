using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projects_api.Data.Models;

namespace projects_api.Data.Configurations.Financial;

public class ProventoDbContextMapping : IEntityTypeConfiguration<Provento>
{
    public void Configure(EntityTypeBuilder<Provento> builder)
    {
        builder.ToTable("Proventos", "fin");

        builder.HasIndex(p => p.UserId);

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Tipo)
               .HasConversion<string>();

        builder.Property(p => p.ValorLiquido)
               .HasPrecision(18, 2);
    }
}
