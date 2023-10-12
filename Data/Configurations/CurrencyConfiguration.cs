using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(u => u.Price)
            .IsRequired();
        builder.Property(u => u.Name)
            .IsRequired();
    }
}
