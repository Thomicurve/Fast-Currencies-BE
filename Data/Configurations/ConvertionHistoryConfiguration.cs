using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class ConvertionHistoryConfiguration : IEntityTypeConfiguration<ConvertionHistory>
{
    public void Configure(EntityTypeBuilder<ConvertionHistory> builder)
    {
        builder.ToTable("ConvertionHistory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CurrencyFromId).IsRequired();
        builder.Property(x => x.CurrencyToId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.PriceToConvert).IsRequired();
        builder.Property(x => x.ConvertedPrice).IsRequired();

        builder.HasOne(x => x.CurrencyFrom)
            .WithMany()
            .HasForeignKey(x => x.CurrencyFromId);
        builder.HasOne(x => x.CurrencyTo)
            .WithMany()
            .HasForeignKey(x => x.CurrencyToId);
        builder.HasOne(x => x.User)
            .WithMany(x => x.ConvertionHistories)
            .HasForeignKey(x => x.UserId);
    }
}
