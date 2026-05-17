using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projects_api.Data.Models;

namespace projects_api.Data.Configurations.Financial;

public class AtivoDbContextMapping : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.ToTable("Ativos", "fin");

        builder.HasIndex(a => a.UserId);

        builder.HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Tipo)
               .HasConversion<string>();

        builder.Property(a => a.Quantidade)
               .HasPrecision(18, 4);

        builder.Property(a => a.PrecoMedio)
               .HasPrecision(18, 2);

        builder.Property(a => a.UltimoPreco)
               .HasPrecision(18, 2);
    }
}
