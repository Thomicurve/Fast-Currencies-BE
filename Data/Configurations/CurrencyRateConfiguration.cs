using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.ToTable("CurrencyRates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CurrencyOriginId).IsRequired();
        builder.Property(x => x.CurrencyDestinationId).IsRequired();
        builder.Property(x => x.Rate).IsRequired();

        builder.HasOne(x => x.CurrencyOrigin)
            .WithMany()
            .HasForeignKey(x => x.CurrencyOriginId);

        builder.HasOne(x => x.CurrencyDestination)
            .WithMany()
            .HasForeignKey(x => x.CurrencyDestinationId);
    }
}
