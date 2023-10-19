using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        builder.HasKey(c => c.Id);

        builder.Property(u => u.Leyend)
            .IsRequired();
        builder.Property(u => u.Symbol)
            .IsRequired();
        builder.Property(u => u.IC)
            .IsRequired();
    }
}
